using Newtonsoft.Json;

namespace WebApp.Api.Models
{
    public class PlayerProfile
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Property[] Properties { get; set; }
    }
}
