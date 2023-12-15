namespace DB_Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalcRes", "ExceptionMessage", c => c.String());
            DropTable("dbo.ExceptionLogs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.CalcRes", "ExceptionMessage");
        }
    }
}
