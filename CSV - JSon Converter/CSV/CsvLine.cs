using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV___JSon_Converter
{
    public class CsvLine
    {
        List<string> values;

        public string[] Values
        {
            get { return values.ToArray() ; }
        }

        public CsvLine()
        {
            values = new List<string>();
        }

        public CsvLine(string[] values)
        {
            this.values = new List<string>();

            this.values.AddRange(values);
        }
        public void AddValue(string value)
        {
            if(values != null)
            {
                values.Add(value);
            }
        }

        public void RemoveValue(string value)
        {
            if(values != null && values.Contains(value))
            {
                values.Remove(value);
            }
        }

        public string GetValue(int index)
        {
            if (values != null && index < values.Count)
            {
                return values[index];
            }
            return string.Empty;
        }

        public static CsvLine Load(string srcText, char delimeter = ',')
        {
            CsvLine line = new CsvLine();

            bool quote = false;
            char escapeChar = '~';
            string value = "";
            string[] tempValues;

            if(escapeChar == delimeter)
            {
                if(!srcText.Contains('^'))
                {
                    escapeChar = '^';
                }
            }

            if(!String.IsNullOrEmpty(srcText))
            { 
                for(int x = 0; x < srcText.Length; x++)
                {
                    if(srcText[x] == '\u0022')
                    {
                        quote = !quote;
                    }

                    if(quote)
                    {
                        value += srcText[x];
                    }
                    else
                    {
                        if(srcText[x] == delimeter)
                        {
                            value += escapeChar;
                        }
                        else
                        {
                            if(srcText[x] != ' ') 
                            {
                                value += srcText[x];
                            }
                        }
                    }

                }
            }

            tempValues = value.Split(new char[] { escapeChar }, StringSplitOptions.RemoveEmptyEntries);

            foreach(string v in tempValues)
            {
                line.AddValue(v.Trim());
            }
            
            return line;
        }

        public string Print()
        {
            string result = "";

            foreach(string v in values)
            {
                result += v + "\t";
            }

            return result.Trim();
        }
    }
}
