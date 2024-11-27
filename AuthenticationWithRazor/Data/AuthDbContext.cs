using AuthenticationWithRazor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationWithRazor.Data;

public class AuthDbContext:IdentityDbContext<User>
{
    public AuthDbContext(DbContextOptions options):base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        var admin = new IdentityRole("admin");
        admin.NormalizedName = "admin";
        
        var staff = new IdentityRole("staff");
        staff.NormalizedName = "staff";
        
        var employee = new IdentityRole("employee");
        employee.NormalizedName = "employee";
        
        builder.Entity<IdentityRole>().HasData(admin, staff, employee);
    }   
}