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

namespace ApiPortal_DataLake.Application.Controllers
{
    [Route("api/ListasSisgeco")]
    [ApiController]
    public class ListasSisgecoController : ControllerBase
    {
        private readonly IListasSisgecoService _proyectoService;
        private readonly ILogger<ListasSisgecoController> _logger;
        private readonly IMapper _mapper;

        public ListasSisgecoController(
            IListasSisgecoService proyectoService,
            ILogger<ListasSisgecoController> logger,
            IMapper mapper
        )
        {
            this._proyectoService = proyectoService;
            this._logger = logger;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListasSisgecoResponse>>> GetAll(string tipo, string subfamilia)
        {
            try
            {
                
                var proyectos = await this._proyectoService.GetListar(tipo, subfamilia);
                var response = this._mapper.Map<IEnumerable<ListasSisgecoResponse>>(proyectos);
                return Ok(response);
            }
            catch (Exception ex)
            {
                this._logger.LogError($"Error Listar Solicitud : {JsonConvert.SerializeObject(ex)}");
                return Ok(ex);
            }
        }

        [HttpGet("ListarComponentesSisgeco")]
        public async Task<ActionResult> ListarComponentesSsigeco()
        {
            var response = await this._proyectoService.ListarComponentesSsigeco();
            if (response.Status == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.Status, response);
            }
        }


    }
}
