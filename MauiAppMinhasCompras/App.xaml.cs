using System.IO;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        // Variável interna que guarda a conexão com o banco
        static Helpers.SQLiteDatabaseHelper _db;

        // Propriedade pública que todas as telas vão usar para acessar o banco (App.Db)
        public static Helpers.SQLiteDatabaseHelper Db
        {
            get
            {
                if (_db == null)
                {
                    // Define o caminho onde o arquivo do banco de dados será salvo no celular
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.db3");

                    _db = new Helpers.SQLiteDatabaseHelper(path);
                }
                return _db;
            }
        }

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Define a tela ListaProduto como a tela inicial do aplicativo, com barra de navegação
            return new Window(new NavigationPage(new Views.ListaProduto()));
        }
    }
}