using SistemaAcademicos.Models;

namespace SistemaAcademicos.Views;

public partial class CriarCursos : ContentPage
{
	public CriarCursos()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Carrega os períodos do banco de dados
        var periodos = await App.Db.GetAll();
        picker_periodo.ItemsSource = periodos;

        var disciplinas = await App.Db.GetAllDisciplinas();
        picker_disciplina.ItemsSource = disciplinas;
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            var periodoSelecionado = picker_periodo.SelectedItem as Periodo;
            var disciplinaSelecionada = picker_disciplina.SelectedItem as Disciplina;

            if (periodoSelecionado == null)
            {
                await DisplayAlert("Atenção", "Selecione um período.", "OK");
                return;
            }
            if (disciplinaSelecionada == null)
            {
                await DisplayAlert("Atenção", "Selecione uma disciplina.", "OK");
                return;
            }

            Curso p = new Curso
            {
                Nome = txt_nome.Text,
                Sigla = txt_sigla.Text,
                Observacoes = txt_observacoes.Text,
                PeriodoId = periodoSelecionado.Id,
                DisciplinaId = disciplinaSelecionada.Id
            };
            await App.Db.InsertCurso(p);
            await DisplayAlert("Sucesso!", "Registro inserido", "OK");

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops...", ex.Message, "OK");
        }
    }
}