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
                txtRa.Text = usuario.Ra;
                txtNome.Text = usuario.Nome;
                txtSenha.Text = usuario.Senha;
                txtEmail.Text = usuario.Email;
                txtMensalidade.Text = usuario.Mensalidade.ToString("F2");
            }
        }

        private void btnLimpar_Clicked(object sender, EventArgs e)
        {
            txtRa.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtSenha.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMensalidade.Text = string.Empty;
        }

        private async void btnAlterar_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is not Usuario usuario)
                return;

            // Atualiza os dados do usuário com os valores das Entry
            usuario.Ra = txtRa.Text;
            usuario.Nome = txtNome.Text;
            usuario.Senha = txtSenha.Text;
            usuario.Email = txtEmail.Text;
            usuario.Mensalidade = decimal.TryParse(txtMensalidade.Text, out var mensalidade) ? mensalidade : 0;

            if (string.IsNullOrWhiteSpace(usuario.Ra) || string.IsNullOrWhiteSpace(usuario.Nome) || string.IsNullOrWhiteSpace(usuario.Senha))
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