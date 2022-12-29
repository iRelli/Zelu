using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;

namespace Zelu.Classes
{
    public class FileManager
    {
        #region FileHelper
        public class Log
        {
            public string Host { get; set; }
            public string Port { get; set; }
            public string Time { get; set; }
            public string Method { get; set; }
            public string Handler { get; set; }

            
        }
        #endregion

        public static void Attack(string Host, string Port, string Time, string Method, string Handler)
        {
            List<Log> ListTest = new List<Log>();
            Log p1 = new Log()
            {
                Host = Host,
                Port = Port,
                Time = Time,
                Method = Method,
                Handler = Handler,
            };

            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), Fusion.App.AppName);
            string file = $"{path}\\Attack.json";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!File.Exists(file))
            {
                File.Create(file).Close();
                File.WriteAllText(file, JsonConvert.SerializeObject(ListTest, Formatting.Indented));

                string json = JsonConvert.SerializeObject(ListTest, Formatting.Indented);
                ListTest = JsonConvert.DeserializeObject<List<Log>>(File.ReadAllText(file));
                ListTest.Add(p1);
                var json1 = JsonConvert.SerializeObject(ListTest, Formatting.Indented);

                File.WriteAllText(file, json1);

            }
            else
            {
                try
                {
                    ListTest = JsonConvert.DeserializeObject<List<Log>>(File.ReadAllText(file));
                    ListTest.Add(p1);
                    var json1 = JsonConvert.SerializeObject(ListTest, Formatting.Indented);

                    File.WriteAllText(file, json1);

                }
                catch
                {
                    System.Console.WriteLine("System Error");

                }
            }
        }
        public static void _RawResponse(string response)
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), Fusion.App.AppName);
            string file = $"{path}\\_RawResponse.txt";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!File.Exists(file))
            {
                File.Create(file).Close();
                File.WriteAllText(file, response);
            }
            else
            {

                File.ReadAllText(file);
                File.AppendAllText(file, $"{response + System.DateTime.Now.ToString() + Environment.NewLine}");
            }
        }
        public static void LoadData(DataGrid datagrid)
        {
            try
            {
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDoc‌​uments), Fusion.App.AppName);
                string file = $"{path}\\Attack.json";
                var jsonText = File.ReadAllText(file);
                var sponsors = JsonConvert.DeserializeObject<IList<Log>>(jsonText);
                if (sponsors == null) return;
                datagrid.Items.Clear();
                foreach (Log System in sponsors)
                {
                    datagrid.Items.Insert(0, System);
                }

            }
            catch
            {
                return;
            }
        }
    }
}
