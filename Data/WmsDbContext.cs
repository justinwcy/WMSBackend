using Microsoft.EntityFrameworkCore;
using WMSBackend.Models;

namespace WMSBackend.Data
{
    public class WmsDbContext : DbContext
    {
        public WmsDbContext(DbContextOptions<WmsDbContext> options)
            : base(options) { }

        public DbSet<Bin> Bins { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<CustomerOrderDetail> CustomerOrderDetails { get; set; }
        public DbSet<IncomingOrder> IncomingOrders { get; set; }
        public DbSet<IncomingOrderProduct> IncomingOrderProducts { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductSku> ProductSkus { get; set; }
        public DbSet<Rack> Racks { get; set; }
        public DbSet<RefundOrder> RefundOrders { get; set; }
        public DbSet<RefundOrderProduct> RefundOrderProducts { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<StaffNotification> StaffNotifications { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Zone> Zones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // many to many relationships
            modelBuilder
                .Entity<Zone>()
                .HasMany(zone => zone.Staffs)
                .WithMany(staff => staff.Zones)
                .UsingEntity<ZoneStaff>(
                    r =>
                        r.HasOne<Staff>()
                            .WithMany()
                            .HasForeignKey(zoneStaff => zoneStaff.StaffId)
                            .OnDelete(DeleteBehavior.Restrict),
                    l =>
                        l.HasOne<Zone>()
                            .WithMany()
                            .HasForeignKey(zoneStaff => zoneStaff.ZoneId)
                            .OnDelete(DeleteBehavior.Restrict)
                );

            modelBuilder
                .Entity<IncomingOrder>()
                .HasMany(incomingOrder => incomingOrder.Products)
                .WithMany(product => product.IncomingOrders)
                .UsingEntity<IncomingOrderProduct>(
                    r =>
                        r.HasOne<Product>()
                            .WithMany()
                            .HasForeignKey(incomingOrderProduct => incomingOrderProduct.ProductId)
                            .OnDelete(DeleteBehavior.Restrict),
                    l =>
                        l.HasOne<IncomingOrder>()
                            .WithMany()
                            .HasForeignKey(incomingOrderProduct =>
                                incomingOrderProduct.IncomingOrderId
                            )
                            .OnDelete(DeleteBehavior.Restrict)
                );

            modelBuilder
                .Entity<RefundOrder>()
                .HasMany(refundOrder => refundOrder.Products)
                .WithMany(product => product.RefundOrders)
                .UsingEntity<RefundOrderProduct>(
                    r =>
                        r.HasOne<Product>()
                            .WithMany()
                            .HasForeignKey(refundOrderProduct => refundOrderProduct.ProductId)
                            .OnDelete(DeleteBehavior.Restrict),
                    l =>
                        l.HasOne<RefundOrder>()
                            .WithMany()
                            .HasForeignKey(refundOrderProduct => refundOrderProduct.RefundOrderId)
                            .OnDelete(DeleteBehavior.Restrict)
                );

            modelBuilder
                .Entity<Shop>()
                .HasMany(shop => shop.Products)
                .WithMany(product => product.Shops)
                .UsingEntity<ProductShop>(
                    r =>
                        r.HasOne<Product>()
                            .WithMany()
                            .HasForeignKey(productShop => productShop.ProductId)
                            .OnDelete(DeleteBehavior.Restrict),
                    l =>
                        l.HasOne<Shop>()
                            .WithMany()
                            .HasForeignKey(productShop => productShop.ShopId)
                            .OnDelete(DeleteBehavior.Restrict)
                );

            modelBuilder
                .Entity<Product>()
                .HasMany(product => product.Racks)
                .WithMany(rack => rack.Products)
                .UsingEntity<ProductRack>(
                    r =>
                        r.HasOne<Rack>()
                            .WithMany()
                            .HasForeignKey(productRack => productRack.RackId)
                            .OnDelete(DeleteBehavior.Restrict),
                    l =>
                        l.HasOne<Product>()
                            .WithMany()
                            .HasForeignKey(productRack => productRack.ProductId)
                            .OnDelete(DeleteBehavior.Restrict)
                );

            modelBuilder
                .Entity<Product>()
                .HasMany(product => product.Sk)
                .WithMany(rack => rack.Products)
                .UsingEntity<ProductRack>(
                    r =>
                        r.HasOne<Rack>()
                            .WithMany()
                            .HasForeignKey(productRack => productRack.RackId)
                            .OnDelete(DeleteBehavior.Restrict),
                    l =>
                        l.HasOne<Product>()
                            .WithMany()
                            .HasForeignKey(productRack => productRack.ProductId)
                            .OnDelete(DeleteBehavior.Restrict)
                );

            // one to many relationships
            modelBuilder
                .Entity<Zone>()
                .HasMany(zone => zone.Racks)
                .WithOne(rack => rack.Zone)
                .HasForeignKey(rack => rack.ZoneId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Customer>()
                .HasMany(customer => customer.CustomerOrders)
                .WithOne(customerOrder => customerOrder.Customer)
                .HasForeignKey(customerOrder => customerOrder.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Courier>()
                .HasMany(courier => courier.CustomerOrders)
                .WithOne(customerOrder => customerOrder.Courier)
                .HasForeignKey(customerOrder => customerOrder.CourierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Bin>()
                .HasMany(bin => bin.CustomerOrders)
                .WithOne(customerOrder => customerOrder.Bin)
                .HasForeignKey(customerOrder => customerOrder.BinId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Vendor>()
                .HasMany(vendor => vendor.IncomingOrders)
                .WithOne(incomingOrder => incomingOrder.Vendor)
                .HasForeignKey(incomingOrder => incomingOrder.VendorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Company>()
                .HasMany(company => company.Staffs)
                .WithOne(staff => staff.Company)
                .HasForeignKey(staff => staff.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Staff>()
                .HasMany(staff => staff.StaffNotifications)
                .WithOne(staffNotification => staffNotification.Staff)
                .HasForeignKey(staffNotification => staffNotification.StaffId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Company>()
                .HasMany(company => company.Warehouses)
                .WithOne(warehouse => warehouse.Company)
                .HasForeignKey(warehouse => warehouse.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<Warehouse>()
                .HasMany(warehouse => warehouse.Zones)
                .WithOne(zone => zone.Warehouse)
                .HasForeignKey(zone => zone.WarehouseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<CustomerOrder>()
                .HasMany(customerOrder => customerOrder.CustomerOrderDetails)
                .WithOne(customerOrderDetail => customerOrderDetail.CustomerOrder)
                .HasForeignKey(customerOrderDetail => customerOrderDetail.CustomerOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<CustomerOrderDetail>()
                .HasOne(customerOrderDetail => customerOrderDetail.Product)
                .WithMany(product => product.CustomerOrderDetails)
                .HasForeignKey(product => product.CustomerOrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // one to one relationship settings
            modelBuilder
                .Entity<Product>()
                .HasOne(product => product.CurrentInventory)
                .WithOne(currentInventory => currentInventory.Product)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
