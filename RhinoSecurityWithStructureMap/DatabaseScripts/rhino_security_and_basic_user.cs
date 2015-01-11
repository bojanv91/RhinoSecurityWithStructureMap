using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RhinoSecurityWithStructureMap.DatabaseScripts
{
    /// <summary>
    /// FluentMigration script
    /// </summary>
    [FluentMigrator.Migration(201501110000)]
    public class rhino_security_and_basic_user : FluentMigrator.Migration
    {
        public override void Up()
        {
            Create.Table("User")
                .WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn("Username").AsString(255).NotNullable();

            //Rhino Security tables

            Create.Table("security_EntitiesGroups")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(255).NotNullable().Unique();

            Create.Table("security_EntityTypes")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(255).NotNullable().Unique();

            Create.Table("security_EntityReferences")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("EntitySecurityKey").AsGuid().NotNullable().Unique()
                .WithColumn("Type").AsGuid().NotNullable().ForeignKey("FK_Type", "security_EntityTypes", "Id");

            Create.Table("security_EntityReferencesToEntitiesGroups")
                .WithColumn("GroupId").AsGuid().NotNullable().ForeignKey("FK_GroupId", "security_EntitiesGroups", "Id")
                .WithColumn("EntityReferenceId").AsGuid().NotNullable().ForeignKey("FK_EntityReferenceId", "security_EntityReferences", "Id");
            Create.PrimaryKey("PK_EntityReferencesToEntitiesGroups")
                .OnTable("security_EntityReferencesToEntitiesGroups")
                .Columns("GroupId", "EntityReferenceId");

            Create.Table("security_Operations")
                .WithColumn("Id").AsGuid().NotNullable()
                    .PrimaryKey()
                    .ForeignKey("FK_", "security_Operations", "Id")
                .WithColumn("Parent").AsGuid().Nullable().ForeignKey("FK_Parent", "security_Operations", "Id")
                .WithColumn("Name").AsString(255).NotNullable().Unique()
                .WithColumn("Comment").AsString(255);

            Create.Table("security_Permissions")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("EntitySecurityKey").AsGuid().Nullable()
                .WithColumn("Allow").AsBoolean().NotNullable()
                .WithColumn("Level").AsInt32().NotNullable()
                .WithColumn("EntityTypeName").AsString(255).Nullable()
                .WithColumn("Operation").AsGuid().NotNullable().ForeignKey("FK_Operation", "security_Operations", "Id")
                .WithColumn("User").AsInt32().Nullable().ForeignKey("FK_User", "User", "Id")
                .WithColumn("UsersGroup").AsGuid().Nullable().ForeignKey("FK_UsersGroup", "security_UsersGroups", "Id")
                .WithColumn("EntitiesGroup").AsGuid().Nullable().ForeignKey("FK_EntitiesGroup", "security_EntitiesGroups", "Id");

            Create.Table("security_UsersGroups")
                .WithColumn("Id").AsGuid().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(255).NotNullable().Unique()
                .WithColumn("Parent").AsGuid().Nullable().ForeignKey("FK_Parent", "security_UsersGroups", "Id");

            Create.Table("security_UsersGroupsHierarchy")
                .WithColumn("ParentGroup").AsGuid().NotNullable().ForeignKey("FK_ParentGroup", "security_UsersGroups", "Id")
                .WithColumn("ChildGroup").AsGuid().NotNullable().ForeignKey("FK_ChildGroup", "security_UsersGroups", "Id");
            Create.PrimaryKey("PK_UsersGroupsHierarchy")
                .OnTable("security_UsersGroupsHierarchy")
                .Columns("ChildGroup", "ParentGroup");

            Create.Table("security_UsersToUsersGroups")
                .WithColumn("GroupId").AsGuid().NotNullable().ForeignKey("FK_GroupId", "security_UsersGroups", "Id")
                .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("FK_UserId", "User", "Id");
            Create.PrimaryKey("PK_UsersToUsersGroups")
                .OnTable("security_UsersToUsersGroups")
                .Columns("GroupId", "UserId");
        }

        public override void Down()
        {
            Delete.Table("security_UsersToUsersGroups");
            Delete.Table("security_UsersGroupsHierarchy");
            Delete.Table("security_UsersGroups");
            Delete.Table("security_Permissions");
            Delete.Table("security_Operations");
            Delete.Table("security_EntityReferencesToEntitiesGroups");
            Delete.Table("security_EntityReferences");
            Delete.Table("security_EntityTypes");
            Delete.Table("security_EntitiesGroups");

            Delete.Table("User");
        }
    }
}
