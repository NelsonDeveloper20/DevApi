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
    [Route("api/Estacion")]
    [ApiController]
    public class EstacionController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;

        public EstacionController(
            IUsuarioService usuarioService,
            ILogger<UsersController> logger,
            IMapper mapper
        )
        {
            this._usuarioService = usuarioService;
            this._logger = logger;
            this._mapper = mapper;
        }

        [HttpGet]
        //[ValidateGroups(RolesConstant.Administrador)]
        public async Task<ActionResult<IEnumerable<Tbl_DetalleProductoEstacion>>> GetEstacion()
        {
            var usuarios = await this._usuarioService.listarusuario();
            var response = this._mapper.Map<IEnumerable<Tbl_DetalleProductoEstacion>>(usuarios); 
            return Ok(response);
        }
         
    }
}
