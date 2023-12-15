namespace DB_Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CalcRes", "ExceptionMessage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CalcRes", "ExceptionMessage", c => c.String());
        }
    }
}
