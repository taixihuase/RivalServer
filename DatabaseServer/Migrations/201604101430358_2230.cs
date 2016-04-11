namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2230 : DbMigration
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
                        IsDefault = c.Boolean(nullable: false),
                        CardId1 = c.Int(),
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
                .ForeignKey("dbo.SummonCard", t => t.CardId10)
                .ForeignKey("dbo.SummonCard", t => t.CardId11)
                .ForeignKey("dbo.SummonCard", t => t.CardId12)
                .ForeignKey("dbo.SummonCard", t => t.CardId13)
                .ForeignKey("dbo.SummonCard", t => t.CardId14)
                .ForeignKey("dbo.SummonCard", t => t.CardId15)
                .ForeignKey("dbo.SummonCard", t => t.CardId16)
                .ForeignKey("dbo.SummonCard", t => t.CardId17)
                .ForeignKey("dbo.SummonCard", t => t.CardId18)
                .ForeignKey("dbo.SummonCard", t => t.CardId19)
                .ForeignKey("dbo.SummonCard", t => t.CardId2)
                .ForeignKey("dbo.SummonCard", t => t.CardId20)
                .ForeignKey("dbo.SummonCard", t => t.CardId21)
                .ForeignKey("dbo.SummonCard", t => t.CardId22)
                .ForeignKey("dbo.SummonCard", t => t.CardId23)
                .ForeignKey("dbo.SummonCard", t => t.CardId24)
                .ForeignKey("dbo.SummonCard", t => t.CardId25)
                .ForeignKey("dbo.SummonCard", t => t.CardId3)
                .ForeignKey("dbo.SummonCard", t => t.CardId4)
                .ForeignKey("dbo.SummonCard", t => t.CardId5)
                .ForeignKey("dbo.SummonCard", t => t.CardId6)
                .ForeignKey("dbo.SummonCard", t => t.CardId7)
                .ForeignKey("dbo.SummonCard", t => t.CardId8)
                .ForeignKey("dbo.SummonCard", t => t.CardId9)
                .ForeignKey("dbo.LordCard", t => t.CardId1)
                .ForeignKey("dbo.Player", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CardId1)
                .Index(t => t.CardId2)
                .Index(t => t.CardId3)
                .Index(t => t.CardId4)
                .Index(t => t.CardId5)
                .Index(t => t.CardId6)
                .Index(t => t.CardId7)
                .Index(t => t.CardId8)
                .Index(t => t.CardId9)
                .Index(t => t.CardId10)
                .Index(t => t.CardId11)
                .Index(t => t.CardId12)
                .Index(t => t.CardId13)
                .Index(t => t.CardId14)
                .Index(t => t.CardId15)
                .Index(t => t.CardId16)
                .Index(t => t.CardId17)
                .Index(t => t.CardId18)
                .Index(t => t.CardId19)
                .Index(t => t.CardId20)
                .Index(t => t.CardId21)
                .Index(t => t.CardId22)
                .Index(t => t.CardId23)
                .Index(t => t.CardId24)
                .Index(t => t.CardId25);
            
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
            DropForeignKey("dbo.Player_Title_Mapping", "TitleId", "dbo.Title");
            DropForeignKey("dbo.Player_Title_Mapping", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.Friend_Mapping", "UserId_Friend", "dbo.User");
            DropForeignKey("dbo.Friend_Mapping", "UserId_Self", "dbo.User");
            DropForeignKey("dbo.Deck", "UserId", "dbo.Player");
            DropForeignKey("dbo.Deck", "CardId1", "dbo.LordCard");
            DropForeignKey("dbo.Deck", "CardId9", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId8", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId7", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId6", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId5", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId4", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId3", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId25", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId24", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId23", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId22", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId21", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId20", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId2", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId19", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId18", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId17", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId16", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId15", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId14", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId13", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId12", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId11", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId10", "dbo.SummonCard");
            DropForeignKey("dbo.CardPool_Mapping", "CardId", "dbo.Card");
            DropForeignKey("dbo.CardPool_Mapping", "UserId", "dbo.Player");
            DropForeignKey("dbo.Card_Effect_Mapping", "EffectId", "dbo.CardEffect");
            DropForeignKey("dbo.Card_Effect_Mapping", "CardId", "dbo.Card");
            DropForeignKey("dbo.Player_Avatar_Mapping", "AvatarId", "dbo.Avatar");
            DropForeignKey("dbo.Player_Avatar_Mapping", "PlayerId", "dbo.Player");
            DropIndex("dbo.LordCard", new[] { "CardId" });
            DropIndex("dbo.MonsterCard", new[] { "CardId" });
            DropIndex("dbo.SpellCard", new[] { "CardId" });
            DropIndex("dbo.SummonCard", new[] { "CardId" });
            DropIndex("dbo.Player", new[] { "TitleId" });
            DropIndex("dbo.Player", new[] { "AvatarId" });
            DropIndex("dbo.Player", new[] { "Level" });
            DropIndex("dbo.Player", new[] { "UserId" });
            DropIndex("dbo.Player_Title_Mapping", new[] { "TitleId" });
            DropIndex("dbo.Player_Title_Mapping", new[] { "PlayerId" });
            DropIndex("dbo.Friend_Mapping", new[] { "UserId_Friend" });
            DropIndex("dbo.Friend_Mapping", new[] { "UserId_Self" });
            DropIndex("dbo.CardPool_Mapping", new[] { "CardId" });
            DropIndex("dbo.CardPool_Mapping", new[] { "UserId" });
            DropIndex("dbo.Card_Effect_Mapping", new[] { "EffectId" });
            DropIndex("dbo.Card_Effect_Mapping", new[] { "CardId" });
            DropIndex("dbo.Player_Avatar_Mapping", new[] { "AvatarId" });
            DropIndex("dbo.Player_Avatar_Mapping", new[] { "PlayerId" });
            DropIndex("dbo.Deck", new[] { "CardId25" });
            DropIndex("dbo.Deck", new[] { "CardId24" });
            DropIndex("dbo.Deck", new[] { "CardId23" });
            DropIndex("dbo.Deck", new[] { "CardId22" });
            DropIndex("dbo.Deck", new[] { "CardId21" });
            DropIndex("dbo.Deck", new[] { "CardId20" });
            DropIndex("dbo.Deck", new[] { "CardId19" });
            DropIndex("dbo.Deck", new[] { "CardId18" });
            DropIndex("dbo.Deck", new[] { "CardId17" });
            DropIndex("dbo.Deck", new[] { "CardId16" });
            DropIndex("dbo.Deck", new[] { "CardId15" });
            DropIndex("dbo.Deck", new[] { "CardId14" });
            DropIndex("dbo.Deck", new[] { "CardId13" });
            DropIndex("dbo.Deck", new[] { "CardId12" });
            DropIndex("dbo.Deck", new[] { "CardId11" });
            DropIndex("dbo.Deck", new[] { "CardId10" });
            DropIndex("dbo.Deck", new[] { "CardId9" });
            DropIndex("dbo.Deck", new[] { "CardId8" });
            DropIndex("dbo.Deck", new[] { "CardId7" });
            DropIndex("dbo.Deck", new[] { "CardId6" });
            DropIndex("dbo.Deck", new[] { "CardId5" });
            DropIndex("dbo.Deck", new[] { "CardId4" });
            DropIndex("dbo.Deck", new[] { "CardId3" });
            DropIndex("dbo.Deck", new[] { "CardId2" });
            DropIndex("dbo.Deck", new[] { "CardId1" });
            DropIndex("dbo.Deck", new[] { "UserId" });
            DropTable("dbo.LordCard");
            DropTable("dbo.MonsterCard");
            DropTable("dbo.SpellCard");
            DropTable("dbo.SummonCard");
            DropTable("dbo.Player");
            DropTable("dbo.Player_Title_Mapping");
            DropTable("dbo.Friend_Mapping");
            DropTable("dbo.CardPool_Mapping");
            DropTable("dbo.Card_Effect_Mapping");
            DropTable("dbo.Player_Avatar_Mapping");
            DropTable("dbo.Level");
            DropTable("dbo.Title");
            DropTable("dbo.Deck");
            DropTable("dbo.CardEffect");
            DropTable("dbo.Card");
            DropTable("dbo.User");
            DropTable("dbo.Avatar");
        }
    }
}
