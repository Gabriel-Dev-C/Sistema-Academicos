using System.Collections.ObjectModel;
using SistemaAcademicos.Models;
using SistemaAcademicos.Views;

namespace SistemaAcademicos.Views
{
    public partial class EditarPeriodos : ContentPage
    {
        ObservableCollection<Periodo> lista = new ObservableCollection<Periodo>();
        public EditarPeriodos()
        {
            InitializeComponent();

            lstperiodos.ItemsSource = lista;
        }

        protected override void OnAppearing()
        {
            carregarLista();
        }

        private async void carregarLista()
        {
            List<Periodo> tmp = await App.Db.GetAll();
            lista.Clear();
            foreach (Periodo periodo in tmp)
            {
                lista.Add(periodo);
            }
        }

        private async void btnInserir_Clicked(object sender, EventArgs e)
        {
            Periodo per = new Periodo();
            per.Nome = txtNome.Text;

            await App.Db.Insert(per);
            await DisplayAlert("Sucesso!", "Registro inserido", "OK");

            carregarLista();
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

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecionado = sender as MenuItem;
                Periodo p = selecionado.BindingContext as Periodo;

                bool confirmacao = await DisplayAlert(
                "Confirmação", "Confirma a remoção?", "Sim", "Não");
                if (confirmacao == true)
                {
                    await App.Db.Delete(p.Id);
                    lista.Remove(p);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
        
        private void lstperiodos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                Periodo p = e.SelectedItem as Periodo;
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
                Periodo p = new Periodo();
                p.Id = int.Parse(txtCodigo.Text);
                p.Nome = txtNome.Text;

                App.Db.Update(p);
                Navigation.PushAsync(new Views.EditandoPeriodos
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
}