using SistemaAcademicos.Views;

namespace SistemaAcademicos
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("telaperiodos", typeof(TelaPeriodos));
        }
    }
}
