using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PhotoBuy.Models;

namespace PhotoBuy.DataBase
{
    public class HistoryDatabase
    {
        readonly SQLiteAsyncConnection database;
        public HistoryDatabase(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<CarInfo>().Wait();
        }
        public Task<List<CarInfo>> GetCarsAsync()
        {
            return database.Table<CarInfo>().ToListAsync();
        }

        public Task<int> SaveCarAsync(CarInfo car)
        {
            return database.InsertAsync(car);
        }
        public Task<CarInfo> GetCarAsync(string name)
        {
            return database.GetAsync<CarInfo>(name);
        }
        public Task<int> DeleteCarAsync(int id)
        {
            return database.DeleteAsync<CarInfo>(id);
        }
        public Task<int> DeleteAll()
        {
            return database.DeleteAllAsync<CarInfo>();
        }
        public Task<int> UpdateAsync(CarInfo car)
        {
            return database.UpdateAsync(car);
        }
    }
}
