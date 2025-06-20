using System.Collections.ObjectModel;
using SistemaAcademicos.Models;

namespace SistemaAcademicos.Views
{
    public partial class EditarPeriodos : ContentPage
    {
        public EditarPeriodos()
        {
            InitializeComponent();

            // Preenche as Entry se vier um Periodo via BindingContext
            if (BindingContext is Periodo periodo)
            {
                txtCodigo.Text = periodo.Id.ToString();
                txtNome.Text = periodo.Nome;
                txtSigla.Text = periodo.Sigla;
            }
        }

        private void btnLimpar_Clicked(object sender, EventArgs e)
        {
            txtNome.Text = string.Empty;
            txtSigla.Text = string.Empty;
        }

        private async void btnAlterar_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is not Periodo periodo)
                return;

            if (string.IsNullOrWhiteSpace(periodo.Nome) || string.IsNullOrWhiteSpace(periodo.Sigla))
            {
                await DisplayAlert("Aten��o", "Preencha todos os campos.", "OK");
                return;
            }

            bool confirm = await DisplayAlert("Confirma��o", "Deseja realmente editar este registro?", "Sim", "N�o");
            if (!confirm)
                return;

            try
            {
                await App.Db.Update(periodo);
                await DisplayAlert("Sucesso!", "Registro editado com sucesso.", "OK");
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }

    }
}