using System.Data.Entity.Migrations;

namespace DatabaseServer.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.User");
        }
    }
}
