using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
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

    private async Task AtualizarLista(string busca)
    {
        try
        {
            List<Produto> resultado;
            if (string.IsNullOrEmpty(busca))
                resultado = await App.Db.GetAll();
            else
                resultado = await App.Db.Search(busca);

            lista_produtos.Clear();
            foreach (var item in resultado)
                lista_produtos.Add(item);
        }
        catch (Exception ex) { await DisplayAlert("Erro", ex.Message, "OK"); }
    }

    private async void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        await AtualizarLista(e.NewTextValue);
    }

    private async void pckFiltroCategoria_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selecionado = pckFiltroCategoria.SelectedItem?.ToString();
        var todos = await App.Db.GetAll();

        lista_produtos.Clear();
        var filtrados = selecionado == "Todas" || selecionado == null
            ? todos
            : todos.Where(p => p.Categoria == selecionado).ToList();

        foreach (var p in filtrados)
            lista_produtos.Add(p);
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e) => Navigation.PushAsync(new NovoProduto());

    private void OnRelatorioClicked(object sender, EventArgs e) => Navigation.PushAsync(new RelatorioCategoria());

    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;
        Navigation.PushAsync(new EditarProduto { BindingContext = (Produto)e.SelectedItem });
        lst_produtos.SelectedItem = null;
    }
}