using SistemaAcademicos.Models;
using System.Collections.ObjectModel;

namespace SistemaAcademicos.Views;

public partial class TelaDisciplinas : ContentPage
{
    ObservableCollection<Disciplina> lista = new ObservableCollection<Disciplina>();

    public TelaDisciplinas()
    {
        InitializeComponent();

        lstdisciplina.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        List<Disciplina> tmp = await App.Db2.GetAll();

        foreach (Disciplina disciplina in tmp)
        {
            lista.Add(disciplina);
        }
    }
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.CriarDisciplinas());
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
        List<Disciplina> tmp = await App.Db2.Search(q);
        foreach (Disciplina disciplina in tmp)
        {
            lista.Add(disciplina);
        }
    }

    private void MenuItem_Clicked(object sender, EventArgs e)
    {
    }
}