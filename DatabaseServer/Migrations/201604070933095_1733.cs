namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1733 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CardPool",
                c => new
                    {
                        CardPoolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardPoolId)
                .ForeignKey("dbo.Player", t => t.CardPoolId, cascadeDelete: true)
                .Index(t => t.CardPoolId);
            
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
            DropForeignKey("dbo.CardPool", "CardPoolId", "dbo.Player");
            DropForeignKey("dbo.CardPool_Card_Mapping", "CardId", "dbo.Card");
            DropForeignKey("dbo.CardPool_Card_Mapping", "CardPoolId", "dbo.CardPool");
            DropIndex("dbo.CardPool_Card_Mapping", new[] { "CardId" });
            DropIndex("dbo.CardPool_Card_Mapping", new[] { "CardPoolId" });
            DropIndex("dbo.CardPool", new[] { "CardPoolId" });
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
            DropTable("dbo.CardPool");
        }
    }
}
