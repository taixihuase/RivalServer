namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2216 : DbMigration
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
                "dbo.Player",
                c => new
                    {
                        PlayerId = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        Experience = c.Int(nullable: false),
                        AvatarId = c.Int(nullable: false),
                        TitleId = c.Int(nullable: false),
                        Currency = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.Avatar", t => t.AvatarId)
                .ForeignKey("dbo.Title", t => t.TitleId)
                .ForeignKey("dbo.Level", t => t.Level)
                .ForeignKey("dbo.User", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId)
                .Index(t => t.Level)
                .Index(t => t.AvatarId)
                .Index(t => t.TitleId);
            
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
                "dbo.Card",
                c => new
                    {
                        CardId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10),
                        Type = c.String(nullable: false, maxLength: 8),
                        MainAttribute = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        Rarity = c.String(nullable: false, maxLength: 2, fixedLength: true),
                    })
                .PrimaryKey(t => t.CardId);
            
            CreateTable(
                "dbo.CardEffect",
                c => new
                    {
                        CardEffectId = c.Int(nullable: false, identity: true),
                        Owner = c.String(nullable: false, maxLength: 10),
                        Condition = c.String(nullable: false, maxLength: 10),
                        Description = c.String(nullable: false, maxLength: 20),
                        Value = c.Int(),
                    })
                .PrimaryKey(t => t.CardEffectId);
            
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
                "dbo.SpellCard",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        Magnitude = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CardId)
                .ForeignKey("dbo.Card", t => t.CardId)
                .Index(t => t.CardId);
            
            CreateTable(
                "dbo.MonsterCard",
                c => new
                    {
                        CardId = c.Int(nullable: false),
                        Magnitude = c.Int(nullable: false),
                        Range = c.Int(nullable: false),
                        Flexibility = c.Int(nullable: false),
                        MonsterAttackAttribute = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        MonsterAttack = c.Int(nullable: false),
                        MonsterShieldAttribute = c.String(nullable: false, maxLength: 1, fixedLength: true),
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
                        LordAttackAttribute = c.String(nullable: false, maxLength: 1, fixedLength: true),
                        LordAttack = c.Int(nullable: false),
                        LordShieldAttribute = c.String(nullable: false, maxLength: 1, fixedLength: true),
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
            DropForeignKey("dbo.Card_Effect_Mapping", "EffectId", "dbo.CardEffect");
            DropForeignKey("dbo.Card_Effect_Mapping", "CardId", "dbo.Card");
            DropForeignKey("dbo.Player", "PlayerId", "dbo.User");
            DropForeignKey("dbo.Friend_Mapping", "UserId_R", "dbo.User");
            DropForeignKey("dbo.Friend_Mapping", "UserId_L", "dbo.User");
            DropForeignKey("dbo.Player_Title_Mapping", "TitleId", "dbo.Title");
            DropForeignKey("dbo.Player_Title_Mapping", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Player", "Level", "dbo.Level");
            DropForeignKey("dbo.Player", "TitleId", "dbo.Title");
            DropForeignKey("dbo.Player", "AvatarId", "dbo.Avatar");
            DropForeignKey("dbo.Player_Avatar_Mapping", "AvatarId", "dbo.Avatar");
            DropForeignKey("dbo.Player_Avatar_Mapping", "PlayerId", "dbo.Player");
            DropIndex("dbo.LordCard", new[] { "CardId" });
            DropIndex("dbo.MonsterCard", new[] { "CardId" });
            DropIndex("dbo.SpellCard", new[] { "CardId" });
            DropIndex("dbo.Card_Effect_Mapping", new[] { "EffectId" });
            DropIndex("dbo.Card_Effect_Mapping", new[] { "CardId" });
            DropIndex("dbo.Friend_Mapping", new[] { "UserId_R" });
            DropIndex("dbo.Friend_Mapping", new[] { "UserId_L" });
            DropIndex("dbo.Player_Title_Mapping", new[] { "TitleId" });
            DropIndex("dbo.Player_Title_Mapping", new[] { "PlayerId" });
            DropIndex("dbo.Player_Avatar_Mapping", new[] { "AvatarId" });
            DropIndex("dbo.Player_Avatar_Mapping", new[] { "PlayerId" });
            DropIndex("dbo.Player", new[] { "TitleId" });
            DropIndex("dbo.Player", new[] { "AvatarId" });
            DropIndex("dbo.Player", new[] { "Level" });
            DropIndex("dbo.Player", new[] { "PlayerId" });
            DropTable("dbo.LordCard");
            DropTable("dbo.MonsterCard");
            DropTable("dbo.SpellCard");
            DropTable("dbo.Card_Effect_Mapping");
            DropTable("dbo.Friend_Mapping");
            DropTable("dbo.Player_Title_Mapping");
            DropTable("dbo.Player_Avatar_Mapping");
            DropTable("dbo.CardEffect");
            DropTable("dbo.Card");
            DropTable("dbo.User");
            DropTable("dbo.Level");
            DropTable("dbo.Title");
            DropTable("dbo.Player");
            DropTable("dbo.Avatar");
        }
    }
}
