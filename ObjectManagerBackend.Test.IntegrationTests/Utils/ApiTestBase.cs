using System.Net;
using System.Net.Http.Json;

namespace ObjectManagerBackend.Test.IntegrationTests.Utils
{
    /// <summary>
    /// Base class to execute the requests easily
    /// </summary>
    public abstract class ApiTestBase
    {
        /// <summary>
        /// Executes a GET request and serializes the response only if the status code is success; otherwise throws an exception
        /// </summary>
        /// <typeparam name="ResponseType">Response type</typeparam>
        /// <param name="client">Http client</param>
        /// <param name="requestUri">Request Uri</param>
        /// <returns>The specified response type</returns>
        /// <exception cref="Exception">Is statys code is not success</exception>
        protected async Task<ResponseType> GetAndReadResponseAsync<ResponseType>(HttpClient client, string requestUri)
        {
            using (var response = await client.GetAsync(requestUri))
            {
                if(!response.IsSuccessStatusCode)
                    throw new Exception($"Invalid status code: {response.StatusCode}");

                return await response.Content.ReadFromJsonAsync<ResponseType>();
            }
        }

        /// <summary>
        /// Executes a GET request and serializes the response only if the status code is success; otherwise throws an exception
        /// </summary>
        /// <typeparam name="ResponseType">Response type</typeparam>
        /// <typeparam name="RequestType">Request type</typeparam>
        /// <param name="client">Http client</param>
        /// <param name="requestUri">Request Uri</param>
        /// <returns>The specified response type</returns>
        /// <exception cref="Exception">Is statys code is not success</exception>
        protected async Task<ResponseType> PostAndReadResponseAsync<ResponseType, RequestType>(HttpClient client, string requestUri, RequestType request)
        {
            using (var response = await client.PostAsJsonAsync(requestUri, request))
            {
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Invalid status code: {response.StatusCode}");

                return await response.Content.ReadFromJsonAsync<ResponseType>();
            }
        }

        /// <summary>
        /// Executes a GET request and returns the http code response
        /// </summary>
        /// <param name="client">Http client</param>
        /// <param name="requestUri">Request Uri</param>
        /// <returns>The Http code status</returns>
        protected async Task<HttpStatusCode> GetAsync(HttpClient client, string requestUri)
        {
            using (var response = await client.GetAsync(requestUri))
            {
                return response.StatusCode;
            }
        }

        /// <summary>
        /// Executes a POST request and returns the http code response
        /// </summary>
        /// <typeparam name="RequestType">Request type</typeparam>
        /// <param name="client">Http client</param>
        /// <param name="requestUri">Request Uri</param>
        /// <returns>The Http code status</returns>
        protected async Task<HttpStatusCode> PostAsync<RequestType>(HttpClient client, string requestUri, RequestType request)
        {
            using (var response = await client.PostAsJsonAsync(requestUri, request))
            {
                return response.StatusCode;
            }
        }

        /// <summary>
        /// Executes a PUT request and returns the http code response
        /// </summary>
        /// <typeparam name="RequestType">Request type</typeparam>
        /// <param name="client">Http client</param>
        /// <param name="requestUri">Request Uri</param>
        /// <returns>The Http code status</returns>
        protected async Task<HttpStatusCode> PutAsync<RequestType>(HttpClient client, string requestUri, RequestType request)
        {
            using (var response = await client.PutAsJsonAsync(requestUri, request))
            {
                return response.StatusCode;
            }
        }

        /// <summary>
        /// Executes a DELETE request and returns the http code response
        /// </summary>
        /// <param name="client">Http client</param>
        /// <param name="requestUri">Request Uri</param>
        /// <returns>The Http code status</returns>
        protected async Task<HttpStatusCode> DeleteAsync(HttpClient client, string requestUri)
        {
            using (var response = await client.DeleteAsync(requestUri))
            {
                return response.StatusCode;
            }
        }
    }
}
