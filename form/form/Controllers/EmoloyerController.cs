using form.Core;
using form.Data;
using NSwag.Annotations;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using form.Dto;

namespace form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmoloyerController : ControllerBase
    {
        private readonly IEmoloyeeFormRepository _emoloyeeFormRepository;
        private readonly IMapper _mapper;
        public EmoloyerController(IEmoloyeeFormRepository emoloyeeFormRepository, IMapper mapper)
        {
            _emoloyeeFormRepository = emoloyeeFormRepository;
            _mapper= mapper;
        }

        [HttpGet("form/{formId}")]
        public async Task<ActionResult<IEnumerable<EmployerFormDto>>>GetEmoyerForms(string formId)
        {
            var result=await _emoloyeeFormRepository.GetEmployeeAsync(formId);
            return Ok(result.Select(x=>_mapper.Map<EmployerFormDto>(x)));

        }

        [HttpPost]
        public async Task<ActionResult<EmployerForm>> CreateTask(EmployerForm form)
        {
            
            form.Id = Guid.NewGuid().ToString();
            form.FormId = form.Id;
            var createdForm= await _emoloyeeFormRepository.CreateFormAsync(form);
            return CreatedAtAction(nameof(GetEmoyerForms), new { id = createdForm.Id, formId = createdForm.FormId }, createdForm);
        }

        [HttpPut("{formId}")]
        //[OpenApiOperation(nameof(UpdateQuestion))]
        public async Task<ActionResult> UpdateQuestion(string formId, CustomQuestion updatequestion)
        {
            var (success, updatedQuestion) = await _emoloyeeFormRepository.UpdateQuestionAsync(formId, updatequestion.QuestionId, updatequestion);
            ActionResult result; // Default return value

            if (success)
            {
                result = Ok(updatedQuestion);
            }
            else
            {
                result = NotFound();
            }
            return Ok();
        }
        [HttpDelete("{formId}")]
        public async Task<IActionResult> DeleteTask(string formId, [FromQuery] string id)
        {
            var existingTask = await _emoloyeeFormRepository.GetEmployeeAsync(formId);
            if (existingTask == null)
            {
                return NotFound();
            }

            await _emoloyeeFormRepository.DeleteFormAsync(id, formId);
            return NoContent();
        }

    }
}
