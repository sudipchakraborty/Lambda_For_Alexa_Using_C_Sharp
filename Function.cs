using System.Net.Http;

using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

using Amazon.Lambda.Core;

using Amazon;
using Amazon.IotData;
using Amazon.IotData.Model;
using Amazon.Runtime.Internal;
using System.Text;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using Amazon.Lambda.Core;
using Amazon.Lambda.Serialization;
using Newtonsoft.Json;




// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace LambdaAlexa
{
    public class Function
    {
        public const string INVOCATION_NAME = "my house";
  //      private static HttpClient _httpClient;

        public Function()
        {
          //  _httpClient = new HttpClient();
        }

        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)

        {
            var requestType = input.GetRequestType();
            /////////////////////////////////////////////
            if (requestType== typeof(LaunchRequest))
            {
                return MakeSkillResponse($"Welcome to my House", false);
            }
            ///////////////////////////////////////////
            else if (requestType == typeof(IntentRequest))
            {
                send_to_iot("on");
                return MakeSkillResponse(
                        $" Other Request, else if",
                        false);

            }
            else
            {
                send_to_iot("on");
                return MakeSkillResponse(
                        $" Other Request, else" +
                        $"" +
                        $"",
                        false);

            }


            //    var intentRequest = (IntentRequest)input.Request;
            //    var device_name = intentRequest.Intent.Slots["device_name"].Value;
            //    var status = intentRequest.Intent.Slots["status"].Value;

            //    // var age = intentRequest.Intent.Slots["age"].Value;






            //    switch (intentRequest.Intent.Name)
            //    {
            //        case "AMAZON.CancelIntent":
            //            return MakeSkillResponse($"cancel.", false);
            //            break;
            //        /////////////////////////////
            //        case "AMAZON.StopIntent":
            //            return MakeSkillResponse($"StopIntent.", false);
            //            break;
            //        /////////////////////////////
            //        case "AMAZON.HelpIntent":
            //            return MakeSkillResponse($"cancel.", false);
            //            break;
            //        /////////////////////////////
            //        case "HelloIntent":
            //            return MakeSkillResponse($"HelpIntent.", false);
            //            break;
            //        /////////////////////////////                
            //        //case "NameIntent":
            //        //    //if(age=="10")
            //        //    //{
            //        //    //    return MakeSkillResponse($"Age is tem.", false);
            //        //    //}
            //        //    //else
            //        //    //{
            //        //    //    return MakeSkillResponse($"Other than tem.", false);
            //        //    //}

            //        //    return MakeSkillResponse($"Name Intent.", false);
            //        //    break;

            //        //case "ram":
            //        //    return MakeSkillResponse($"ram.", false);

            //        //    break;

            //        case "CountryInfoIntent":

            //            if (device_name=="light")
            //            {
            //                send_to_iot(status);
            //                return MakeSkillResponse($"turn {device_name} {status}", false);
            //            }

            //            if (device_name=="203")
            //            {
            //                return MakeSkillResponse(
            //          $"you entered={device_name}",
            //          false);
            //            }


            //            else if (device_name=="nepal")
            //            {
            //                return MakeSkillResponse(
            //          $"nepal",
            //          false);
            //            }

            //            else
            //            {
            //                return MakeSkillResponse(
            //              $"You'd like more information about {device_name}",
            //              false);
            //            }


            //            //  return MakeSkillResponse($"Country Info intent", false);
            //            break;

            //        default:
            //            return MakeSkillResponse($"Command not matched...", false);
            //            break;
            //            //////////////////////////
            //    }
            //    //////////////////////////////////////////////////////////
            //}
            //else
            //{
            //    return MakeSkillResponse(
            //            $" Other Request, ask {INVOCATION_NAME}.Programmer",
            //            false);
            //}
        }


            private SkillResponse MakeSkillResponse(string outputSpeech,
            bool shouldEndSession,
            string repromptText = "Just say, tell me about Canada to learn more. To exit, say, exit.")
            {
            var response = new ResponseBody
            {
                ShouldEndSession = shouldEndSession,
                OutputSpeech = new PlainTextOutputSpeech { Text = outputSpeech }
            };

            if (repromptText != null)
            {
                response.Reprompt = new Reprompt() { OutputSpeech = new PlainTextOutputSpeech() { Text = repromptText } };
            }

            var skillResponse = new SkillResponse
            {
                Response = response,
                Version = "1.0"
            };
            return skillResponse;
        }

        public void send_to_iot(string state)
        {
           

            UpdateThingShadowRequest req = new UpdateThingShadowRequest();
            req.ShadowName= "Light";
            req.ThingName= "Virtual_Device";

            string jsonState = "{\"state\":{\"desired\":{\"key\":\""+DateTime.Now.ToString()+"\"}}}";
            req.Payload=new MemoryStream(Encoding.UTF8.GetBytes(jsonState));

            AmazonIotDataClient client = new AmazonIotDataClient("https://a3go886l5i56ev-ats.iot.us-east-1.amazonaws.com/things/Virtual_Device/shadow?name=Light");

            client.UpdateThingShadowAsync(req);
            //  value++;
            client.Dispose();














            ////////////////////<1>/////////////////////////////////////////////////////
            //string DEVICE_NAME = "Virtual_Device";
            //string DEVICE_ENDPOINT_URL = "https://a3go886l5i56ev-ats.iot.us-east-1.amazonaws.com/things/Virtual_Device/shadow?name=Light";//  "arn:aws:iot:us-east-1:143251701433:thing/Virtual_Device"; //  "a3go886l5i56ev-ats.iot.us-east-1.amazonaws.com";
            //string DEVICE_SHADOW_NAME = "Light";

            //AmazonIotDataClient client;
            //client=new AmazonIotDataClient(DEVICE_ENDPOINT_URL);

            //UpdateThingShadowRequest req = new UpdateThingShadowRequest();
            //req.ShadowName= DEVICE_SHADOW_NAME;
            //req.ThingName= DEVICE_NAME;

            //string jsonState = "{\"state\":{\"updated\":{\"status\":\""+state+"\"}}}";
            //req.Payload=new MemoryStream(Encoding.UTF8.GetBytes(jsonState));
            //client.UpdateThingShadowAsync(req);
            ///////////////////////////////////////////////////////////////////////////
            //string access_key = "AKIASCWTXP24YUXN4WFL";
            //string secret_key = "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";

            //var iotDataClient = new AmazonIotDataClient(access_key, secret_key, RegionEndpoint.USEast1.ToString());

            //string jsonState = "{\"state\":{\"desired\":{\"status\":\""+state+"\"}}}";
            //// var iotDataClient = new AmazonIotDataClient("ACCESS_KEY", "SECRET_KEY", RegionEndpoint.USEast1.ToString());
            //var request = new PublishRequest
            //{
            //    Topic =  "$aws/things/Virtual_Device/shadow/name/Light/update",


            //    //req.Payload=new MemoryStream(Encoding.UTF8.GetBytes(jsonState));
            //    //Payload = new MemoryStream(Encoding.UTF8.GetBytes("your message"))

            //    // Topic = "your/topic",
            //    Payload = new MemoryStream(Encoding.UTF8.GetBytes(jsonState))
            //};
            //iotDataClient.PublishAsync(request);

















            //List<Country> countries = new List<Country>();
            //var uri = new Uri($"https://450b-2401-4900-1cc9-3e09-e47e-d1aa-914c-6543.in.ngrok.io");
            //try
            //{
            //    //HttpContent cntx = new HttpContent();
            //    //cntx.Headers=new Dictionary<string, string>();

            //    var response=_httpClient.GetStringAsync(uri);

            //    //var response =  _httpClient.        GetStringAsync(uri);
            //    //countries = JsonConvert.DeserializeObject<List<Country>>(response.ToString());
            //}
            //catch (Exception ex)
            //{
            //   string s= ex.Message;
            //}



















        }


        //private async Task<Country> GetCountryInfo(string countryName, ILambdaContext context)
        //{
        //    countryName = countryName.ToLowerInvariant();
        //    var countries = new List<Country>();

        //    // search by "North Korea" or "Vatican City" gives us poor results
        //    // instead search by both "North" and "Korea" to get better results
        //    var countryPartNames = countryName.Split(' ');
        //    if (countryPartNames.Length > 1)
        //    {
        //        foreach (var searchPart in countryPartNames)
        //        {
        //            // The United States of America results in too many search requests.
        //            if (searchPart != "the" || searchPart != "of")
        //            {
        //                countries.AddRange(await GetResultsForCountrySearch(searchPart, context));
        //            }
        //        }
        //    }
        //    else
        //    {
        //        countries.AddRange(await GetResultsForCountrySearch(countryName, context));
        //    }

        //    // try to find a match on the name "korea" could return both north korea and south korea
        //    var bestMatch = (from c in countries
        //                     where c.name.ToLowerInvariant() == countryName ||
        //                     c.demonym.ToLowerInvariant() == $"{countryName}n"   // north korea hack (name is not North Korea, by demonym is North Korean)
        //                     orderby c.population descending
        //                     select c).FirstOrDefault();

        //    var match = bestMatch ?? (from c in countries
        //                              where c.name.ToLowerInvariant().IndexOf(countryName) > 0
        //                              || c.demonym.ToLowerInvariant().IndexOf(countryName) > 0
        //                              orderby c.population descending
        //                              select c).FirstOrDefault();

        //    if (match == null && countries.Count > 0)
        //    {
        //        match = countries.FirstOrDefault();
        //    }

        //    return match;
        //}



        //private async Task<List<Country>> GetResultsForCountrySearch(string countryName, ILambdaContext context)
        //{
        //    List<Country> countries = new List<Country>();
        //    var uri = new Uri($"https://restcountries.eu/rest/v2/name/{countryName}");
        //    context.Logger.LogLine($"Attempting to fetch data from {uri.AbsoluteUri}");
        //    try
        //    {
        //        var response = await _httpClient.GetStringAsync(uri);
        //        context.Logger.LogLine($"Response from URL:\n{response}");
        //        // TODO: (PMO) Handle bad requests
        //        countries = JsonConvert.DeserializeObject<List<Country>>(response);
        //    }
        //    catch (Exception ex)
        //    {
        //        context.Logger.LogLine($"\nException: {ex.Message}");
        //        context.Logger.LogLine($"\nStack Trace: {ex.StackTrace}");
        //    }
        //    return countries;
        //}


        //public class Country
        //{
        //    public string name { get; set; }
        //    public string[] topLevelDomain { get; set; }
        //    public string alpha2Code { get; set; }
        //    public string alpha3Code { get; set; }
        //    public string[] callingCodes { get; set; }
        //    public string capital { get; set; }
        //    public string[] altSpellings { get; set; }
        //    public string region { get; set; }
        //    public int population { get; set; }
        //    public float[] latlng { get; set; }
        //    public string demonym { get; set; }
        //    public float area { get; set; }
        //    public float? gini { get; set; }
        //    public string[] timezones { get; set; }
        //    public string[] borders { get; set; }
        //    public string nativeName { get; set; }
        //    public string numericCode { get; set; }
        //    public Currency[] currencies { get; set; }
        //    public Language[] languages { get; set; }
        //    public Translations translations { get; set; }
        //}

        //public class Translations
        //{
        //    public string de { get; set; }
        //    public string es { get; set; }
        //    public string fr { get; set; }
        //    public string ja { get; set; }
        //    public string it { get; set; }
        //    public string br { get; set; }
        //    public string pt { get; set; }
        //}

        //public class Currency
        //{
        //    public string code { get; set; }
        //    public string name { get; set; }
        //    public string symbol { get; set; }
        //}

        //public class Language
        //{
        //    public string iso639_1 { get; set; }
        //    public string iso639_2 { get; set; }
        //    public string name { get; set; }
        //    public string nativeName { get; set; }
        //}






    }
}