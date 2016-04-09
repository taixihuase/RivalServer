namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2159 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Avatar",
                c => new
                    {
                        AvatarId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.AvatarId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Account = c.String(nullable: false, maxLength: 30),
                        Nickname = c.String(nullable: false, maxLength: 10),
                        Password = c.String(nullable: false, maxLength: 30),
                        RegistTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.CardPool",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Player", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Card",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                        Type = c.Byte(nullable: false),
                        MainAttribute = c.Byte(nullable: false),
                        Rarity = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.CardId);
            
            CreateTable(
                "dbo.CardEffect",
                c => new
                    {
                        CardEffectId = c.Int(nullable: false, identity: true),
                        Owner = c.Byte(nullable: false),
                        Condition = c.Byte(nullable: false),
                        Description = c.String(nullable: false, maxLength: 30),
                        Value = c.Int(),
                    })
                .PrimaryKey(t => t.CardEffectId);
            
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
                "dbo.Title",
                c => new
                    {
                        TitleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                    })
                .PrimaryKey(t => t.TitleId);
            
            CreateTable(
                "dbo.Level",
                c => new
                    {
                        Level = c.Int(nullable: false, identity: true),
                        UpgradeExperience = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Level);
            
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
            
            CreateTable(
                "dbo.Friend_Mapping",
                c => new
                    {
                        UserId_L = c.Int(nullable: false),
                        UserId_R = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId_L, t.UserId_R })
                .ForeignKey("dbo.User", t => t.UserId_L)
                .ForeignKey("dbo.User", t => t.UserId_R)
                .Index(t => t.UserId_L)
                .Index(t => t.UserId_R);
            
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
                .ForeignKey("dbo.Level", t => t.Level)
                .ForeignKey("dbo.Avatar", t => t.AvatarId)
                .ForeignKey("dbo.Title", t => t.TitleId)
                .Index(t => t.UserId)
                .Index(t => t.Level)
                .Index(t => t.AvatarId)
                .Index(t => t.TitleId);
            
            CreateTable(
                "dbo.SpellCard",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        Magnitude = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.Card", t => t.CardId)
                .Index(t => t.CardId);
            
            CreateTable(
                "dbo.MonsterCard",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        Magnitude = c.Byte(nullable: false),
                        Range = c.Int(nullable: false),
                        Flexibility = c.Int(nullable: false),
                        MonsterAttackAttribute = c.Byte(nullable: false),
                        MonsterAttack = c.Int(nullable: false),
                        MonsterShieldAttribute = c.Byte(nullable: false),
                        MonsterShield = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.Card", t => t.CardId)
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
            DropForeignKey("dbo.MonsterCard", "CardId", "dbo.Card");
            DropForeignKey("dbo.SpellCard", "CardId", "dbo.Card");
            DropForeignKey("dbo.Player", "TitleId", "dbo.Title");
            DropForeignKey("dbo.Player", "AvatarId", "dbo.Avatar");
            DropForeignKey("dbo.Player", "Level", "dbo.Level");
            DropForeignKey("dbo.Player", "UserId", "dbo.User");
            DropForeignKey("dbo.Player_Title_Mapping", "TitleId", "dbo.Title");
            DropForeignKey("dbo.Player_Title_Mapping", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Friend_Mapping", "UserId_R", "dbo.User");
            DropForeignKey("dbo.Friend_Mapping", "UserId_L", "dbo.User");
            DropForeignKey("dbo.CardPool", "UserId", "dbo.Player");
            DropForeignKey("dbo.Deck", "UserId", "dbo.CardPool");
            DropForeignKey("dbo.CardPool_Card_Mapping", "CardId", "dbo.Card");
            DropForeignKey("dbo.CardPool_Card_Mapping", "CardPoolId", "dbo.CardPool");
            DropForeignKey("dbo.Card_Effect_Mapping", "EffectId", "dbo.CardEffect");
            DropForeignKey("dbo.Card_Effect_Mapping", "CardId", "dbo.Card");
            DropForeignKey("dbo.Player_Avatar_Mapping", "AvatarId", "dbo.Avatar");
            DropForeignKey("dbo.Player_Avatar_Mapping", "PlayerId", "dbo.Player");
            DropIndex("dbo.LordCard", new[] { "CardId" });
            DropIndex("dbo.MonsterCard", new[] { "CardId" });
            DropIndex("dbo.SpellCard", new[] { "CardId" });
            DropIndex("dbo.Player", new[] { "TitleId" });
            DropIndex("dbo.Player", new[] { "AvatarId" });
            DropIndex("dbo.Player", new[] { "Level" });
            DropIndex("dbo.Player", new[] { "UserId" });
            DropIndex("dbo.Player_Title_Mapping", new[] { "TitleId" });
            DropIndex("dbo.Player_Title_Mapping", new[] { "PlayerId" });
            DropIndex("dbo.Friend_Mapping", new[] { "UserId_R" });
            DropIndex("dbo.Friend_Mapping", new[] { "UserId_L" });
            DropIndex("dbo.CardPool_Card_Mapping", new[] { "CardId" });
            DropIndex("dbo.CardPool_Card_Mapping", new[] { "CardPoolId" });
            DropIndex("dbo.Card_Effect_Mapping", new[] { "EffectId" });
            DropIndex("dbo.Card_Effect_Mapping", new[] { "CardId" });
            DropIndex("dbo.Player_Avatar_Mapping", new[] { "AvatarId" });
            DropIndex("dbo.Player_Avatar_Mapping", new[] { "PlayerId" });
            DropIndex("dbo.Deck", new[] { "UserId" });
            DropIndex("dbo.CardPool", new[] { "UserId" });
            DropTable("dbo.LordCard");
            DropTable("dbo.MonsterCard");
            DropTable("dbo.SpellCard");
            DropTable("dbo.Player");
            DropTable("dbo.Player_Title_Mapping");
            DropTable("dbo.Friend_Mapping");
            DropTable("dbo.CardPool_Card_Mapping");
            DropTable("dbo.Card_Effect_Mapping");
            DropTable("dbo.Player_Avatar_Mapping");
            DropTable("dbo.Level");
            DropTable("dbo.Title");
            DropTable("dbo.Deck");
            DropTable("dbo.CardEffect");
            DropTable("dbo.Card");
            DropTable("dbo.CardPool");
            DropTable("dbo.User");
            DropTable("dbo.Avatar");
        }
    }
}
