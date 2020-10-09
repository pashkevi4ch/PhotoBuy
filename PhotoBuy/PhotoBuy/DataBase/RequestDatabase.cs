using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PhotoBuy.DataBase
{
    public class RequestDatabase
    {
        readonly SQLiteAsyncConnection database;
        public RequestDatabase(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<Request>().Wait();
        }
        public Task<List<Request>> GetRequestsAsync()
        {
            return database.Table<Request>().ToListAsync();
        }

        public Task<int> SaveRequestAsync(Request request)
        {
            return database.InsertAsync(request);
        }
        public Task<Request> GetRequestAsync(string name)
        {
            return database.GetAsync<Request>(name);
        }
        public Task<int> DeleteRequestAsync(int id)
        {
            return database.DeleteAsync<Request>(id);
        }
        public Task<int> DeleteAll()
        {
            return database.DeleteAllAsync<Request>();
        }
        public Task<int> UpdateAsync(Request request)
        {
            return database.UpdateAsync(request);
        }
    }
}
