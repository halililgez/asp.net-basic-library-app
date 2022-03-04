using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kitaphane.Models.Entity
{
    public class kitapkisiveislem
    {
        public string kitapAdi { get; set; }

        public string kisiAdi { get; set; }

        public string kisiTelefon { get; set; }

        public string beklenenTeslim { get; set; }

        public string gercekTeslim { get; set; }

        public string toplamCeza { get; set; }

        

        public kitapkisiveislem()
        {
            this.kitapAdi = "";
            this.kisiAdi = "";
            this.beklenenTeslim = "";
            this.gercekTeslim = "";
            this.kisiTelefon = "";
            this.toplamCeza = "";

        }
    }
}