using SistemaAcademicos.Models;
using SQLite;

namespace SistemaAcademicos.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;
        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Periodo>().Wait();
        }

        public Task<int> Insert(Periodo p)
        {
            return _conn.InsertAsync(p);
        }

        public Task<List<Periodo>> Update(Periodo p)
        {
            string sql = "UPDATE Periodo SET Nome=?, Sigla=? WHERE Id=?";

            return _conn.QueryAsync<Periodo>(sql, p.Nome, p.Sigla, p.Id);
        }

        public Task<List<Periodo>> GetAll()
        {
            return _conn.Table<Periodo>().ToListAsync();
        }

        public Task<List<Periodo>> Search(string p)
        {
            string sql = "SELECT * FROM Periodo WHERE Nome LIKE ?";

            return _conn.QueryAsync<Periodo>(sql, $"%{p}%");
        }

        public Task<int> Delete(int p)
        {
            return _conn.Table<Periodo>().DeleteAsync(i => i.Id == p);
        }
    }
}
