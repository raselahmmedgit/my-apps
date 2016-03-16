using System.Data.Entity.ModelConfiguration.Conventions;
using SPEDU.Common.Utility;
using SPEDU.Domain.Models.Application;
using System.Data.Entity;
using System.Collections.Generic;
using System;

namespace SPEDU.Data
{
    public class AppDbContext : DbContext
    {
        #region Accounts
        
        #endregion

        #region Application

        public DbSet<DefaultSetting> DefaultSetting { get; set; }
        public DbSet<EmailTemplate> EmailTemplate { get; set; }
        public DbSet<EmailTemplateCategory> EmailTemplateCategory { get; set; }
        public DbSet<ApplicationInfo> ApplicationInfo { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuRolePermission> MenuRolePermission { get; set; }
        public DbSet<MenuUserPermission> MenuUserPermission { get; set; }
        public DbSet<Right> Right { get; set; }
        public DbSet<RightRolePermission> RightRolePermission { get; set; }
        public DbSet<RightUserPermission> RightUserPermission { get; set; }
        public DbSet<SMSTemplate> SMSTemplate { get; set; }
        public DbSet<SMSTemplateCategory> SMSTemplateCategory { get; set; }
        public DbSet<UserActivity> UserActivity { get; set; }
        public DbSet<UserMetadata> UserMetadata { get; set; }
        public DbSet<Widget> Widget { get; set; }
        public DbSet<WidgetCategory> WidgetCategory { get; set; }
        public DbSet<WidgetRolePermission> WidgetRolePermission { get; set; }
        public DbSet<WidgetUserPermission> WidgetUserPermission { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        #endregion

        #region Employee

        #endregion

        #region Lidrary

        #endregion

        #region Settings

        #endregion

        #region Student

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ContactJobTiteRatingStaffingRate>()
            //        .Map(e => e.ToTable("ContactJobTiteRatingStaffingRate"));
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

    }

    #region Initial data

    // Change the base class as follows if you want to drop and create the database during development:
    //public class DbInitializer : DropCreateDatabaseAlways<AppDbContext>
    //public class DbInitializer : CreateDatabaseIfNotExists<AppDbContext>
    public class DbInitializer : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {

        #region Accounts
        
        #endregion

        #region Application

        // Create Default User.
        var userList = new List<User>
                {
                    new User {UserId = Convert.ToInt32(UserEnum.SuperAdmin), UserName = UserEnum.SuperAdmin.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now, CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                    new User {UserId = Convert.ToInt32(UserEnum.Admin), UserName = UserEnum.Admin.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now, CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                    new User {UserId = Convert.ToInt32(UserEnum.HeadTeacher), UserName = UserEnum.HeadTeacher.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now, CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                    new User {UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher), UserName = UserEnum.AssistantHeadTeacher.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now, CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                    new User {UserId = Convert.ToInt32(UserEnum.Teacher), UserName = UserEnum.Teacher.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now, CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                    new User {UserId = Convert.ToInt32(UserEnum.Employee), UserName = UserEnum.Employee.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now, CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                    new User {UserId = Convert.ToInt32(UserEnum.Student), UserName = UserEnum.Student.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now, CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                    new User {UserId = Convert.ToInt32(UserEnum.Parent), UserName = UserEnum.Parent.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now, CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                    new User {UserId = Convert.ToInt32(UserEnum.Guardian), UserName = UserEnum.Guardian.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now, CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now}

                };
        userList.ForEach(item => context.User.Add(item));
        context.SaveChanges();

        // Create Default Role.
        var roleList = new List<Role>
                    {
                        new Role {RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), RoleName = RoleEnum.SuperAdmin.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.Admin), RoleName = RoleEnum.Admin.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.HeadTeacher), RoleName = RoleEnum.HeadTeacher.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.AssistantHeadTeacher), RoleName = RoleEnum.AssistantHeadTeacher.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.Teacher), RoleName = RoleEnum.Teacher.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.Employee), RoleName = RoleEnum.Employee.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.Student), RoleName = RoleEnum.Student.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.Parent), RoleName = RoleEnum.Parent.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.Guardian), RoleName = RoleEnum.Guardian.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now}

                    };
        roleList.ForEach(item => context.Role.Add(item));
        context.SaveChanges();

        

        // Create Default UserRole.
        var userRoleList = new List<UserRole>
                    {
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), UserId = Convert.ToInt32(UserEnum.SuperAdmin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Admin), UserId = Convert.ToInt32(UserEnum.SuperAdmin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.HeadTeacher), UserId = Convert.ToInt32(UserEnum.SuperAdmin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.AssistantHeadTeacher), UserId = Convert.ToInt32(UserEnum.SuperAdmin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Teacher), UserId = Convert.ToInt32(UserEnum.SuperAdmin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Employee), UserId = Convert.ToInt32(UserEnum.SuperAdmin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.SuperAdmin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Parent), UserId = Convert.ToInt32(UserEnum.SuperAdmin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Guardian), UserId = Convert.ToInt32(UserEnum.SuperAdmin)},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Admin), UserId = Convert.ToInt32(UserEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.HeadTeacher), UserId = Convert.ToInt32(UserEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.AssistantHeadTeacher), UserId = Convert.ToInt32(UserEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Teacher), UserId = Convert.ToInt32(UserEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Employee), UserId = Convert.ToInt32(UserEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Parent), UserId = Convert.ToInt32(UserEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Guardian), UserId = Convert.ToInt32(UserEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.HeadTeacher), UserId = Convert.ToInt32(UserEnum.HeadTeacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.AssistantHeadTeacher), UserId = Convert.ToInt32(UserEnum.HeadTeacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Teacher), UserId = Convert.ToInt32(UserEnum.HeadTeacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Employee), UserId = Convert.ToInt32(UserEnum.HeadTeacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.HeadTeacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Parent), UserId = Convert.ToInt32(UserEnum.HeadTeacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Guardian), UserId = Convert.ToInt32(UserEnum.HeadTeacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.AssistantHeadTeacher), UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Teacher), UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Employee), UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Parent), UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Guardian), UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Teacher), UserId = Convert.ToInt32(UserEnum.Teacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.Teacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Parent), UserId = Convert.ToInt32(UserEnum.Teacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Guardian), UserId = Convert.ToInt32(UserEnum.Teacher), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Employee), UserId = Convert.ToInt32(UserEnum.Employee), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.Employee), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Parent), UserId = Convert.ToInt32(UserEnum.Employee), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Guardian), UserId = Convert.ToInt32(UserEnum.Employee), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.Student), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.Parent), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Parent), UserId = Convert.ToInt32(UserEnum.Parent), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.Guardian), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Guardian), UserId = Convert.ToInt32(UserEnum.Guardian), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now}

                    };
        userRoleList.ForEach(item => context.UserRole.Add(item));
        context.SaveChanges();

        // Create Default Right.
        var appRightList = new List<Right>
                    {
                        new Right {RightId = Convert.ToInt32(RightEnum.Add), Name = RightEnum.Add.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.Edit), Name = RightEnum.Edit.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.Details), Name = RightEnum.Details.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.Delete), Name = RightEnum.Delete.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.DeleteBulk), Name = RightEnum.DeleteBulk.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.Archive), Name = RightEnum.Archive.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.ArchiveBulk), Name = RightEnum.ArchiveBulk.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.Remove), Name = RightEnum.Remove.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.RemoveBulk), Name = RightEnum.RemoveBulk.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.Assign), Name = RightEnum.Assign.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.Approve), Name = RightEnum.Approve.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.SendEmail), Name = RightEnum.SendEmail.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.SendEmailBulk), Name = RightEnum.SendEmailBulk.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.SendSMS), Name = RightEnum.SendSMS.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.SendSMSBulk), Name = RightEnum.SendSMSBulk.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.ImportExcel), Name = RightEnum.ImportExcel.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.ImportCsv), Name = RightEnum.ImportCsv.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.ExportExcel), Name = RightEnum.ExportExcel.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.ExportCsv), Name = RightEnum.ExportCsv.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.ImportVCard), Name = RightEnum.ImportVCard.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.ExportVCard), Name = RightEnum.ExportVCard.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.ExportVCardBulk), Name = RightEnum.ExportVCardBulk.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.Call), Name = RightEnum.Call.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.Print), Name = RightEnum.Print.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.Download), Name = RightEnum.Download.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new Right {RightId = Convert.ToInt32(RightEnum.Upload), Name = RightEnum.Upload.ToString(), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now}

                    };
        appRightList.ForEach(item => context.Right.Add(item));
        context.SaveChanges();

        // Create Default RightRolePermission.
        var appRightPermissionList = new List<RightRolePermission>
                    {
                        #region SuperAdmin
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Add), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Edit), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Details), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Delete), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.DeleteBulk), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Archive), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ArchiveBulk), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Remove), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.RemoveBulk), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Assign), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Approve), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.SendEmail), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.SendEmailBulk), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.SendSMS), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.SendSMSBulk), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ImportExcel), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ImportCsv), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ExportExcel), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ExportCsv), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ImportVCard), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ExportVCard), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ExportVCardBulk), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Call), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Print), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Download), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Upload), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
	                    #endregion
                        
                        #region Admin
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Add), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Edit), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Details), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Archive), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ArchiveBulk), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Remove), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.RemoveBulk), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Assign), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Approve), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.SendEmail), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.SendEmailBulk), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.SendSMS), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.SendSMSBulk), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ImportExcel), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ImportCsv), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ExportExcel), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ExportCsv), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ImportVCard), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ExportVCard), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.ExportVCardBulk), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Call), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Print), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Download), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new RightRolePermission {RightId = Convert.ToInt32(RightEnum.Upload), RoleId = Convert.ToInt32(RoleEnum.Admin), CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        #endregion

                    };
        appRightPermissionList.ForEach(item => context.RightRolePermission.Add(item));
        context.SaveChanges();

        // Create Default DefaultSetting.
        var appDefaultSettingList = new List<DefaultSetting>
                    {
                        new DefaultSetting {DefaultSettingId = Convert.ToInt32(DefaultSettingEnum.PageSize), Name = "Page Size", Key = DefaultSettingEnum.PageSize.ToString(), Value = "20", Description = "Grid, List View Default Page Size", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new DefaultSetting {DefaultSettingId = Convert.ToInt32(DefaultSettingEnum.Version), Name = "Application Version", Key = DefaultSettingEnum.Version.ToString(), Value = "1.0", Description = "Application Version", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new DefaultSetting {DefaultSettingId = Convert.ToInt32(DefaultSettingEnum.CacheTimeout), Name = "Cache Timeout", Key = DefaultSettingEnum.CacheTimeout.ToString(), Value = "5", Description = "Cache Timeout", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new DefaultSetting {DefaultSettingId = Convert.ToInt32(DefaultSettingEnum.SessionTimeout), Name = "Session Timeout", Key = DefaultSettingEnum.SessionTimeout.ToString(), Value = "5", Description = "Session Timeout", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new DefaultSetting {DefaultSettingId = Convert.ToInt32(DefaultSettingEnum.SendEmailPerMinute), Name = "Send Email Per Minute", Key = DefaultSettingEnum.SendEmailPerMinute.ToString(), Value = "30", Description = "Send Email Per Minute", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new DefaultSetting {DefaultSettingId = Convert.ToInt32(DefaultSettingEnum.SendSMSPerMinute), Name = "Send SMS Per Minute", Key = DefaultSettingEnum.SendSMSPerMinute.ToString(), Value = "30", Description = "Send SMS Per Minute", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now}
                    };
        appDefaultSettingList.ForEach(item => context.DefaultSetting.Add(item));
        context.SaveChanges();

        // Create Default ApplicationInfo.
        var appInformationList = new List<ApplicationInfo>
                    {
                        new ApplicationInfo {ApplicationInfoId = Convert.ToInt32(ApplicationInformationEnum.HeaderTitle), Name = "Application Title", Key = ApplicationInformationEnum.HeaderTitle.ToString(), Value = "title", Description = "Application Title", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new ApplicationInfo {ApplicationInfoId = Convert.ToInt32(ApplicationInformationEnum.HeaderText), Name = "Application Header Text", Key = ApplicationInformationEnum.HeaderText.ToString(), Value = "header", Description = "Application Header Text", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new ApplicationInfo {ApplicationInfoId = Convert.ToInt32(ApplicationInformationEnum.HeaderUrl), Name = "Application Header Url", Key = ApplicationInformationEnum.HeaderUrl.ToString(), Value = "#", Description = "Application Header Url", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new ApplicationInfo {ApplicationInfoId = Convert.ToInt32(ApplicationInformationEnum.MetaAuthor), Name = "Head Meta Author", Key = ApplicationInformationEnum.MetaAuthor.ToString(), Value = "meta author", Description = "Head Meta Author", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new ApplicationInfo {ApplicationInfoId = Convert.ToInt32(ApplicationInformationEnum.MetaKeywords), Name = "Head Meta Keywords", Key = ApplicationInformationEnum.MetaKeywords.ToString(), Value = "meta keywords", Description = "Head Meta Keywords", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new ApplicationInfo {ApplicationInfoId = Convert.ToInt32(ApplicationInformationEnum.MetaDescription), Name = "Head Meta Description", Key = ApplicationInformationEnum.MetaDescription.ToString(), Value = "meta description", Description = "Head Meta Description", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new ApplicationInfo {ApplicationInfoId = Convert.ToInt32(ApplicationInformationEnum.FooterText), Name = "Application Footer Text", Key = ApplicationInformationEnum.FooterText.ToString(), Value = "footer", Description = "Application Footer Text", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now},
                        new ApplicationInfo {ApplicationInfoId = Convert.ToInt32(ApplicationInformationEnum.FooterUrl), Name = "Application Footer Url", Key = ApplicationInformationEnum.FooterUrl.ToString(), Value = "#", Description = "Application Footer Url", CreatedBy = Convert.ToInt32(UserEnum.SuperAdmin), CreatedDate = DateTime.Now, UpdatedBy = Convert.ToInt32(UserEnum.SuperAdmin), UpdatedDate = DateTime.Now, IsDelete = false, DeletedBy = Convert.ToInt32(UserEnum.SuperAdmin), DeletedDate = DateTime.Now}
                    };
        appInformationList.ForEach(item => context.ApplicationInfo.Add(item));
        context.SaveChanges();

        #endregion

        #region Employee

        #endregion

        #region Lidrary

        #endregion

        #region Settings

        #endregion

        #region Student

        #endregion

        }
    }

    #endregion
}
