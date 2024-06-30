using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatWeb.Core.Models;
using Microsoft.EntityFrameworkCore;


namespace ChatWeb.Infrastructure.Data
{
    public class ChatWebDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public DbSet<Connection> Connections { get; set; }

        public ChatWebDbContext(DbContextOptions<ChatWebDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //to configure relation, map models to table
            base.OnModelCreating(modelBuilder);

            //maping models to database tables
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Message>().ToTable("Messages");
            modelBuilder.Entity<Connection>().ToTable("Connections");

            //Configuring constraints
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Connection>()
                .HasOne(c => c.User)
                .WithMany(u => u.Connections)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            
        }

    }
}
