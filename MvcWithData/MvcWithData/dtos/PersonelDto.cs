using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWithData.dtos
{
    public class PersonelDto
    {
        public int PersonelId { get; set; }
        public string AdSoy { get; set; }
        public string Ikamet { get; set; }
        public string Uyruk { get; set; }
        public string Yonetici { get; set; }


    }
}