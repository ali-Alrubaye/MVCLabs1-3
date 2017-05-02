namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumId = c.Guid(nullable: false),
                        AlbumName = c.String(),
                        AlbumDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.AlbumId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Guid(nullable: false),
                        PhotoName = c.String(),
                        PhotoDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        PhotoPath = c.String(),
                        AlbumId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.Albums", t => t.AlbumId, cascadeDelete: true)
                .Index(t => t.AlbumId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "AlbumId", "dbo.Albums");
            DropIndex("dbo.Photos", new[] { "AlbumId" });
            DropTable("dbo.Photos");
            DropTable("dbo.Albums");
        }
    }
}
