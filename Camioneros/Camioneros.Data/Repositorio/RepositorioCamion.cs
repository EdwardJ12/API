using Dapper;
using MySql.Data.MySqlClient;

namespace Camioneros.Data.Repositorio
{
    public class RepositorioCamion : InterfaceCamion
    {
        private readonly MySQLConfiguration _connectionString;

        public RepositorioCamion(MySQLConfiguration connectionString)
        {
            _connectionString = connectionString;
        }
        protected MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString.ConnectionString);
        }

        public Task<IEnumerable<Camion>> GetAllCamion()
        {
            var db = dbConnection();
            var sql = @"SELECT id,marca,Patente,peso_permitido,Id_camionero FROM camion";

            return db.QueryAsync<Camion>(sql, new { });
        }

        public async Task<Camion> GetCamion(int id )
        {
            var db = dbConnection();
            var sql = @"SELECT id,marca,Patente,peso_permitido,Id_camionero
                      FROM camion
                      WHERE id = @id";

            return await db.QueryFirstOrDefaultAsync<Camion>(sql, new { Id = id});
        }

        public async Task<bool> InsertCamion(Camion camion)
        {
            var db = dbConnection();
            var sql = "INSERT INTO camion(id,marca,Patente,peso_permitido,id) VALUES (@id,@marca,@Patente,@peso_permitido,@id)";

            var result = await db.ExecuteAsync(sql, new
            {
                camion.id,
                camion.marca,
                camion.Patente,
                camion.peso_permitido,
                camion.Id_camionero,
            });
            return result > 0;
        }

        public async Task<bool> UpdateCamion(Camion camion)
        {
            var db = dbConnection();
            var sql = @"UPDATE camion SET marca=@marca,Patente=@Patente,peso_permitido=@peso_permitido,
                      id=@id
                      WHERE id = @id"
                      ;


            var result = await db.ExecuteAsync(sql, new
            {
                camion.id,
                camion.marca,
                camion.Patente,
                camion.peso_permitido,
                camion.Id_camionero,
            });
            return result > 0;
        }

        public async Task<bool> DeleteCamion(Camion camion)
        {
            var db = dbConnection();
            var sql = @"DELETE FROM camion WHERE id = @id";

            var result = await db.ExecuteAsync(sql, new { Id = camion.id });

            return result > 0;
        }

    }
}
