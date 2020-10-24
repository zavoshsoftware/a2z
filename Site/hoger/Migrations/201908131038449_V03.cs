namespace Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V03 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "SizeId", "dbo.Sizes");
            DropIndex("dbo.Products", new[] { "SizeId" });
            DropColumn("dbo.Products", "SizeId");
            DropTable("dbo.Sizes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Order = c.Int(nullable: false),
                        Title = c.String(),
                        ImageUrl = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreateUserId = c.Guid(),
                        LastModifiedDate = c.DateTime(),
                        IsDeleted = c.Boolean(nullable: false),
                        DeletionDate = c.DateTime(),
                        DeleteUserId = c.Guid(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Products", "SizeId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Products", "SizeId");
            AddForeignKey("dbo.Products", "SizeId", "dbo.Sizes", "Id", cascadeDelete: true);
        }
    }
}
