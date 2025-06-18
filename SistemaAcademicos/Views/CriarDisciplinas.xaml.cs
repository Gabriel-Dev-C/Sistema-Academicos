using SistemaAcademicos.Models;

namespace SistemaAcademicos.Views;

public partial class CriarDisciplinas : ContentPage
{
	public CriarDisciplinas()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Disciplina p = new Disciplina
            {
                Nome = txt_nome.Text,
                Sigla = txt_sigla.Text
            };
            await App.Db2.Insert(p);
            await DisplayAlert("Sucesso!", "Registro inserido", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}