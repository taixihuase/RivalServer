namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2345 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deck", "CardCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deck", "CardCount");
        }
    }
}
