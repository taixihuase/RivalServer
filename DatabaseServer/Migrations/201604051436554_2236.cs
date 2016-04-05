namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2236 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CardEffect", "Owner", c => c.String(nullable: false, maxLength: 16));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CardEffect", "Owner", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
