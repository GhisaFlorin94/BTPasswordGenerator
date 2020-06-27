using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BtPasswordGenerator.BusinessLogic;
using BtPasswordGenerator.BusinessLogic.ServicesInterfaces;
using BtPasswordGenerator.Model;
using BtPasswordGenerator.Model.RequestsModel;
using BtPasswordGenerator.Model.ResponseModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BtPasswordGenerator.Controllers
{
    [Route("api/password")]
    [ApiController]
    [EnableCors("AllowAll")]

    public class PasswordController : ControllerBase
    {

        private readonly ILogger<PasswordController> _logger;
        private readonly IPasswordGeneratorService _passwordGeneratorService;
        private readonly IPasswordValidatorService _passwordValidatorService;

        public PasswordController(ILogger<PasswordController> logger, IPasswordGeneratorService passwordGeneratorService, IPasswordValidatorService passwordValidatorService)
        {
            _logger = logger;
            _passwordGeneratorService = passwordGeneratorService;
            _passwordValidatorService = passwordValidatorService;

        }

        [HttpGet]
        public string Get()
        {
            return "Password API";
        }

        [HttpPost]
        [EnableCors("AllowAll")]
        public ActionResult GenerateOneTimePassword([FromBody] PasswordRequestModel passwordRequestModel)
        {
            try
            {
                if (passwordRequestModel.UserId ==0)
                {
                    return BadRequest();
                }
                if (string.IsNullOrEmpty(passwordRequestModel.DateTime))
                { 
                    passwordRequestModel.DateTime = Convert.ToString(DateTime.Now);
                }
                var password = _passwordGeneratorService.GenerateOneTimePassword(passwordRequestModel.UserId,
                    passwordRequestModel.DateTime);
                return Ok(new PasswordResponseModel()
                    {SecondsValidity = Constants.PASSWORD_LIFETIME_SECONDS, Password = password});
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred");
            }
        }

        [HttpPost]
        [Route("validate")]
        public ActionResult ValidatePassword([FromBody] PasswordValidationModel passwordValidationModel)
        {
            try
            {
                if (string.IsNullOrEmpty(passwordValidationModel.Password))
                {
                    return BadRequest();
                }
                return Ok(_passwordValidatorService.ValidatePassword(passwordValidationModel.Password, passwordValidationModel.UserId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred");
            }
        }
    }
}