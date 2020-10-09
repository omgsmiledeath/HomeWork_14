using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BankClassLibrary;

using System.Diagnostics;
using System.Windows;

namespace HomeWork_13.Models
{
    public class Serializer
    {
        private BaseRepository repo;

        public BaseRepository Repo { get => repo; }

        public Serializer()
        {
            
        }

        public Serializer(BaseRepository repository)
        {
            repo = repository;
        }

        public void Save(string path)
        {
            var jset = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All};

            StringBuilder sb = new StringBuilder();
            
           // string json = JsonConvert.SerializeObject(repo, Formatting.Indented, jset);
            
            using (StreamWriter sw = new StreamWriter(path))
            {
                using (JsonTextWriter jtw = new JsonTextWriter(sw))
                {
                    JsonSerializer js = new JsonSerializer() { TypeNameHandling = TypeNameHandling.All };
                    
                    js.Serialize(jtw, repo, typeof(BaseRepository));
                    jtw.Flush();
                }

                
            }

            File.Copy(path, "base.json", true);
        }

        public bool Load(string path)
        {
            try
            {
                string json;
                using (Stream st = File.Open(path, FileMode.OpenOrCreate))
                {
                    StreamReader sr = new StreamReader(st);
                    json = sr.ReadToEnd();
                }

                repo = JsonConvert.DeserializeObject<BaseRepository>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
                return true;
            }
            catch(Exception ex)
            {
                
                Debug.WriteLine($" {ex.Message} ");
                return false;
            }
            
        }
    }
}
