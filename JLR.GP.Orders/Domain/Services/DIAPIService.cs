
using Api_Dc.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using SAPbobsCOM;
using System.Runtime.InteropServices;
namespace ApiPortal_DataLake.Domain.Services
{
    public class DIAPIService : IDIAPIService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DIAPIService> _logger;

        public DIAPIService(IConfiguration configuration, ILogger<DIAPIService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public Company GetConnection()
        {
            Company company = null;

            try
            {
                // ✅ 1️⃣ Verificamos si la DI API está registrada antes de usarla
                if (!IsDIAPIRegistered())
                {
                    var msg = "SAP DI API no está instalada o registrada en este servidor. " +
                              "Instale el SDK de SAP Business One o use Service Layer.";
                    _logger.LogCritical(msg);
                    throw new InvalidOperationException(msg);
                }

                try
                {
                    // Crear instancia COM de SAP directamente desde el CLSID
                    var sapCompanyType = Marshal.GetTypeFromCLSID(
                        new Guid("632F4591-AA62-4219-8FB6-22BCF5F60100")
                    );
                    company = (Company)Activator.CreateInstance(sapCompanyType);
                }
                catch (Exception ex)
                {
                    var msg = "No se pudo crear la instancia de SAP DI API. Verifique que el SDK esté instalado.";
                    _logger.LogCritical(ex, msg);
                    throw new InvalidOperationException(msg, ex);
                }

                // ✅ 3️⃣ Configuración normal de conexión



                company.Server = _configuration["ConexionHana:Server"];
                company.DbServerType = (BoDataServerTypes)9; // 9 = HANA
                company.DbUserName = _configuration["ConexionHana:DBUser"];
                company.DbPassword = _configuration["ConexionHana:DBPassword"];
                company.UserName = _configuration["ConexionHana:SAPUser"];
                company.Password = _configuration["ConexionHana:SAPPassword"];
                company.CompanyDB = _configuration["ConexionHana:Schema"];
                company.language = BoSuppLangs.ln_Spanish_La;


                _logger.LogInformation("Intentando conectar a SAP B1 - Server: {Server}, DB: {DB}",
                    company.Server, company.CompanyDB);

                int result = company.Connect();

                if (result != 0)
                {
                    string errorMessage = company.GetLastErrorDescription();
                    int errorCode = company.GetLastErrorCode();

                    _logger.LogError("Error DI API: [{Code}] {Message}", errorCode, errorMessage);

                    ReleaseConnection(company);
                    throw new Exception($"Error al conectar con SAP B1: [{errorCode}] {errorMessage}");
                }

                _logger.LogInformation("Conexión DI API establecida exitosamente - Versión: {Version}",
                    company.Version);

                return company;
            }
            catch (InvalidOperationException ex)
            {
                // Mensajes controlados (sin stacktrace innecesario)
                _logger.LogWarning(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error crítico al conectar con SAP B1 DI API");
                ReleaseConnection(company);
                throw;
            }
        }

        public void ReleaseConnection(Company company)
        {
            if (company == null) return;

            try
            {
                if (company.Connected)
                {
                    company.Disconnect();
                }

                Marshal.ReleaseComObject(company);
                GC.Collect();
                GC.WaitForPendingFinalizers();

                _logger.LogInformation("Conexión DI API liberada correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al liberar conexión DI API");
            }
        }

        // ✅ 4️⃣ Método auxiliar para detectar si la DI API está registrada
        private bool IsDIAPIRegistered()
        {
            try
            {
                Type diapiType = Type.GetTypeFromProgID("SAPbobsCOM.Company");
                return diapiType != null;
            }
            catch
            {
                return false;
            }
        }
    }
}