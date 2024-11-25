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
using Api_Dc.Domain.Request;
using Api_Dc.Application.Models.Request;

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/MaestroArticulo")]
    [ApiController]
    public class MaestroArticuloController : ControllerBase
    {
        private readonly IMaestroArticuloService _usuarioService;
        private readonly ILogger<MaestroArticuloController> _logger;
        private readonly IMapper _mapper;

        public MaestroArticuloController(
            IMaestroArticuloService usuarioService,
            ILogger<MaestroArticuloController> logger,
            IMapper mapper
        )
        {
            this._usuarioService = usuarioService;
            this._logger = logger;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult> ListarMaestroArticulos()
        {
            try
            {

                var response = await this._usuarioService.GetAllAsync();

                return Ok(response);
            } catch (Exception ex) { 
            return BadRequest(ex.Message);
            } 
        }
         
    }
}
