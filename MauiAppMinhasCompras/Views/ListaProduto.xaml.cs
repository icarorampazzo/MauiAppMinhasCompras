using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    public ListaProduto()
    {
        InitializeComponent();
    }

    // Esse método roda toda vez que a tela aparece para o usuário
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            // O App.Db é a chamada para o seu banco de dados. 
            // Ele pega todos os produtos (GetAll) e joga na lista da tela.
            lst_produtos.ItemsSource = await App.Db.GetAll();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    // Botão "Novo +" da barra superior
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        // Navega para a tela de criar um novo produto
        Navigation.PushAsync(new NovoProduto());
    }

    // Quando o usuário pesquisa um produto na barra de busca
    private async void txtSearch_SearchButtonPressed(object sender, EventArgs e)
    {
        try
        {
            string busca = txtSearch.Text;
            lst_produtos.ItemsSource = await App.Db.Search(busca);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    // Quando o usuário toca em um produto da lista (para editar ou excluir)
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        Produto p = (Produto)e.SelectedItem;

        // Vai para a tela de Editar, passando o produto selecionado na "bagagem"
        Navigation.PushAsync(new EditarProduto
        {
            BindingContext = p
        });

        // Tira a seleção visual do item
        lst_produtos.SelectedItem = null;
    }
}