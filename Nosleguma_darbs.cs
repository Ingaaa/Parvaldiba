using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Pārvaldība
{
    enum Nosleguma_darba_veids { Maģistra_darbs, Doktora_disertācija, Kvalifikācijas_darbs, Bakalaura_darbs };
    class Nosleguma_darbs : Bibliografiskais_vienums
    {
        private Nosleguma_darba_veids darba_veids;
        private string autora_vards;
        private string autora_uzvards;
        private string izglitibas_iestades_nos;

        public Nosleguma_darba_veids Darba_veids
        {
            get { return darba_veids; }
            set { darba_veids = value; }
        }
        public string Autora_vards
        {
            get { return autora_vards; }
            set { autora_vards = value; }
        }
        public string Autora_uzvards
        {
            get { return autora_uzvards; }
            set { autora_uzvards = value; }
        }
        public string Izglitibas_iestades_nos
        {
            get { return izglitibas_iestades_nos; }
            set { izglitibas_iestades_nos = value; }
        }

        public Nosleguma_darbs(string autora_vards, string autora_uzvards, string izglitibas_iestades_nos, Nosleguma_darba_veids darba_veids, string nosaukums, int gads, DateTime izveidosanas_datums) : base(nosaukums, gads, izveidosanas_datums)
        {
            this.autora_vards = autora_vards;
            this.autora_uzvards = autora_uzvards;
            this.izglitibas_iestades_nos = izglitibas_iestades_nos;
            this.darba_veids = darba_veids;
            this.nosaukums = nosaukums;
            this.gads = gads;
        }

        public override void Izdrukat()
        {
            string format = "yyyy.MM.dd";
            string teksts = "";
            if (this.darba_veids == Nosleguma_darba_veids.Doktora_disertācija)
            {
                teksts = String.Format("@PHDTHESIS{{\r\nauthor = {{{0} {1}}},\r\ntitle = {{{2}}},\r\nschool = {{{3}}},\r\nyear = {{{4}}},\r\ntimestamp = {{{5}}}\r\n}}\r\n\r\n", this.autora_vards, this.autora_uzvards, this.nosaukums, this.izglitibas_iestades_nos, this.gads.ToString(), this.izveidosanas_datums.ToString(format));
            }
            if (this.darba_veids == Nosleguma_darba_veids.Maģistra_darbs)
            {
                teksts = String.Format("@MASTERSTHESIS{{\r\nauthor = {{{0} {1}}},\r\ntitle = {{{2}}},\r\nschool = {{{3}}},\r\nyear = {{{4}}},\r\ntimestamp = {{{5}}}\r\n}}\r\n\r\n", this.autora_vards, this.autora_uzvards, this.nosaukums, this.izglitibas_iestades_nos, this.gads.ToString(), this.izveidosanas_datums.ToString(format));
            }
            if (this.darba_veids == Nosleguma_darba_veids.Bakalaura_darbs)
            {
                teksts = String.Format("@BACHELORTHESIS{{\r\nauthor = {{{0} {1}}},\r\ntitle = {{{2}}},\r\nschool = {{{3}}},\r\nyear = {{{4}}},\r\ntimestamp = {{{5}}}\r\n}}\r\n\r\n", this.autora_vards, this.autora_uzvards, this.nosaukums, this.izglitibas_iestades_nos, this.gads.ToString(), this.izveidosanas_datums.ToString(format));
            }
            if (this.darba_veids == Nosleguma_darba_veids.Kvalifikācijas_darbs)
            {
                teksts = String.Format("@QUALIFICATIONTHESIS{{\r\nauthor = {{{0} {1}}},\r\ntitle = {{{2}}},\r\nschool = {{{3}}},\r\nyear = {{{4}}},\r\ntimestamp = {{{5}}}\r\n}}\r\n\r\n", this.autora_vards, this.autora_uzvards, this.nosaukums, this.izglitibas_iestades_nos, this.gads.ToString(), this.izveidosanas_datums.ToString(format));
            }
            File.AppendAllText(@"C:\Temp\WriteText.txt", teksts);
        }
    }
}
