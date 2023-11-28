using Newtonsoft.Json;

namespace Additional.Extensions
{
    public static class JsonExtensions
    {
        public static T Deserialize<T>(this string jsonString) 
            => JsonConvert.DeserializeObject<T>(jsonString);

        public static string Serialize<T>(this T obj) 
            => JsonConvert.SerializeObject(obj);
    }
}