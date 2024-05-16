using form.Core;

namespace form.Data
{
    public interface IClientFormRepository
    {
        Task<ClientForm> CreateFormAsync(ClientForm form);

        Task<IEnumerable<ClientForm>> GetFormAsync(string questionType);


     }
}
