﻿using SQLite;

namespace SistemaAcademicos.Models
{
    public class Periodo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nome { get; set; }

        [NotNull, MaxLength(10)]
        public string Sigla { get; set; }
    }
}
