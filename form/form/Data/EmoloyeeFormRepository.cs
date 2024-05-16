using form.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Threading.Tasks;

namespace form.Data
{
    public class EmoloyeeFormRepository : IEmoloyeeFormRepository
    {
        private readonly CosmosClient cosmosClient;
        private readonly IConfiguration configuration;
        private readonly Container _container;

        public EmoloyeeFormRepository(CosmosClient cosmosClient ,IConfiguration configuration)
        {
            this.cosmosClient= cosmosClient;
            this.configuration= configuration;
            var datBaseName = configuration["CosmosDbSettings:DatabaseName"];
            var containerName = "employerform";
            _container=cosmosClient.GetContainer(datBaseName, containerName);
        }


        public async Task<EmployerForm> CreateFormAsync(EmployerForm form)
        {

          
            form.AdditionalQuestions.ForEach(s =>
            {
                if (string.IsNullOrEmpty(s.QuestionId))
                {
                    s.QuestionId = Guid.NewGuid().ToString();
                }
            });
            form.CustomQuestions.ForEach(s =>
            {
                if (string.IsNullOrEmpty(s.QuestionId))
                {
                    s.QuestionId = Guid.NewGuid().ToString();
                }
            });

            form.PersonalInformation.AdditionalMultipleChoiceQuestion.ForEach(s =>

            {
                if (string.IsNullOrEmpty(s.QuestionId))
                {
                    s.QuestionId = Guid.NewGuid().ToString();

                }

            });
                

            var response = await _container.CreateItemAsync(form);
            return response.Resource;
        }
        public async Task<IEnumerable<EmployerForm>> GetEmployeeAsync(string formId)
        {
            var query = _container.GetItemLinqQueryable<EmployerForm>().
                      Where(e => e.FormId == formId).ToFeedIterator();
            var lists = new List<EmployerForm>();
            while (query.HasMoreResults)
            {
                try
                {
                    var response = await query.ReadNextAsync();
                    lists.AddRange(response.ToList());

                }
                catch (Exception ex)
                {

                }
               
              
            }
            return lists;
            
        }

        public async Task<(bool, CustomQuestion)> UpdateQuestionAsync(string formId,string questionId, CustomQuestion newquestion)
        {
            var query = _container.GetItemLinqQueryable<EmployerForm>()
                  .Where(t => t.FormId == formId)
                  .Take(1)
                  .ToFeedIterator();

            var response = await query.ReadNextAsync();
            var existingform = response.FirstOrDefault();

            (bool success, CustomQuestion updatedQuestion) result = (false, null);
            if (existingform != null)
            {
                var questionToUpdate = FindQuestionById(existingform, questionId);
                if (questionToUpdate != null)
                {
                    // Update the question
                    questionToUpdate.Question = newquestion.Question;
                    questionToUpdate.Type = newquestion.Type;
                    questionToUpdate.Other= newquestion.Other;
                    questionToUpdate.Choices= newquestion.Choices;
                    questionToUpdate.MaxChoices= newquestion.MaxChoices;
                    // Save the updated form back to Cosmos DB
                    var updateResponse = await _container.ReplaceItemAsync(existingform, existingform.Id, new PartitionKey(existingform.FormId));
                    // Handle update response
                    result=(true, newquestion);
                }
                else
                {
                    result=(false,null);
                }
            }

            return result;
        }


        private CustomQuestion FindQuestionById(EmployerForm form, string questionId)
        {
            // Search in PersonalInformation
            var question = form.PersonalInformation?.AdditionalMultipleChoiceQuestion?.FirstOrDefault(q => q.QuestionId == questionId);
            if (question != null)
                return question;

            // Search in CustomQuestions
            question = form.CustomQuestions?.FirstOrDefault(q => q.QuestionId == questionId);
            if (question != null)
                return question;

            // Search in AdditionalQuestions
            question = form.AdditionalQuestions?.FirstOrDefault(q => q.QuestionId == questionId);
            return question;
        }

        public async Task DeleteFormAsync(string Id, string formId)
        {
            await _container.DeleteItemAsync<EmployerForm>(Id, new PartitionKey(formId));
        }

    }
}
