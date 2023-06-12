using Microsoft.EntityFrameworkCore;

namespace Ecclesiastical.Treasury.App.Data;

public class EcclesiasticalDbContext : DbContext
{
    public EcclesiasticalDbContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<FinancialGroup> FinancialGroups { get; set; }
    public DbSet<FinancialCategory> FinancialCategories { get; set; }
    public DbSet<FinancialSubcategory> FinancialSubcategories { get; set; }
    public DbSet<FinancialUser> FinancialUsers { get; set; }
    public DbSet<FinancialMovement> FinancialMovements { get; set; }
}

public class User
{
    public int UserId { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

/// <summary>
/// Income or outcome
/// </summary>
public class FinancialGroup
{
    public int FinancialGroupId { get; set; }
    public string? Name { get; set; }
}

/// <summary>
/// Tuition, Services
/// </summary>
public class FinancialCategory
{
    public int FinancialCategoryId { get; set; }
    public int FinancialGroupId { get; set; }
    public string? Name { get; set; }
}

/// <summary>
/// First grade, Internet
/// </summary>
public class FinancialSubcategory
{
    public int FinancialSubcategoryId { get; set; }
    public int FinancialCategoryId { get; set; }
    public string? Name { get; set; }
}

/// <summary>
/// Student
/// </summary>
public class FinancialUser
{
    public int FinancialUserId { get; set; }
    public int FinancialSubcategoryId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}

public class FinancialMovement
{
    public int FinancialMovementId { get; set; }
    public int FinancialSubcategoryId { get; set; }
    public int FinancialUserId { get; set; }
    public decimal Amount { get; set; }
}

