using System;
using BTTestAppl.BusinessLayer;
using BTTestAppl.Models;
using Microsoft.AspNetCore.Mvc;

namespace BTTestAppl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeneratePasswordController : Controller
    {
        private PasswordHandler _passwordHandler = null;
        public GeneratePasswordController(PasswordHandler passwordHandler) {
            _passwordHandler = passwordHandler;
        }

        [HttpPost("GenerateOneTimePassword")]
        public Password GenerateOneTimePassword([FromBody]Member member) {

            if(string.IsNullOrWhiteSpace(member.MemberId))
                throw new Exception("Invalid member Id");

            var oneTimePassword = _passwordHandler.GenerateOneTimePassword(member, DateTime.Now.AddSeconds(30));
            
            return oneTimePassword;
        }
    }
}