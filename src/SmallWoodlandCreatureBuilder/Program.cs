using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace SmallWoodlandCreatureBuilder
{
    class Program
    {
        private const string filePath = "C:\\Git\\ChrisPSheehan\\SmallWoodlandCreatures\\Data.json";

        static void Main(string[] args)
        {
            var json = System.IO.File.ReadAllText(filePath);

            Console.Write(json);

            dynamic objects = JsonConvert.DeserializeObject<List<CreatureInput>>(json);

            var client = new RestClient("https://localhost:5001/api");

            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            
            foreach(CreatureInput creature in objects)
            {
                IRestRequest request = new RestRequest("/Creatures", Method.POST);

                Console.WriteLine(creature.Name);

                string postString = JsonConvert.SerializeObject(creature).ToString();

                Console.WriteLine(postString);

                request.AddParameter("application/json", postString, ParameterType.RequestBody);;

                IRestResponse resp = client.Execute(request);            
            }
        }
    }
}
