using System;
using RestSharp;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace SmallWoodlandCreatureBuilder
{
    class Program
    {
        private const string filePath = "C:\\Git\\ChrisPSheehan\\SmallWoodlandCreatures\\Data.json";

        static void Main(string[] args)
        {
            Console.WriteLine($"Loading critters from {filePath}");

            var json = System.IO.File.ReadAllText(filePath);

            dynamic objects = JsonConvert.DeserializeObject<List<CreatureInput>>(json);

            var client = new RestClient("https://localhost:5001/api");

            client.RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            
            foreach(CreatureInput creature in objects)
            {
                IRestRequest request = new RestRequest("/Creatures", Method.POST);

                Console.WriteLine($"Found {creature.Name}...");

                string postString = JsonConvert.SerializeObject(creature).ToString();

                request.AddParameter("application/json", postString, ParameterType.RequestBody);;

                IRestResponse resp = client.Execute(request);            
            }
        }
    }
}
