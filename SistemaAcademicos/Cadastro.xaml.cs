using SistemaAcademicos.Models;
using System.Collections.ObjectModel;

namespace SistemaAcademicos;

public partial class Cadastro : ContentPage
{
    ObservableCollection<Usuario> lista = new ObservableCollection<Usuario>();

    public Cadastro()
	{
		InitializeComponent();

        lstusuarios.ItemsSource = lista;
    }

    private async void Voltar(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing(); // Call the base method to ensure proper page lifecycle handling
        lista.Clear(); // Clear the existing list to avoid duplicates

        List<Usuario> tmp = await App.Db.GetAllUsuarios();

        foreach (Usuario usuario in tmp)
        {
            lista.Add(usuario);
        }
    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new CriarUsuarios());
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
                List<Usuario> tmp = await App.Db.GetAllUsuarios();
                foreach (Usuario usuario in tmp)
                    lista.Add(usuario);
            }
            else
            {
                // Busca filtrada
                List<Usuario> tmp = await App.Db.SearchUsuario(q);
                foreach (Usuario usuario in tmp)
                    lista.Add(usuario);
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
            var usuario = menuItem?.BindingContext as Usuario;

            if (usuario == null)
                return;

            // Confirmação do usuário
            bool confirm = await DisplayAlert("Confirmação", $"Remover o usuário '{usuario.Nome}'?", "Sim", "Não");
            if (!confirm)
                return;

            // Remove do banco de dados
            await App.Db.DeleteUsuario(usuario.Id);

            // Remove da lista (atualiza a tela automaticamente)
            lista.Remove(usuario);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private async void MenuItemEditar_Clicked(object sender, EventArgs e)
    {
        var menuItem = sender as MenuItem;
        var usuario = menuItem?.BindingContext as Usuario;

        if (usuario == null)
            return;

        // Navega para a tela de edição, passando o período selecionado
        await Navigation.PushAsync(new EditarUsuarios()
        {
            BindingContext = usuario
        });
    }
}