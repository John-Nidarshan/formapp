using form.Core;
using Newtonsoft.Json;

namespace form.Dto
{
    public class ClientFormDto
    {
        
        public string Id { get; set; }

       
        public string FormId { get; set; }
       
        public string FirstName { get; set; }

       
        public string LastName { get; set; }

      
        public string Email { get; set; }

        
        public string Phone { get; set; }

        public List<CustomQuestionDtos> majorquestions { get; set; }
        public List<CustomQuestionDtos> AdditionalQuestion { get; set; }
    }
}
