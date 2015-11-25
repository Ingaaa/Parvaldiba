using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pārvaldība
{
    class Nepublicetie : Bibliografiskais_vienums
    {
        private string piezimes;
        private string autori;

        public string Piezimes
        {
            get { return piezimes; }
            set { piezimes = value; }
        }
        public string Autori
        {
            get { return autori; }
            set { autori = value; }
        }
        public Nepublicetie(string nosaukums, string piezimes, string autori, int gads, DateTime izveidosanas_datums) : base(nosaukums, gads, izveidosanas_datums)
        {
            this.nosaukums = nosaukums;
            this.piezimes = piezimes;
            this.autori = autori;
            this.gads = gads;
        }
        public override void Izdrukat()
        {
            string format = "yyyy.MM.dd";
            string teksts = String.Format("@UNPUBLISHED{{\r\nauthor = {{{0}}},\r\ntitle = {{{1}}},\r\nnote = {{{2}}},\r\nyear = {{{3}}},\r\ntimestamp = {{{4}}}\r\n}}\r\n\r\n", this.autori, this.nosaukums, this.piezimes, this.gads.ToString(), this.izveidosanas_datums.ToString(format));
            File.AppendAllText(@"C:\Temp\WriteText.txt", teksts);
        }
    }
}
