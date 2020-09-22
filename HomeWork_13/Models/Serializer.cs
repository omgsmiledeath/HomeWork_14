using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            var jset = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            string json = JsonConvert.SerializeObject(repo, Formatting.Indented, jset);

            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.WriteLine(json);
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
            catch
            {
                return false;
            }
            
        }
    }
}
