using SistemaAcademicos.Models;
using System.Collections.ObjectModel;

namespace SistemaAcademicos.Views;

public partial class TelaDisciplinas : ContentPage
{
    ObservableCollection<Disciplina> lista = new ObservableCollection<Disciplina>();

    public TelaDisciplinas()
    {
        InitializeComponent();

        lstdisciplinas.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing(); // Call the base method to ensure proper page lifecycle handling
        lista.Clear(); // Clear the existing list to avoid duplicates

        List<Disciplina> tmp = await App.Db.GetAllDisciplinas();

        foreach (Disciplina disciplina in tmp)
        {
            lista.Add(disciplina);
        }
    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new CriarDisciplinas());
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
                List<Disciplina> tmp = await App.Db.GetAllDisciplinas();
                foreach (Disciplina disciplina in tmp)
                    lista.Add(disciplina);
            }
            else
            {
                // Busca filtrada
                List<Disciplina> tmp = await App.Db.SearchDisciplina(q);
                foreach (Disciplina disciplina in tmp)
                    lista.Add(disciplina);
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
            var disciplina = menuItem?.BindingContext as Disciplina;

            if (disciplina == null)
                return;

            // Confirmação do usuário
            bool confirm = await DisplayAlert("Confirmação", $"Remover a disciplina '{disciplina.Nome}'?", "Sim", "Não");
            if (!confirm)
                return;

            // Remove do banco de dados
            await App.Db.DeleteDisciplina(disciplina.Id);

            // Remove da lista (atualiza a tela automaticamente)
            lista.Remove(disciplina);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private async void MenuItemEditar_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var disciplina = menuItem?.BindingContext as Disciplina;

        if (disciplina == null)
            return;

        // Navega para a tela de edição, passando o período selecionado
        await Navigation.PushAsync(new EditarDisciplinas()
        {
            BindingContext = disciplina
        });
    }

}