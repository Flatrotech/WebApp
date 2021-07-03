using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApp.Api.Contracts;
using WebApp.Api.Models;


namespace WebApp.Api.Services
{
    public class MojangService : IMojangService
    {
        private static IRestClient _client;
        private readonly IConfiguration _config;

        public MojangService(IConfiguration config, IRestClient client)
        {
            _client = client;
            _config = config;
        }

        public async Task<PlayerProfile> GetPlayer(string username)
        {
            _client = new RestClient();

            var request = new RestRequest($"{ _config["UsernameToUuid"] }/{ username }", Method.GET, DataFormat.Json);

            var headerResponse = await _client.ExecuteAsync(request);
            dynamic profileHeader = JsonConvert.DeserializeObject(headerResponse.Content);

            var uuid = profileHeader.id;

            _client = new RestClient();

            var playerRequest = new RestRequest($"{ _config["UuidToProfile"] }/{ uuid }", Method.GET, DataFormat.Json);

            var playerResponse = await _client.ExecuteAsync(playerRequest);
            dynamic playerJson = JsonConvert.DeserializeObject(playerResponse.Content);

            var props = playerJson.properties;

            var propList = new List<Property>();

            foreach (var prop in props)
            {
                propList.Add(new Property()
                {
                    Name = prop.name,
                    Value = prop.value,
                    Signature = prop.signature
                });
            }    

            return new PlayerProfile()
            {
                Id = playerJson.id,
                Name = playerJson.name, 
                Properties = propList.ToArray()
            };
        }
    }
}
