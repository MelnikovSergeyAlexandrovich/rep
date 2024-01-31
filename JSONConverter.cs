using Newtonsoft.Json;
using System;
using System.IO;


namespace AssetsIS
{
    public class JSONConverter
    {
        public static void Serialize<T>(T obj, string filename)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = JsonConvert.SerializeObject(obj); // Данные, которые запишутся в файл
            File.WriteAllText(desktop + "\\" + filename, json);
        }
        public static T Deserialize<T>(string filename)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (File.Exists(desktop + "\\" + filename))
            {
                string json = File.ReadAllText(desktop + "\\" + filename);
                T objects = JsonConvert.DeserializeObject<T>(json);
                return objects;
            }
            else
            {
                File.Create(desktop + "\\" + filename);
                string json = File.ReadAllText(desktop + "\\" + filename);
                T objects = JsonConvert.DeserializeObject<T>(json);
                return objects; // вернет пустоту, но при этом не выкенет ошибку, что файла не существует 
            }
            
        }
    }
}
