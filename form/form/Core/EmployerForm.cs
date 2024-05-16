using Newtonsoft.Json;
using NSwag.Annotations;

namespace form.Core
{
    public class CustomQuestion
    {
       
        [JsonProperty("questionId")]
        public string QuestionId { get; set; }  // Added questionId

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("question")]
        public string Question { get; set; }

        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty( "other")]
        public bool Other { get; set; }=false;

        [JsonProperty("choices")]
        public List<string> Choices { get; set; }

        [JsonProperty("maxChoices")]
        public int MaxChoices { get; set; }
    }

    public class PersonalInformation
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("nationality")]
        public string Nationality { get; set; }

        [JsonProperty("currentResidence")]
        public string CurrentResidence { get; set; }

        [JsonProperty("idNumber")]
        public string IdNumber { get; set; }

        [JsonProperty("dateOfBirth")]
        public string DateOfBirth { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("additionalMultipleChoiceQuestion")]
        public List<CustomQuestion> AdditionalMultipleChoiceQuestion { get; set; }
    }

    public class EmployerForm
    {
        
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("formid")]
        public string FormId { get; set; }

        [JsonProperty("programTitle")]
        public string ProgramTitle { get; set; }

        [JsonProperty( "programDescription")]
        public string ProgramDescription { get; set; }

        [JsonProperty("personalInformation")]
        public PersonalInformation PersonalInformation { get; set; }

        [JsonProperty("customQuestions")]
        public List<CustomQuestion> CustomQuestions { get; set; }

        [JsonProperty("additionalQuestions")]
        public List<CustomQuestion> AdditionalQuestions { get; set; }
    }

}
