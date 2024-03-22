using Microsoft.EntityFrameworkCore;

namespace CalendarConsole;

public class CalendarContext : DbContext
{
    public DbSet<Identifier> Identifiers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=FABIO\\MSSQLSERVER01;Database=OxygeniusCalendarAPI;Trusted_Connection=True;trustServerCertificate=true;MultipleActiveResultSets=true;");
    }
}