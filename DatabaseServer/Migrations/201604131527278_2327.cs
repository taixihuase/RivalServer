namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2327 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Card", "Type", c => c.Byte());
            AlterColumn("dbo.Card", "MainAttribute", c => c.Byte());
            AlterColumn("dbo.Card", "Rarity", c => c.Byte());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Card", "Rarity", c => c.Byte(nullable: false));
            AlterColumn("dbo.Card", "MainAttribute", c => c.Byte(nullable: false));
            AlterColumn("dbo.Card", "Type", c => c.Byte(nullable: false));
        }
    }
}
