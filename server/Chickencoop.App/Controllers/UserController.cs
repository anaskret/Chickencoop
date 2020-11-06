using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chickencoop.App.Controllers
{
    public class UserController: Controller
    {
        private readonly AmazonCognitoIdentityProviderClient _client = new AmazonCognitoIdentityProviderClient();

        private readonly string _clientId = "5cm4rfk4h6grbvcbtrbcv6sb8k";

        [HttpPost(ApiRoutes.Users.Signup)]
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
        }
    }
}
