using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Datalager.Models;
using Datalager.Repositories;
using labTwo.Models;

namespace labTwo.Controllers
{
    public class AlbumController : Controller
    {
        public AlbumRepository AlbumRepository { get; set; }
        public PhotoRepository PhotoRepository { get; set; }
        public AlbumController()
        {
            AlbumRepository = new AlbumRepository();
            PhotoRepository = new PhotoRepository();
        }

   
        //
        // GET: /Album/
        public ActionResult Index()
        {
            var g = AlbumRepository.Albums;
            return View(g);
        }
        public ActionResult AlbumList()
        {
            return PartialView("_AlbumList");
        }
        //
        // GET: /Album/Details/5
        public  ActionResult Details(Guid id)
        {
            var r =  AlbumRepository.Albums.FirstOrDefault(x=>x.AlbumId== id);
            return PartialView("_Details",r);
        }

        //
        // GET: /Album/Create
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView("_Create");
        }

        //
        // POST: /Album/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid)
            {
                album.AlbumId = Guid.NewGuid();
                 AlbumRepository.Albums.Add(album);
                return RedirectToAction("Index");
            }

            return PartialView("_AlbumList",AlbumRepository.Albums);
        }

        //
        // GET: /Album/Edit/5
        [HttpGet]
        public ActionResult Edit(Guid id)
        {

            var edit = AlbumRepository.Albums.FirstOrDefault(x=>x.AlbumId==id);

            return PartialView("_Edit", edit);
        }

        //
        // POST: /Album/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid)
            {
                 AlbumRepository.Albums.Add(album);
                return RedirectToAction("Index");
            }
            return PartialView("_AlbumList", AlbumRepository.Albums);
        }

        //
        // GET: /Album/Delete/5
        public ActionResult Delete(Guid id)
        {
            AlbumRepository.RemoveAlbum(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult AddPhotoToAlbum()
        {
            var model = new AlbumPhoto();
            model.Albums = AlbumRepository.Albums;
            model.Photos = PhotoRepository.Photos;
            return View(model);
        }
        [HttpPost]
        public ActionResult AddPhotoToAlbum(IEnumerable<Guid> photos, Guid albumId)
        {
            var album = AlbumRepository.Albums.FirstOrDefault(x => x.AlbumId == albumId);
            foreach (var item in photos)
            {
                album.Photos.Add(PhotoRepository.Photos.FirstOrDefault(x => x.PhotoId == item));
            }
            return Content("OK!");
        }
    }
}