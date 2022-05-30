using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using MvcWithData.Connection;
using MvcWithData.Models.Classes;
using MvcWithData.Models.Views;

namespace MvcWithData.Controllers
{
    public class UnvanController : Controller
    {
        // GET: Unvan
        SqlConnection con = DbConnect.GetConnection();
        public ActionResult List()
        {
            var unvanList = con.Query<Unvan>("Select * from Unvan").ToList();
            //ViewBag.UnvanList = unvanList;
            return View(unvanList);
        }
        [HttpGet]
        public ActionResult Update(int Id)
        {
            UnvanModel model = new UnvanModel();
            model.Unvan = con.Query<Unvan>($"Select * from Unvan where UnvanId= '{Id}' ").First();
            model.BtnVal = "Update";
            model.BtnClass = "btn- btn-success";
            model.Baslik = "Güncelleme İşlemi";
            return View("Crud", model);
        }
        [HttpPost]
        public ActionResult Update(Unvan model)
        {
            DynamicParameters par = new DynamicParameters();
            //1.YOL
            //var unvan = con.ExecuteScalar<int>($"update unvan set unvanAd = @UnvanAd where UnvanAd = @UnvanAd",model);
            //2.YOL
            par.Add("@UnvanAd", model.UnvanAd);
            par.Add("@UnvanId", model.UnvanId);
            var unvan = con.ExecuteScalar<int>($"update unvan set unvanAd = @UnvanAd where UnvanId = @UnvanId", par);
            //var unvan = con.ExecuteScalar<int>($"update unvan set UnvanAd = @UnvanAd " , par);
            return RedirectToAction("List");
        }


        //[HttpGet]
        //public ActionResult Delete(int Id)
        //{
        //     var unvan = con.Query<Unvan>($"Delete * from Unvan where UnvanId= '{Id}' ");
        //    return RedirectToAction("List");
        //}
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            UnvanModel model = new UnvanModel();
            model.Unvan = new Unvan();
            model.BtnVal = "Sil";
            model.BtnClass = "btn btn-danger";
            model.Baslik = "Silme İşlemi";
            return View("Crud", model);
        }
        [HttpPost]
        public ActionResult Delete(Unvan model)
        {
            //string qry = "delete from unvan where UnvanId = @UnvanId";
            string qry = "delete from unvan where UnvanAd = @UnvanAd";
            con.ExecuteScalar<int>(qry, model);
            return RedirectToAction("List");
        }



        [HttpGet]
        public ActionResult Create()
        {
            UnvanModel model = new UnvanModel();
            model.Unvan = new Unvan();
            model.BtnVal = "Ekle";
            model.BtnClass = "btn- btn-primary";
            model.Baslik = "Yeni Giriş İşlemi";
            return View("Crud", model);
        }
        [HttpPost]
        public ActionResult Create(Unvan model)
        {
            //string qry = "insert into unvan (UnvanId, UnvanAd) values(@UnvanId,@UnvanAd)";
            string qry = "insert into unvan (UnvanAd) values(@UnvanAd)";
            con.ExecuteScalar<int>(qry, model);
            return RedirectToAction("List");
        }
    }

}