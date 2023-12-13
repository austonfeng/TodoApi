using DemoProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace DemoProject.Domain.Context
{
    public interface IDemoDbContext
    {
        DbSet<Domain.Entities.Task> Tasks { get; set; }
        DbSet<AspNetRole> AspNetRoles { get; set; } 
        DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }
        DbSet<AspNetUser> AspNetUsers { get; set; } 
        DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } 
        DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } 
        DbSet<AspNetUserToken> AspNetUserTokens { get; set; } 
    }
}
