namespace Glider_WPF_1._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateReport = c.DateTime(nullable: false, precision: 0),
                        Revenue = c.String(unicode: false),
                        Order = c.String(unicode: false),
                        Checks = c.String(unicode: false),
                        ChecksAmount = c.String(unicode: false),
                        Notes = c.String(unicode: false),
                        Done = c.Boolean(nullable: false),
                        Login = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nomination = c.String(unicode: false),
                        Quantity = c.String(unicode: false),
                        CustomerPhone = c.String(unicode: false),
                        Login = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Heading = c.String(unicode: false),
                        Tast = c.String(unicode: false),
                        Alarm = c.DateTime(nullable: false, precision: 0),
                        Replay = c.DateTime(nullable: false, precision: 0),
                        Done = c.Boolean(nullable: false),
                        Login = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserMails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Heading = c.String(unicode: false),
                        BodyMessage = c.String(unicode: false),
                        TimeMessage = c.DateTime(nullable: false, precision: 0),
                        Done = c.Boolean(nullable: false),
                        Sender = c.String(unicode: false),
                        Recipient = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Login = c.String(nullable: false, maxLength: 128, storeType: "nvarchar"),
                        Password = c.String(unicode: false),
                        Company = c.String(unicode: false),
                        Address = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Login);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.UserMails");
            DropTable("dbo.Tasks");
            DropTable("dbo.Requests");
            DropTable("dbo.Reports");
        }
    }
}
