using BTTestAppl.BusinessLayer;
using BTTestAppl.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BTTestAppl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private PasswordHandler _passwordHandler = null;

        public LoginController(PasswordHandler passwordHandler)
        {
            _passwordHandler = passwordHandler;
        }

        [HttpPost("Login")]
        public LoginResult Login([FromBody] Login loginModel)
        {
            var result = new LoginResult();
            var oneTimePassword = _passwordHandler.GetOneTimePassword(loginModel.MemberId);

            if (oneTimePassword == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Incorrect member id";
            }
            else if (oneTimePassword.Value != loginModel.Password)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Incorrect password";
            }
            else if (oneTimePassword.ExpirationDate < DateTime.Now)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Your password expired";
            }
            else if (oneTimePassword.WasUsed)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Your password was already used";
            }
            else {
                _passwordHandler.SetPasswordUsed(loginModel.MemberId);
                result.IsSuccess = true;
            }
            return result;
        }
    }
}