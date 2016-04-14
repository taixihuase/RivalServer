namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _0025 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avatar",
                c => new
                    {
                        AvatarId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.AvatarId);
            
            CreateTable(
                "dbo.CardEffect",
                c => new
                    {
                        CardEffectId = c.Int(nullable: false),
                        Owner = c.Byte(nullable: false),
                        Condition = c.Byte(nullable: false),
                        Description = c.String(nullable: false, maxLength: 30),
                        Value = c.Int(),
                    })
                .PrimaryKey(t => t.CardEffectId);
            
            CreateTable(
                "dbo.CardPack",
                c => new
                    {
                        CardPackId = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardPackId);
            
            CreateTable(
                "dbo.Card",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10),
                        Type = c.Byte(),
                        MainAttribute = c.Byte(),
                        Rarity = c.Byte(),
                        CardPackId = c.Int(),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.CardPack", t => t.CardPackId)
                .Index(t => t.CardPackId);
            
            CreateTable(
                "dbo.Deck",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        DeckIndex = c.Int(nullable: false),
                        Name = c.String(maxLength: 8),
                        IsDefault = c.Boolean(nullable: false),
                        LordCardId = c.Int(),
                        CardCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.DeckIndex })
                .ForeignKey("dbo.LordCard", t => t.LordCardId)
                .ForeignKey("dbo.Player", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.LordCardId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 30),
                        Nickname = c.String(nullable: false, maxLength: 10),
                        Password = c.String(nullable: false, maxLength: 30),
                        RegistTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Title",
                c => new
                    {
                        TitleId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.TitleId);
            
            CreateTable(
                "dbo.Level",
                c => new
                    {
                        Level = c.Int(nullable: false),
                        UpgradeExperience = c.Int(nullable: false),
                        WinExperience = c.Int(nullable: false),
                        LoseExperience = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Level);
            
            CreateTable(
                "dbo.Player_CardPack_Mapping",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CardPackId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CardPackId })
                .ForeignKey("dbo.CardPack", t => t.CardPackId, cascadeDelete: true)
                .ForeignKey("dbo.Player", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CardPackId);
            
            CreateTable(
                "dbo.Card_Effect_Mapping",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        EffectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CardId, t.EffectId })
                .ForeignKey("dbo.Card", t => t.CardId, cascadeDelete: true)
                .ForeignKey("dbo.CardEffect", t => t.EffectId, cascadeDelete: true)
                .Index(t => t.CardId)
                .Index(t => t.EffectId);
            
            CreateTable(
                "dbo.Player_Avatar_Mapping",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        AvatarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.AvatarId })
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Avatar", t => t.AvatarId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.AvatarId);
            
            CreateTable(
                "dbo.CardPool_Mapping",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CardId })
                .ForeignKey("dbo.Player", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Card", t => t.CardId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CardId);
            
            CreateTable(
                "dbo.Friend_Mapping",
                c => new
                    {
                        UserId_Self = c.Int(nullable: false),
                        UserId_Friend = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId_Self, t.UserId_Friend })
                .ForeignKey("dbo.User", t => t.UserId_Self)
                .ForeignKey("dbo.User", t => t.UserId_Friend)
                .Index(t => t.UserId_Self)
                .Index(t => t.UserId_Friend);
            
            CreateTable(
                "dbo.Player_Title_Mapping",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        TitleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlayerId, t.TitleId })
                .ForeignKey("dbo.Player", t => t.PlayerId, cascadeDelete: true)
                .ForeignKey("dbo.Title", t => t.TitleId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.TitleId);
            
            CreateTable(
                "dbo.Deck_Card_Mapping",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        DeckIndex = c.Int(nullable: false),
                        CardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.DeckIndex, t.CardId })
                .ForeignKey("dbo.Deck", t => new { t.UserId, t.DeckIndex }, cascadeDelete: true)
                .ForeignKey("dbo.SummonCard", t => t.CardId, cascadeDelete: true)
                .Index(t => new { t.UserId, t.DeckIndex })
                .Index(t => t.CardId);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        Experience = c.Int(nullable: false),
                        AvatarId = c.Int(nullable: false),
                        TitleId = c.Int(nullable: false),
                        Currency = c.Int(nullable: false),
                        Win = c.Int(nullable: false),
                        Total = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Level", t => t.Level, cascadeDelete: true)
                .ForeignKey("dbo.Avatar", t => t.AvatarId)
                .ForeignKey("dbo.Title", t => t.TitleId)
                .Index(t => t.UserId)
                .Index(t => t.Level)
                .Index(t => t.AvatarId)
                .Index(t => t.TitleId);
            
            CreateTable(
                "dbo.SummonCard",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        Magnitude = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.Card", t => t.CardId)
                .Index(t => t.CardId);
            
            CreateTable(
                "dbo.SpellCard",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.SummonCard", t => t.CardId)
                .Index(t => t.CardId);
            
            CreateTable(
                "dbo.MonsterCard",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        Range = c.Int(nullable: false),
                        Flexibility = c.Int(nullable: false),
                        MonsterAttackAttribute = c.Byte(nullable: false),
                        MonsterAttack = c.Int(nullable: false),
                        MonsterShieldAttribute = c.Byte(nullable: false),
                        MonsterShield = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.SummonCard", t => t.CardId)
                .Index(t => t.CardId);
            
            CreateTable(
                "dbo.LordCard",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        LordAttackAttribute = c.Byte(nullable: false),
                        LordAttack = c.Int(nullable: false),
                        LordShieldAttribute = c.Byte(nullable: false),
                        LordShield = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.Card", t => t.CardId)
                .Index(t => t.CardId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LordCard", "CardId", "dbo.Card");
            DropForeignKey("dbo.MonsterCard", "CardId", "dbo.SummonCard");
            DropForeignKey("dbo.SpellCard", "CardId", "dbo.SummonCard");
            DropForeignKey("dbo.SummonCard", "CardId", "dbo.Card");
            DropForeignKey("dbo.Player", "TitleId", "dbo.Title");
            DropForeignKey("dbo.Player", "AvatarId", "dbo.Avatar");
            DropForeignKey("dbo.Player", "Level", "dbo.Level");
            DropForeignKey("dbo.Player", "UserId", "dbo.User");
            DropForeignKey("dbo.Deck_Card_Mapping", "CardId", "dbo.SummonCard");
            DropForeignKey("dbo.Deck_Card_Mapping", new[] { "UserId", "DeckIndex" }, "dbo.Deck");
            DropForeignKey("dbo.Deck", "UserId", "dbo.Player");
            DropForeignKey("dbo.Player_Title_Mapping", "TitleId", "dbo.Title");
            DropForeignKey("dbo.Player_Title_Mapping", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Player_CardPack_Mapping", "UserId", "dbo.Player");
            DropForeignKey("dbo.Player_CardPack_Mapping", "CardPackId", "dbo.CardPack");
            DropForeignKey("dbo.Friend_Mapping", "UserId_Friend", "dbo.User");
            DropForeignKey("dbo.Friend_Mapping", "UserId_Self", "dbo.User");
            DropForeignKey("dbo.CardPool_Mapping", "CardId", "dbo.Card");
            DropForeignKey("dbo.CardPool_Mapping", "UserId", "dbo.Player");
            DropForeignKey("dbo.Player_Avatar_Mapping", "AvatarId", "dbo.Avatar");
            DropForeignKey("dbo.Player_Avatar_Mapping", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Deck", "LordCardId", "dbo.LordCard");
            DropForeignKey("dbo.Card", "CardPackId", "dbo.CardPack");
            DropForeignKey("dbo.Card_Effect_Mapping", "EffectId", "dbo.CardEffect");
            DropForeignKey("dbo.Card_Effect_Mapping", "CardId", "dbo.Card");
            DropIndex("dbo.LordCard", new[] { "CardId" });
            DropIndex("dbo.MonsterCard", new[] { "CardId" });
            DropIndex("dbo.SpellCard", new[] { "CardId" });
            DropIndex("dbo.SummonCard", new[] { "CardId" });
            DropIndex("dbo.Player", new[] { "TitleId" });
            DropIndex("dbo.Player", new[] { "AvatarId" });
            DropIndex("dbo.Player", new[] { "Level" });
            DropIndex("dbo.Player", new[] { "UserId" });
            DropIndex("dbo.Deck_Card_Mapping", new[] { "CardId" });
            DropIndex("dbo.Deck_Card_Mapping", new[] { "UserId", "DeckIndex" });
            DropIndex("dbo.Player_Title_Mapping", new[] { "TitleId" });
            DropIndex("dbo.Player_Title_Mapping", new[] { "PlayerId" });
            DropIndex("dbo.Friend_Mapping", new[] { "UserId_Friend" });
            DropIndex("dbo.Friend_Mapping", new[] { "UserId_Self" });
            DropIndex("dbo.CardPool_Mapping", new[] { "CardId" });
            DropIndex("dbo.CardPool_Mapping", new[] { "UserId" });
            DropIndex("dbo.Player_Avatar_Mapping", new[] { "AvatarId" });
            DropIndex("dbo.Player_Avatar_Mapping", new[] { "PlayerId" });
            DropIndex("dbo.Card_Effect_Mapping", new[] { "EffectId" });
            DropIndex("dbo.Card_Effect_Mapping", new[] { "CardId" });
            DropIndex("dbo.Player_CardPack_Mapping", new[] { "CardPackId" });
            DropIndex("dbo.Player_CardPack_Mapping", new[] { "UserId" });
            DropIndex("dbo.Deck", new[] { "LordCardId" });
            DropIndex("dbo.Deck", new[] { "UserId" });
            DropIndex("dbo.Card", new[] { "CardPackId" });
            DropTable("dbo.LordCard");
            DropTable("dbo.MonsterCard");
            DropTable("dbo.SpellCard");
            DropTable("dbo.SummonCard");
            DropTable("dbo.Player");
            DropTable("dbo.Deck_Card_Mapping");
            DropTable("dbo.Player_Title_Mapping");
            DropTable("dbo.Friend_Mapping");
            DropTable("dbo.CardPool_Mapping");
            DropTable("dbo.Player_Avatar_Mapping");
            DropTable("dbo.Card_Effect_Mapping");
            DropTable("dbo.Player_CardPack_Mapping");
            DropTable("dbo.Level");
            DropTable("dbo.Title");
            DropTable("dbo.User");
            DropTable("dbo.Deck");
            DropTable("dbo.Card");
            DropTable("dbo.CardPack");
            DropTable("dbo.CardEffect");
            DropTable("dbo.Avatar");
        }
    }
}
