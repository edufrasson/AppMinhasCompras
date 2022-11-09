using AppMinhasCompras.Model;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Reflection;

namespace AppMinhasCompras.Helper
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _conn;

        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path);
            _conn.CreateTableAsync<Produto>().Wait();

        }

        public Task<int> insert(Produto p)
        {
            return _conn.InsertAsync(p);
        }

        public void update(Produto p)
        {
            string sql = "UPDATE Produto SET descricao = ?, quantidade = ?, preco = ? WHERE id = ?";

            _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
        }

        public Produto getById()
        {
            return new Produto();
        }

        public Task<List<Produto>> getAll()
        {
            return _conn.Table<Produto>().ToListAsync();
        }

        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
        }
    }
}
