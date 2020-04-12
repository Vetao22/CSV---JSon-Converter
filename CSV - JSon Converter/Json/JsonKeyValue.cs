using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV___JSon_Converter
{
    public class JsonKeyValue
    {
        public string Key { get; set; }
        public object Value { get; set; }

        public JsonKeyValue()
        {
            Key = "";
            Value = null;
        }

        public JsonKeyValue(string key, object value)
        {
            if(!String.IsNullOrEmpty(key))
            {
                Key = key;
            }

            if(value != null)
            {
                Value = value;
            }
        }

        public static JsonKeyValue Load(string src)
        {
            JsonKeyValue keyValue = null;

            if(!String.IsNullOrEmpty(src) && src.Contains(":"))
            {
                string[] pair = src.Split(':');

                if (pair != null && pair.Count() == 2)
                {
                    keyValue = new JsonKeyValue(pair[0].Trim(), pair[1].Trim());
                }
            }

            return keyValue;
        }

        public string Print()
        {
            string print = "";

            print += Key + ": " + Value;          

            return print;
        }
    }
}
