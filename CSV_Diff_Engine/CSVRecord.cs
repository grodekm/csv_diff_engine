using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_Diff_Engine
{
    class CSVRecord
    {
        //Zmienna length wskazuje liczbę kolumn rekordu. Jej wartość przypisywana jest w momencie utworzenia pierwszej
        //instancji klasy i nie może być później zmieniana (wszystkie rekordy w programie mają tę samę liczbę kolumn)
        private static int length = 0;

        private string[] recordFields;

        public CSVRecord(string[] record)
        {
            if(length == 0)
            {
                length = record.Length;
                recordFields = record;
            }
            else
            {
                if (length == record.Length)
                {
                    recordFields = record;
                }
                else
                    throw new ArgumentOutOfRangeException("Dodawany rekord jest nieprawidlowej długości");
            }
            
        }
        public int Length
        {
            get { return length; }
        }
        //TODO: Zastapić GetValue(i) indeksatorem :)
        public string GetValue(int i)
        {
            if (i < length)
                return recordFields[i];
            else
                throw new ArgumentException("Nieprawidłowy indeks");
        }
        public override string ToString()
        {
            string temp = "";
            foreach (string str in recordFields)
                temp += "[" + str + "]";
            return temp;
        }
        public string[] ToArray()
        {
            return recordFields;
        }
        //Value equal method
        public override bool Equals(object obj)
        {
            //Console.WriteLine("Wchodzimy w equals!");
            CSVRecord eq_obj = obj as CSVRecord;
            if (eq_obj == null)
                return false;

            for(int i = 0; i < recordFields.Length; i++)
            {
                if (recordFields[i] != eq_obj.recordFields[i])
                    return false;
            }
            return true;
        }

    }
}
