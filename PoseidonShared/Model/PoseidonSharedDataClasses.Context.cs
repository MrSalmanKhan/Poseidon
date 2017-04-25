﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PoseidonShared.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PoseidonDBEntities : DbContext
    {
        public PoseidonDBEntities()
            : base("name=PoseidonDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<InvoiceItem> InvoiceItems { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }
        public virtual DbSet<MonthlyPurchasesValue> MonthlyPurchasesValues { get; set; }
        public virtual DbSet<MonthlyStockValue> MonthlyStockValues { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SoldItem> SoldItems { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockItem> StockItems { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<viewProductDetail> viewProductDetails { get; set; }
    }
}
