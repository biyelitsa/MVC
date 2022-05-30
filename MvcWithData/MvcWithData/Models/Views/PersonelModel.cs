using MvcWithData.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWithData.Models.Views
{
    public class PersonelModel
    {
        public Personel Personel { get; set; }
        public string Baslik { get; set; }
        public string BtnVal { get; set; }
        public string BtnClass { get; set; }
        public List<Unvan> UnvanList { get; set; }
        public List<Ulke> IkametList { get; set; }
        public List<Ulke> UyrukList { get; set; }
        public List<Personel> YoneticiList { get; set; }
       

    }
}