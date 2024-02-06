
using MauiCrud.SQLite.Models;
using SQLite;
using SQLitePCL;


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
        //public int GetLastInsertId()
        //{
        //    return (int)SQLite3.LastInsertRowid(_dbConnection);
        //}

        private long GetLastInsertRowId(SQLiteConnection connection)
        {
            var handle = connection.Handle ?? throw new NullReferenceException("The connection is not open.");
            return raw.sqlite3_last_insert_rowid(handle);
        }
        public async Task<int> SalvarPedidoCompleto(Pedido pedido, List<PedidoItem> itens)
        {
            await _dbConnection.RunInTransactionAsync(async (SQLiteConnection connection) =>
            {
                if (pedido.NrOrder > 0)
                {
                    var i = connection.Update(pedido);
                }
                else
                {
                    var i =  connection.Insert(pedido);
                    //var l= connection.LastInsertRowId;
                    long id = GetLastInsertRowId(connection);
                        Pedido oPedido = await GetPedidoById(id);
                        pedido.NrOrder = oPedido.NrOrder;
                }
                    
                foreach (PedidoItem _pedidoItem in itens)
                {
                    _pedidoItem.NrOrder = pedido.NrOrder;
                    var result = connection.Update(_pedidoItem);
                    if (result==0)
                    {
                        connection.Insert(_pedidoItem);
                    }

                }
            });
            return 1;
        }
        public async Task<int> AddPedidoItem(PedidoItem pedidoItem)
        {
            return await _dbConnection.InsertAsync(pedidoItem);
        }

        public async Task<int> AddProduto(Produto produto)
        {
            //_dbConnection.RunInTransactionAsync()
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

        public async Task<Cliente> GetClienteById(long codeCliente)
        {
            return await _dbConnection.Table<Cliente>().FirstOrDefaultAsync(x => x.Code == codeCliente);
        }

        public async Task<List<Cliente>> GetClientes()
        {
            return await _dbConnection.Table<Cliente>().ToListAsync();
        }

        public async Task<Pedido> GetPedidoById(long codePedido)
        {
            return await _dbConnection.Table<Pedido>().FirstOrDefaultAsync(x => x.NrOrder == codePedido);
        }

        public async Task<PedidoItem> GetPedidoItemById(long codePedidoItem)
        {
            return await _dbConnection.Table<PedidoItem>().FirstOrDefaultAsync(x => x.CodePedidoItem == codePedidoItem);
        }
        public async Task<List<PedidoItem>> GetPedidoItemByNrOrder(long nrOrder)
        {
            return await _dbConnection.Table<PedidoItem>().Where(x => x.NrOrder == nrOrder).ToListAsync();
        }

        public async Task<List<PedidoItem>> GetPedidoItems()
        {
            return await _dbConnection.Table<PedidoItem>().ToListAsync();
        }

        public async Task<List<Pedido>> GetPedidos()
        {
            return await _dbConnection.Table<Pedido>().ToListAsync();
        }

        public async Task<Produto> GetProdutoById(long codeProduto)
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
            //await _dbConnection.DropTableAsync<Pedido>();
            //await _dbConnection.DropTableAsync<PedidoItem>();
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
