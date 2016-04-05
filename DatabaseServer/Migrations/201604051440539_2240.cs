namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2240 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CardEffect", "Condition", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CardEffect", "Description", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CardEffect", "Description", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CardEffect", "Condition", c => c.String(nullable: false, maxLength: 10));
        }
    }
}
