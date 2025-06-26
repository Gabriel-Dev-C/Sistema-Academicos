using SistemaAcademicos.Models;

namespace SistemaAcademicos;

public partial class CriarUsuarios : ContentPage
{
	public CriarUsuarios()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Usuario p = new Usuario
            {
                Ra = etrRa.Text,
                Nome = etrNomeC.Text,
                Senha = etrSenha.Text,
                Email = etrEmail.Text,
                Mensalidade = decimal.TryParse(etrMensalidade.Text, out var mensalidade) ? mensalidade : 0
            };
            await App.Db.InsertUsuario(p);
            await DisplayAlert("Sucesso!", "Registro inserido", "OK");

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops...", ex.Message, "OK");
        }
    }
}