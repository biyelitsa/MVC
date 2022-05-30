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
    public class UlkeController : Controller
    {
        // GET: Ulke
        BaseRepositoryy<Ulke> repUlke = new BaseRepositoryy<Ulke>();
        public ActionResult List()
        {
            return View(repUlke.List());
        }
        [HttpGet]
        public ActionResult Create()
        {
            UlkeModel model = new UlkeModel();
            model.Baslik = "Yeni Giriş";
            model.BtnClass = "btn btn-primary";
            model.BtnVal = "Yeni Giriş";
            model.Ulke = new Ulke();
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Create(Ulke model, bool Idremove=true)
        {
            repUlke.Create(model, Idremove);
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Update(dynamic Id)
        {
            UlkeModel model = new UlkeModel();
            model.Baslik = "Güncelle";
            model.BtnClass = "btn btn-success";
            model.BtnVal = "Güncelle";
            model.Ulke = repUlke.Find(Id);
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Update(Ulke model)
        {
            repUlke.Update(model, model.UlkeId);
            return RedirectToAction("List");
        }
        [HttpGet]
        public ActionResult Delete(dynamic Id)
        {
            UlkeModel model = new UlkeModel();
            model.Baslik = "Silme";
            model.BtnClass = "btn btn-danger";
            model.BtnVal = "Sil";
            model.Ulke = repUlke.Find(Id);
            return View("crud", model);
        }
        [HttpPost]
        public ActionResult Delete(Ulke model)
        {
            repUlke.Delete(model.UlkeId);
            return RedirectToAction("List");
        }
    }
}