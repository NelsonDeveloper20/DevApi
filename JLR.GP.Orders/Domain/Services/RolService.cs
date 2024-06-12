using Api_Dc.Application.Models.Response;
using Api_Dc.Domain.Contracts;
using Api_Dc.Domain.Models;
using ApiPortal_DataLake.Application.Models.Request;
using ApiPortal_DataLake.Application.Models.Response;
using ApiPortal_DataLake.Domain.Contracts;
using ApiPortal_DataLake.Domain.Models;
using ApiPortal_DataLake.Domain.Response;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace ApiPortal_DataLake.Domain.Services
{
    public class RolService : IRol
    {
        private readonly IConfiguration _configuration;
        private readonly DBContext _context;
        private readonly ILogger<ProductoService> _logger;

        public RolService(
            IConfiguration configuration,
            DBContext context,
            ILogger<ProductoService> logger
        )
        {
            this._configuration = configuration;
            this._context = context;
            this._logger = logger;
        }

        public async Task<IEnumerable<Tbl_Rol>> listarRol()
        {

            var _user = this._context.Tbl_Rol.ToList();
            return _user;
        }


    }
}
