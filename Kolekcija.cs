using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace Pārvaldība
{
    public class Kolekcija
    {

        public static List<Bibliografiskais_vienums> kolekcija = new List<Bibliografiskais_vienums>();

        //Metodes ierakstu apstrādei
        public static void title_match(ref string title, string ieraksts)
        {

            var matches_title = Regex.Match(ieraksts, @"title\s*=\s*{[A-Z,a-z,0-9, ,\#,\:,\.,\\,\&]*}");
            string title_untrimed = matches_title.Value;
            var spppp = title_untrimed.Split('=');
            if (spppp.Length > 1)
            {
                title = (spppp[1].Trim(' ', '\n', '\r', '{', '}', ','));

            }

        }

        public static void year_match(ref int year1, string ieraksts)
        {

            string year = "";
            var matches_year = Regex.Match(ieraksts, @"year\s*=\s*{[A-Z,a-z,0-9, ,\#,\:,\.,\\,\&]*}");
            string year_untrimed = matches_year.Value;
            var spppp1 = year_untrimed.Split('=');
            if (spppp1.Length > 1)
            {
                year = (spppp1[1].Trim(' ', '\n', '\r', '{', '}', ','));
                if (Int32.TryParse(year, out year1))
                {
                    // you know that the parsing attempt
                    // was successful
                }

            }

        }
        public static void publisher_match(ref string publisher, string ieraksts)
        {

            var matches_publisher = Regex.Match(ieraksts, @"publisher\s*=\s*{[A-Z,a-z,0-9, ,\#,\:,\.,\\,\&]*}");
            string publisher_untrimed = matches_publisher.Value;
            var spppp2 = publisher_untrimed.Split('=');
            if (spppp2.Length > 1)
            {
                publisher = (spppp2[1].Trim(' ', '\n', '\r', '{', '}', ','));


            }

        }
        public static void author_match(ref string author, string ieraksts)
        {

            var matches_author = Regex.Match(ieraksts, @"author\s*=\s*{[A-Z,a-z,0-9, ,\#,\:,\.,\\,\&]*}");
            string author_untrimed = matches_author.Value;
            var spppp3 = author_untrimed.Split('=');
            if (spppp3.Length > 1)
            {
                author = (spppp3[1].Trim(' ', '\n', '\r', '{', '}'));


            }

        }
        public static void address_match(ref string address, string ieraksts)
        {

            var matches_address = Regex.Match(ieraksts, @"address\s*=\s*{[A-Z,a-z,0-9, ,\#,\:,\.,\\,\&]*}");
            string address_untrimed = matches_address.Value;
            var spppp4 = address_untrimed.Split('=');
            if (spppp4.Length > 1)
            {
                address = (spppp4[1].Trim(' ', '\n', '\r', '{', '}', ','));


            }

        }
        public static void timestamp_match(ref int timestamp_1, ref int timestamp_2, ref int timestamp_3, string ieraksts)
        {
            string timestamp = "";
            var matches_timestamp = Regex.Match(ieraksts, @"timestamp\s*=\s*{[A-Z,a-z,0-9, ,\#,\:,\.,\\,\&]*}");
            string timestamp_untrimed = matches_timestamp.Value;
            var spppp5 = timestamp_untrimed.Split('=');
            if (spppp5.Length > 1)
            {
                timestamp = (spppp5[1].Trim(' ', '\n', '\r', '{', '}', ','));

                var spppp6 = timestamp.Split('.');

                if (Int32.TryParse(spppp6[0], out timestamp_1))
                {
                    // you know that the parsing attempt
                    // was successful
                }
                if (Int32.TryParse(spppp6[1], out timestamp_2))
                {
                    // you know that the parsing attempt
                    // was successful
                }
                if (Int32.TryParse(spppp6[2], out timestamp_3))
                {
                    // you know that the parsing attempt
                    // was successful
                }


            }
        }
        public static void author_match2(ref string author_name, ref string author_surname, string ieraksts)
        {
            string author = "";
            var matches_author = Regex.Match(ieraksts, @"author\s*=\s*{[A-Z,a-z,0-9, ,\#,\:,\.,\\,\&]*}");
            string author_untrimed = matches_author.Value;
            var spppp3 = author_untrimed.Split('=');
            if (spppp3.Length > 1)
            {
                author = (spppp3[1].Trim(' ', '\n', '\r', '{', '}'));
                var spp1 = author.Split(' ');
                author_name = spp1[0];
                author_surname = spp1[1];

            }
        }
        public static void school_match(ref string school, string ieraksts)
        {
            var matches_school = Regex.Match(ieraksts, @"school\s*=\s*{[A-Z,a-z,0-9, ,\#,\:,\.,\\,\&]*}");
            string school_untrimed = matches_school.Value;
            var spppp4 = school_untrimed.Split('=');
            if (spppp4.Length > 1)
            {
                school = (spppp4[1].Trim(' ', '\n', '\r', '{', '}'));


            }
        }
        public static void note_match(ref string note, string ieraksts)
        {
            var matches_note = Regex.Match(ieraksts, @"note\s*=\s*{[A-Z,a-z,0-9, ,\#,\:,\.,\\,\&]*}");
            string note_untrimed = matches_note.Value;
            var spppp7 = note_untrimed.Split('=');
            if (spppp7.Length > 1)
            {
                note = (spppp7[1].Trim(' ', '\n', '\r', '{', '}', ','));


            }
        }


    }

}
