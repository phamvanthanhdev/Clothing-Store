using System.Text.Json;
namespace ASPBanHangOnline.Infrastructure
{
    public static class SessionExtensions
    {
        //Gui du lieu len Session
        public static void SetJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        //Lay du lieu tu session
        public static T? GetJson<T>(this ISession session, string key)
        {
            var sessionData = session.GetString(key);
            return sessionData == null
                ? default(T) : JsonSerializer.Deserialize<T>(sessionData);
        }
    }
}
