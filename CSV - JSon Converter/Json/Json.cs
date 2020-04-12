using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSV___JSon_Converter
{
    public class Json
    {
        List<JsonObject> objects;

        public JsonObject[] Objects { get { return objects.ToArray(); } }
        
        public Json()
        {
            objects = new List<JsonObject>();
        }

        public void AddObject(JsonObject obj)
        {
            if(obj != null)
            {
                objects.Add(obj);
            }
        }

        public JsonObject GetObject(int index)
        {
            if(index >= 0 && index < objects.Count)
            {
                return objects[index];
            }

            return null;
        }

        public void RemoveObject(JsonObject obj)
        {
            if(obj != null && objects.Contains(obj))
            {
                objects.Remove(obj);
            }
        }

        public static List<string> SeparateObjectsSrc(string src)
        {
            List<string> objectSrcs = new List<string>();

            int startIndex = 0, finalIndex = 0;

            for(int x = 0; x < src.Length; x++)
            {
                if(src[x] == '{')
                {
                    startIndex = x;
                    finalIndex = x + 1;
                }
                else if (src[x] == '}')
                {
                    finalIndex = x + 1;

                    string objSrc = src.Substring(startIndex, finalIndex - startIndex);
                    if (objSrc.Contains(":"))
                    {
                        objectSrcs.Add(objSrc);
                    }

                    startIndex = finalIndex;
                }
            
            }
            return objectSrcs;
        }

        public static bool IsSrcValid(string src)
        {            
            if(!String.IsNullOrEmpty(src))
            { 
                string test = src.Trim();

                if(test.StartsWith("{") && test.EndsWith("}"))
                {
                    if(test.Contains(":"))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public static Json Load(string jsonSrc)
        {
            Json json;

            if(IsSrcValid(jsonSrc))
            {
                json = new Json();

                List<string> separtedObjSrcs = SeparateObjectsSrc(jsonSrc.Trim(new char[] { '{', '}' }));

                foreach(string src in separtedObjSrcs)
                {
                    JsonObject obj = JsonObject.Load(src);

                    if (obj != null)
                    {
                        json.AddObject(obj);
                    }
                }
            }
            else
            {
                json = null;
            }            

            return json;
        }

        public string Print()
        {
            string print = "{\n";

            foreach(JsonObject obj in objects)
            {
                print += obj.Print();

                if(obj != objects.Last())
                {
                    print += ",\n";
                }
                else
                {
                    print += "\n";
                }
            }

            print += "}";

            return print;
        }
    }
}
