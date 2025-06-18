namespace SistemaAcademicos;

public partial class Cadastro : ContentPage
{
	public Cadastro()
	{
		InitializeComponent();
	}

    private async void Voltar(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}