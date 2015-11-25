using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Pārvaldība
{
    class Gramata : Bibliografiskais_vienums
    {
        private string izdevejs;
        private string izdeveja_adrese;
        private string autori;
        public string Izdevejs
        {
            get { return izdevejs; }
            set { izdevejs = value; }
        }
        public string Izdeveja_adrese
        {
            get { return izdeveja_adrese; }
            set { izdeveja_adrese = value; }
        }
        public string Autori
        {
            get { return autori; }
            set { autori = value; }
        }
        public Gramata(string nosaukums, string izdevejs, string autori, int gads, DateTime izveidosanas_datums, string izdeveja_adrese = "") : base(nosaukums, gads, izveidosanas_datums)
        {
            this.nosaukums = nosaukums;
            this.izdevejs = izdevejs;
            this.izdeveja_adrese = izdeveja_adrese;
            this.gads = gads;
            this.autori = autori;
        }

        public override void Izdrukat()
        {
            string format = "yyyy.MM.dd";
            string teksts = String.Format("@BOOK{{\r\ntitle = {{{0}}},\r\npublisher = {{{1}}},\r\nyear = {{{2}}},\r\nauthor = {{{3}}},", this.nosaukums, this.izdevejs, this.gads.ToString(), this.autori);
            string teksts2 = "";
            if (izdeveja_adrese != "")
            {
                teksts2 = String.Format("\r\naddress = {{{0}}},", izdeveja_adrese);
            }
            string teksts3 = String.Format("\r\ntimestamp = {{{0}}}\r\n}}\r\n\r\n", this.izveidosanas_datums.ToString(format));
            teksts = teksts + teksts2 + teksts3;
            File.AppendAllText(@"C:\Temp\WriteText.txt", teksts);

        }
    }
}
