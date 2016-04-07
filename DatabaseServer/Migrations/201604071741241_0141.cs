namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0141 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Player", name: "PlayerId", newName: "UserId");
            RenameIndex(table: "dbo.Player", name: "IX_PlayerId", newName: "IX_UserId");
            CreateTable(
                "dbo.CardPool",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Player", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Deck",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        DeckIndex = c.Int(nullable: false),
                        Name = c.String(maxLength: 8),
                        CardId1 = c.Int(),
                        IsDefault = c.Boolean(nullable: false),
                        CardId2 = c.Int(),
                        CardId3 = c.Int(),
                        CardId4 = c.Int(),
                        CardId5 = c.Int(),
                        CardId6 = c.Int(),
                        CardId7 = c.Int(),
                        CardId8 = c.Int(),
                        CardId9 = c.Int(),
                        CardId10 = c.Int(),
                        CardId11 = c.Int(),
                        CardId12 = c.Int(),
                        CardId13 = c.Int(),
                        CardId14 = c.Int(),
                        CardId15 = c.Int(),
                        CardId16 = c.Int(),
                        CardId17 = c.Int(),
                        CardId18 = c.Int(),
                        CardId19 = c.Int(),
                        CardId20 = c.Int(),
                        CardId21 = c.Int(),
                        CardId22 = c.Int(),
                        CardId23 = c.Int(),
                        CardId24 = c.Int(),
                        CardId25 = c.Int(),
                    })
                .PrimaryKey(t => new { t.UserId, t.DeckIndex })
                .ForeignKey("dbo.CardPool", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CardPool_Card_Mapping",
                c => new
                    {
                        CardPoolId = c.Int(nullable: false),
                        CardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CardPoolId, t.CardId })
                .ForeignKey("dbo.CardPool", t => t.CardPoolId, cascadeDelete: true)
                .ForeignKey("dbo.Card", t => t.CardId, cascadeDelete: true)
                .Index(t => t.CardPoolId)
                .Index(t => t.CardId);
            
            AddColumn("dbo.Player", "Win", c => c.Int(nullable: false));
            AddColumn("dbo.Player", "Total", c => c.Int(nullable: false));
            AlterColumn("dbo.Card", "Type", c => c.Byte(nullable: false));
            AlterColumn("dbo.Card", "MainAttribute", c => c.Byte(nullable: false));
            AlterColumn("dbo.Card", "Rarity", c => c.Byte(nullable: false));
            AlterColumn("dbo.LordCard", "LordAttackAttribute", c => c.Byte(nullable: false));
            AlterColumn("dbo.LordCard", "LordShieldAttribute", c => c.Byte(nullable: false));
            AlterColumn("dbo.MonsterCard", "Magnitude", c => c.Byte(nullable: false));
            AlterColumn("dbo.MonsterCard", "MonsterAttackAttribute", c => c.Byte(nullable: false));
            AlterColumn("dbo.MonsterCard", "MonsterShieldAttribute", c => c.Byte(nullable: false));
            AlterColumn("dbo.SpellCard", "Magnitude", c => c.Byte(nullable: false));
            AlterColumn("dbo.CardEffect", "Owner", c => c.Byte(nullable: false));
            AlterColumn("dbo.CardEffect", "Condition", c => c.Byte(nullable: false));
            AlterColumn("dbo.CardEffect", "Description", c => c.String(nullable: false, maxLength: 30));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CardPool", "UserId", "dbo.Player");
            DropForeignKey("dbo.Deck", "UserId", "dbo.CardPool");
            DropForeignKey("dbo.CardPool_Card_Mapping", "CardId", "dbo.Card");
            DropForeignKey("dbo.CardPool_Card_Mapping", "CardPoolId", "dbo.CardPool");
            DropIndex("dbo.CardPool_Card_Mapping", new[] { "CardId" });
            DropIndex("dbo.CardPool_Card_Mapping", new[] { "CardPoolId" });
            DropIndex("dbo.Deck", new[] { "UserId" });
            DropIndex("dbo.CardPool", new[] { "UserId" });
            AlterColumn("dbo.CardEffect", "Description", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CardEffect", "Condition", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.CardEffect", "Owner", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.SpellCard", "Magnitude", c => c.Int(nullable: false));
            AlterColumn("dbo.MonsterCard", "MonsterShieldAttribute", c => c.String(nullable: false, maxLength: 1, fixedLength: true));
            AlterColumn("dbo.MonsterCard", "MonsterAttackAttribute", c => c.String(nullable: false, maxLength: 1, fixedLength: true));
            AlterColumn("dbo.MonsterCard", "Magnitude", c => c.Int(nullable: false));
            AlterColumn("dbo.LordCard", "LordShieldAttribute", c => c.String(nullable: false, maxLength: 1, fixedLength: true));
            AlterColumn("dbo.LordCard", "LordAttackAttribute", c => c.String(nullable: false, maxLength: 1, fixedLength: true));
            AlterColumn("dbo.Card", "Rarity", c => c.String(nullable: false, maxLength: 2, fixedLength: true));
            AlterColumn("dbo.Card", "MainAttribute", c => c.String(nullable: false, maxLength: 1, fixedLength: true));
            AlterColumn("dbo.Card", "Type", c => c.String(nullable: false, maxLength: 8));
            DropColumn("dbo.Player", "Total");
            DropColumn("dbo.Player", "Win");
            DropTable("dbo.CardPool_Card_Mapping");
            DropTable("dbo.Deck");
            DropTable("dbo.CardPool");
            RenameIndex(table: "dbo.Player", name: "IX_UserId", newName: "IX_PlayerId");
            RenameColumn(table: "dbo.Player", name: "UserId", newName: "PlayerId");
        }
    }
}
