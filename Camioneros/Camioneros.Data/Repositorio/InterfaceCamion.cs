using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Camioneros.Data.Repositorio
{
    public interface InterfaceCamion
    {
        Task<IEnumerable<Camion>> GetAllCamion();
        Task<Camion> GetCamion(int Id_camion);
        Task<bool> InsertCamion(Camion camion);
        Task<bool> UpdateCamion(Camion camion);
        Task<bool> DeleteCamion(Camion camion);
    }
}
