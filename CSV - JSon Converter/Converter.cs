using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV___JSon_Converter
{
    public class Converter
    {
        public static Json CsvToJson(string csvSrc)
        {
            Json json = null;

            if(CSV.IsSrcValid(csvSrc))
            {
                CSV csv = CSV.Load(csvSrc);

                string[] fields = csv.Lines.First().Values;

                CsvLine[] lines = csv.Lines;

                json = new Json();

                for(int x = 1; x < lines.Length; x++)
                {
                    JsonObject obj = new JsonObject();

                    for(int y = 0; y < fields.Length; y++)
                    {
                        JsonKeyValue keyValue = new JsonKeyValue(fields[y], lines[x].Values[y]);
                        obj.AddKeyValue(keyValue);
                    }

                    json.AddObject(obj);
                }
            }

            return json;
        }

        public static Json CsvToJson(CSV csv)
        {
            Json json = null;

            if(csv != null)
            {
                string[] fields = csv.Lines.First().Values;

                CsvLine[] lines = csv.Lines;

                json = new Json();

                for (int x = 1; x < lines.Length; x++)
                {
                    JsonObject obj = new JsonObject();

                    for (int y = 0; y < fields.Length; y++)
                    {
                        JsonKeyValue keyValue = new JsonKeyValue(fields[y], lines[x].Values[y]);
                        obj.AddKeyValue(keyValue);
                    }

                    json.AddObject(obj);
                }
            }

            return json;
        }

        public static CSV JsonToCsv(string jsonSrc)
        {
            CSV csv = null;

            if(Json.IsSrcValid(jsonSrc))
            {
                Json json = Json.Load(jsonSrc);

                List<CsvLine> lines = new List<CsvLine>();

                CsvLine fields = new CsvLine();

                foreach (JsonKeyValue keyValue in json.Objects.First().KeyValues)
                {                 
                    fields.AddValue(keyValue.Key);
                    
                    if(keyValue == json.Objects.First().KeyValues.Last())
                    {
                        lines.Add(fields);
                    }
                }

                foreach(JsonObject obj in json.Objects)
                {
                    CsvLine line = new CsvLine();

                    foreach(JsonKeyValue keyValue in obj.KeyValues)
                    {
                        line.AddValue(keyValue.Value.ToString());
                    }

                    lines.Add(line);
                }

                csv = new CSV();

                csv.AddLines(lines);
            }

            return csv;
        }

        public static CSV JsonToCsv(Json json)
        {
            CSV csv = null;

            if (json != null)
            {                
                List<CsvLine> lines = new List<CsvLine>();

                CsvLine fields = new CsvLine();

                foreach (JsonKeyValue keyValue in json.Objects.First().KeyValues)
                {
                    fields.AddValue(keyValue.Key);

                    if (keyValue == json.Objects.First().KeyValues.Last())
                    {
                        lines.Add(fields);
                    }
                }

                foreach (JsonObject obj in json.Objects)
                {
                    CsvLine line = new CsvLine();

                    foreach (JsonKeyValue keyValue in obj.KeyValues)
                    {
                        line.AddValue(keyValue.Value.ToString());
                    }

                    lines.Add(line);
                }

                csv = new CSV();

                csv.AddLines(lines);
            }

            return csv;
        }
    }
}
