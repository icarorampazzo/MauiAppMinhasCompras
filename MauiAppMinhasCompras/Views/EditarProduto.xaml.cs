using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
    public EditarProduto()
    {
        InitializeComponent();
    }

    // Função do botão Verde (Update)
    private async void OnAtualizarClicked(object sender, EventArgs e)
    {
        try
        {
            // Pega o produto que foi passado para essa tela
            Produto p = (Produto)BindingContext;

            // Atualiza o objeto com o que o usuário modificou nas caixinhas
            p.Descricao = txtDescricao.Text;
            p.Quantidade = Convert.ToDouble(txtQuantidade.Text);
            p.Preco = Convert.ToDouble(txtPreco.Text);

            // Manda o banco atualizar
            await App.Db.Update(p);

            await DisplayAlert("Sucesso", "Produto atualizado!", "OK");
            await Navigation.PopAsync(); // Volta pra lista
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    // Função do botão Vermelho (Delete)
    private async void OnExcluirClicked(object sender, EventArgs e)
    {
        try
        {
            // Pergunta antes para evitar exclusões acidentais
            bool confirma = await DisplayAlert("Certeza?", "Deseja mesmo excluir este produto?", "Sim", "Não");

            if (confirma)
            {
                Produto p = (Produto)BindingContext;

                // Manda o banco deletar usando o ID do produto
                await App.Db.Delete(p.Id);

                await DisplayAlert("Sucesso", "Produto excluído!", "OK");
                await Navigation.PopAsync(); // Volta pra lista
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}