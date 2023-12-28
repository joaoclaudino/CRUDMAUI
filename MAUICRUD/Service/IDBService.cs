using MauiCrud.SQLite.Models;

namespace MauiCrud.Service
{
    public interface IDbService
    {
        Task InicializeAsync();
        public Task<List<Cliente>> GetClientes();
        Task<Cliente> GetClienteById(int codeCliente);
        Task<int> AddCliente(Cliente cliente);
        Task<int> UpdateCliente(Cliente cliente);
        Task<int> DeleteCliente(Cliente cliente);

        Task<List<Produto>> GetProdutos();
        Task<Produto> GetProdutoById(int codeProduto);
        Task<int> AddProduto(Produto produto);
        Task<int> UpdateProduto(Produto produto);
        Task<int> DeleteProduto(Produto produto);

        Task<List<Pedido>> GetPedidos();
        Task<Pedido> GetPedidoById(int codePedido);
        Task<int> AddPedido(Pedido pedido);
        Task<int> UpdatePedido(Pedido pedido);
        Task<int> DeletePedido(Pedido pedido);

        Task<List<PedidoItem>> GetPedidoItems();
        Task<PedidoItem> GetPedidoItemById(int codePedidoItem);
        Task<int> AddPedidoItem(PedidoItem pedidoItem);
        Task<int> UpdatePedidoItem(PedidoItem pedidoItem);
        Task<int> DeletePedidoItem(PedidoItem pedidoItem);

    }
}
