using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using MvcWithData.Models.Classes;

namespace MvcWithData.Controllers
{
    public class UnvanController : Controller
    {
        // GET: Unvan
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);
        public ActionResult List()
        {
            var unvanList = con.Query<Unvan>("Select * from Unvan").ToList();
            //ViewBag.UnvanList = unvanList;
            return View(unvanList);
        }
        [HttpGet]
        public ActionResult Update(int Id)
        {
            var unvan = con.Query<Unvan>($"Select * from Unvan where UnvanId= '{Id}' ").First();
            return View(unvan);
        }
        [HttpPost]
        public ActionResult Update(Unvan model)
        {
            DynamicParameters par = new DynamicParameters();
            //1.YOL
            //var unvan = con.ExecuteScalar<int>($"update unvan set unvanAd = @UnvanAd where UnvanId = @UnvanId",model);
            //2.YOL
            par.Add("@UnvanAd", model.UnvanAd);
            //par.Add("@UnvanId", model.UnvanId);
            var unvan = con.ExecuteScalar<int>($"update unvan set unvanAd = @UnvanAd where UnvanId = @UnvanId", par);
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
            var unvan = con.Query<Unvan>($"Select * from Unvan where UnvanId= '{Id}' ").First();
            return View(unvan);
        }
        [HttpPost]
        public ActionResult Delete(Unvan model)
        {
            string qry = "delete from unvan where UnvanId = @UnvanId";
            con.ExecuteScalar<int>(qry, model);
            return RedirectToAction("List");
        }



        [HttpGet]
        public ActionResult Create()
        {
            Unvan u = new Unvan();
            return View(u);
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