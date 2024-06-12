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
using Api_Dc.Domain.Models;

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/Roles")]
    [ApiController]
    public class Rolcontroller : ControllerBase
    {
        private readonly IRol _usuarioService;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public Rolcontroller(
            IRol usuarioService,
            ILogger<UsersController> logger,
            IMapper mapper
        )
        {
            this._usuarioService = usuarioService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Tbl_Rol>>> GetRol()
        {
            var usuarios = await this._usuarioService.listarRol();
            var response = this._mapper.Map<IEnumerable<Tbl_Rol>>(usuarios); 
            return Ok(response);
        }
         
    }
}
