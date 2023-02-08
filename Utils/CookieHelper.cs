namespace esabzi.Utils
{
    using esabzi.Models;
    using System.Text.Json;


    public static class CookieHelper
    {
        public static void SetCookie(this HttpResponse response,string key, object obj, int? expireDays = null)
        {
            var options = new CookieOptions();
            //if user didn't specify a cookie expiry time then set it to session i.e., cookie will destroy once user close the browser
            options.Expires = expireDays.HasValue ? DateTime.Now.AddDays(expireDays.Value) : default(DateTime?);
            response.Cookies.Append(key, JsonSerializer.Serialize(obj), options);
        }

        public static T GetCookie<T>(this HttpRequest request,string key)
        {
         

            if (request.Cookies.TryGetValue(key, out string cookie))
            {
                return JsonSerializer.Deserialize<T>(cookie);
            }
            //If the cookie is not found, the default value of the desired type T is returned
            return default;
        }

        public static void RemoveCookie(this HttpResponse response,string key)
        {
            response.Cookies.Delete(key);
        }
    }
}
