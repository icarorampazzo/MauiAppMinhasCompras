using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
    }

    private async void OnSalvarClicked(object sender, EventArgs e)
    {
        try
        {
            // 1. Peguei os dados que o usuário digitou nas caixinhas de texto (Entry)
            // e montei um novo objeto da classe Produto.
            Produto p = new Produto
            {
                Descricao = txtDescricao.Text,
                Quantidade = Convert.ToDouble(txtQuantidade.Text),
                Preco = Convert.ToDouble(txtPreco.Text)
            };

            // 2. Chamei o banco de dados (App.Db) e usei a função Insert (Create do CRUD)
            await App.Db.Insert(p);

            // 3. Mostrei um aviso na tela dizendo que deu tudo certo
            await DisplayAlert("Sucesso!", "Produto cadastrado com sucesso.", "OK");

            // 4. Fechei essa tela e voltamos para a tela anterior (Lista de Produtos)
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Se algo der errado (ex: usuário digitou letra no lugar de número), mostra o erro
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}