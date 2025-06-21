using System.Collections.ObjectModel;
using SistemaAcademicos.Models;

namespace SistemaAcademicos.Views
{
    public partial class EditarDisciplinas : ContentPage
    {
        public EditarDisciplinas()
        {
            InitializeComponent();

            // Preenche as Entry se vier uma Disciplina via BindingContext
            if (BindingContext is Disciplina disciplina)
            {
                txtCodigo.Text = disciplina.Id.ToString();
                txtNome.Text = disciplina.Nome;
                txtSigla.Text = disciplina.Sigla;
                txtObservacoes.Text = disciplina.Observacao;
            }
        }

        private void btnLimpar_Clicked(object sender, EventArgs e)
        {
            txtNome.Text = string.Empty;
            txtSigla.Text = string.Empty;
            txtObservacoes.Text = string.Empty;
        }

        private async void btnAlterar_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is not Disciplina disciplina)
                return;

            if (string.IsNullOrWhiteSpace(disciplina.Nome) || string.IsNullOrWhiteSpace(disciplina.Sigla))
            {
                await DisplayAlert("Aten��o", "Preencha todos os campos.", "OK");
                return;
            }

            bool confirm = await DisplayAlert("Confirma��o", "Deseja realmente editar este registro?", "Sim", "N�o");
            if (!confirm)
                return;

            try
            {
                await App.Db.UpdateDisciplina(disciplina);
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