using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using OCDS_Mapper.src.Interfaces;

namespace OCDS_Mapper.src.Model
{
    /* Clase del componente de empaquetado de datos */

    public class Packager : IPackager
    {

        public static string Package(IList<JObject> data, int count)
        {
            JArray array = new JArray();
            foreach (JObject entry in data)
            {
                array.Add(entry);
            }
            
            string filePath = $"./tmp/mapped{count}.json";
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.WriteLine(array.ToString());
            }

            return filePath;
        }
    }
}