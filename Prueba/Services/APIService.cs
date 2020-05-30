using Newtonsoft.Json;
using Prueba.Helpers;
using Prueba.Models.Base;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Services
{
    public interface IApiService
    {
        Task<TU> Get<TU>(string method, string obj = default(string)) where TU : BaseTransaction, new();
        Task<TU> Post<T, TU>(string method, T obj) where TU : BaseTransaction, new();
        Task<TU> Put<T, TU>(string method, T obj) where TU : BaseTransaction, new();
        Task<TU> Delete<TU>(string method) where TU : BaseTransaction, new();
        Task<TU> Post<TU>(string method, MultipartFormDataContent multiContent) where TU : BaseTransaction, new();
    }
    public class ApiService : IApiService
    {
        private string endPoint = Configuration.EndpointUrl;

        public ApiService()
        {
        }
        public async Task<TU> Get<TU>(string method, string obj = default(string)) where TU : BaseTransaction, new()
        {
            try
            {
                using (var client = new HttpClient
                {
                    MaxResponseContentBufferSize = 2147483647,
                    Timeout = TimeSpan.FromSeconds(60)
                })
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync($"{endPoint}/{method}{obj}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var json = JsonConvert.DeserializeObject<TU>(content);
                        return json;
                    }

                    var instance = Activator.CreateInstance<TU>();
                    instance.Success = false;
                    return instance;
                }
            }
            catch (Exception ex)
            {
                var instance = Activator.CreateInstance<TU>();
                instance.Success = false;
                return instance;
            }
        }

        /// <summary>
        /// Post the specified method, endPoint, obj and needAuthentication
        /// this POST already encrypts all the string objects.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="method">Method.</param>
        /// <param name="endPoint">End point.</param>
        /// <param name="obj">Object.</param>
        /// <param name="needAuthentication">If set to <c>true</c> need authentication.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        /// <typeparam name="TU">The 2nd type parameter.</typeparam>
        public async Task<TU> Post<T, TU>(string method, T obj) where TU : BaseTransaction, new()
        {
            try
            {
                using (var client = new HttpClient
                {
                    MaxResponseContentBufferSize = 2147483647,
                    Timeout = TimeSpan.FromSeconds(60)
                })
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var json = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
                    var postContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync($"{endPoint}/{method}", postContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var deserializedObject = JsonConvert.DeserializeObject<TU>(content);
                        return deserializedObject;
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                    }
                    var instance = Activator.CreateInstance<TU>();
                    instance.Success = false;
                    return instance;
                }
            }
            catch (Exception e)
            {
                var instance = Activator.CreateInstance<TU>();
                instance.Success = false;
                return instance;
            }
        }

        public async Task<TU> Put<T, TU>(string method, T obj) where TU : BaseTransaction, new()
        {

            try
            {
                using (var client = new HttpClient
                {
                    MaxResponseContentBufferSize = 2147483647,
                    Timeout = TimeSpan.FromSeconds(60)
                })
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var json = JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                    var putContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync($"{endPoint}/{method}", putContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<TU>(content);
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                    }
                    var instance = Activator.CreateInstance<TU>();
                    instance.Success = false;
                    return instance;
                }
            }
            catch (Exception e)
            {
                var instance = Activator.CreateInstance<TU>();
                instance.Success = false;
                return instance;
            }
        }
        public async Task<TU> Delete<TU>(string method) where TU : BaseTransaction, new()
        {


            try
            {
                using (var client = new HttpClient
                {
                    MaxResponseContentBufferSize = 2147483647,
                    Timeout = TimeSpan.FromSeconds(60)
                })
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.DeleteAsync($"{endPoint}/{method}");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<TU>(content);
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                    }
                    var instance = Activator.CreateInstance<TU>();
                    instance.Success = false;
                    return instance;
                }
            }
            catch (Exception e)
            {
                var instance = Activator.CreateInstance<TU>();
                instance.Success = false;
                return instance;
            }
        }

        /// <summary>
        /// Post to Upload Photos.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="method">Method.</param>
        /// <param name="endPoint">End point.</param>
        /// <param name="multiContent">Content</param>
        /// <typeparam name="TU">The 1st type parameter.</typeparam>
        public async Task<TU> Post<TU>(string method, MultipartFormDataContent multiContent) where TU : BaseTransaction, new()
        {

            try
            {
                using (var client = new HttpClient
                {
                    MaxResponseContentBufferSize = 2147483647,
                    Timeout = TimeSpan.FromSeconds(60)
                })
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.PostAsync($"{endPoint}/{method}", multiContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var json = JsonConvert.DeserializeObject<TU>(content);
                        return json;
                    }
                    else
                    {
                        var content = await response.Content.ReadAsStringAsync();
                    }
                    return default(TU);
                }
            }
            catch (Exception e)
            {
                return default(TU);
            }
        }
    }
}
