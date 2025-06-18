using SistemaAcademicos.Models;
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
            Navigation.PushAsync(new Views.CriarPeriodos());
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro.", ex.Message, "OK");
        }
    }
    
    private async void txtsearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue;
        lista.Clear();
        List<Periodo> tmp = await App.Db.Search(q);
        foreach (Periodo periodo in tmp)
        {
            lista.Add(periodo);
        }
    }
    
    private void MenuItem_Clicked(object sender, EventArgs e)
    {
    }
}