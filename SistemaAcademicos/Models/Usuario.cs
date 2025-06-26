using SQLite;

namespace SistemaAcademicos.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(20)]
        public string Ra { get; set; }

        [NotNull, MaxLength(50)]
        public string Nome { get; set; }

        [NotNull, MaxLength(30)]
        public string Senha { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        public decimal Mensalidade { get; set; }
    }
}
