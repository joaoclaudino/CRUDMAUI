
using MauiCrud.SQLite.Models;
using SQLite;


namespace MauiCrud.Service
{
    public class DbService : IDbService
    {
        private readonly SQLiteAsyncConnection _dbConnection = new(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MauiCrud.db3"));
        public async Task<int> AddCliente(Cliente cliente)
        {
            await EnsureDbConnection();
            return await _dbConnection.InsertAsync(cliente);
        }

        public async Task<int> AddPedido(Pedido pedido)
        {
            await EnsureDbConnection();
            return await _dbConnection.InsertAsync(pedido);
        }

        public async Task<int> AddPedidoItem(PedidoItem pedidoItem)
        {
            return await _dbConnection.InsertAsync(pedidoItem);
        }

        public async Task<int> AddProduto(Produto produto)
        {
            return await _dbConnection.InsertAsync(produto);
        }

        public async Task<int> DeleteCliente(Cliente cliente)
        {
            return await _dbConnection.DeleteAsync(cliente);
        }

        public async Task<int> DeletePedido(Pedido pedido)
        {
            return await _dbConnection.DeleteAsync(pedido);
        }

        public async Task<int> DeletePedidoItem(PedidoItem pedidoItem)
        {
            return await _dbConnection.DeleteAsync(pedidoItem);
        }

        public async Task<int> DeleteProduto(Produto produto)
        {
            return await _dbConnection.DeleteAsync(produto);
        }

        public async Task<Cliente> GetClienteById(int codeCliente)
        {
            return await _dbConnection.Table<Cliente>().FirstOrDefaultAsync(x => x.Code == codeCliente);
        }

        public async Task<List<Cliente>> GetClientes()
        {
            return await _dbConnection.Table<Cliente>().ToListAsync();
        }

        public async Task<Pedido> GetPedidoById(int codePedido)
        {
            return await _dbConnection.Table<Pedido>().FirstOrDefaultAsync(x => x.NrPedido == codePedido);
        }

        public async Task<PedidoItem> GetPedidoItemById(int codePedidoItem)
        {
            return await _dbConnection.Table<PedidoItem>().FirstOrDefaultAsync(x => x.CodePedidoItem == codePedidoItem);
        }

        public async Task<List<PedidoItem>> GetPedidoItems()
        {
            return await _dbConnection.Table<PedidoItem>().ToListAsync();
        }

        public async Task<List<Pedido>> GetPedidos()
        {
            return await _dbConnection.Table<Pedido>().ToListAsync();
        }

        public async Task<Produto> GetProdutoById(int codeProduto)
        {
            return await _dbConnection.Table<Produto>().FirstOrDefaultAsync(x => x.Code == codeProduto);
        }

        public async Task<List<Produto>> GetProdutos()
        {
            return await _dbConnection.Table<Produto>().ToListAsync();
        }

        private async Task SetUpDb()
        {
            //if (_dbConnection==null)
            //{
            //    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MauiCrud.db3");
            //    _dbConnection = new SQLiteAsyncConnection(dbPath);
            await _dbConnection.CreateTableAsync<Cliente>();
            await _dbConnection.CreateTableAsync<Produto>();
            await _dbConnection.CreateTableAsync<Pedido>();
            await _dbConnection.CreateTableAsync<PedidoItem>();

            //}
        }
        private async Task EnsureDbConnection()
        {
            await SetUpDb();
        }
        public async Task InicializeAsync()
        {
            await SetUpDb();
        }

        public async Task<int> UpdateCliente(Cliente cliente)
        {
            return await _dbConnection.UpdateAsync(cliente);
        }

        public async Task<int> UpdatePedido(Pedido pedido)
        {
            return await _dbConnection.UpdateAsync(pedido);
        }

        public async Task<int> UpdatePedidoItem(PedidoItem pedidoItem)
        {
            return await _dbConnection.UpdateAsync(pedidoItem);
        }

        public async Task<int> UpdateProduto(Produto produto)
        {
            return await _dbConnection.UpdateAsync(produto);
        }
    }
}
