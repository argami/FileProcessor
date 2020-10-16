using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace FileProcessor.Entities
{
    public class Record
    {
        [XmlAttribute]
        public string Description;

        [XmlAttribute]
        public string KeyValue;

        public List<Field> Fields;

        public int[] GetFieldSizesDisposition() => Fields.ConvertAll<int>(field => field.Length).ToArray();

        public List<string> Headers()
        {
            return Fields.ConvertAll<string>(field => field.Name);
        }

        public int LineWidth()
        {
            int result = 0;
            foreach (var field in Fields)
            {
                result += field.Length;
            }
            return result;
        }

        public string[] Parse(string line)
        {
            // validate that the line has the same amount of 
            // chars than the expected fields
            line = line.PadRight(LineWidth());

            List<string> result = new List<string>();

            foreach (var field in Fields)
            {
                result.Add(line.Substring(0, field.Length));
                line = line.Remove(0, field.Length);
            }

            return result.ToArray();
        }

        public bool IsKey(string key)
        {
            return key.Equals(KeyValue);
        }
    }
}