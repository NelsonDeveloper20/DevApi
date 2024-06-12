using AutoMapper;
using ApiPortal_DataLake.Application.Filters;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using ApiPortal_DataLake.Domain.Shared.Constants;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using Api_Dc.Domain.Contracts;
using Api_Dc.Domain.Models;

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _iservice;
        private readonly ILogger<LoginController> _logger;
        private readonly IMapper _mapper;

        public LoginController(
            ILoginService iservice,
            ILogger<LoginController> logger,
            IMapper mapper
        )
        {
            this._iservice = iservice;
            this._logger = logger;
            this._mapper = mapper;
        }
          
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetAll(string usuario, string contrasena)
        {
            try
            {
                 var proyectos = await _iservice.ValidarLogin( usuario,  contrasena);
                 var response = this._mapper.Map<IEnumerable<dynamic>>(proyectos);

                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error Login : {JsonConvert.SerializeObject(ex)}");
                return Ok(ex);
            }
        } 
    }
}
