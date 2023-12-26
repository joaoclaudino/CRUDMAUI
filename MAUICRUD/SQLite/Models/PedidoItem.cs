﻿using SQLite;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUICRUD.SQLite.Models
{
    [Table("PedidoItens")]
    public class PedidoItem
    {
        [PrimaryKey, AutoIncrement]
        public int CodigoPedidoItem { get; set; }
        [NotNull]
        public int NrPedido { get; set; }
        [NotNull]
        public int CodigoProduto { get; set; }
        [MaxLength(200), NotNull]
        public string DescricaoProduto { get; set; }
        [NotNull]
        public double Quantidade { get; set; }
        [NotNull]
        public double PrecoUnitario { get; set; }
        [NotNull]
        public double PesoLiquido { get; set; }
        [NotNull]
        public double PrecoTotal { get; set; }
        [NotNull]
        public double PesoTotal { get; set; }

    }
}