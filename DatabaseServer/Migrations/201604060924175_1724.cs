namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1724 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CardEffect", "Owner", c => c.Byte(nullable: false));
            AlterColumn("dbo.CardEffect", "Condition", c => c.Byte(nullable: false));
            AlterColumn("dbo.Card", "Type", c => c.Byte(nullable: false));
            AlterColumn("dbo.Card", "MainAttribute", c => c.Byte(nullable: false));
            AlterColumn("dbo.Card", "Rarity", c => c.Byte(nullable: false));
            AlterColumn("dbo.LordCard", "LordAttackAttribute", c => c.Byte(nullable: false));
            AlterColumn("dbo.LordCard", "LordShieldAttribute", c => c.Byte(nullable: false));
            AlterColumn("dbo.MonsterCard", "Magnitude", c => c.Byte(nullable: false));
            AlterColumn("dbo.MonsterCard", "MonsterAttackAttribute", c => c.Byte(nullable: false));
            AlterColumn("dbo.MonsterCard", "MonsterShieldAttribute", c => c.Byte(nullable: false));
            AlterColumn("dbo.SpellCard", "Magnitude", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SpellCard", "Magnitude", c => c.Int(nullable: false));
            AlterColumn("dbo.MonsterCard", "MonsterShieldAttribute", c => c.String(nullable: false, maxLength: 1, fixedLength: true));
            AlterColumn("dbo.MonsterCard", "MonsterAttackAttribute", c => c.String(nullable: false, maxLength: 1, fixedLength: true));
            AlterColumn("dbo.MonsterCard", "Magnitude", c => c.Int(nullable: false));
            AlterColumn("dbo.LordCard", "LordShieldAttribute", c => c.String(nullable: false, maxLength: 1, fixedLength: true));
            AlterColumn("dbo.LordCard", "LordAttackAttribute", c => c.String(nullable: false, maxLength: 1, fixedLength: true));
            AlterColumn("dbo.Card", "Rarity", c => c.String(nullable: false, maxLength: 2, fixedLength: true));
            AlterColumn("dbo.Card", "MainAttribute", c => c.String(nullable: false, maxLength: 1, fixedLength: true));
            AlterColumn("dbo.Card", "Type", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.CardEffect", "Condition", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.CardEffect", "Owner", c => c.String(nullable: false, maxLength: 16));
        }
    }
}
