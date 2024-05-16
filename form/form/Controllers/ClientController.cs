using AutoMapper;
using form.Core;
using form.Data;
using form.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientFormRepository _clientFormRepository;
        private readonly IMapper _mapper;
        public ClientController(IClientFormRepository clientFormRepository, IMapper mapper)
        {
            _clientFormRepository = clientFormRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ClientForm>> CreateTask(ClientForm form)
        {

            form.Id = Guid.NewGuid().ToString();
            form.FormId = form.Id;
            var createdForm = await _clientFormRepository.CreateFormAsync(form);
            //return CreatedAtAction(nameof(GetEmoyerForms), new { id = createdForm.Id, formId = createdForm.FormId }, createdForm);
            return Ok(createdForm);
        }

        [HttpGet("form/{questionType}")]
        public async Task<ActionResult<IEnumerable<ClientFormDto>>> GetEmoyerForms(string questionType)
        {
            var result = await _clientFormRepository.GetFormAsync(questionType);
            return Ok(result.Select(x => _mapper.Map<ClientFormDto>(x)));
        }
    }
}
