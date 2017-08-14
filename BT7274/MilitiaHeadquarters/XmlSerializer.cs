using System.IO;

namespace BT7274.MilitiaHeadquarters
{
    public class XmlSerializer<T> where T : class
    {
        public void SerializeToFile(T subject, string filepath)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var wr = new StreamWriter(filepath))
            {
                xs.Serialize(wr, subject);
            }
        }

        public T DeserializeFromFile(string filepath)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var rd = new StreamReader(filepath))
            {
                return xs.Deserialize(rd) as T;
            }
        }
    }
}