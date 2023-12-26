using MAUICRUD.SQLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUICRUD.Service
{
    public interface IDBService
    {
        Task InicializeAsync();
        public Task<List<Cliente>> GetClientes();
        Task<Cliente> GetClienteById(int codigoCliente);
        Task<int> AddCliente(Cliente cliente);
        Task<int> UpdateCliente(Cliente cliente);
        Task<int> DeleteCliente(Cliente cliente);

        Task<List<Produto>> GetProdutos();
        Task<Produto> GetProdutoById(int codigoProduto);
        Task<int> AddProduto(Produto produto);
        Task<int> UpdateProduto(Produto produto);
        Task<int> DeleteProduto(Produto produto);

        Task<List<Pedido>> GetPedidos();
        Task<Pedido> GetPedidoById(int codigoPedido);
        Task<int> AddPedido(Pedido pedido);
        Task<int> UpdatePedido(Pedido pedido);
        Task<int> DeletePedido(Pedido pedido);

        Task<List<PedidoItem>> GetPedidoItems();
        Task<PedidoItem> GetPedidoItemById(int codigoPedidoItem);
        Task<int> AddPedidoItem(PedidoItem pedidoItem);
        Task<int> UpdatePedidoItem(PedidoItem pedidoItem);
        Task<int> DeletePedidoItem(PedidoItem pedidoItem);

    }
}
