using System;
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


        public Record RetrieveRecordByKey(string key)
        {
            foreach (var record in Records)
            {
                if (record.IsKey(key))
                {
                    return record;
                }
            }

            return null;
        }

        public string GetKey(string line)
        {
            return line.Substring(KeyPos - 1, Size);
        }

        public KeyValuePair<string, string[]> ParseLine(string line)
        {
            var key = GetKey(line);
            var record = RetrieveRecordByKey(key);

            if (record != null)
            {
                return KeyValuePair.Create(key, record.Parse(line));
            }

            throw new System.Exception($"Type of record unknown: {key}");
        }
    }

}