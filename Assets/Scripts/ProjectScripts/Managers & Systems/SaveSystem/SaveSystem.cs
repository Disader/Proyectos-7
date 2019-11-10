using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class SaveSystem
{
    public static readonly string SAVE_FOLDER = Application.dataPath + "/Resources/SAVES/";

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Serialize(object item)
    {
        XmlSerializer serializer = new XmlSerializer(item.GetType());
        StreamWriter writer = new StreamWriter(SAVE_FOLDER + "save.xml");
        serializer.Serialize(writer.BaseStream, item);
        writer.Close();
    }

    public static T Deserialize<T>() where T : GameManager.SaveObject
    {
        if (File.Exists(SAVE_FOLDER + "save.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamReader reader = new StreamReader(SAVE_FOLDER + "save.xml");
            T deserialized = (T)serializer.Deserialize(reader.BaseStream);
            reader.Close();
            return deserialized;
        }

        else
            return null;
    }
}
