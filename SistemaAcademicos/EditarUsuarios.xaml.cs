using System.Collections.ObjectModel;
using SistemaAcademicos.Models;

namespace SistemaAcademicos
{
	public partial class EditarUsuarios : ContentPage
	{
		public EditarUsuarios()
		{
			InitializeComponent();

            // Preenche as Entry se vier um Usuario via BindingContext
            if (BindingContext is Usuario usuario)
            {
                txtCodigo.Text = usuario.Id.ToString();
                txtNome.Text = usuario.Nome;
                txtSenha.Text = usuario.Senha;
            }
        }

        private void btnLimpar_Clicked(object sender, EventArgs e)
        {
            txtNome.Text = string.Empty;
            txtSenha.Text = string.Empty;
        }

        private async void btnAlterar_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is not Usuario usuario)
                return;

            if (string.IsNullOrWhiteSpace(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Senha))
            {
                await DisplayAlert("Atenção", "Preencha todos os campos.", "OK");
                return;
            }

            bool confirm = await DisplayAlert("Confirmação", "Deseja realmente editar este registro?", "Sim", "Não");
            if (!confirm)
                return;

            try
            {
                await App.Db.UpdateUsuario(usuario);
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