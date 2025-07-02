using SistemaAcademicos.Models;
using System.Collections.ObjectModel;

namespace SistemaAcademicos.Views;

public partial class TelaCursos : ContentPage
{
    ObservableCollection<Curso> lista = new ObservableCollection<Curso>();

    public TelaCursos()
	{
		InitializeComponent();

		lstcursos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        lista.Clear();

        // Busca todos os cursos e períodos
        List<Curso> tmp = await App.Db.GetAllCursos();
        var periodos = await App.Db.GetAll();
        var disciplinas = await App.Db.GetAllDisciplinas();

        // Preenche a propriedade Periodo de cada curso
        foreach (Curso curso in tmp)
        {
            curso.Periodo = periodos.FirstOrDefault(p => p.Id == curso.PeriodoId);
            curso.Disciplina = disciplinas.FirstOrDefault(d => d.Id == curso.DisciplinaId);
            lista.Add(curso);
        }
    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new CriarCursos());
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro.", ex.Message, "OK");
        }
    }

    private bool _isSearching = false;
    private async void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (_isSearching) return; // Evita concorrência
        _isSearching = true;

        try
        {
            string q = e.NewTextValue?.Trim() ?? "";
            lista.Clear();

            if (string.IsNullOrWhiteSpace(q))
            {
                // Se o campo está vazio, recarrega todos os itens
                List<Curso> tmp = await App.Db.GetAllCursos();
                foreach (Curso curso in tmp)
                    lista.Add(curso);
            }
            else
            {
                // Busca filtrada
                List<Curso> tmp = await App.Db.SearchCurso(q);
                foreach (Curso curso in tmp)
                    lista.Add(curso);
            }
        }
        finally
        {
            _isSearching = false;
        }
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Recupera o MenuItem e o Periodo associado
            var menuItem = sender as MenuItem;
            var curso = menuItem?.BindingContext as Curso;

            if (curso == null)
                return;

            // Confirmação do usuário
            bool confirm = await DisplayAlert("Confirmação", $"Remover o curso '{curso.Nome}'?", "Sim", "Não");
            if (!confirm)
                return;

            // Remove do banco de dados
            await App.Db.DeleteCurso(curso.Id);

            // Remove da lista (atualiza a tela automaticamente)
            lista.Remove(curso);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private async void MenuItemEditar_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var curso = menuItem?.BindingContext as Curso;

        if (curso == null)
            return;

        // Navega para a tela de edição, passando o período selecionado
        await Navigation.PushAsync(new EditarCursos
        {
            BindingContext = curso
        });
    }
}