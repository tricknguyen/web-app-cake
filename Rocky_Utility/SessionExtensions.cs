
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Rocky_Utility
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
         {
             //chuyển value thành chuỗi json và lưu lại trong session
             session.SetString(key, JsonSerializer.Serialize(value));
         }
         //8.8
         public static T Get<T>(this ISession session, string key)
         {
             var value = session.GetString(key);
             return value == null ? default : JsonSerializer.Deserialize<T>(value);
             //chuyển chuỗi json thành đối tượng 

         }
        
        
    }
}
