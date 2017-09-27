// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DocumentController.cs" company="">
//   
// </copyright>
// <summary>
//   The document controller.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DocumentStorage.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;

    using DocumentStorage.DAL.Repository;
    using DocumentStorage.Helpers;
    using DocumentStorage.Models.Document;

    /// <summary>
    /// The document controller.
    /// </summary>
    public class DocumentController : Controller
    {

        // GET: /Document/
        [Authorize]
        [HttpGet]
        public ActionResult ViewDocuments()
        {
            var model =
                DocumentRepository.GetDocuments()
                    .Select(
                        x =>
                        new DocumentViewModel
                            {
                                Name = x.Name,
                                DisplayName = x.Name.Length > ConfigHelper.DocumentDisplayNameSize ? x.Name.Substring(0, ConfigHelper.DocumentDisplayNameSize) + "..." : x.Name,
                                Author = x.Author.UserName,
                                CreationDate = x.CreationDate
                            }).ToList();

            return this.View("ViewDocuments", model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult ViewDocuments(string order, string columnName)
        {
            if (string.IsNullOrEmpty(order) || string.IsNullOrEmpty(columnName))
            {
                return this.ViewDocuments();
            }

            var model =
                DocumentRepository.GetDocuments()
                    .Select(
                        x =>
                        new DocumentViewModel
                        {
                            Name = x.Name,
                            DisplayName = x.Name.Length > ConfigHelper.DocumentDisplayNameSize ? x.Name.Substring(0, ConfigHelper.DocumentDisplayNameSize) + "..." : x.Name,
                            Author = x.Author.UserName,
                            CreationDate = x.CreationDate
                        });

            columnName = columnName.ToLower().Replace(" ", string.Empty);

            if (order == "desc")
            {
                switch (columnName)
                {
                    case "name":
                        model = model.OrderByDescending(x => x.Name);
                        break;
                    case "author":
                        model = model.OrderByDescending(x => x.Author);
                        break;
                    case "creationdate":
                        model = model.OrderByDescending(x => x.CreationDate);
                        break;
                    default:
                        model = model.OrderByDescending(x => x.Name);
                        break;
                }
            }
            else
            {
                switch (columnName)
                {
                    case "name":
                        model = model.OrderBy(x => x.Name);
                        break;
                    case "author":
                        model = model.OrderBy(x => x.Author);
                        break;
                    case "creationdate":
                        model = model.OrderBy(x => x.CreationDate);
                        break;
                    default:
                        model = model.OrderBy(x => x.Name);
                        break;
                }
            }

            return this.View("ViewDocuments", model.ToList());
        }

        [Authorize]
        public ActionResult UploadDocument()
        {
            this.ViewBag.MaxFileSize = ConfigHelper.MaxFileSize;
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult UploadDocument(DocumentViewModel model)
        {
            var dirPath = StoreHelper.GetUserDirectory(User.Identity.Name);
            if (string.IsNullOrEmpty(dirPath))
            {
                ModelState.AddModelError(string.Empty, "Unable to find the user directory.");
                this.TempData["UploadStatus"] = "failed";
            }
            else
            {
                if (Request.Files.Count > 0)
                {
                    var file = model.File;

                    if (file != null && file.ContentLength > 0)
                    {
                        if (!Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }

                        var extention = Path.GetFileName(Path.GetExtension(file.FileName));
                        var path = Path.Combine(
                            dirPath,
                            string.Format("{0}{1}", model.Name, string.IsNullOrEmpty(extention) ? ".txt" : extention));

                        try
                        {
                            file.SaveAs(path);

                            byte[] data;
                            using (var inputStream = file.InputStream)
                            {
                                var memoryStream = inputStream as MemoryStream;
                                if (memoryStream == null)
                                {
                                    memoryStream = new MemoryStream();
                                    inputStream.CopyTo(memoryStream);
                                }

                                data = memoryStream.ToArray();
                            }

                            if (DocumentRepository.Create(model.Name, User.Identity.Name, data))
                            {
                                this.TempData["UploadStatus"] = "success";
                            }
                        }
                        catch (Exception exc)
                        {
                            ModelState.AddModelError(
                                string.Empty,
                                string.Format("Unable to upload the file. Original error: {0}.", exc.Message));
                            this.TempData["UploadStatus"] = "failed";
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "No file provided.");
                        this.TempData["UploadStatus"] = "failed";
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No file provided.");
                    this.TempData["UploadStatus"] = "failed";
                }
            }
            return this.UploadDocument();
        }

        [HttpGet]
        [Authorize]
        public FileResult DownloadDocument(string fileName, string author)
        {
            var doc = DocumentRepository.GetDocumentByAuthorAndName(fileName, author);
            if (doc != null)
            {
                return this.File(doc.FileContent, System.Net.Mime.MediaTypeNames.Application.Octet, StoreHelper.GetFileNameWithExtension(doc.Name, User.Identity.Name));
            }

            this.RedirectToAction("ViewDocuments");
            return null;
        }

        [HttpPost]
        [Authorize]
        public ActionResult SearchDocuments(SearchViewModel search)
        {
            if (string.IsNullOrEmpty(search.SearchString))
            {
                return this.ViewDocuments();
            }

            var model =
                DocumentRepository.SearchDocuments(search.SearchString)
                    .Select(
                        x =>
                        new DocumentViewModel
                            {
                                Name = x.Name,
                                DisplayName =
                                    x.Name.Length > ConfigHelper.DocumentDisplayNameSize
                                        ? x.Name.Substring(0, ConfigHelper.DocumentDisplayNameSize)
                                          + "..."
                                        : x.Name,
                                Author = x.Author.UserName,
                                CreationDate = x.CreationDate
                            })
                    .ToList();

            return this.View("ViewDocuments", model);
        }
    }
}
