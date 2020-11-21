using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Chickencoop.Models.Dtos.CreateDtos;
using Chickencoop.Models.Dtos.UpdateDtos;
using Chickencoop.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chickencoop.App.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;
        private readonly AmazonCognitoIdentityProviderClient _client = new AmazonCognitoIdentityProviderClient();

        //private readonly string_clientId = "1s15mbpqua63v46o0qbgd20m71";
        private readonly string _poolId = "eu-central-1_jvYBzSwNe";
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
        public async Task<IActionResult> Get([FromRoute] string username)
        {
            try
            {
                return Ok(await _playerService.GetPlayer(username));
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
        }
        [HttpGet(ApiRoutes.Players.GetById)]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                return Ok(await _playerService.GetPlayerById(id));
            }
            catch(Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost(ApiRoutes.Players.Create)]
        public async Task<IActionResult> Create([FromBody] string token)
        {
            try
            {
                GetUserRequest getUserRequest = new GetUserRequest() { AccessToken = token };
                var cognitoUser = _client.GetUserAsync(getUserRequest);

                CreatePlayerDto createPlayer = new CreatePlayerDto() { Nickname = cognitoUser.Result.Username };

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

        [HttpPost(ApiRoutes.Players.CheckForUpdates)]
        public async Task<IActionResult> CheckPlayers()
        {
            try 
            { 
                ListUsersRequest request = new ListUsersRequest() { UserPoolId = _poolId };

                var listOfUsers = await _client.ListUsersAsync(request);

                if (!await _playerService.CheckPlayers(listOfUsers))
                    return BadRequest("Failed to update the list of users");

                return Ok("User database is up to date");
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        /*[HttpPost(ApiRoutes.Users.Signup)]
        public async Task<IActionResult> SignUp(string password, string username, string email)
        {
            SignUpRequest signUpRequest = new SignUpRequest()
            {
                ClientId = _clientId,
                Password = password,
                Username = username
            };
            AttributeType emailAttribute = new AttributeType()
            {
                Name = "email",
                Value = email
            };

            signUpRequest.UserAttributes.Add(emailAttribute);

            var signUpResult = await _client.SignUpAsync(signUpRequest);

            return Ok(signUpResult);
        }

        [HttpPost(ApiRoutes.Users.Signin)]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var authReq = new InitiateAuthRequest()
            {
                ClientId = _clientId,
                AuthFlow = AuthFlowType.USER_PASSWORD_AUTH
            };
            authReq.AuthParameters.Add("USERNAME", username);
            authReq.AuthParameters.Add("PASSWORD", password);

            var authResponse = await _client.InitiateAuthAsync(authReq);

            return Ok(authResponse);
        }*/
    }
}
