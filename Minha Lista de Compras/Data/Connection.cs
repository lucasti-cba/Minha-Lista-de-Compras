using Minha_Lista_de_Compras.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Minha_Lista_de_Compras.Data
{
    public class ContaDataBase
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<ContaDataBase> Instance = new AsyncLazy<ContaDataBase>(async () =>
        {
            var instance = new ContaDataBase();
            CreateTableResult result = await Database.CreateTableAsync<Conta>();
            return instance;
        });

        public ContaDataBase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<Conta>> GetItemsAsync()
        {
            return Database.Table<Conta>().ToListAsync();
        }

        public Task<List<Conta>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Conta>("SELECT * FROM [Conta] WHERE [Done] = 0");
        }



        public Task<int> SaveItemAsync(Conta item)
        {

            return Database.InsertAsync(item);

        }

        public Task<int> DeleteItemAsync(Conta item)
        {
            return Database.DeleteAsync(item);
        }
    }


    public class AsyncLazy<T>
    {
        readonly Lazy<Task<T>> instance;

        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }

        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }
    }


}