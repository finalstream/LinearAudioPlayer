using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Xml.XPath;
using Finalstream.LinearAudioPlayer.Plugin.Lastfm.Api;
using Lpfm.LastFmScrobbler;
using Lpfm.LastFmScrobbler.Api;
using RestSharp;

namespace Finalstream.LinearAudioPlayer.Plugin.Lastfm
{
    internal class LastFmApiUtils
    {
        const string LastFmStatusOk = "ok";
        const string LastFmErrorXPath = "/lfm/error";
        const string LastFmErrorCodeXPath = "/lfm/error/@code";
        const string LastFmStatusXPath = "/lfm/@status";
        
        /// <summary>
        /// The Last.fm web service root URL
        /// </summary>
        public const string LastFmWebServiceRootUrl = "http://ws.audioscrobbler.com/2.0/";

        public const string MethodParamName = "method";
        public const string ApiKeyParamName = "api_key";
        public const string ApiSignatureParamName = "api_sig";
        public const string SessionKeyParamName = "sk";

        public static RestClient LastFmRestClient = new RestClient(LastFmWebServiceRootUrl);

        /// <summary>
        /// Check the Last.fm status of the response and throw a <see cref="LastFmApiException"/> if an error is detected
        /// </summary>
        /// <param name="navigator">The response as <see cref="XPathNavigator"/></param>
        /// <exception cref="LastFmApiException"/>
        public static void CheckLastFmStatus(XPathNavigator navigator)
        {
            CheckLastFmStatus(navigator, null);
        }

        /// <summary>
        /// Check the Last.fm status of the response and throw a <see cref="LastFmApiException"/> if an error is detected
        /// </summary>
        /// <param name="navigator">The response as <see cref="XPathNavigator"/></param>
        /// <param name="webException">An optional <see cref="WebException"/> to be set as the inner exception</param>
        /// <exception cref="LastFmApiException"/>
        public static void CheckLastFmStatus(XPathNavigator navigator, WebException webException)
        {
            var node = SelectSingleNode(navigator, LastFmStatusXPath);

            if (node.Value == LastFmStatusOk) return;

            throw new LastFmApiException(string.Format("LastFm status = \"{0}\". Error code = {1}. {2}",
                                                       node.Value,
                                                       SelectSingleNode(navigator, LastFmErrorCodeXPath),
                                                       SelectSingleNode(navigator, LastFmErrorXPath)), webException);
        }

        /// <summary>
        /// Helper method to select a single node from an <see cref="XPathNavigator"/> as <see cref="XPathNavigator"/>
        /// </summary>
        public static XPathNavigator SelectSingleNode(XPathNavigator navigator, string xpath)
        {
            var node = navigator.SelectSingleNode(xpath);
            if (node == null) throw new InvalidOperationException("Node is null. Cannot select single node. XML response may be mal-formed");
            return node;
        }

        /// <summary>
        /// Adds the parameters that are required by the Last.Fm API to the <see cref="parameters"/> dictionary
        /// </summary>
        public static RestRequest AddRequiredParams(RestSharp.Method method, Dictionary<string, string> parameters, string methodName, Authentication authentication, bool addApiSignature = true)
        {
            var request = new RestRequest(method);

            

            // method
            parameters.Add(MethodParamName, methodName);

            // api key
            parameters.Add(ApiKeyParamName, authentication.ApiKey);

            // session key
            if (authentication.Session != null) parameters.Add(SessionKeyParamName, authentication.Session.Key);

            // api_sig
            if (addApiSignature)
            {
                parameters.Add(ApiSignatureParamName, GetApiSignature(parameters, authentication.ApiSecret));
            }

            // json
            parameters.Add("format", "json");

            foreach (var parameter in parameters)
            {
                request.AddParameter(parameter.Key, parameter.Value);
            }

            // JSonデコード
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            
            return request;

        }

        /// <summary>
        /// Generates a hashed Last.fm API Signature from the parameter name-value pairs, and the API secret
        /// </summary>
        public static string GetApiSignature(Dictionary<string, string> nameValues, string apiSecret)
        {
            string parameters = GetStringOfOrderedParamsForHashing(nameValues);
            parameters += apiSecret;

            return Hash(parameters);
        }

        /// <summary>
        /// Gets a string of ordered parameter values for hashing
        /// </summary>
        public static string GetStringOfOrderedParamsForHashing(Dictionary<string, string> nameValues)
        {
            var paramsBuilder = new StringBuilder();

            SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>(nameValues);

            //foreach (KeyValuePair<string, string> nameValue in nameValues.OrderBy(nv => nv.Key))
            foreach (KeyValuePair<string, string> nameValue in sortedDictionary)
            {
                paramsBuilder.Append(string.Format("{0}{1}", nameValue.Key, nameValue.Value));
            }
            return paramsBuilder.ToString();
        }

        // http://msdn.microsoft.com/en-us/library/system.security.cryptography.md5.aspx
        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        public static string Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public static void checkResponce(IRestResponse response)
        {
            // TODO : エラーチェックを共通化する。
            if (response == null)
            {
                throw new LastFmApiException("null");
            }


        }
    }
}
