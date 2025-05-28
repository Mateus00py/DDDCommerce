using DDDCommerceBCC.Domain;
using Microsoft.EntityFrameworkCore;

namespace DDDCommerceBCC.infra
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EcommerceDB");
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItensPedido { get; set; }
        public DbSet<Entrega> Entregas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cliente
            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.ClienteId);

            // ItemPedido
            modelBuilder.Entity<ItemPedido>()
                .HasKey(i => i.ItemPedidoId);

            // Pedido
            modelBuilder.Entity<Pedido>()
                .HasKey(p => p.PedidoId);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Cliente)
                .WithMany() // ou .WithMany(c => c.Pedidos) se tiver coleção em Cliente
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); // evita exclusão em cascata

            modelBuilder.Entity<Pedido>()
                .HasMany(p => p.Itens)
                .WithOne() // você pode criar uma prop Pedido no ItemPedido e referenciar aqui
                .OnDelete(DeleteBehavior.Cascade);

            // Entrega
            modelBuilder.Entity<Entrega>()
                .HasKey(e => e.EntregaId);

            modelBuilder.Entity<Entrega>()
                .HasOne(e => e.Cliente)
                .WithMany() // ou .WithMany(c => c.Entregas) se tiver prop
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Entrega>()
                .HasOne(e => e.Pedido)
                .WithMany() // ou .WithMany(p => p.Entregas)
                .HasForeignKey(e => e.PedidoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}