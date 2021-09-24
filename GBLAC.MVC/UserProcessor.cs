using GBLAC.Models.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GBLAC.MVC
{
    public class UserProcessor
    {
        public static async Task<string> PostAsync<T>(string uri, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            var requestData = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await APIConnection.ApiClient.PostAsync(uri, requestData))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
               throw new Exception(response.ReasonPhrase);
            }
        }
        public static async Task<string> GetAsync<T>(string uri, T data)
        {
            var json = JsonConvert.SerializeObject(data);
            var requestData = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = await APIConnection.ApiClient.PostAsync(uri, requestData))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
               throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
