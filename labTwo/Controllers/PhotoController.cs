using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Datalager.Models;
using Datalager.Repositories;
using labTwo.Models;

namespace labTwo.Controllers
{
    public class PhotoController : Controller
    {
        public PhotoRepository PhotoRepository { get; set; }
        public AlbumRepository AlbumRepository { get; set; }
        public PhotoController()
        {
            PhotoRepository = new PhotoRepository();
            AlbumRepository = new AlbumRepository();
        }
        // GET: Gallery
        public ActionResult Index()
        {
            var p = PhotoRepository.Photos;
            return View(p);
        }
        public ActionResult PhotoList()
        {
            return PartialView("_PhotoList");
        }
        // GET: Gallery/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var Detail = PhotoRepository.Photos.FirstOrDefault(g => g.PhotoId == id);
            if (Detail == null)
            {
                return HttpNotFound();
            }
            return PartialView("_Details", Detail);
        }

        //GET 
        [HttpGet]
        public ActionResult Upload()
        {
            return PartialView("_Upload");
        }
        // POST: Gallery/Create
        [HttpPost]
        public ActionResult Upload(Photo addGallery, HttpPostedFileBase file)
        {
            if (string.IsNullOrWhiteSpace(addGallery.PhotoName))
            {
                ModelState.AddModelError("error", "Namnet får inte vara tomt!");
                return View("error",addGallery);
            }
            if (file == null || file.ContentLength == 0)
            {
                ModelState.AddModelError("error", "En fil vill jag gärna att du laddar upp!");

                // Vi skickar med model tillbaka så att vi kan få Name förifyllt!
                return PartialView("error", addGallery);
            }
            var destination = Server.MapPath("~/GalleryFolder/");
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }
            file.SaveAs(Path.Combine(destination, file.FileName));
            var photo = new Photo
            {
                PhotoId = Guid.NewGuid(),
                PhotoPath = file.FileName,
                PhotoName = addGallery.PhotoName,
                PhotoComment = addGallery.PhotoComment
            };
            PhotoRepository.Add(photo);
            return RedirectToAction("Index");
        }
        [HttpGet]
        // GET: Gallery/Edit/5
        public ActionResult Edit(Guid id)
        {
            var E = PhotoRepository.Photos.FirstOrDefault(p => p.PhotoId == id);
            return PartialView("_Edit",E);
        }

        // POST: Gallery/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, Photo EG)
        {
            try
            {
                var E = PhotoRepository.Photos.FirstOrDefault(p => p.PhotoId == id);
                E.PhotoId = EG.PhotoId;
                E.PhotoName = EG.PhotoName;
                E.PhotoComment = EG.PhotoComment;

                return RedirectToAction("Index");
            }
            catch
            {
                return PartialView("_Edit",EG);
            }
        }

        // GET: Gallery/Delete/5
        public ActionResult Delete(Guid id)
        {
            //var D =  PhotoRepository.Photos.Where(d => d.ID == id).FirstOrDefault();
            PhotoRepository.Remove(id);

            return RedirectToAction("Index");
        }

        // POST: Gallery/Delete/5
        //[HttpPost]
        //public ActionResult Delete(Guid id, Gallery DG)
        //{
        //    try
        //    {
        //        var D = gallerys.Where(d => d.GalleryID == id).FirstOrDefault();
        //        gallerys.Remove(D);

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
        public List<Photo> GalleryList()
        {
            var gallerisModels = new List<Photo>();
            #region
            //var Gga = new List<Gallery>();
            //var path = Directory.GetDirectories(Server.MapPath("~/GalleryFolder"));

            //foreach (var G in path)
            //{
            //    var info = new FileInfo(G);
            //    GallerisModels.Add(new Gallery
            //    {
            //        GalleryID = info.Name,
            //        Path = $"/GalleryFolder/{info.Name}",
            //        PhotoN = info.Name

            //    });
            //}

            //var Gall = (from ga in GallerisModels
            //         select ga.GalleryID).LastOrDefault();
            #endregion
            var path2 = Directory.GetFiles(Server.MapPath("~/GalleryFolder/"));
            foreach (var item in path2)
            {
                var infoa = new FileInfo(item);
                gallerisModels.Add(new Photo
                {
                    PhotoId = new Guid(),
                    PhotoName = infoa.Name,
                    PhotoPath = $"/GalleryFolder/{infoa.Name}",
                    PhotoComment = "New Photo"

                });

            }
            #region
            //var p = (from ga in GallerisModels
            //         select ga.AlbumID).Last();

            //var path3 = Directory.GetFiles(Server.MapPath("~/GalleryFolder/" + a+"/"+p));
            //foreach (var aa in path2)
            //{
            //    var infoa = new FileInfo(aa);
            //    GallerisModels.Add(new Gallery
            //    {
            //        AlbumID = infoa.Name,
            //        Path = infoa.DirectoryName

            //    });
            //}
            #endregion

            return gallerisModels;
        }
    }
}