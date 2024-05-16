using Newtonsoft.Json;

namespace form.Dto
{
    public class CustomQuestionDtos
    {
       
        public string QuestionId { get; set; }  // Added questionId
        public string Type { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public bool Other { get; set; } = false;
        public List<string> Choices { get; set; }
        public int MaxChoices { get; set; }
    }
}
