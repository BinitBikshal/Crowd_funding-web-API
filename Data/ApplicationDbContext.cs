using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string,
     IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>,
     IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>(b =>
            {
                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                b.HasMany(e => e.Campaigns)
                   .WithOne(e => e.User)
                   .HasForeignKey(ur => ur.UserId)
                   .IsRequired();

                b.HasMany(e => e.Comments)
                   .WithOne(e => e.User)
                   .HasForeignKey(ur => ur.UserId)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.NoAction);
                   
                b.HasMany(e => e.Donations)
                  .WithOne(e => e.DonatedBy)
                  .HasForeignKey(ur => ur.DonatedById)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.NoAction); 
                   
            });
            builder.Entity<ApplicationRole>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
             });
            builder.Entity<Campaign>(b =>
            {
                b.HasMany(e => e.Donations)
                    .WithOne(e => e.Campaign)
                    .HasForeignKey(ur => ur.CampaignId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction); 


                b.HasMany(e => e.Comments)
                  .WithOne(e => e.Campaign)
                  .HasForeignKey(ur => ur.CampaignId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.NoAction); 
                   
            });
            
        }
    }
}
