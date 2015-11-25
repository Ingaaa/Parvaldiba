using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
namespace Pārvaldība
{
    public partial class Form1 : Form
    {
        string failasaturs;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Gramata examp_gramata = new Gramata("Saule", "Jumava", "Karlis Skalbe", 1998, new DateTime(2011, 6, 10), "Riga");
            Gramata examp_gramata1 = new Gramata("Vejs", "Zvaigzne ABC", "Rainis", 2000, new DateTime(2011, 8, 9), "Valmiera");
            Nepublicetie examp_nepublicetie = new Nepublicetie("Vinnijs Puks", "Nav publiceta", "Aspazija", 1996, new DateTime(2011, 8, 9));
            Nosleguma_darbs examp_nosleguma_d = new Nosleguma_darbs("Inga", "Pire", "LU", Nosleguma_darba_veids.Kvalifikācijas_darbs, "Ingas darbs", 2015, new DateTime(2015, 10, 18));

            Kolekcija.kolekcija.Add(examp_gramata);
            Kolekcija.kolekcija.Add(examp_gramata1);
            Kolekcija.kolekcija.Add(examp_nepublicetie);
            Kolekcija.kolekcija.Add(examp_nosleguma_d);

            listBox1.DataSource = Kolekcija.kolekcija;  //Par datu avotu uzstādu vienumus, kas atrodas kolekcijā
            listBox1.DisplayMember = "Nosaukums";       //Listboxā tiks parādīts katra vienuma nosaukums.

            comboBox1.Items.Add("Bakalaura darbs");
            comboBox1.Items.Add("Maģistra darbs");
            comboBox1.Items.Add("Kvalifikācijas darbs");
            comboBox1.Items.Add("Doktora disertācija");
        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Listbox, kurā atrodas visi kolekcijas vienumi
            button5.Hide();
            button8.Hide();
            textBox3.Hide();
            button4.Hide();
            listBox3.Hide();
            listBox3.Items.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Pirmā poga "Mainīt"
            object item = listBox1.SelectedItem;    //Saglabāju, kurš objekts ir izvēlēts listBoxā
            listBox3.Show();
            var proplist = item.GetType().GetProperties();  //Nosaku objekta tipu un masīvā saglabāju visas šī objekta tipa īpašības
            foreach (var prop in proplist)
            {
                listBox3.Items.Add(prop.Name + ": " + prop.GetValue(item));    //Apakšējā listboxā pievienoju visas šī objekta īpašības un vērtības

            }
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Listbox, kurā atrodas atlasītā objekta īpašības un to vērtības
            button5.Hide();
            button8.Hide();
            textBox3.Hide();
            dateTimePicker2.Hide();
            dateTimePicker1.Hide();
            button4.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Poga "Pievienot" (no faila)
            DialogResult result = openFileDialog1.ShowDialog(); // Atver dialogu, kur var izvēlēties failu
            if (result == DialogResult.OK) //Lai nospiežot cancel programma nenosprāgtu
            {


                string file = openFileDialog1.FileName;     //Saglabā faila atrašanās vietu
                failasaturs = File.ReadAllText(file);   // Simbolu virknē failasaturs saglabā visu faila tekstu

                listBox1.DataSource = null;

                textBox2.Show();
                label4.Show();
                textBox2.Text = failasaturs;
                //Veicu faila apstrādi un saglabāju nolasītos objektus kolekcijā
                var ieraksti = failasaturs.Split('@');
                foreach (string ieraksts in ieraksti)
                {

                    int i = ieraksts.IndexOf('{');
                    if (i > 0)
                    {
                        string kods = ieraksts.Substring(0, i);

                        if (kods == "BOOK")
                        {
                            string author = "", address = "", publisher = "", title = "";
                            int year = 0, timestamp_1 = 0, timestamp_2 = 0, timestamp_3 = 0;
                            Kolekcija.title_match(ref title, ieraksts);
                            Kolekcija.year_match(ref year, ieraksts);
                            Kolekcija.publisher_match(ref publisher, ieraksts);
                            Kolekcija.author_match(ref author, ieraksts);
                            Kolekcija.address_match(ref address, ieraksts);
                            Kolekcija.timestamp_match(ref timestamp_1, ref timestamp_2, ref timestamp_3, ieraksts);
                            Kolekcija.kolekcija.Add(new Gramata(title, publisher, author, year, new DateTime(timestamp_1, timestamp_2, timestamp_3), address));

                        }
                        if (kods == "PHDTHESIS" || kods == "MASTERSTHESIS" || kods == "BACHELORTHESIS" || kods == "QUALIFICATIONTHESIS")
                        {
                            string title = "", school = "", author_name = "", author_surname = "";
                            int year = 0, timestamp_1 = 0, timestamp_2 = 0, timestamp_3 = 0;

                            Kolekcija.title_match(ref title, ieraksts);
                            Kolekcija.year_match(ref year, ieraksts);
                            Kolekcija.author_match2(ref author_name, ref author_surname, ieraksts);
                            Kolekcija.school_match(ref school, ieraksts);
                            Kolekcija.timestamp_match(ref timestamp_1, ref timestamp_2, ref timestamp_3, ieraksts);

                            if (kods == "PHDTHESIS")
                            {
                                Kolekcija.kolekcija.Add(new Nosleguma_darbs(author_name, author_surname, school, Nosleguma_darba_veids.Doktora_disertācija, title, year, new DateTime(timestamp_1, timestamp_2, timestamp_3)));
                            }
                            if (kods == "MASTERSTHESIS")
                            {
                                Kolekcija.kolekcija.Add(new Nosleguma_darbs(author_name, author_surname, school, Nosleguma_darba_veids.Maģistra_darbs, title, year, new DateTime(timestamp_1, timestamp_2, timestamp_3)));
                            }
                            if (kods == "BACHELORTHESIS")
                            {
                                Kolekcija.kolekcija.Add(new Nosleguma_darbs(author_name, author_surname, school, Nosleguma_darba_veids.Bakalaura_darbs, title, year, new DateTime(timestamp_1, timestamp_2, timestamp_3)));
                            }
                            if (kods == "QUALIFICATIONTHESIS")
                            {
                                Kolekcija.kolekcija.Add(new Nosleguma_darbs(author_name, author_surname, school, Nosleguma_darba_veids.Kvalifikācijas_darbs, title, year, new DateTime(timestamp_1, timestamp_2, timestamp_3)));
                            }
                        }
                        if (kods == "UNPUBLISHED")
                        {
                            string author = "", title = "", note = "";
                            int year = 0, timestamp_1 = 0, timestamp_2 = 0, timestamp_3 = 0;
                            Kolekcija.title_match(ref title, ieraksts);
                            Kolekcija.year_match(ref year, ieraksts);
                            Kolekcija.author_match(ref author, ieraksts);
                            Kolekcija.note_match(ref note, ieraksts);
                            Kolekcija.timestamp_match(ref timestamp_1, ref timestamp_2, ref timestamp_3, ieraksts);

                            Kolekcija.kolekcija.Add(new Nepublicetie(title, note, author, year, new DateTime(timestamp_1, timestamp_2, timestamp_3)));
                        }
                    }
                }

                listBox1.DataSource = Kolekcija.kolekcija; //Pēc faila apstrādes un objektu pievienošanas, no jauna uzstādu datu avotu, lai listboxā parādītos tikko pievienotie vienumi
                listBox1.DisplayMember = "Nosaukums";

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Poga "Drukāt"
            int selected_item = listBox1.SelectedIndex; //Saglabāju atlasītā objekta indexu
            Kolekcija.kolekcija[selected_item].Izdrukat();  //Kolekcijā šim elementam ir tāds pats index kā listboxā, tāpēc ar indeksu atrodu viņu un izdrukāju atlasīto vienumu failā
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Otrā poga "Mainīt"
            object item = listBox1.SelectedItem; //saglabāju pirmajā listboxā atlasīto obejktu

            //Izveidoju no katras klases pa vienam objektam, lai vēlāk veiktu salīdzināšanu
            Gramata salidz1 = new Gramata("aaa", "aaa", "aaa", 1995, new DateTime(1995, 2, 3));
            Nosleguma_darbs salidz2 = new Nosleguma_darbs("aaa", "aaa", "aaa", Nosleguma_darba_veids.Bakalaura_darbs, "aaa", 1995, new DateTime(1995, 2, 3));
            Nepublicetie salidz3 = new Nepublicetie("aaa", "aaa", "aaa", 1995, new DateTime(1995, 2, 3));


            //Salīdzinu saglabātā objekta tipu ar zināmiem objektu tipiem. Tādā veidā atrodu no kuras klases objekts ir nācis
            //Pēc tam atkarībā no tā kādu objekta īpašību vēlas mainīt, parādu attiecīgos kontroļus
            if (item.GetType() == salidz1.GetType())
            {

                if (listBox3.SelectedIndex == 0 || listBox3.SelectedIndex == 1 || listBox3.SelectedIndex == 2 || listBox3.SelectedIndex == 3)
                {

                    textBox3.Show();

                }
                if (listBox3.SelectedIndex == 4)
                {
                    dateTimePicker2.Show();
                }
                if (listBox3.SelectedIndex == 5)
                {
                    dateTimePicker1.Show();
                }
            }
            if (item.GetType() == salidz2.GetType())
            {
                if (listBox3.SelectedIndex == 0)
                {


                    comboBox1.Show();
                }
                if (listBox3.SelectedIndex == 1 || listBox3.SelectedIndex == 2 || listBox3.SelectedIndex == 3 || listBox3.SelectedIndex == 4)
                {

                    textBox3.Show();

                }
                if (listBox3.SelectedIndex == 5)
                {
                    dateTimePicker2.Show();
                }
                if (listBox3.SelectedIndex == 6)
                {
                    dateTimePicker1.Show();
                }
            }
            if (item.GetType() == salidz3.GetType())
            {

                if (listBox3.SelectedIndex == 0 || listBox3.SelectedIndex == 1 || listBox3.SelectedIndex == 2)
                {

                    textBox3.Show();

                }
                if (listBox3.SelectedIndex == 3)
                {
                    dateTimePicker2.Show();
                }
                if (listBox3.SelectedIndex == 4)
                {
                    dateTimePicker1.Show();
                }
            }

            button5.Show();
            button8.Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Poga "Saglabāt"
            comboBox1.ResetText();
            object item = listBox1.SelectedItem;
            bool saglabat = true;


            Gramata salidz1 = new Gramata("aaa", "aaa", "aaa", 1995, new DateTime(1995, 2, 3));
            Nosleguma_darbs salidz2 = new Nosleguma_darbs("aaa", "aaa", "aaa", Nosleguma_darba_veids.Bakalaura_darbs, "aaa", 1995, new DateTime(1995, 2, 3));
            Nepublicetie salidz3 = new Nepublicetie("aaa", "aaa", "aaa", 1995, new DateTime(1995, 2, 3));

            //Atkarībā no objekta tipa un izvēlētās īpašības, ko girb mainīt, uzstādu jauno vērtību
            if (item.GetType() == salidz1.GetType())
            {

                if (listBox3.SelectedIndex == 0)
                {
                    string text = textBox3.Text;
                    Type t = item.GetType();
                    if (!string.IsNullOrWhiteSpace(text) && text != "Ievadiet jauno vērtību")
                    {
                        t.GetProperty("Izdevejs").SetValue(item, text);
                    }
                    else
                    {
                        label8.Show();
                        saglabat = false;
                    }

                }
                if (listBox3.SelectedIndex == 1)
                {
                    string text = textBox3.Text;
                    Type t = item.GetType();
                    if (!string.IsNullOrWhiteSpace(text) && text != "Ievadiet jauno vērtību")
                    {
                        t.GetProperty("Izdeveja_adrese").SetValue(item, text);
                    }
                    else
                    {
                        label8.Show();
                        saglabat = false;
                    }

                }
                if (listBox3.SelectedIndex == 2)
                {
                    string text = textBox3.Text;
                    Type t = item.GetType();
                    if (!string.IsNullOrWhiteSpace(text) && text != "Ievadiet jauno vērtību")
                    {
                        t.GetProperty("Autori").SetValue(item, text);
                    }
                    else
                    {
                        label8.Show();
                        saglabat = false;
                    }

                }
                if (listBox3.SelectedIndex == 3)
                {
                    string text = textBox3.Text;
                    Type t = item.GetType();
                    if (!string.IsNullOrWhiteSpace(text) && text != "Ievadiet jauno vērtību")
                    {
                        t.GetProperty("Nosaukums").SetValue(item, text);
                    }
                    else
                    {
                        label8.Show();
                        saglabat = false;
                    }

                }
                if (listBox3.SelectedIndex == 4)
                {
                    int gads = dateTimePicker2.Value.Year;

                    Type t = item.GetType();
                    t.GetProperty("Gads").SetValue(item, gads);
                }
                if (listBox3.SelectedIndex == 5)
                {

                    DateTime date = dateTimePicker1.Value.Date;

                    Type t = item.GetType();
                    t.GetProperty("Izveidosanas_datums").SetValue(item, date);
                }

            }
            if (item.GetType() == salidz2.GetType())
            {

                if (listBox3.SelectedIndex == 0)
                {
                    comboBox1.Show();

                    Type t = item.GetType();
                    if (comboBox1.SelectedIndex == 0)
                    {
                        t.GetProperty("Darba_veids").SetValue(item, Nosleguma_darba_veids.Bakalaura_darbs);
                    }
                    if (comboBox1.SelectedIndex == 1)
                    {
                        t.GetProperty("Darba_veids").SetValue(item, Nosleguma_darba_veids.Maģistra_darbs);
                    }
                    if (comboBox1.SelectedIndex == 2)
                    {
                        t.GetProperty("Darba_veids").SetValue(item, Nosleguma_darba_veids.Kvalifikācijas_darbs);
                    }
                    if (comboBox1.SelectedIndex == 3)
                    {
                        t.GetProperty("Darba_veids").SetValue(item, Nosleguma_darba_veids.Doktora_disertācija);
                    }

                }
                if (listBox3.SelectedIndex == 1)
                {
                    string text = textBox3.Text;
                    Type t = item.GetType();
                    if (!string.IsNullOrWhiteSpace(text) && text != "Ievadiet jauno vērtību")
                    {
                        t.GetProperty("Autora_vards").SetValue(item, text);
                    }
                    else
                    {
                        label8.Show();
                        saglabat = false;
                    }


                }
                if (listBox3.SelectedIndex == 2)
                {
                    string text = textBox3.Text;
                    Type t = item.GetType();
                    if (!string.IsNullOrWhiteSpace(text) && text != "Ievadiet jauno vērtību")
                    {
                        t.GetProperty("Autora_uzvards").SetValue(item, text);
                    }
                    else
                    {
                        label8.Show();
                        saglabat = false;
                    }

                }
                if (listBox3.SelectedIndex == 3)
                {
                    string text = textBox3.Text;
                    Type t = item.GetType();
                    if (!string.IsNullOrWhiteSpace(text) && text != "Ievadiet jauno vērtību")
                    {
                        t.GetProperty("Izglitibas_iestades_nos").SetValue(item, text);
                    }
                    else
                    {
                        label8.Show();
                        saglabat = false;
                    }
                }
                if (listBox3.SelectedIndex == 4)
                {
                    string text = textBox3.Text;
                    Type t = item.GetType();
                    if (!string.IsNullOrWhiteSpace(text) && text != "Ievadiet jauno vērtību")
                    {
                        t.GetProperty("Nosaukums").SetValue(item, text);
                    }
                    else
                    {
                        label8.Show();
                        saglabat = false;
                    }

                }
                if (listBox3.SelectedIndex == 5)
                {

                    int gads = dateTimePicker2.Value.Year;

                    Type t = item.GetType();
                    t.GetProperty("Gads").SetValue(item, gads);
                }
                if (listBox3.SelectedIndex == 6)
                {

                    DateTime date = dateTimePicker1.Value.Date;

                    Type t = item.GetType();
                    t.GetProperty("Izveidosanas_datums").SetValue(item, date);
                }
            }
            if (item.GetType() == salidz3.GetType())
            {

                if (listBox3.SelectedIndex == 0)
                {
                    string text = textBox3.Text;
                    Type t = item.GetType();
                    if (!string.IsNullOrWhiteSpace(text) && text != "Ievadiet jauno vērtību")
                    {
                        t.GetProperty("Piezimes").SetValue(item, text);
                    }
                    else
                    {
                        label8.Show();
                        saglabat = false;
                    }

                }
                if (listBox3.SelectedIndex == 1)
                {
                    string text = textBox3.Text;
                    Type t = item.GetType();
                    if (!string.IsNullOrWhiteSpace(text) && text != "Ievadiet jauno vērtību")
                    {
                        t.GetProperty("Autori").SetValue(item, text);
                    }
                    else
                    {
                        label8.Show();
                        saglabat = false;
                    }

                }
                if (listBox3.SelectedIndex == 2)
                {
                    string text = textBox3.Text;
                    Type t = item.GetType();
                    if (!string.IsNullOrWhiteSpace(text) && text != "Ievadiet jauno vērtību")
                    {
                        t.GetProperty("Nosaukums").SetValue(item, text);
                    }
                    else
                    {
                        label8.Show();
                        saglabat = false;
                    }

                }

                if (listBox3.SelectedIndex == 3)
                {

                    int gads = dateTimePicker2.Value.Year;

                    Type t = item.GetType();
                    t.GetProperty("Gads").SetValue(item, gads);
                }
                if (listBox3.SelectedIndex == 4)
                {

                    DateTime date = dateTimePicker1.Value.Date;

                    Type t = item.GetType();
                    t.GetProperty("Izveidosanas_datums").SetValue(item, date);
                }
            }


            if (saglabat == true)
            {
                button5.Hide();
               
                textBox3.Clear();
                textBox3.Text = "Ievadiet jauno vērtību";
                textBox3.Hide();
                button4.Hide();
                listBox3.Hide();
                dateTimePicker2.Hide();
                dateTimePicker1.Hide();
                comboBox1.Hide();
                listBox1.DataSource = null;
                listBox1.DataSource = Kolekcija.kolekcija;
                listBox1.DisplayMember = "Nosaukums";
                label8.Hide();

            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //Poga "Jauns"
            var form2 = new Form2();
            form2.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //Poga "Refresh"
            listBox1.DataSource = null;
            listBox1.DataSource = Kolekcija.kolekcija;
            listBox1.DisplayMember = "Nosaukums";

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Poga "Atcelt"
            button5.Hide();
            button4.Hide();
            comboBox1.Hide();
            button8.Hide();
            listBox3.Hide();
            comboBox1.ResetText();
            dateTimePicker1.Hide();
            dateTimePicker2.Hide();
            textBox3.Text = "Ievadiet jauno vērtibu";
            textBox3.Hide();
            label8.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
