using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace DeployManager.Test.Helper
{
    public static class Extensions
    {
        public static StringContent ToRequestBody(this object requestEntity)
            => new StringContent(JsonConvert.SerializeObject(requestEntity), Encoding.UTF8, "application/json");

        public static T DeserializeJson<T>(this string json)
            => JsonConvert.DeserializeObject<T>(json);
    }
}
