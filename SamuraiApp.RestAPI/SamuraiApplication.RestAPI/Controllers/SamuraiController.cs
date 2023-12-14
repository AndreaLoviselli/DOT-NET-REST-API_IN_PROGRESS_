using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using SamuraiApp.Core.Model;
using SamuraiApp.Core.Model.Errors;
using SamuraiApp.Core.Service;
using SamuraiApp.Core.Service.Storage;
using SamuraiApplication.RestAPI.Controllers.Model;
using System;

namespace SamuraiApplication.RestAPI.Controllers
{
    [Route("samurais")]
    [ApiController]
    public class SamuraiController : ControllerBase
    {
        SamuraiApplicationManager _applicationManager;
        public SamuraiController(SamuraiApplicationManager applicationManager)
        {
            _applicationManager = applicationManager;
        }

        [HttpGet]
        public ActionResult<ICollection<SamuraiDTO>> GetAll()
        {
            return _applicationManager.GetSamurais().Select(ConvertSamuraiInDTO).ToList();
        }

        [HttpGet]
        [Route("{samurai-id}")]
        public ActionResult<SamuraiDTO> GetById([FromRoute(Name = "samurai-id")] int samuraiId) 
        {
            var samurai = _applicationManager.GetById(samuraiId);
            if (samurai == null) return NotFound(Error($"Samurai {samuraiId} non trovato"));
            else return ConvertSamuraiInDTO(samurai);
        }

        [HttpPost]
        public ActionResult<SamuraiDTO> Create([FromBody] SamuraiCreateRequest samuraiToCreate)
        {
            try {
                var newSamurai = _applicationManager.CreateSamurai(samuraiToCreate.Name, samuraiToCreate.LifePoints);
                return ConvertSamuraiInDTO(newSamurai);

            }
            catch (NegativeLifePointsException ex)
            {
                return BadRequest(Error(ex.Message));
            }
        }

        [HttpPut]
        [Route("{samurai-id}")]
        public ActionResult<SamuraiDTO> Update(
            [FromRoute(Name = "samurai-id")] int samuraiId,
            [FromBody] SamuraiUpdateRequest samuraiToUpdate
            )
        {
            try
            {
                var updatedSamurai = _applicationManager.SaveSamurai(samuraiId, samuraiToUpdate.Name, samuraiToUpdate.LifePoints);

                if (updatedSamurai == null) return NotFound(Error($"Samurai {samuraiId} non trovato"));
                else return ConvertSamuraiInDTO(updatedSamurai);

            }
            catch (NegativeLifePointsException ex)
            {
                return BadRequest(Error(ex.Message));
            }
        }

        [HttpDelete]
        [Route("{samurai-id}")]
        public ActionResult Delete([FromRoute(Name = "samurai-id")] int samuraiId)
        {
            var samuraiToDelete = _applicationManager.GetById(samuraiId);
            if (samuraiToDelete != null)
            {
                _applicationManager.DeleteSamurai(samuraiId);
                return NoContent();
            }
            else
            {
                return NotFound(Error($"Samurai {samuraiId} non trovato"));
            }
        }

        private SamuraiDTO ConvertSamuraiInDTO (Samurai? samurai)
        { 
            return new SamuraiDTO
            {
                Id = samurai.Id,
                Name = samurai.Name,
                LifePoints = samurai.LifePoints,
            };
        }

        private ErrorMessage Error(string msg) => new ErrorMessage
        {
            Error = msg
        };
                  
    }
}
