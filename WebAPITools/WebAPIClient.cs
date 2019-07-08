using System.Net;
using Newtonsoft.Json;

namespace CSharpLibraries.WebAPITools
{
    public class WebAPIClient
    {
        /// <summary>
        /// The base URL of the WebAPI server
        /// </summary>
        private string baseAPIString = "http://localhost/";

        public WebAPIClient(string apiString)
        {
            baseAPIString = apiString;
        }

        /// <summary>
        /// GET request
        /// </summary>
        /// <param name="http">HTTP request string</param>
        /// <param name="objectToSend">Object to send if needed (can be null)</param>
        /// <returns>Requested Object</returns>
        public T Get<T>(string http, object objectToSend)
        {
            using (var request = new WebClient())
            {
                if (objectToSend != null)
                {
                    string json = JsonConvert.SerializeObject(objectToSend);
                    return JsonConvert.DeserializeObject<T>(request.DownloadString(baseAPIString + http + "/" + json));
                }
                else
                    return JsonConvert.DeserializeObject<T>(request.DownloadString(baseAPIString + http));
            }
        }

        /// <summary>
        /// Send PUT request
        /// </summary>
        /// <param name="http">HTTP request string</param>
        /// <param name="objectToSend">Object to send if needed (can be null)</param>
        public void Put(string http, object objectToSend)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("content-type", "application/json");
                if (objectToSend != null)
                    client.UploadString(baseAPIString + http, "PUT", JsonConvert.SerializeObject(objectToSend));
                else
                    client.UploadString(baseAPIString + http, "PUT", "");
            }
        }

        /// <summary>
        /// Send POST request
        /// </summary>
        /// <param name="http">HTTP request string</param>
        /// <param name="objectToSend">Object to send if needed (can be null)</param>
        public void Post(string http, object objectToSend)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("content-type", "application/json");
                if (objectToSend != null)
                    client.UploadString(baseAPIString + http, "POST", JsonConvert.SerializeObject(objectToSend));
                else
                    client.UploadString(baseAPIString + http, "POST", "");
            }
        }

        /// <summary>
        /// Download a file
        /// </summary>
        /// <param name="http">HTTP location</param>
        /// <param name="destination">Destination location</param>
        public void DownloadFile(string http, string destination)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(http, destination);
            }
        }
    }
}