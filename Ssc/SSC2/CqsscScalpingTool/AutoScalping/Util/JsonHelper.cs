using Newtonsoft.Json;

namespace AutoScalping.Util
{
    public class JsonHelper
    {
        public static string ObjectToJson(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            string myJson = JsonConvert.SerializeObject(obj);
            return myJson;
        }

        public static T JsonToObject<T>(string jsonString) where T : new()
        {
            if (string.IsNullOrEmpty(jsonString))
                return default(T);
            else
                return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}