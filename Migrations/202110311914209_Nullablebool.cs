namespace _1262228_Arosh.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nullablebool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Status", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Status", c => c.Boolean(nullable: false));
        }
    }
}
