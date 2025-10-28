using SAPbobsCOM;
using System.Runtime.InteropServices;

namespace Api_Dc.Domain.Contracts
{
    public interface IDIAPIService
    {
        Company GetConnection();
        void ReleaseConnection(Company company);
    }
}