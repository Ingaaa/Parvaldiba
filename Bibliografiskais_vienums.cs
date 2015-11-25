using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pārvaldība
{
    abstract public class Bibliografiskais_vienums
    {
        protected string nosaukums;
        protected int gads;
        protected DateTime izveidosanas_datums;

        public string Nosaukums
        {
            get { return nosaukums; }
            set { nosaukums = value; }
        }
        public int Gads
        {
            get { return gads; }
            set
            {
                if (value > 1800 && DateTime.Today.Year >= value)
                    gads = value;
            }
        }
        public DateTime Izveidosanas_datums
        {
            get { return izveidosanas_datums; }
            set { izveidosanas_datums = value; }
        }

        public Bibliografiskais_vienums(DateTime izveidosanas_datums)
        {
            this.izveidosanas_datums = izveidosanas_datums;
        }
        public Bibliografiskais_vienums(string nosaukums, int gads, DateTime izveidosanas_datums) //Bāzes konstruktors priekš klases Noslēguma_darbs, Gramatas un Nepublicetie
        {
            this.nosaukums = nosaukums;
            this.gads = gads;
            this.izveidosanas_datums = izveidosanas_datums;
        }
        
        public virtual void Izdrukat()
        {
            string format = "yyyy.MM.dd";
            string teksts = String.Format("@BOOK{{\r\ntitle = {{{0}}},\r\nyear = {{{1}}},\r\timestamp = {{{2}}}\r\n}}\r\n\r\n", this.nosaukums, this.gads.ToString(), this.izveidosanas_datums.ToString(format));
            File.AppendAllText(@"C:\Temp\WriteText.txt", teksts);
        }
    }
}
