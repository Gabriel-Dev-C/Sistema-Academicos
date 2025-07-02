using SistemaAcademicos.Models;

namespace SistemaAcademicos.Views
{
	public partial class EditarCursos : ContentPage
	{
        private List<Periodo> _periodos = new();
        private List<Disciplina> _disciplinas = new();

        public EditarCursos()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Carrega os períodos no Picker
            _periodos = await App.Db.GetAll();
            pickerPeriodo.ItemsSource = _periodos;

            _disciplinas = await App.Db.GetAllDisciplinas();
            pickerDisciplina.ItemsSource = _disciplinas;

            // Preenche os campos se vier um Curso via BindingContext
            if (BindingContext is Curso curso)
            {
                txtCodigo.Text = curso.Id.ToString();
                txtNome.Text = curso.Nome;
                txtSigla.Text = curso.Sigla;
                txtObservacoes.Text = curso.Observacoes;

                // Seleciona o período correspondente no Picker
                var periodoSelecionado = _periodos.FirstOrDefault(p => p.Id == curso.PeriodoId);
                if (periodoSelecionado != null)
                    pickerPeriodo.SelectedItem = periodoSelecionado;

                var disciplinaSelecionada = _disciplinas.FirstOrDefault(d => d.Id == curso.DisciplinaId);
                if (disciplinaSelecionada != null)
                    pickerDisciplina.SelectedItem = disciplinaSelecionada;
            }
        }

        private void btnLimpar_Clicked(object sender, EventArgs e)
        {
            txtNome.Text = string.Empty;
            txtSigla.Text = string.Empty;
            txtObservacoes.Text = string.Empty;
            pickerPeriodo.SelectedItem = null; // Limpa a seleção do Picker
            pickerDisciplina.SelectedItem = null; // Limpa a seleção do Picker
        }

        private async void btnAlterar_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is not Curso curso)
                return;

            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtSigla.Text) || pickerPeriodo.SelectedItem == null || pickerDisciplina.SelectedItem == null)
            {
                await DisplayAlert("Atenção", "Preencha todos os campos.", "OK");
                return;
            }

            bool confirm = await DisplayAlert("Confirmação", "Deseja realmente editar este registro?", "Sim", "Não");
            if (!confirm)
                return;

            try
            {
                // Atualiza os dados do curso com os valores das Entry e Picker
                curso.Nome = txtNome.Text;
                curso.Sigla = txtSigla.Text;
                curso.Observacoes = txtObservacoes.Text;
                curso.PeriodoId = ((Periodo)pickerPeriodo.SelectedItem).Id;
                curso.DisciplinaId = ((Disciplina)pickerDisciplina.SelectedItem).Id;

                await App.Db.UpdateCurso(curso);
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