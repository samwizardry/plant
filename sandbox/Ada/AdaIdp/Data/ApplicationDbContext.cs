using AdaIdp.Models;
using Microsoft.EntityFrameworkCore;

namespace AdaIdp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    public virtual DbSet<Character> Characters { get; set; }
}
