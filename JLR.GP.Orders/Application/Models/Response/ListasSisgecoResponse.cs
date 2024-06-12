using ApiPortal_DataLake.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiPortal_DataLake.Application.Models.Response
{
    public class ListasSisgecoResponse
    {

        public string Codigo { get; set; }
        public string? Nombre { get; set; } 

    }
}
