using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Game
{
    public class SaveLoadManager : MonoBehaviour
    {
        public static void SaveData(GameData data)
        {
            string fileName = Consts.DataPath;

            Stream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);

            StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);
            XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
            xmlSerializer.Serialize(sw, data);
            sw.Close();
            stream.Close();
        }

        public static GameData LoadData()
        {
            GameData data = new GameData();
            Stream stream = new FileStream(Consts.DataPath, FileMode.Open, FileAccess.Read);
            // 忽略标记 = true
            StreamReader sr = new StreamReader(stream, true);
            XmlSerializer xmlSerializer = new XmlSerializer(data.GetType());
            data = xmlSerializer.Deserialize(sr) as GameData;
            stream.Close();
            sr.Close();
            return data;
        }
    }
}
