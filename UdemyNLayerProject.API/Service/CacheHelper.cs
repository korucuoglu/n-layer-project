using Newtonsoft.Json;

namespace UdemyNLayerProject.API.Service
{
    public class CacheHelper
    {
        protected virtual string Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);
            return jsonString;
        }
        protected virtual T Deserialize<T>(string serializedObject)
        {
            return JsonConvert.DeserializeObject<T>(serializedObject);
        }
    }
}
