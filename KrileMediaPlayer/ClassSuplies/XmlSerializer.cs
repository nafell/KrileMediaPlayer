namespace KrileMediaPlayer.ClassSuplies
{
    public class XmlSerializer<T> where T : class
    {
        public void SerializeToFile(T subject, string filepath)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (System.IO.StreamWriter wr = new System.IO.StreamWriter(filepath))
            {
                xs.Serialize(wr, subject);
            }
        }

        public T DeserializeFromFile(string filepath)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (System.IO.StreamReader rd = new System.IO.StreamReader(filepath))
            {
                return xs.Deserialize(rd) as T;
            }
        }
    }
}
