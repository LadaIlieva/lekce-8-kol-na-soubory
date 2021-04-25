using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace lekce_8__úkol_na_soubory
{
//1. Do proměnné typu string uložte cestu na plochu aktuálního uživatele
//2. Zkontrolujte, že na ploše existuje složka Czechitas, a pokud ne, vytvořte ji
//3. Vytvořte nebo použijte třídu Navsteva(z lekce) a vytvořte list s alespoň 2 návštěvami
//4. Do složky Czechitas uložte soubor navstevy.csv, ve kterém bude na každém řádku uložena informace o jedné návštěvě z listu, oddělená čárkou např:
//      Jarda,10,
//      Vitek,11,
//5. Potom z CSV souboru načtěte návštěvy a zobrazte na konzoli
//6. Pokud soubor csv již existuje, při spuštění programu jej přepište.
//7. Nepoužívejte pomocné knihovny pro práci s CSV(CsvHelper apod), csv je jednoduchý formát, který načte třeba Excel :)
    class Program
    {
        static void Main(string[] args)
        {

            //string cestaKPlose = @"C:\Users\tl - smidova\Desktop\IT\C#2 jaro 2021\lekce 8, úkol na soubory\lekce 8, úkol na soubory\lekce 8, úkol na soubory\";
            
            //string celaCesta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nazevSlozky);

            string cestaKPlose = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
            Console.WriteLine(cestaKPlose);

            string nazevSlozky = "Czechitas";

            string celaCesta = cestaKPlose + "\\" + nazevSlozky;

            Console.WriteLine(celaCesta);

            if (!Directory.Exists(celaCesta))
            {
                Directory.CreateDirectory(celaCesta);
            }



            Navsteva navsteva1 = new Navsteva("Angel", 35);
            //navsteva1.Jmeno = "Angel";
            //navsteva1.Vek = 35;

            Navsteva navsteva2 = new Navsteva("Lada", 30);
            //navsteva2.Jmeno = "Lada";
            //navsteva2.Vek = 30;

            List<Navsteva> listNavstev = new List<Navsteva>();
            listNavstev.Add(navsteva1);
            listNavstev.Add(navsteva2);

            string databazeSouboru = "navstevy.csv"; //coma separeitet value (data oddělena čárkou)

            Console.WriteLine(Path.Combine(celaCesta, databazeSouboru));


            //Tímto je zajištěna 6. při spuštění se mi data vymažou a zapíší jen nové
            if (File.Exists(Path.Combine(celaCesta, databazeSouboru)))
            {
                File.Delete(Path.Combine(celaCesta, databazeSouboru));
            }

            StreamWriter writer = new StreamWriter(Path.Combine(celaCesta, databazeSouboru));//zapíšu do souboru

            foreach (Navsteva item in listNavstev)
            {
               // Console.WriteLine(item.Jmeno + "," + item.Vek); //výstup na konzoli
                writer.WriteLine(item.Jmeno + "," + item.Vek);//vypíše do souboru, středním mi pak v excelu udělá políčka od sebe do A,B a ne jen, že se mi to oddělí čáskou
                                                              //kdybych dala writer.Close(); tady, tak se mi to zavře po prvním řádku
            }
            writer.Close();

            StreamReader reader = new StreamReader(Path.Combine(celaCesta, databazeSouboru));//načtu ze souboru
            string radek = "";

            while ((radek = reader.ReadLine()) != null)
            {
                Console.WriteLine(radek);
                string[] hodnoty = radek.Split(','); //je to char, jednoduché závorky, rozdělení hodnot podle čárek do pole, hodnota 0 je jméno, hodnota 1 je vek
                Console.WriteLine("Jméno je: " + hodnoty[0]);
                Console.WriteLine("Věk je: " + hodnoty[1]);

                listNavstev.Add(new Navsteva(hodnoty[0], Int32.Parse(hodnoty[1])));//mohu přidávat další návštěvy, parsuji na int, druhý parametr je int
                //protože v souboru je všechno jako text
            }

            reader.Close();// měl by se správně zavřít, jinak mi XMLSerializer nebude fungovat

            listNavstev.Add(new Navsteva("Adam", 27));//mohu přidávat další návštěvy, přidal se až potom, co se zapisuje do souboru,takže v excelu nevyskočí




            XmlSerializer serializer = new XmlSerializer(typeof(List<Navsteva>));//musí být class PUBLIC, jinak na to nevidí!

            using (StreamWriter writer1 = new StreamWriter(Path.Combine(celaCesta, "navstevy.xml")))
            {
                serializer.Serialize(writer1, listNavstev);
            }

            //List<Navsteva> deserializovanaNavsteva;       měla bych v paměti dvě pole, použití při více třídách

            using (StreamReader reader1 = new StreamReader(Path.Combine(celaCesta, "navstevy.xml")))
            {
                //deserializovanaNavsteva = serializer.Deserialize(reader1) as List<Navsteva>;
                listNavstev = serializer.Deserialize(reader1) as List<Navsteva>;//takto se mi to načte všechno z list návštěv
            }








            Console.ReadLine();
        }
    }
}
