using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    // Coleção dinâmica para atualizar a tela em tempo real (Agenda 04)
    ObservableCollection<Produto> lista_produtos = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = lista_produtos;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await AtualizarLista("");
    }

    // Lógica da busca e atualização da lista
    private async Task AtualizarLista(string busca)
    {
        try
        {
            List<Produto> resultadoBusca;

            if (string.IsNullOrEmpty(busca))
                resultadoBusca = await App.Db.GetAll();
            else
                resultadoBusca = await App.Db.Search(busca);

            lista_produtos.Clear();
            foreach (var item in resultadoBusca)
            {
                lista_produtos.Add(item);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    // Evento disparado a cada letra digitada na busca
    private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        string textoDigitado = e.NewTextValue;
        await AtualizarLista(textoDigitado);
    }

    // --- AS FUNÇÕES QUE ESTAVAM FALTANDO! ---

    // Botão "Novo +" da barra superior
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NovoProduto());
    }

    // Quando o usuário toca em um produto da lista (para editar ou excluir)
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
            return;

        Produto p = (Produto)e.SelectedItem;

        Navigation.PushAsync(new EditarProduto
        {
            BindingContext = p
        });

        lst_produtos.SelectedItem = null;
    }
}