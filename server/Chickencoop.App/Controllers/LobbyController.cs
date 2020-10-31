﻿using AutoMapper;
using Chickencoop.App.Hubs;
using Chickencoop.App.Hubs.Interfaces;
using Chickencoop.Models.Dtos.CreateDtos;
using Chickencoop.Models.Dtos.UpdateDtos;
using Chickencoop.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chickencoop.App.Controllers
{
    public class LobbyController:Controller
    {
        private readonly ILobbyService _lobbyService;
        private readonly IHubContext<LobbyHub, ILobbyHub> _hubContext;

        public LobbyController(ILobbyService lobbyService, IHubContext<LobbyHub, ILobbyHub> hubContext)
        {
            _lobbyService = lobbyService;
            _hubContext = hubContext;
        }

        [HttpGet(ApiRoutes.Lobby.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _lobbyService.GetAllLobbies());
        }

        [HttpGet(ApiRoutes.Lobby.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _lobbyService.GetLobby(id));
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost(ApiRoutes.Lobby.Create)]
        public async Task<IActionResult> Create([FromBody] CreateLobbyDto createLobby)
        {
            try
            {
                var record = await _lobbyService.CreateLobby(createLobby);

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                var locationUri = baseUrl + "/" + ApiRoutes.Lobby.Get.Replace("{Id}", record.Id.ToString());

                return Created(locationUri, record);
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

        [HttpPut(ApiRoutes.Lobby.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateLobbyDto updateLobby)
        {
            try
            {
                updateLobby.Id = id;

                if (updateLobby.PlayerTwoId != null)
                {
                    await _hubContext
                      .Clients
                      .Group(updateLobby.Id.ToString())
                      .NewPlayer(updateLobby.PlayerTwoId, updateLobby.Id);
                }

                return Ok(await _lobbyService.UpdateLobby(updateLobby));
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

        [HttpDelete(ApiRoutes.Lobby.Delete)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _lobbyService.DeleteLobby(id));
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
    }
}
