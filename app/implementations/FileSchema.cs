using System.Collections.Generic;
using System.Xml.Serialization;

namespace FileProcessor.Entities
{
    public class FileSchema
    {
        [XmlAttribute]
        public string Filter { get; set; }

        [XmlAttribute]
        public int KeyPos { get; set; }

        [XmlAttribute]
        public int Size { get; set; }

        public List<Record> Records;
    }

}