using form.Core;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace form.Data
{
    public class ClientFormRepository: IClientFormRepository
    {

        private readonly CosmosClient cosmosClient;
        private readonly IConfiguration configuration;
        private readonly Container _container;

        public ClientFormRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            this.cosmosClient = cosmosClient;
            this.configuration = configuration;
            var datBaseName = configuration["CosmosDbSettings:DatabaseName"];
            var containerName = "clientForm";
            _container = cosmosClient.GetContainer(datBaseName, containerName);
        }


        public async Task<ClientForm> CreateFormAsync(ClientForm form)
        {
            form.AdditionalQuestion.ForEach(s =>

            {
                if (string.IsNullOrEmpty(s.QuestionId))
                {
                    s.QuestionId = Guid.NewGuid().ToString();

                }

            });
            form.majorquestions.ForEach(s =>

            {
                if (string.IsNullOrEmpty(s.QuestionId))
                {
                    s.QuestionId = Guid.NewGuid().ToString();

                }

            });


            var response = await _container.CreateItemAsync(form);
            return response.Resource;
        }


        public async Task<IEnumerable<ClientForm>> GetFormAsync(string questionType)
        {
            var query = _container.GetItemLinqQueryable<ClientForm>()
                         .Where(e => 
                                     (e.majorquestions.Any(q => q.Type == questionType) ||
                                      e.AdditionalQuestion.Any(q => q.Type == questionType)))
                         .ToFeedIterator();
            var lists = new List<ClientForm>();
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
    }
}
