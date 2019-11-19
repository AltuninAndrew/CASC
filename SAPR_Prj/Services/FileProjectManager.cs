using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SAPR_Prj.Models;
using SAPR_Prj.Objects;

namespace SAPR_Prj.Services
{
    static class FileProjectManager
    {


        public static void SaveFile(ModelToSave model,string path)
        {
            string jsonData = JsonConvert.SerializeObject(model);
            var Model = JsonConvert.DeserializeObject<ModelToSave>(jsonData);
            File.WriteAllText(path,jsonData);
            
        }

        public static ModelToSave LoadFile(string path)
        {
            if(File.Exists(path))
            {
                return JsonConvert.DeserializeObject<ModelToSave>(File.ReadAllText(path));
            }
            else
            {
                return new ModelToSave();
            }
            
        }
    }
}
