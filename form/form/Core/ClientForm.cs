using Newtonsoft.Json;

namespace form.Core
{
    public class ClientForm
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("formid")]
        public string FormId { get; set; }
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        public List<CustomQuestion> majorquestions { get; set; }
        public List<CustomQuestion> AdditionalQuestion { get; set; }
    }


   
}
