using form.Core;

namespace form.Data
{
    public interface IEmoloyeeFormRepository
    {
        Task<IEnumerable<EmployerForm>> GetEmployeeAsync(string formId);

        Task<EmployerForm> CreateFormAsync(EmployerForm form);

        Task<(bool, CustomQuestion)> UpdateQuestionAsync(string formId, string questionId, CustomQuestion newquestion);

        Task DeleteFormAsync(string Id, string formId);
    }
}
