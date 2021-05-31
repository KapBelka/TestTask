using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestTask.Models;

namespace TestTask
{
    public class ProductDataModel
    {
        public int id { get; set; }
        public int count { get; set; }
        public string name { get; set; }
    }
    public class StorageDataModel
    {
        public int Id;
        public string Name;
        public List<ProductDataModel> Products;
    }
    public class TransferDataModel
    {
        public int Id;
        public DateTime Date;
        public Object StorageFrom;
        public Object StorageTo;
        public List<ProductDataModel> Products;
    }
    public class StorageService
    {
        private DataContext _Db { get; }
        public StorageService(DataContext db)
        {
            _Db = db;
        }
        public object GetProductsData()
        {
            return _Db.Products.Select(x => new { Id = x.Id, Name = x.Name }).ToList();
        }
        public object GetTransfersData()
        {
            return _Db.Transfers.Select(x => new { id = x.Id, date = x.Date, fromstorage = new { Id = x.FromStorage.Id, Name = x.FromStorage.Name }, tostorage = new { Id = x.ToStorage.Id, Name = x.ToStorage.Name }, products = x.Products.Select(p => new { id = p.Product.Id, name = p.Product.Name, count = p.Count}).ToList() });
        }
        public object GetStoragesData()
        {
            return _Db.Storages.Select(x => new { Id = x.Id, Name = x.Name }).ToList();
        }
        public object AddStorage()
        {
            return null;
        }
        public object AddProductStorage()
        {
            return null;
        }
        public object AddProduct()
        {
            return null;
        }
        public object DeleteTransfer(int transferid)
        {
            Transfer transfer = _Db.Transfers.Include(t => t.Products).Include(t => t.FromStorage).ThenInclude(s => s.Products).Include(t => t.ToStorage).ThenInclude(s => s.Products).Where(t => t.Id == transferid).FirstOrDefault();
            if (transfer == null) throw new Exception("Неверный номер перевозки" );
            foreach (var product in transfer.Products)
            {
                transfer.FromStorage.Products.Where(x => x.ProductId == product.ProductId).First().Count += product.Count;
                transfer.ToStorage.Products.Where(x => x.ProductId == product.ProductId).First().Count -= product.Count;
            }
            _Db.UpdateRange(transfer.FromStorage, transfer.ToStorage);
            _Db.Transfers.Remove(transfer);
            _Db.SaveChanges();
            return new { status = "Ok" };
        }
        public object GetStorageData(int storageid, DateTime dt)
        {
            Storage storage = _Db.Storages.Include(s => s.DeliveryIn).ThenInclude(t => t.Products).Include(s => s.DeliveryOut).ThenInclude(t => t.Products).Include(s => s.Products).ThenInclude(p => p.Product).Where(s => s.Id == storageid).FirstOrDefault();
            if (storage == null) throw new Exception("Неверный номер склада");
            StorageDataModel temps = new StorageDataModel { Id = storage.Id, Name = storage.Name, Products = storage.Products.Select(x => new ProductDataModel { id = x.Product.Id, name = x.Product.Name, count = x.Count }).ToList() };
            foreach (var transfer in storage.DeliveryIn.Where(x => x.Date.CompareTo(dt) > 0))
            {
                foreach (var c in transfer.Products)
                {
                    temps.Products.Where(x => x.id == c.ProductId).First().count -= c.Count;
                }
            }
            foreach (var transfer in storage.DeliveryOut.Where(x => x.Date.CompareTo(dt) > 0))
            {
                foreach (var c in transfer.Products)
                {
                    temps.Products.Where(x => x.id == c.ProductId).First().count += c.Count;
                }
            }
            return temps;
        }
        public object Transfer(int fromstorageid, int tostorageid, List<ProductDataModel> products)
        {
            Storage fromstorage = _Db.Storages.Include(s => s.Products).ThenInclude(p => p.Product).Where(s => s.Id == fromstorageid).FirstOrDefault();
            Storage tostorage = _Db.Storages.Include(s => s.Products).ThenInclude(p => p.Product).Where(s => s.Id == tostorageid).FirstOrDefault();
            if (fromstorage == null || tostorage == null) throw new Exception("Неверный номер склада");
            Transfer t = new Transfer { FromStorage = fromstorage, ToStorage = tostorage, Products = new List<TransferProduct>() };
            foreach (var productfromdata in products)
            {
                Product product = _Db.Products.Where(p => p.Id == productfromdata.id).FirstOrDefault();
                ProductStorage fromproductstorage = fromstorage.Products.Where(s => s.Product.Id == product.Id).FirstOrDefault();
                if (product == null) throw new Exception(String.Format("Неверный id товара! ({})", productfromdata.id));
                if (fromproductstorage == null || fromproductstorage != null && fromproductstorage.Count < productfromdata.count) throw new Exception(String.Format("На складе недостаточно товара! ({0})", productfromdata.id));
                TransferProduct tansferproduct = t.Products.Where(s => s.Product.Id == product.Id).FirstOrDefault();
                if (tansferproduct == null)
                {
                    t.Products.Add(new TransferProduct { Count = productfromdata.count, Transfer = t, Product = product });
                }
                else { tansferproduct.Count += productfromdata.count; }
            }
            if (t.Products.Count == 0) throw new Exception("Укажите хоть один товар!");
            foreach (var prod in t.Products)
            {
                ProductStorage fromproductstorage = fromstorage.Products.Where(s => s.Product.Id == prod.Product.Id).FirstOrDefault();
                ProductStorage toproductstorage = tostorage.Products.Where(s => s.Product.Id == prod.Product.Id).FirstOrDefault();
                fromproductstorage.Count -= prod.Count;
                if (toproductstorage == null) tostorage.Products.Add(new ProductStorage { Storage = tostorage, Count = prod.Count, Product = prod.Product });
                else toproductstorage.Count += prod.Count;
            }
            t.Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            _Db.Transfers.Add(t);
            _Db.Storages.UpdateRange(fromstorage, tostorage);
            _Db.SaveChanges();
            return new { status = "Ok" };
        }
    }
}
