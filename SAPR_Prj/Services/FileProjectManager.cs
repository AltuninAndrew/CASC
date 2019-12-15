using System.IO;
using Newtonsoft.Json;

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
