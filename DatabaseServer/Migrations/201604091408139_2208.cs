namespace DatabaseServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2208 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Friend_Mapping", name: "UserId_L", newName: "UserId_Self");
            RenameColumn(table: "dbo.Friend_Mapping", name: "UserId_R", newName: "UserId_Friend");
            RenameIndex(table: "dbo.Friend_Mapping", name: "IX_UserId_L", newName: "IX_UserId_Self");
            RenameIndex(table: "dbo.Friend_Mapping", name: "IX_UserId_R", newName: "IX_UserId_Friend");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Friend_Mapping", name: "IX_UserId_Friend", newName: "IX_UserId_R");
            RenameIndex(table: "dbo.Friend_Mapping", name: "IX_UserId_Self", newName: "IX_UserId_L");
            RenameColumn(table: "dbo.Friend_Mapping", name: "UserId_Friend", newName: "UserId_R");
            RenameColumn(table: "dbo.Friend_Mapping", name: "UserId_Self", newName: "UserId_L");
        }
    }
}
