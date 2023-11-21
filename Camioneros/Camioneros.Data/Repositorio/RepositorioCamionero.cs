using Model;
using Dapper;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Camioneros.Data.Repositorio
{
    public class RepositorioCamionero : InterfaceCamionero
    {
        private readonly MySQLConfiguration _connectionString;

        public RepositorioCamionero(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }

        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteCamionero(Camionero camionero)
        {
            var db = dbConnection();
            var sql = @"DELETE FROM camionero WHERE id = @id";

            var result = await db.ExecuteAsync(sql, new { Id = camionero.id });

            return result > 0;
        }

        public Task<IEnumerable<Camionero>> GetAllCamionero()
        {
            var db = dbConnection();
            var sql = @"SELECT id,Nombre,Apellido,Direccion,Telefono FROM camionero";

            return db.QueryAsync<Camionero>(sql, new {});
        }

        public async Task<Camionero> GetCamionero(int id)
        {
            var db = dbConnection();

            var sql = @" SELECT id,Nombre,Apellido,Direccion,Telefono
                      FROM camionero
                      WHERE id = @id ";

            return await db.QueryFirstOrDefaultAsync<Camionero>(sql, new { Id = id });
        }

        public async Task<bool> InsertCamionero(Camionero camionero)
        {
            var db = dbConnection();
            var sql = @"INSERT INTO camionero(Nombre,Apellido,Direccion,Telefono)
                      VALUES(@Nombre,@Apellido,@Direccion,@Telefono)";

            var result = await db.ExecuteAsync(sql, new
            {
                camionero.Nombre,
                camionero.Apellido,
                camionero.Direccion,
                camionero.Telefono,
            });
            return result > 0;
        }

        public async Task<bool> UpdateCamionero(Camionero camionero)
        {
            var db = dbConnection();
            var sql = @"UPDATE camionero SET Nombre=@Nombre,Apellido=@Apellido,Direccion=@Direccion,
                      Telefono=@Telefono
                      WHERE id = @id"
                      ;

            var result = await db.ExecuteAsync(sql, new
            {
                camionero.Nombre,
                camionero.Apellido,
                camionero.Direccion,
                camionero.Telefono,
            });
            return result > 0;
        }
    }
}
