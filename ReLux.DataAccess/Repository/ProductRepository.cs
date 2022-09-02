using ReLux.DataAccess.Data;
using ReLux.DataAccess.Repository.IRepository;
using ReLux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReLux.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product product)
        {
            var objFromDb = _db.Product.FirstOrDefault(u => u.Id == product.Id);
            objFromDb.Name = product.Name;
            objFromDb.Description = product.Description;
            objFromDb.EstRetailValue = product.EstRetailValue;
            objFromDb.Price = product.Price;
            objFromDb.IsSold = product.IsSold;
            objFromDb.CategoryId = product.CategoryId;
            objFromDb.RateConditionId = product.RateConditionId;
            if (objFromDb.Image != null)
            {
                objFromDb.Image = product.Image;
            }
        }
    }
}
