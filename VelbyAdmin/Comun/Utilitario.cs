using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun
{
    public class Utilitario
    {
        public static string SerializarIdentado(object oObjeto)
        {
            return JsonConvert.SerializeObject(oObjeto, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        /// <summary>
        /// Deserializa cadena Json como objeto de un tipo dado
        /// </summary>
        /// <typeparam name="T">Tipo de dato a serailizar</typeparam>
        /// <param name="strCadenaJson">Cadena serializada de objeto</param>
        /// <returns>Objeto serializado</returns>
        public static T Deserializar<T>(string strCadenaJson)
        {
            try
            {
                Newtonsoft.Json.Linq.JToken.Parse(strCadenaJson);
            }
            catch //la cadena no es un formato Json válido
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(strCadenaJson, new BooleanJsonConverter());
        }
    }
}
