using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kitaphane.Models.Entity
{
    public class kitapvecheck
    {
        public TBLKITAP tblkitaplar { get; set; }
        public TBLCHECK tblcheck { get; set; }
        public string kisiadi { get; set; }

        public kitapvecheck()
        {
            this.tblcheck = new TBLCHECK();
            this.tblkitaplar = new TBLKITAP();
            this.kisiadi = "";
        }
    }
}