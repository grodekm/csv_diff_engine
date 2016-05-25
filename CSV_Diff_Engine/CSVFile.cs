using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CSV_Diff_Engine
{
    class CSVFile
    {
        private int colCount;       //liczba kolumn
        string[] colDescription;    //opisy kolumn
        string path;                //ścieżka do pliku
        CSVRecord header;           //nagłówek pliku
        List<CSVRecord> records;    //rekordy pliku
        
        public CSVFile(string path)
        {
            this.path = path;
            records = new List<CSVRecord>();
        }

        public List<CSVRecord> Record
        {
            get { return records; }
        }
        public CSVRecord Header
        {
            get { return header; }
        }

        public void Parse()
        {
            StreamReader file = new StreamReader(path);

            //Przypisz header
            if (!file.EndOfStream)
                header = new CSVRecord(file.ReadLine().Split(','));

            //Przypisz rekordy
            while (!file.EndOfStream)
            {
                try
                {
                    records.Add(new CSVRecord(file.ReadLine().Split(',')));
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            file.Close();
            colCount = records[0].Length;
            colDescription = records[0].ToArray();
        }
        /// <summary>
        /// Metoda porównuje oba pliki CSV z pominięciem ich nagłówków
        /// </summary>
        /// <param name="f1">First file to compare</param>
        /// <param name="f2">Second file to compare</param>
        /// <returns> [commonPart, f1 only part, f2 only part] table</returns>
        public static CSVFile[] Compare(CSVFile f1, CSVFile f2)
        {
            CSVFile commonPart = new CSVFile("");
            CSVFile f1Part = new CSVFile("");
            CSVFile f2Part = new CSVFile("");
            int found = 0;

            //Zał: f2 musi być większy od f1

            //Jeśli nie jest - zamieniamy pliki
            if (f2.Record.Count < f1.Record.Count)
            {
                CSVFile temp = f1;
                f1 = f2;
                f2 = temp;
            }

            for (int i = 0; i < f1.Record.Count; i++)
            {
                if ((found = f2.records.FindIndex(x => x.Equals(f1.records[i])))>=0)
                {
                    commonPart.Record.Add(f1.records[i]);
                    //f1.Record.Remove(f1.Record[i]);
                    f2.Record.RemoveAt(found);
                }
                else
                {
                    f1Part.Record.Add(f1.records[i]);
                    //f1.Record.Remove(f1.Record[i]);
                }
            }
            foreach (CSVRecord rec in f2.Record)
                f2Part.Record.Add(rec);


            return new CSVFile[] { commonPart, f1Part, f2Part };
        }
    }
}
