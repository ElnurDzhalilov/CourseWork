namespace DB_Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalcRes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LBorder = c.Double(nullable: false),
                        RBorder = c.Double(nullable: false),
                        Foo = c.String(),
                        Result = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExceptionLogs");
            DropTable("dbo.CalcRes");
        }
    }
}
