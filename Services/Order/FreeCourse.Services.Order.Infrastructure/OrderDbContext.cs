using System.Runtime.Intrinsics.X86;
using FreeCourse.Services.Order.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FreeCourse.Services.Order.Infrastructure;

public class OrderDbContext : DbContext
{
    public const string DEFAULT_SHEMA = "ordering";

    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }
    
    public DbSet<Domain.Model.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Model.Order>().ToTable("Orders", DEFAULT_SHEMA);
        modelBuilder.Entity<OrderItem>().ToTable("OrderItems", DEFAULT_SHEMA);
        modelBuilder.Entity<OrderItem>().Property(x => x.Price).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Domain.Model.Order>().OwnsOne(x => x.Address).WithOwner();
        base.OnModelCreating(modelBuilder);
    }
}