using SQLite;

namespace SistemaAcademicos.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull, MaxLength(50)]
        public string Nome { get; set; }

        [NotNull, MaxLength(30)]
        public string Senha { get; set; }
    }
}
