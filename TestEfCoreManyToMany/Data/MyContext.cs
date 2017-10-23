using System;
using Microsoft.EntityFrameworkCore;

namespace TestEfCoreManyToMany.Data
{
    public class MyContext : DbContext
    {

        #region users


        #endregion

        #region Extras
        public DbSet<Hobbie> Hobbies
        {
            get;
            set;
        }

        public DbSet<Location> Locations
        {
            get;
            set;
        }

        /*public DbSet<Follower> Followers
        {
            get;
            set;
        }*/
        #endregion



        public MyContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSqlServer(
                  @"Server=(localdb)\mssqllocaldb;Database=Test;ConnectRetryCount=0");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hobbie>().HasIndex(h => h.Name).IsUnique(true);

            modelBuilder.Ignore<User>();
            modelBuilder.Entity<FacebookUser>().ToTable("FacebookUsers");
            modelBuilder.Entity<FacebookUser>().HasIndex(u => u.UserName).IsUnique(true);
            modelBuilder.Entity<FacebookUser>().HasIndex(u => u.FacebookId).IsUnique(true);

            modelBuilder.Entity<HobbieUser>().ToTable("HobbieUser");
            modelBuilder.Entity<HobbieUser>().HasIndex(u => u.Email).IsUnique(true);



            modelBuilder.Entity<FacebookUsersHobbies>()
                        .HasKey(uh => new { uh.UserId, uh.HobbieId });

            modelBuilder.Entity<HobbieUsersHobbies>()
                        .HasKey(uh => new { uh.HobbieUserId, uh.HobbieId });


            //modelBuilder.Entity<User>().HasChangeTrackingStrategy(ChangeTrackingStrategy.);
        }
    }
}
