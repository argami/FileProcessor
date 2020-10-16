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

        private Array LoadSchemas()
        {
            return Fields.ConvertAll<int>(field => field.Length).ToArray();
        }
    }

}