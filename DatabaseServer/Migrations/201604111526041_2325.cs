namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2325 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardPack",
                c => new
                    {
                        CardPackId = c.Int(nullable: false, identity: true),
                        Type = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardPackId);
            
            AddColumn("dbo.Card", "CardPackId", c => c.Int());
            CreateIndex("dbo.Card", "CardPackId");
            AddForeignKey("dbo.Card", "CardPackId", "dbo.CardPack", "CardPackId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Card", "CardPackId", "dbo.CardPack");
            DropIndex("dbo.Card", new[] { "CardPackId" });
            DropColumn("dbo.Card", "CardPackId");
            DropTable("dbo.CardPack");
        }
    }
}
