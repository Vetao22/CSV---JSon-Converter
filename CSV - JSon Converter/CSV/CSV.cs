using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV___JSon_Converter
{
    public class CSV
    {
        List<CsvLine> lines;

        public CsvLine[] Lines
        {
            get { return lines.ToArray(); }
        }

        public CSV()
        {
            lines = new List<CsvLine>();
        }

        public CSV(string src)
        {
            lines = new List<CsvLine>();
        }

        public static bool IsSrcValid(string src)
        {
            if(!String.IsNullOrEmpty(src) && src.Contains(','))
            {
                string[] lines = src.Split('\n');

                if(!(lines.Length > 1))
                {
                    return false;
                }
                else
                {
                    int fieldsLength = lines.First().Split(',').Length;

                    foreach(string line in lines)
                    {
                        int lineLength = line.Split(',').Length;

                        if(lineLength != fieldsLength)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public static CSV Load(string srcText, char delimeter = ',')
        {
            if (IsSrcValid(srcText))
            {
                List<CsvLine> lines = new List<CsvLine>();
                string[] srcLines = srcText.Split('\n');

                foreach (string l in srcLines)
                {
                    lines.Add(CsvLine.Load(l.Trim(), delimeter));
                }

                CSV csv = new CSV();
                csv.lines.AddRange(lines);

                return csv;
            }
            return null;
        }

        public void AddLine(CsvLine line)
        {
            if(line != null)
            {
                lines.Add(line);
            }
        }

        public void AddLine(string srcLine)
        {
            if (!String.IsNullOrEmpty(srcLine))
            {
                lines.Add(CsvLine.Load(srcLine));
            }
        }

        public void AddLines(IEnumerable<CsvLine> linesToAdd)
        {
            if(linesToAdd != null)
            {
                lines.AddRange(linesToAdd);
            }
        }

        public void ClearLines()
        {
            lines.Clear();
        }

        public bool UpdateLine(CsvLine line)
        {
            bool updated = false;

            if(Lines.Contains(line))
            {
                int index = lines.IndexOf(line);

                lines.RemoveAt(index);
                lines.Insert(index, line);

                updated = true;
            }

            return updated;
        }

        public bool RemoveLine(CsvLine line)
        {
            bool removed = false;

            if (Lines.Contains(line))
            {
                lines.Remove(line);
                removed = true;
            }

            return removed;
        }

        public bool RemoveLine(int index)
        {
            bool removed = false;

            if (index > 0 && index < lines.Count)
            {
                lines.RemoveAt(index);
                removed = true;
            }

            return removed;
        }

        public string Print()
        {
            string print = "";

            foreach(CsvLine line in lines)
            {
                print += line.Print() + "\n";
            }

            return print;
        }
    }
}
