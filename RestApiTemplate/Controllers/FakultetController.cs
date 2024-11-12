using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiTemplate.BussinesLogic.Interface;
using RestApiTemplate.Models.Domain;
using RestApiTemplate.Models.DTO;
using System.ComponentModel;

namespace RestApiTemplate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakultetController : ControllerBase
    {
        private readonly IRestApiTemplateBussinesLogic _apiTemplateBussinesLogic;
        private readonly ILogger<FakultetController> _logger;


        public FakultetController(IRestApiTemplateBussinesLogic apiTemplateBussinesLogic, ILogger<FakultetController> logger)
        {
            _apiTemplateBussinesLogic = apiTemplateBussinesLogic;
            _logger = logger;
        }

        [HttpGet("getAllFaculties",Name ="GetAllFaculties")]
        public async Task<IActionResult> GetAll()
        {

            var fakulteti = await _apiTemplateBussinesLogic.GetAllFaculties();
            return Ok(fakulteti);

        }
        
        [HttpGet("getFacultyById/{id}", Name = "GetFacultyById")]
        public async Task<IActionResult> GetFacultyById(long id)
        {

            var fakulteti = await _apiTemplateBussinesLogic.GetFakultetById(id);
            return Ok(fakulteti);

        }

        [HttpPost("addNewFaculty",Name = "AddNewFaculty")]
        public async Task<IActionResult> InsertNewFakultet([FromBody] AddNewFakultet fakultet)
        {
            var response = await _apiTemplateBussinesLogic.InsertNewFakultet(fakultet);
            if (response.StatusCode == 200)
                return Ok(response);
            else
                return StatusCode(response.StatusCode, response.Message);
        }

        [HttpDelete("deleteFaculty", Name = "DeleteFaculty")] 
        public async Task<IActionResult> DeleteFakultet([FromQuery] long id)
        {
            var response = await _apiTemplateBussinesLogic.DeleteFakultetById(id);
            if (response.StatusCode == 200)
                return Ok(response);
            else
                return StatusCode(response.StatusCode, response.Message);
        }

        [HttpPost("updateFaculty/{id}", Name = "UpdateFaculty")]
        public async Task<IActionResult> UpdateFakultet(long id,[FromBody] UpdateFakultet fakultet)
        {
            var response = await _apiTemplateBussinesLogic.UpdateFakultet(id,fakultet);
            if (response.StatusCode == 200)
                return Ok(response);
            else
                return StatusCode(response.StatusCode, response.Message);
        }

        
    }
}
