using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;
using ViewModels.Models;

namespace Gallery_UI.Controllers
{
    public class HomeController : Controller
    {
        private AlbumAutomapper AlbumAutomapper { get; set; }
        private PhotoAutomapper PhotoAutomapper { get; set; }

        public HomeController()
        {
            AlbumAutomapper = new AlbumAutomapper();
            PhotoAutomapper = new PhotoAutomapper();
        }
        // GET: Home
        public ActionResult Index()
        {
            var p = PhotoAutomapper.FromBltoUiGetAll().Count();
            return View();
        }
        [HttpGet]
        public ActionResult List()
        {
            var p = PhotoAutomapper.FromBltoUiGetAll().OrderBy(x => x.PhotoDate);
            return PartialView("_list", p);
        }
        // GET: /photo/Create
        [HttpGet]
        public PartialViewResult Create()
        {
            ViewBag.AlbumId = new SelectList(AlbumAutomapper.FromBltoUiGetAll().OrderBy(x => x.AlbumId), "AlbumId", "AlbumName");
            return PartialView("_CreatePhotos");
        }

        //
        // POST: /photo/Create
        [HttpPost]
        
        public async Task<ActionResult> Create([Bind(Include = "PhotoID,PhotoName,PhotoDate,Description,photoPath,AlbumId")] PhotoViewModel photo, HttpPostedFileBase photoPath)
        {
            if (string.IsNullOrWhiteSpace(photo.PhotoName))
            {
                return Json(new { status = 0, Message = "Namnet får inte vara tomt!" }, JsonRequestBehavior.AllowGet);
                //ModelState.AddModelError("_Error", "Namnet får inte vara tomt!");
                //return PartialView("_Error", photo);
            }
            if (string.IsNullOrWhiteSpace(photo.Description))
            {
                return Json(new { status = 0, Message = "Description får inte vara tomt!" }, JsonRequestBehavior.AllowGet);
                //ModelState.AddModelError("_Error", "Description får inte vara tomt!");
                //return PartialView("_Error", photo);
            }
            if (photoPath == null || photoPath.ContentLength == 0)
            {
                return Json(new { status = 0, Message = "En fil vill jag gärna att du laddar upp!" }, JsonRequestBehavior.AllowGet);
                //ModelState.AddModelError("_Error", "En fil vill jag gärna att du laddar upp!");
                //return PartialView("_Error", photo);
            }
            var destination = Server.MapPath("~/GalleryImages/");
            if (!Directory.Exists(destination))
            {
                Directory.CreateDirectory(destination);
            }

            photoPath.SaveAs(Path.Combine(destination, photoPath.FileName));
            photo.PhotoPath = photoPath.FileName;
            photo.PhotoId = Guid.NewGuid();

            await PhotoAutomapper.FromBltoUiInser(photo);
            ViewBag.AlbumId = new SelectList(AlbumAutomapper.FromBltoUiGetAll().OrderBy(x => x.AlbumId), "AlbumId", "AlbumName");
            return Json(new { status = 1, Message = "Added Photo Success" });


            //return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> AddCom(Guid? id)
        {
            var comment = await PhotoAutomapper.FromBltoUiGetById((Guid)id);
            ViewBag.AlbumId = new SelectList(AlbumAutomapper.FromBltoUiGetAll().Where(x=> x.AlbumId == comment.AlbumId ), "AlbumId", "AlbumName");


            return PartialView("_AddCom", comment);
        }
        [HttpPost]
        public async Task<ActionResult> AddCom(PhotoViewModel photo)
        {
            if (ModelState.IsValid)
            {
                await PhotoAutomapper.FromBltoUiEditAsync(photo);
                //return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        //
        // GET: /photo/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(Guid photoIdGuid)
        {
            var editMap = await PhotoAutomapper.FromBltoUiGetById((Guid)photoIdGuid);
            ViewBag.AlbumId = new SelectList(AlbumAutomapper.FromBltoUiGetAll().OrderBy(x => x.AlbumId), "AlbumId", "AlbumName");


            return PartialView("_Edit", editMap);
        }

        //
        // POST: /photo/Edit/5
        [HttpPost]
       
        public async Task<ActionResult> Edit(PhotoViewModel photo)
        {
            if (ModelState.IsValid)
            {
                await PhotoAutomapper.FromBltoUiEditAsync(photo);
                //return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        // GET: /photo/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(Guid? id)
        {

            //ViewBag.AlbumId = new SelectList(AlbumAutomapper.FromBltoUiGetAll(), "AlbumId", "AlbumName");
            var getFromR = await PhotoAutomapper.FromBltoUiGetById((Guid)id);

            return PartialView("_Delete", getFromR);
        }

        //
        // POST: /photo/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(Guid? id)
        {
            await PhotoAutomapper.FromBltoUiDeleteAsync((Guid)id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}