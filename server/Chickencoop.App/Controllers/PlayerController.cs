using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chickencoop.Models.Dtos.CreateDtos;
using Chickencoop.Models.Dtos.UpdateDtos;
using Chickencoop.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chickencoop.App.Controllers
{
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet(ApiRoutes.Players.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _playerService.GetAllPlayers());
        }

        [HttpGet(ApiRoutes.Players.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _playerService.GetPlayer(id));
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost(ApiRoutes.Players.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePlayerDto createPlayer)
        {
            try
            {
                var player = await _playerService.CreatePlayer(createPlayer);

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                var locationUri = baseUrl + "/" + ApiRoutes.Players.Get.Replace("{Id}", player.Id.ToString());

                return Created(locationUri, player);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut(ApiRoutes.Players.Update)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePlayerDto updatePlayer)
        {
            try
            {
                updatePlayer.Id = id;
                return Ok(await _playerService.UpdatePlayer(updatePlayer));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete(ApiRoutes.Players.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _playerService.DeletePlayer(id));
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
    }
}
