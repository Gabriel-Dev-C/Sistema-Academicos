using SQLite;

namespace SistemaAcademicos.Models
{
    public class Curso
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(50)]
        public string Nome { get; set; }

        [NotNull, MaxLength(25)]
        public string Sigla { get; set; }

        [MaxLength(600)]
        public string Observacoes { get; set; }

        // Chave estrangeira para Periodo
        [Indexed]
        public int PeriodoId { get; set; }

        // Chave estrangeira para Disciplina
        [Indexed]
        public int DisciplinaId { get; set; }

        // Propriedade de navegação (não mapeada no banco)
        [Ignore]
        public Periodo Periodo { get; set; }

        [Ignore]
        public Disciplina Disciplina { get; set; }
    }
}
