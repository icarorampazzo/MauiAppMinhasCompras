using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Quantidade { get; set; }
        public double Preco { get; set; }

        // CAMPO CADEGORIA ADICIONADO AQUI:
        public string Categoria { get; set; }

        // Propriedade calculada que não vai para o banco de dados
        [Ignore]
        public double Total { get { return Quantidade * Preco; } }
    }
}
