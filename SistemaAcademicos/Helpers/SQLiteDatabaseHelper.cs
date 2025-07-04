﻿using SistemaAcademicos.Models;
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
            _conn.CreateTableAsync<Disciplina>().Wait();
            _conn.CreateTableAsync<Curso>().Wait();
            _conn.CreateTableAsync<Usuario>().Wait();

            _conn.ExecuteAsync("ALTER TABLE Usuario ADD COLUMN Ra TEXT").ContinueWith(t => { });
            _conn.ExecuteAsync("ALTER TABLE Usuario ADD COLUMN Email TEXT").ContinueWith(t => { });
            _conn.ExecuteAsync("ALTER TABLE Usuario ADD COLUMN Mensalidade REAL").ContinueWith(t => { });

            _conn.ExecuteAsync("ALTER TABLE Curso ADD COLUMN DisciplinaId INTEGER").ContinueWith(t => { });
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

        // Métodos para Disciplina
        public Task<int> InsertDisciplina(Disciplina d)
        {
            return _conn.InsertAsync(d);
        }

        public Task<List<Disciplina>> UpdateDisciplina(Disciplina d)
        {
            string sql = "UPDATE Disciplina SET Nome=?, Sigla=?, Observacao=? WHERE Id=?";
            return _conn.QueryAsync<Disciplina>(sql, d.Nome, d.Sigla, d.Observacao, d.Id);
        }

        public Task<List<Disciplina>> GetAllDisciplinas()
        {
            return _conn.Table<Disciplina>().ToListAsync();
        }

        public Task<List<Disciplina>> SearchDisciplina(string nome)
        {
            string sql = "SELECT * FROM Disciplina WHERE Nome LIKE ?";
            return _conn.QueryAsync<Disciplina>(sql, $"%{nome}%");
        }

        public Task<int> DeleteDisciplina(int id)
        {
            return _conn.Table<Disciplina>().DeleteAsync(i => i.Id == id);
        }

        // Métodos para Curso

        public Task<int> InsertCurso(Curso c)
        {
            return _conn.InsertAsync(c);
        }

        public Task<List<Curso>> UpdateCurso(Curso c)
        {
            string sql = "UPDATE Curso SET Nome=?, Sigla=?, Observacoes=?, PeriodoId=?, DisciplinaId=? WHERE Id=?";
            return _conn.QueryAsync<Curso>(sql, c.Nome, c.Sigla, c.Observacoes, c.PeriodoId, c.DisciplinaId, c.Id);
        }

        public Task<List<Curso>> GetAllCursos()
        {
            return _conn.Table<Curso>().ToListAsync();
        }

        public Task<List<Curso>> SearchCurso(string nome)
        {
            string sql = "SELECT * FROM Curso WHERE Nome LIKE ?";
            return _conn.QueryAsync<Curso>(sql, $"%{nome}%");
        }

        public Task<int> DeleteCurso(int id)
        {
            return _conn.Table<Curso>().DeleteAsync(i => i.Id == id);
        }

        // Métodos CRUD para Usuario
        public Task<int> InsertUsuario(Usuario usuario)
        {
            return _conn.InsertAsync(usuario);
        }

        public Task<List<Usuario>> UpdateUsuario(Usuario usuario)
        {
            string sql = "UPDATE Usuario SET Ra=?, Nome=?, Senha=?, Email=?, Mensalidade=? WHERE Id=?";
            return _conn.QueryAsync<Usuario>(sql, usuario.Ra, usuario.Nome, usuario.Senha, usuario.Email, usuario.Mensalidade, usuario.Id);
        }

        public Task<List<Usuario>> GetAllUsuarios()
        {
            return _conn.Table<Usuario>().ToListAsync();
        }

        public Task<List<Usuario>> SearchUsuario(string ra)
        {
            string sql = "SELECT * FROM Usuario WHERE Ra LIKE ?";
            return _conn.QueryAsync<Usuario>(sql, $"%{ra}%");
        }

        public Task<int> DeleteUsuario(int id)
        {
            return _conn.Table<Usuario>().DeleteAsync(u => u.Id == id);
        }
    }
}
