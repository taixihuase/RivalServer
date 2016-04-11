namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2339 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deck", "CardId10", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId11", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId12", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId13", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId14", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId15", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId16", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId17", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId18", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId19", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId2", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId20", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId21", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId22", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId23", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId24", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId25", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId3", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId4", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId5", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId6", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId7", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId8", "dbo.SummonCard");
            DropForeignKey("dbo.Deck", "CardId9", "dbo.SummonCard");
            DropIndex("dbo.Deck", new[] { "CardId2" });
            DropIndex("dbo.Deck", new[] { "CardId3" });
            DropIndex("dbo.Deck", new[] { "CardId4" });
            DropIndex("dbo.Deck", new[] { "CardId5" });
            DropIndex("dbo.Deck", new[] { "CardId6" });
            DropIndex("dbo.Deck", new[] { "CardId7" });
            DropIndex("dbo.Deck", new[] { "CardId8" });
            DropIndex("dbo.Deck", new[] { "CardId9" });
            DropIndex("dbo.Deck", new[] { "CardId10" });
            DropIndex("dbo.Deck", new[] { "CardId11" });
            DropIndex("dbo.Deck", new[] { "CardId12" });
            DropIndex("dbo.Deck", new[] { "CardId13" });
            DropIndex("dbo.Deck", new[] { "CardId14" });
            DropIndex("dbo.Deck", new[] { "CardId15" });
            DropIndex("dbo.Deck", new[] { "CardId16" });
            DropIndex("dbo.Deck", new[] { "CardId17" });
            DropIndex("dbo.Deck", new[] { "CardId18" });
            DropIndex("dbo.Deck", new[] { "CardId19" });
            DropIndex("dbo.Deck", new[] { "CardId20" });
            DropIndex("dbo.Deck", new[] { "CardId21" });
            DropIndex("dbo.Deck", new[] { "CardId22" });
            DropIndex("dbo.Deck", new[] { "CardId23" });
            DropIndex("dbo.Deck", new[] { "CardId24" });
            DropIndex("dbo.Deck", new[] { "CardId25" });
            RenameColumn(table: "dbo.Deck", name: "CardId1", newName: "LordCardId");
            RenameIndex(table: "dbo.Deck", name: "IX_CardId1", newName: "IX_LordCardId");
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
            
            DropColumn("dbo.Deck", "CardId2");
            DropColumn("dbo.Deck", "CardId3");
            DropColumn("dbo.Deck", "CardId4");
            DropColumn("dbo.Deck", "CardId5");
            DropColumn("dbo.Deck", "CardId6");
            DropColumn("dbo.Deck", "CardId7");
            DropColumn("dbo.Deck", "CardId8");
            DropColumn("dbo.Deck", "CardId9");
            DropColumn("dbo.Deck", "CardId10");
            DropColumn("dbo.Deck", "CardId11");
            DropColumn("dbo.Deck", "CardId12");
            DropColumn("dbo.Deck", "CardId13");
            DropColumn("dbo.Deck", "CardId14");
            DropColumn("dbo.Deck", "CardId15");
            DropColumn("dbo.Deck", "CardId16");
            DropColumn("dbo.Deck", "CardId17");
            DropColumn("dbo.Deck", "CardId18");
            DropColumn("dbo.Deck", "CardId19");
            DropColumn("dbo.Deck", "CardId20");
            DropColumn("dbo.Deck", "CardId21");
            DropColumn("dbo.Deck", "CardId22");
            DropColumn("dbo.Deck", "CardId23");
            DropColumn("dbo.Deck", "CardId24");
            DropColumn("dbo.Deck", "CardId25");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deck", "CardId25", c => c.Int());
            AddColumn("dbo.Deck", "CardId24", c => c.Int());
            AddColumn("dbo.Deck", "CardId23", c => c.Int());
            AddColumn("dbo.Deck", "CardId22", c => c.Int());
            AddColumn("dbo.Deck", "CardId21", c => c.Int());
            AddColumn("dbo.Deck", "CardId20", c => c.Int());
            AddColumn("dbo.Deck", "CardId19", c => c.Int());
            AddColumn("dbo.Deck", "CardId18", c => c.Int());
            AddColumn("dbo.Deck", "CardId17", c => c.Int());
            AddColumn("dbo.Deck", "CardId16", c => c.Int());
            AddColumn("dbo.Deck", "CardId15", c => c.Int());
            AddColumn("dbo.Deck", "CardId14", c => c.Int());
            AddColumn("dbo.Deck", "CardId13", c => c.Int());
            AddColumn("dbo.Deck", "CardId12", c => c.Int());
            AddColumn("dbo.Deck", "CardId11", c => c.Int());
            AddColumn("dbo.Deck", "CardId10", c => c.Int());
            AddColumn("dbo.Deck", "CardId9", c => c.Int());
            AddColumn("dbo.Deck", "CardId8", c => c.Int());
            AddColumn("dbo.Deck", "CardId7", c => c.Int());
            AddColumn("dbo.Deck", "CardId6", c => c.Int());
            AddColumn("dbo.Deck", "CardId5", c => c.Int());
            AddColumn("dbo.Deck", "CardId4", c => c.Int());
            AddColumn("dbo.Deck", "CardId3", c => c.Int());
            AddColumn("dbo.Deck", "CardId2", c => c.Int());
            DropForeignKey("dbo.Deck_Card_Mapping", "CardId", "dbo.SummonCard");
            DropForeignKey("dbo.Deck_Card_Mapping", new[] { "UserId", "DeckIndex" }, "dbo.Deck");
            DropIndex("dbo.Deck_Card_Mapping", new[] { "CardId" });
            DropIndex("dbo.Deck_Card_Mapping", new[] { "UserId", "DeckIndex" });
            DropTable("dbo.Deck_Card_Mapping");
            RenameIndex(table: "dbo.Deck", name: "IX_LordCardId", newName: "IX_CardId1");
            RenameColumn(table: "dbo.Deck", name: "LordCardId", newName: "CardId1");
            CreateIndex("dbo.Deck", "CardId25");
            CreateIndex("dbo.Deck", "CardId24");
            CreateIndex("dbo.Deck", "CardId23");
            CreateIndex("dbo.Deck", "CardId22");
            CreateIndex("dbo.Deck", "CardId21");
            CreateIndex("dbo.Deck", "CardId20");
            CreateIndex("dbo.Deck", "CardId19");
            CreateIndex("dbo.Deck", "CardId18");
            CreateIndex("dbo.Deck", "CardId17");
            CreateIndex("dbo.Deck", "CardId16");
            CreateIndex("dbo.Deck", "CardId15");
            CreateIndex("dbo.Deck", "CardId14");
            CreateIndex("dbo.Deck", "CardId13");
            CreateIndex("dbo.Deck", "CardId12");
            CreateIndex("dbo.Deck", "CardId11");
            CreateIndex("dbo.Deck", "CardId10");
            CreateIndex("dbo.Deck", "CardId9");
            CreateIndex("dbo.Deck", "CardId8");
            CreateIndex("dbo.Deck", "CardId7");
            CreateIndex("dbo.Deck", "CardId6");
            CreateIndex("dbo.Deck", "CardId5");
            CreateIndex("dbo.Deck", "CardId4");
            CreateIndex("dbo.Deck", "CardId3");
            CreateIndex("dbo.Deck", "CardId2");
            AddForeignKey("dbo.Deck", "CardId9", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId8", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId7", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId6", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId5", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId4", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId3", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId25", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId24", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId23", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId22", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId21", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId20", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId2", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId19", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId18", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId17", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId16", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId15", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId14", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId13", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId12", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId11", "dbo.SummonCard", "CardId");
            AddForeignKey("dbo.Deck", "CardId10", "dbo.SummonCard", "CardId");
        }
    }
}
