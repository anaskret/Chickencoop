using Chickencoop.Models.Dtos.CreateDtos;
using Chickencoop.Models.Dtos.UpdateDtos;
using Chickencoop.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chickencoop.App.Controllers
{
    public class PersonalLeaderboeardController: Controller
    {
        private readonly IPersonalLeaderboardService _service;

        public PersonalLeaderboeardController(IPersonalLeaderboardService service)
        {
            _service = service;
        }

        [HttpGet(ApiRoutes.PersonalLeaderboard.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllRecords());
        }

        [HttpGet(ApiRoutes.PersonalLeaderboard.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.GetRecord(id));
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost(ApiRoutes.PersonalLeaderboard.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePersonalLeaderboardDto createRecord)
        {
            try
            {
                var record = await _service.CreateRecord(createRecord);

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                var locationUri = baseUrl + "/" + ApiRoutes.PersonalLeaderboard.Get.Replace("{Id}", record.Id.ToString());

                return Created(locationUri, record);
            }
            catch(NullReferenceException ex)
            {
                return NotFound(ex);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut(ApiRoutes.PersonalLeaderboard.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatePersonalLeaderboardDto updateRecord)
        {
            try
            {
                updateRecord.Id = id;
                return Ok(await _service.UpdateRecord(updateRecord));
            }
            catch(NullReferenceException ex)
            {
                return NotFound(ex);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete(ApiRoutes.PersonalLeaderboard.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _service.DeleteRecord(id));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
