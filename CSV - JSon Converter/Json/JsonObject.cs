using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV___JSon_Converter
{
    public class JsonObject
    {
        List<JsonKeyValue> keyValues;
        
        public JsonKeyValue[] KeyValues { get { return keyValues.ToArray(); } }
        public JsonObject()
        {
            keyValues = new List<JsonKeyValue>();
        }

        public JsonObject(JsonKeyValue[] keyValues)
        {
            this.keyValues = new List<JsonKeyValue>(keyValues);
        }
    
        public void AddKeyValue(JsonKeyValue keyValue)
        {
            if(keyValue != null)
            {
                keyValues.Add(keyValue);
            }
        }

        public void RemoveKeyValue(JsonKeyValue keyValue)
        {
            if(keyValue != null && keyValues.Contains(keyValue))
            {
                keyValues.Remove(keyValue);
            }
        }

        public static JsonObject Load(string src)
        {
            JsonObject jsonObject = new JsonObject();

            string[] keyValuesSrc = src.Trim(new char[] { '{', '}' }).Split(',');
            
            foreach(string keyValue in keyValuesSrc)
            {
                jsonObject.AddKeyValue(JsonKeyValue.Load(keyValue));
            }
            return jsonObject;
        }

        public string Print()
        {
            string print = "{\n";

            foreach (JsonKeyValue keyValue in keyValues)
            {
                print += "    " + keyValue.Print();

                if(keyValue == keyValues.Last())
                {
                    print += "\n";
                }
                else
                {
                    print += ",\n";
                }
            }

            print += "}";

            return print;
        }
    }
}
