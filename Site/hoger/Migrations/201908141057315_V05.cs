namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V05 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsVip", c => c.Boolean(nullable: false));
            AddColumn("dbo.Products", "IsHotSell", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsHotSell");
            DropColumn("dbo.Products", "IsVip");
        }
    }
}
