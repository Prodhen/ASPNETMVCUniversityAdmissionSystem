namespace _1262228_Arosh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUnitTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Unit", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Unit");
        }
    }
}
