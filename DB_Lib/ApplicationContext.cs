
using DB_Lib.Tables;
using System.Data.Entity;

public class ApplicationContext : DbContext
{
    public ApplicationContext()
        : base(@"Server=localhost;Database=ElnurCourseWork;Trusted_Connection=True;TrustServerCertificate=True")
    {
        var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
    }

    public DbSet<CalcRes> CalcRes { get; set; }
    //public DbSet<ExceptionLog> ExceptionLog { get; set; }

    
}