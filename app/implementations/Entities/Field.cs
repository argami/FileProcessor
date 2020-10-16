using System;
using System.Xml.Serialization;

namespace FileProcessor.Entities
{
    public class Field
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public int Length { get; set; }
    }
}