using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using web_api_eventz.Models;

namespace web_api_eventz.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketHistory> TicketHistorys { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // builder.Entity<TicketHistory>(x => x.HasKey(ticketHistory => new { ticketHistory.AppUserId, ticketHistory.EventId, ticketHistory.TicketId }));
            builder.Entity<TicketHistory>()
                        .HasKey(ue => ue.Id);
                        
            builder.Entity<TicketHistory>()
                .HasOne(user => user.AppUser)
                .WithMany(u => u.TicketHistories)
                .HasForeignKey(t => t.AppUserId);

            builder.Entity<TicketHistory>()
                .HasOne(th => th.Event)
                .WithMany(e => e.TicketHistories)
                .HasForeignKey(th => th.EventId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<TicketHistory>()
                .HasOne(th => th.Ticket)
                .WithMany(t => t.TicketHistories)
                // .WithMany()
                .HasForeignKey(th => th.TicketId)
                .OnDelete(DeleteBehavior.NoAction);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole
                {
                    Name = "Organizer",
                    NormalizedName = "ORGANIZER"
                },
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}