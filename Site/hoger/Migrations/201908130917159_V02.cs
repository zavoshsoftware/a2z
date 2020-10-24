namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V02 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "ColorId", "dbo.Colors");
            DropIndex("dbo.Products", new[] { "ColorId" });
            CreateTable(
                "dbo.Product_Color",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ColorId = c.Guid(nullable: false),
                        ProductId = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        DeleteUserId = c.Guid(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Colors", t => t.ColorId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ColorId)
                .Index(t => t.ProductId);
            
            DropColumn("dbo.Products", "ColorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "ColorId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Product_Color", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Product_Color", "ColorId", "dbo.Colors");
            DropIndex("dbo.Product_Color", new[] { "ProductId" });
            DropIndex("dbo.Product_Color", new[] { "ColorId" });
            DropTable("dbo.Product_Color");
            CreateIndex("dbo.Products", "ColorId");
            AddForeignKey("dbo.Products", "ColorId", "dbo.Colors", "Id", cascadeDelete: true);
        }
    }
}
