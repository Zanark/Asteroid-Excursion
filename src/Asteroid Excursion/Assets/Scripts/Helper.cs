using System.IO;
using System.Xml.Serialization;

public static class Helper
{
    //Serialize
    public static string Serialize<T>(this T toSerialize)
    {
        XmlSerializer xml = new XmlSerializer(toSerialize.GetType());
        StringWriter writer = new StringWriter();
        xml.Serialize(writer, toSerialize);
        return writer.ToString();
    }

    //Deserialize
    public static T Deserialize<T>(this string toDeserialize)
    {
        XmlSerializer xml = new XmlSerializer(toDeserialize.GetType());
        StringReader reader = new StringReader(toDeserialize);
        return (T)xml.Deserialize(reader);
    }
}
