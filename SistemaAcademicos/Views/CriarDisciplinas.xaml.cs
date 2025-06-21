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
                Sigla = txt_sigla.Text,
                Observacao = txt_observacoes.Text
            };
            await App.Db.InsertDisciplina(p);
            await DisplayAlert("Sucesso!", "Registro inserido", "OK");

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops...", ex.Message, "OK");
        }
    }
}