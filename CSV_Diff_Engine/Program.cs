using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSV_Diff_Engine
{
    class Program
    {
        static void Main(string[] args)
        {
            
            CSVFile file1 = new CSVFile(@"C:\Users\Mariusz\Downloads\proba.csv");
            CSVFile file2 = new CSVFile(@"C:\Users\Mariusz\Downloads\proba2.csv");


            file1.Parse();
            file2.Parse();

            Console.WriteLine("Porównujemy:");
            Console.WriteLine("{0}", file1.Header);
            CSVFile[] result_tab = CSVFile.Compare(file1, file2);

            Console.WriteLine("Common Part:");
            foreach (CSVRecord rec in result_tab[0].Record)
                Console.WriteLine("{0}", rec.ToString());
            Console.WriteLine();

            Console.WriteLine("f1 part:");
            foreach (CSVRecord rec in result_tab[1].Record)
                Console.WriteLine("{0}", rec.ToString());
            Console.WriteLine();

            Console.WriteLine("f2 part:");
            foreach (CSVRecord rec in result_tab[2].Record)
                Console.WriteLine("{0}", rec.ToString());

            Console.WriteLine();

            List<int> lista1 = new List<int>();
            lista1.Add(1);
            lista1.Add(3);
            lista1.Add(5);

            List<MariuszInt> lista2 = new List<MariuszInt>();
            MariuszInt mint1 = new MariuszInt(1);
            MariuszInt mint3 = new MariuszInt(3);
            MariuszInt mint5 = new MariuszInt(5);

            lista2.Add(mint1);
            lista2.Add(mint3);
            lista2.Add(mint5);

            List<string> list = new List<string>();
            string s1 = "Hello";
            string s2 = "Elo";
            list.Add(s1);
            list.Add(s2);

            Console.WriteLine("lista1 zawiera 3?: {0}", lista1.Contains(2));
            Console.WriteLine("lista2 zawiera 3 (MariuszInt): {0}", lista2.Exists(el => el.x == 3));
            Console.WriteLine("list zawiera Hello? : {0}", list.Contains("Hello"));
            Console.WriteLine("Czy 1 linijka pliku 1 i 1 pliku 2 to to samo?: {0}", file1.Record[0].Equals(file2.Record[0]));


            Console.ReadLine();
        }
    }
    class MariuszInt
    {
        public int x;
        public MariuszInt(int val)
        {
            x = val;
        }
    }
}
