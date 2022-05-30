using Framework;
using MVCProjesi.Classes;
using MVCProjesi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjesi.Controllers
{
    public class UnvanController : Controller
    {
        // GET: Unvan
        BaseRepositoryy<Unvan> repUnvan = new BaseRepositoryy<Unvan>();
        public ActionResult List()
        {
            return View(repUnvan.List());
        }
        [HttpGet]
        public ActionResult Create()
        {
            UnvanModel model = new UnvanModel();
            model.Baslik = "Yeni Giriş";
            model.BtnClass = "btn btn-primary";
            model.BtnVal = "Yeni Giriş";
            model.Unvan = new Unvan();
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Create(Unvan model, bool Idremove=true)
        {
            repUnvan.Create(model, Idremove);
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Update(dynamic Id)
        {
            UnvanModel model = new UnvanModel();
            model.Baslik = "Güncelle";
            model.BtnClass = "btn btn-success";
            model.BtnVal = "Güncelle";
            model.Unvan = repUnvan.Find(Id);
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Update(Unvan model)
        {
            repUnvan.Update(model,model.UnvanId);
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Delete(dynamic Id)
        {
            UnvanModel model = new UnvanModel();
            model.Baslik = "Silme";
            model.BtnClass = "btn btn-danger";
            model.BtnVal = "Sil";
            model.Unvan = repUnvan.Find(Id);
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Delete(Unvan model)
        {
            repUnvan.Delete(model.UnvanId);
            return RedirectToAction("List");
        }
    }
}