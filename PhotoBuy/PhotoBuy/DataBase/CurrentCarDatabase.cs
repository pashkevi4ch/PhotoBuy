using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using PhotoBuy.Models;

namespace PhotoBuy.DataBase
{
    public class CurrentCarDatabase
    {
        readonly SQLiteAsyncConnection database;
        public CurrentCarDatabase(string databasePath)
        {
            database = new SQLiteAsyncConnection(databasePath);
            database.CreateTableAsync<AlocatedCar>().Wait();
        }
        public Task<List<AlocatedCar>> GetCarsAsync()
        {
            return database.Table<AlocatedCar>().ToListAsync();
        }

        public Task<int> SaveCarAsync(AlocatedCar car)
        {
            return database.InsertAsync(car);
        }
        public Task<AlocatedCar> GetCarAsync(string name)
        {
            return database.GetAsync<AlocatedCar>(name);
        }
        public Task<int> DeleteCarAsync(int id)
        {
            return database.DeleteAsync<AlocatedCar>(id);
        }
        public Task<int> DeleteAll()
        {
            return database.DeleteAllAsync<AlocatedCar>();
        }
        public Task<int> UpdateAsync(AlocatedCar car)
        {
            return database.UpdateAsync(car);
        }
    }
}
