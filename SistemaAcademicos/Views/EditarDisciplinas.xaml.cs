using System.Collections.ObjectModel;
using SistemaAcademicos.Models;
using SistemaAcademicos.Views;

namespace SistemaAcademicos.Views;

public partial class EditarDisciplinas : ContentPage
{
    ObservableCollection<Disciplina> lista = new ObservableCollection<Disciplina>();
    public EditarDisciplinas()
    {
        InitializeComponent();

        lstdisciplinas.ItemsSource = lista;
    }

    protected override void OnAppearing()
    {
        carregarLista();
    }

    private async void carregarLista()
    {
        List<Disciplina> tmp = await App.Db2.GetAll();
        lista.Clear();
        foreach (Disciplina disciplina in tmp)
        {
            lista.Add(disciplina);
        }
    }

    private async void btnInserir_Clicked(object sender, EventArgs e)
    {
        Disciplina per = new Disciplina();
        per.Nome = txtNome.Text;

        await App.Db2.Insert(per);
        await DisplayAlert("Sucesso!", "Registro inserido", "OK");

        carregarLista();
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

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = sender as MenuItem;
            Disciplina p = selecionado.BindingContext as Disciplina;

            bool confirmacao = await DisplayAlert(
            "Confirmação", "Confirma a remoção?", "Sim", "Não");
            if (confirmacao == true)
            {
                await App.Db2.Delete(p.Id);
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private void lstdisciplinas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            Disciplina p = e.SelectedItem as Disciplina;
            txtCodigo.Text = p.Id.ToString();
            txtNome.Text = p.Nome.ToString();
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro.", ex.Message, "OK");
        }
    }

    private void btnLimpar_Clicked(object sender, EventArgs e)
    {
        txtCodigo.Text = String.Empty;
        txtNome.Text = String.Empty;
    }

    private void btnAlterar_Clicked(object sender, EventArgs e)
    {
        try
        {
            Disciplina p = new Disciplina();
            p.Id = int.Parse(txtCodigo.Text);
            p.Nome = txtNome.Text;

            App.Db2.Update(p);
            Navigation.PushAsync(new Views.EditandoDisciplinas
            {
                BindingContext = p
            });
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro.", ex.Message, "OK");
        }
    }
}