﻿using ReLux.DataAccess.Data;
using ReLux.DataAccess.Repository.IRepository;
using ReLux.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReLux.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            RateCondition = new RateConditionRepository(_db); 
            Product = new ProductRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetails = new OrderDetailsRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }

        public IRateConditionRepository RateCondition { get; private set; }

        public IProductRepository Product { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public IOrderHeaderRepository OrderHeader { get; private set; }

        public IOrderDetailsRepository OrderDetails { get; private set; }

        public IApplicationUserRepository ApplicationUser { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
