using SQLite;

namespace SistemaAcademicos.Models
{
    public class Disciplina
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nome { get; set; }

        [NotNull, MaxLength(10)]
        public string Sigla { get; set; }

        [MaxLength(100)]
        public string Observacao { get; set; }
    }
}
