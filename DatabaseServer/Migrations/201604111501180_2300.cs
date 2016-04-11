namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2300 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Player", "Level", "dbo.Level");
            AddForeignKey("dbo.Player", "Level", "dbo.Level", "Level", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Player", "Level", "dbo.Level");
            AddForeignKey("dbo.Player", "Level", "dbo.Level", "Level");
        }
    }
}
