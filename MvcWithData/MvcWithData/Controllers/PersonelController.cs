using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using MvcWithData.Connection;
using MvcWithData.dtos;
using MvcWithData.Models.Classes;
using MvcWithData.Models.Views;


namespace MvcWithData.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        SqlConnection con = DbConnect.GetConnection();
        public ActionResult List()
        {
            string qry = "select p.PersonelId , p.Ad + ' ' + p.Soyad AdSoy , U.UlkeAd Ikamet , uy.UlkeAd Uyruk, " +
                "isnull(m.Ad + ' ' + m.Soyad, 'Başkan') Yonetici from Personel p " +
                "inner join Ulke u on(u.UlkeId = p.UlkeId) " +
                "inner join Ulke uy on(uy.UlkeId = p.UyrukId) " +
                "left outer join Personel m on(m.PersonelId = p.PersonelId) ";
            var plist = con.Query<PersonelDto>(qry).ToList();
            return View(plist);
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            PersonelModel model = new PersonelModel();
            model.Baslik = "Güncelleme İşlemi";
            model.BtnClass = "btn btn-access";
            model.BtnVal = "Güncelle";
            model.Personel = GetPersonel(id);
            model.UnvanList = GetUnvanList();
            model.IkametList = GetIkametList();
            model.UyrukList = GetUyrukList();
            model.YoneticiList = GetYoneticiList();
            return View("CRUD", model);
        }
        [HttpPost]
        public ActionResult Update(PersonelModel model)
        {
            string qry = "Update personel set ad = @ad, soyad = @soyad, maas = @maas, UnvanId = @UnvanId, UlkeId = @UlkeId, UyrukId = @UyrukId, YoneticiId = @YoneticiId where personelId = @personelId";
            con.ExecuteScalar<int>(qry, model.Personel);
            return RedirectToAction("List");
        }

        private List<Unvan> GetUnvanList()
        {
            string qry = $"Select * from Unvan";
            return con.Query<Unvan>(qry).ToList();
        }

        private List<Ulke> GetIkametList()
        {
            string qry = $"Select * from Ulke";
            return con.Query<Ulke>(qry).ToList();
        }
        private List<Ulke> GetUyrukList()
        {
            string qry = $"Select * from Ulke";
            return con.Query<Ulke>(qry).ToList();
        }

        //private List<Yonetici> GetYoneticiList()
        //{
        //    string qry = $"Select * from Personel";
        //    return con.Query<Personel>(qry).ToList();
        //}

        private List<Personel> GetYoneticiList()
        {
            string qry = $"Select * from Personel ";
            return con.Query<Personel>(qry).ToList();
        }

        private Personel GetPersonel(int id)
        {
            string qry = $"Select * from Personel where personelId = '{id}'";
            return con.Query<Personel>(qry).First();
        }

        [HttpGet]
        public ActionResult Create()
        {
            PersonelModel model = new PersonelModel();
            model.Personel = new Personel();
            model.Baslik = "Ekleme İşlemi";
            model.BtnClass = "btn btn-primary";
            model.BtnVal = "Ekle";
            model.UnvanList = GetUnvanList();
            model.IkametList = GetIkametList();
            model.UyrukList = GetUyrukList();
            model.YoneticiList = GetYoneticiList();
            return View("CRUD", model);
        }

        [HttpPost]
        public ActionResult Create(Personel model)
        {
            string qry = "insert into personel (ad,soyad,maas, UnvanId, UlkeId, UyrukId, YöneticiId) values(@ad,@soyad,@maas, @UnvanId, @UlkeId, @UyrukId, @YoneticiId)";
            con.ExecuteScalar<int>(qry, model);
            return RedirectToAction("List");
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            PersonelModel model = new PersonelModel();
            model.Baslik = "Silme İşlemi";
            model.BtnClass = "btn btn-danger";
            model.BtnVal = "Sil";
            model.Personel = GetPersonel(id);
            model.UnvanList = GetUnvanList();
            model.IkametList = GetIkametList();
            model.UyrukList = GetUyrukList();
            model.YoneticiList = GetYoneticiList();
            return View("CRUD", model);
        }
        [HttpPost]
        public ActionResult Delete(PersonelModel model)
        {
            string qry = "delete from personel where PersonelId = @PersonelId";
            con.ExecuteScalar<int>(qry, model);
            return RedirectToAction("List");
        }

        
    }
}