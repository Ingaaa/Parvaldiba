using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pārvaldība
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
            listBox1.Items.Add("Grāmata");
            listBox1.Items.Add("Noslēguma darbs");
            listBox1.Items.Add("Nepublicētie");
            listBox2.Items.Add(Nosleguma_darba_veids.Bakalaura_darbs);
            listBox2.Items.Add(Nosleguma_darba_veids.Maģistra_darbs);
            listBox2.Items.Add(Nosleguma_darba_veids.Kvalifikācijas_darbs);
            listBox2.Items.Add(Nosleguma_darba_veids.Doktora_disertācija);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label9.Hide();
            label10.Hide();
            //Ja ir izvēlēts vienums ar indeksu 0, tad tā ir grāmata un tiek parādīti attiecīgie lauki
            button1.Show();
            if (listBox1.SelectedIndex == 0)
            {
                textBox1.Clear();
                textBox1.Show();
                textBox2.Clear();
                textBox2.Show();
                dateTimePicker2.Show();
                textBox4.Clear();
                textBox4.Show();
                textBox5.Clear();
                textBox5.Show();


                label2.Text = "Nosaukums";
                label2.Show();
                label3.Text = "Autors";
                label3.Show();
                label4.Text = "Gads";
                label4.Show();
                label5.Text = "Izdevējs";
                label5.Show();
                label6.Text = "Izdevēja adrese";
                label6.Show();
                label7.Text = "Izveidošanas datums";
                label7.Show();


                label8.Hide();

                dateTimePicker1.Show();
                listBox2.Hide();
                


            }
            //Ja ir izvēlēts vienums ar indeksu 1, tad tas ir nosleguma darbs un tiek parādīti attiecīgie lauki
            if (listBox1.SelectedIndex == 1)
            {
                textBox1.Clear();
                textBox1.Show();
                textBox2.Clear();
                textBox2.Show();
                dateTimePicker2.Show();
                textBox4.Clear();
                textBox4.Show();
                textBox5.Clear();
                textBox5.Show();


                label2.Show();
                label2.Text = "Autora vārds";
                label3.Show();
                label3.Text = "Autora uzvārds";
                label4.Show();
                label4.Text = "Gads";
                label5.Show();
                label5.Text = "Skola";
                label6.Show();
                label6.Text = "Nosaukums";
                label7.Text = "Izveidošanas datums";
                label7.Show();

                label8.Show();
                label8.Text = "Noslēguma darba veids";
                dateTimePicker1.Show();

                listBox2.Show();

            }
            //Ja ir izvēlēts vienums ar indeksu 2, tad tas ir nepublicets un tiek parādīti attiecīgie lauki
            if (listBox1.SelectedIndex == 2)
            {
                textBox1.Clear();
                textBox1.Show();
                textBox2.Clear();
                textBox2.Show();
                dateTimePicker2.Show();
                textBox4.Clear();
                textBox4.Show();

                textBox5.Hide();

                label2.Show();
                label2.Text = "Autors";
                label3.Show();
                label3.Text = "Nosaukums";
                label4.Show();
                label4.Text = "Gads";
                label5.Show();
                label5.Text = "Piezīmes";
                label6.Hide();
                dateTimePicker1.Show();
                label7.Text = "Izveidošanas datums";
                label7.Show();
                label8.Hide();
                listBox2.Hide();

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Poga "Saglabāt"
            //Tāpat kā iepriekš, skatoties pēc izvēlētā indeksa, tiek saglabāts jaunais objekts
            if (listBox1.SelectedIndex == 0)
            {
                string Nosauk = textBox1.Text;
                string Autors = textBox2.Text;
                int gads = dateTimePicker2.Value.Year;
                string Izdev = textBox4.Text;
                string IzdevAdr = textBox5.Text;
                DateTime izveid_dat = dateTimePicker1.Value.Date;
                if (string.IsNullOrWhiteSpace(Autors) || string.IsNullOrWhiteSpace(Nosauk) || string.IsNullOrWhiteSpace(Izdev))
                {
                    label9.Show();
                    label10.Show();
                }
                else
                {
                    Kolekcija.kolekcija.Add(new Gramata(Nosauk, Izdev, Autors, gads, izveid_dat, IzdevAdr));

                    this.Close();
                }


            }
            if (listBox1.SelectedIndex == 1)
            {
                string Autora_v = textBox1.Text;
                string Autora_uzv = textBox2.Text;
                string Nosaukums = textBox5.Text;
                string Skola = textBox4.Text;

                int gads = dateTimePicker2.Value.Year;
                DateTime izveid_dat = dateTimePicker1.Value.Date;
                Nosleguma_darba_veids darba_v= Nosleguma_darba_veids.Bakalaura_darbs;
                if (listBox2.SelectedIndex == 0)
                {
                    darba_v = Nosleguma_darba_veids.Bakalaura_darbs;
                }
                if (listBox2.SelectedIndex == 1)
                {
                    darba_v = Nosleguma_darba_veids.Maģistra_darbs;
                }
                if (listBox2.SelectedIndex == 2)
                {
                    darba_v = Nosleguma_darba_veids.Kvalifikācijas_darbs;
                }
                if (listBox2.SelectedIndex == 3)
                {
                    darba_v = Nosleguma_darba_veids.Doktora_disertācija;
                }
                if (string.IsNullOrWhiteSpace(Autora_v) || string.IsNullOrWhiteSpace(Autora_uzv) || string.IsNullOrWhiteSpace(Skola) || string.IsNullOrWhiteSpace(Nosaukums))
                {
                    label9.Show();
                }
                else
                {

                    Kolekcija.kolekcija.Add(new Nosleguma_darbs(Autora_v, Autora_uzv, Skola, darba_v, Nosaukums, gads, izveid_dat));
                    this.Close();
                }


            }
            if (listBox1.SelectedIndex == 2)
            {
                string Autors = textBox1.Text;
                string Nosauk = textBox2.Text;
                string Piezīmes = textBox4.Text;
                int gads = dateTimePicker2.Value.Year;
                DateTime izveid_dat = dateTimePicker1.Value.Date;
                if (string.IsNullOrWhiteSpace(Autors) || string.IsNullOrWhiteSpace(Nosauk) || string.IsNullOrWhiteSpace(Piezīmes))
                {

                    label9.Show();

                }
                else
                {
                    Kolekcija.kolekcija.Add(new Nepublicetie(Nosauk, Piezīmes, Autors, gads, izveid_dat));
                    this.Close();
                }
            }


        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
