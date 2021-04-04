using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;
using OCDS_Mapper.src.Interfaces;

namespace OCDS_Mapper.src.Model
{
    /* Clase del componente de empaquetado de datos */

    public class Packager : IPackager
    {

        public static void Package(IList<JObject> data, int count)
        {
            JArray array = new JArray();
            foreach (JObject entry in data)
            {
                array.Add(entry);
            }
            using (StreamWriter sw = new StreamWriter($"./tmp/mapped{count}.json"))
            {
                sw.WriteLine(array.ToString());
            }
        }
    }
}