using SistemaAcademicos.Models;
using SistemaAcademicos.Views;
using System.Collections.ObjectModel;

namespace SistemaAcademicos.Views;

public partial class TelaPeriodos : ContentPage
{
	ObservableCollection<Periodo> lista = new ObservableCollection<Periodo>();

    public TelaPeriodos()
	{
		InitializeComponent();

		lstperiodos.ItemsSource = lista;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing(); // Call the base method to ensure proper page lifecycle handling
        lista.Clear(); // Clear the existing list to avoid duplicates

        List<Periodo> tmp = await App.Db.GetAll();

        foreach (Periodo periodo in tmp)
        {
            lista.Add(periodo);
        }
    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new CriarPeriodos());
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
                List<Periodo> tmp = await App.Db.GetAll();
                foreach (Periodo periodo in tmp)
                    lista.Add(periodo);
            }
            else
            {
                // Busca filtrada
                List<Periodo> tmp = await App.Db.Search(q);
                foreach (Periodo periodo in tmp)
                    lista.Add(periodo);
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
            var periodo = menuItem?.BindingContext as Periodo;

            if (periodo == null)
                return;

            // Confirmação do usuário
            bool confirm = await DisplayAlert("Confirmação", $"Remover o período '{periodo.Nome}'?", "Sim", "Não");
            if (!confirm)
                return;

            // Remove do banco de dados
            await App.Db.Delete(periodo.Id);

            // Remove da lista (atualiza a tela automaticamente)
            lista.Remove(periodo);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private async void MenuItemEditar_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var periodo = menuItem?.BindingContext as Periodo;

        if (periodo == null)
            return;

        // Navega para a tela de edição, passando o período selecionado
        await Navigation.PushAsync(new EditarPeriodos
        {
            BindingContext = periodo
        });
    }
}