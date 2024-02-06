using MauiCrud.SQLite.Models;

namespace MauiCrud.Service
{
    public interface IDbService
    {
        Task InicializeAsync();
        public Task<List<Cliente>> GetClientes();
        Task<Cliente> GetClienteById(long codeCliente);
        Task<int> AddCliente(Cliente cliente);
        Task<int> UpdateCliente(Cliente cliente);
        Task<int> DeleteCliente(Cliente cliente);

        Task<List<Produto>> GetProdutos();
        Task<Produto> GetProdutoById(long codeProduto);
        Task<int> AddProduto(Produto produto);
        Task<int> UpdateProduto(Produto produto);
        Task<int> DeleteProduto(Produto produto);

        Task<List<Pedido>> GetPedidos();
        Task<Pedido> GetPedidoById(long codePedido);
        Task<int> AddPedido(Pedido pedido);
        Task<int> UpdatePedido(Pedido pedido);
        Task<int> DeletePedido(Pedido pedido);

        Task<List<PedidoItem>> GetPedidoItems();
        Task<PedidoItem> GetPedidoItemById(long codePedidoItem);
        Task<List<PedidoItem>> GetPedidoItemByNrOrder(long nrOrder);
        Task<int> AddPedidoItem(PedidoItem pedidoItem);
        Task<int> UpdatePedidoItem(PedidoItem pedidoItem);
        Task<int> DeletePedidoItem(PedidoItem pedidoItem);

        Task<int> SalvarPedidoCompleto(Pedido pedido, List<PedidoItem> itens);

    }
}
