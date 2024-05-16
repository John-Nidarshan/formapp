using form.Core;
using Newtonsoft.Json;

namespace form.Dto
{
    public class EmployerFormDto
    {  
        public string Id { get; set; }
        public string FormId { get; set; }
        public string ProgramTitle { get; set; }
        public string ProgramDescription { get; set; }
        public PersonalInformationDto PersonalInformation { get; set; }
        public List<CustomQuestionDtos> CustomQuestions { get; set; }
        public List<CustomQuestionDtos> AdditionalQuestions { get; set; }
    }
}
