using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBLaba2.Models;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DBLaba2.StoredProcedures
{
    public class StorProcedures
    {
        private static System.Data.Common.DbCommand GetCom2(System.Data.Common.DbCommand cmd)
        {
            WarehouseContext db = new WarehouseContext();

            StringBuilder sql = new StringBuilder();
            var cmd2 = db.Database.Connection.CreateCommand();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                    sql.AppendLine($"FETCH ALL IN \"{reader.GetString(0)}\";");

                cmd2.Connection = cmd.Connection;
                cmd2.Transaction = cmd.Transaction;
                cmd2.CommandTimeout = cmd.CommandTimeout;
                cmd2.CommandText = sql.ToString();
                cmd2.CommandType = CommandType.Text;
            }
            return cmd2;
        }

        internal static async Task<List<Product>> GetProductsAsync()
        {
            List<Product> products = new List<Product>();
            
            return await Task.Run(() =>
            {
                try
                {
                    using (WarehouseContext db = new WarehouseContext())
                    {
                        using (var cmd = db.Database.Connection.CreateCommand())
                        {
                            db.Database.Connection.Open();
                            string sqlExpression = "show_products";
                            cmd.CommandText = sqlExpression;
                            cmd.CommandType = CommandType.StoredProcedure;                        
                            var tran = db.Database.Connection.BeginTransaction();
                            var cmd2 = GetCom2(cmd);
                            //Execute cmd2 and process the results as normal
                            using (var reader2 = cmd2.ExecuteReader())
                            {

                                while (reader2.Read())
                                {
                                    Product product = new Product();
                                    product.Id = reader2.GetInt32(0);
                                    product.Name = reader2.GetString(1);
                                    product.Material = reader2.GetString(2);
                                    product.Length = reader2.GetInt32(3);
                                    product.Width = reader2.GetInt32(4);
                                    product.Heigth = reader2.GetInt32(5);
                                    products.Add(product);
                                }
                            }
                            tran.Commit();
                            db.Database.Connection.Close();
                        }
                    }
                    return products;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return products;
                }

            });
        }

        internal static async Task<List<Producer>> GetProducersAsync()
        {
            List<Producer> producers = new List<Producer>();
            return await Task.Run(() =>
            {
                try
                {
                    using (WarehouseContext db = new WarehouseContext())
                    {
                        using (var cmd = db.Database.Connection.CreateCommand())
                        {
                            db.Database.Connection.Open();
                            string sqlExpression = "show_producers";
                            cmd.CommandText = sqlExpression;
                            cmd.CommandType = CommandType.StoredProcedure;
                            var tran = db.Database.Connection.BeginTransaction();
                            var cmd2 = GetCom2(cmd);
                            //Execute cmd2 and process the results as normal
                            using (var reader2 = cmd2.ExecuteReader())
                            {

                                while (reader2.Read())
                                {
                                    Producer producer = new Producer();
                                    producer.Id = reader2.GetInt32(0);
                                    producer.Name = reader2.GetString(1);
                                    producer.City = reader2.GetString(2);
                                    producers.Add(producer);
                                }
                            }
                            tran.Commit();
                            db.Database.Connection.Close();
                        }
                    }
                    return producers;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return producers;
                }

            });
        }

        internal static async Task<List<Shelf>> GetShelvesAsync()
        {
            List<Shelf> shelves = new List<Shelf>();
            return await Task.Run(() =>
            {
                try
                {
                    using (WarehouseContext db = new WarehouseContext())
                    {
                        using (var cmd = db.Database.Connection.CreateCommand())
                        {
                            db.Database.Connection.Open();
                            string sqlExpression = "show_shelves";
                            cmd.CommandText = sqlExpression;
                            cmd.CommandType = CommandType.StoredProcedure;
                            var tran = db.Database.Connection.BeginTransaction();
                            var cmd2 = GetCom2(cmd);
                            //Execute cmd2 and process the results as normal
                            using (var reader2 = cmd2.ExecuteReader())
                            {

                                while (reader2.Read())
                                {
                                    Shelf shelf = new Shelf();
                                    shelf.Id = reader2.GetInt32(0);
                                    shelf.Name = reader2.GetString(1);
                                    shelf.Position = reader2.GetString(2);
                                    shelves.Add(shelf);
                                }
                            }
                            tran.Commit();
                            db.Database.Connection.Close();
                        }
                    }
                    return shelves;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return shelves;
                }

            });
        }

        internal static async Task<List<Location>> GetLocationsAsync()
        {
            List<Location> locations = new List<Location>();
            return await Task.Run(() =>
            {
                try
                {
                    using (WarehouseContext db = new WarehouseContext())
                    {
                        using (var cmd = db.Database.Connection.CreateCommand())
                        {
                            db.Database.Connection.Open();
                            string sqlExpression = "show_locations";
                            cmd.CommandText = sqlExpression;
                            cmd.CommandType = CommandType.StoredProcedure;
                            var tran = db.Database.Connection.BeginTransaction();
                            var cmd2 = GetCom2(cmd);
                            //Execute cmd2 and process the results as normal
                            using (var reader2 = cmd2.ExecuteReader())
                            {

                                while (reader2.Read())
                                {
                                    Location location = new Location();
                                    location.Id = reader2.GetInt32(0);
                                    location.ProductId = reader2.GetInt32(1);
                                    location.ProducerId = reader2.GetInt32(2);
                                    location.ShelfId = reader2.GetInt32(3);
                                    location.Count = reader2.GetInt32(4);
                                    locations.Add(location);
                                }
                            }
                            tran.Commit();
                            db.Database.Connection.Close();
                        }
                    }
                    return locations;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return locations;
                }

            });
        }


    }

}