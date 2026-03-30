using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class RelatorioCategoria : ContentPage
{
    public RelatorioCategoria()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            // Pega todos os produtos do banco
            var todosProdutos = await App.Db.GetAll();

            // Agrupa por categoria e soma o Total
            var dadosRelatorio = todosProdutos
                .GroupBy(p => p.Categoria)
                .Select(grupo => new
                {
                    Categoria = grupo.Key ?? "Sem Categoria",
                    TotalGasto = grupo.Sum(item => item.Total)
                })
                .ToList();

            // Joga na lista da tela
            lst_relatorio.ItemsSource = dadosRelatorio;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}