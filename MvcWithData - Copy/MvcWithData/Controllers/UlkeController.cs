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
    public class UlkeController : Controller
    {
        // GET: Ulke
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Baglanti"].ConnectionString);
        public ActionResult List()
        {
            var ulkeList = con.Query<Ulke>("Select * from Ulke").ToList();
            //ViewBag.UlkeList = ulkeList;
            return View(ulkeList);
        }
        [HttpGet]
        public ActionResult Update(string Id)
        {
            var ulke = con.Query<Ulke>($"Select * from Ulke where UlkeId= '{Id}' ").First();
            return View(ulke);
        }
        [HttpPost]
        public ActionResult Update(Ulke model)
        {
            DynamicParameters par = new DynamicParameters();
            //1.YOL
            //var ulke = con.ExecuteScalar<int>($"update ulke set ulkeAd = @UlkeAd where UlkeId = @UlkeId",model);
            //2.YOL
            par.Add("@UlkeAd", model.UlkeAd);
            par.Add("@UlkeId", model.UlkeId);
            var ulke = con.ExecuteScalar<int>($"update ulke set ulkeAd = @UlkeAd where UlkeId = @UlkeId", par);
            return RedirectToAction("List");
        }


        //[HttpGet]
        //public ActionResult Delete(string Id)
        //{
        //     var ulke = con.Query<Ulke>($"Delete * from Ulke where UlkeId= '{Id}' ");
        //    return RedirectToAction("List");
        //}
        [HttpGet]
        public ActionResult Delete(string Id)
        {
            var ulke = con.Query<Ulke>($"Select * from Ulke where UlkeId= '{Id}' ").First();
            return View(ulke);
        }
        [HttpPost]
        public ActionResult Delete(Ulke model)
        {
            string qry = "delete from ulke where UlkeId = @UlkeId";
            con.ExecuteScalar<int>(qry, model);
            return RedirectToAction("List");
        }



        [HttpGet]
        public ActionResult Create()
        {
            Ulke u = new Ulke();
            return View(u);
        }
        [HttpPost]
        public ActionResult Create(Ulke model)
        {
            string qry = "insert into ulke (UlkeId, UlkeAd) values(@UlkeId,@UlkeAd)";
            con.ExecuteScalar<int>(qry, model);
            return RedirectToAction("List");
        }
    }

}