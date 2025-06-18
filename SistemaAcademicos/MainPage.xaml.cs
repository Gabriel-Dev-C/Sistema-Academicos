namespace SistemaAcademicos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void CadastrarUser(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Cadastro());
        }

    }

}
