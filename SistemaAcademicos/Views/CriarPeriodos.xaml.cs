using SistemaAcademicos.Models;

namespace SistemaAcademicos.Views;

public partial class CriarPeriodos : ContentPage
{
	public CriarPeriodos()
	{
        InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Periodo p = new Periodo
            {
                Nome = txt_nome.Text,
                Sigla = txt_sigla.Text
            };
            await App.Db.Insert(p);
            await DisplayAlert("Sucesso!", "Registro inserido", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops...", ex.Message, "OK");
        }
    }
}