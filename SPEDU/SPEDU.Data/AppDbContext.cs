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

        public DbSet<AppDefaultSetting> AppDefaultSetting { get; set; }
        public DbSet<AppEmailTemplate> AppEmailTemplate { get; set; }
        public DbSet<AppEmailTemplateCategory> AppEmailTemplateCategory { get; set; }
        public DbSet<AppInformation> AppInformation { get; set; }
        public DbSet<AppMenu> AppMenu { get; set; }
        public DbSet<AppMenuPermission> AppMenuPermission { get; set; }
        public DbSet<AppRight> AppRight { get; set; }
        public DbSet<AppRightPermission> AppRightPermission { get; set; }
        public DbSet<AppSMSTemplate> AppSMSTemplate { get; set; }
        public DbSet<AppSMSTemplateCategory> AppSMSTemplateCategory { get; set; }
        public DbSet<AppUserActivity> AppUserActivity { get; set; }
        public DbSet<AppUserMetadata> AppUserMetadata { get; set; }
        public DbSet<AppWidget> AppWidget { get; set; }
        public DbSet<AppWidgetCategory> AppWidgetCategory { get; set; }
        public DbSet<AppWidgetPermission> AppWidgetPermission { get; set; }
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

        // Create Default Role.
        var roleList = new List<Role>
                    {
                        new Role {RoleId = Convert.ToInt32(RoleEnum.SuperAdmin), RoleName = RoleEnum.SuperAdmin.ToString()},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.Admin), RoleName = RoleEnum.Admin.ToString()},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.HeadTeacher), RoleName = RoleEnum.HeadTeacher.ToString()},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.AssistantHeadTeacher), RoleName = RoleEnum.AssistantHeadTeacher.ToString()},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.Teacher), RoleName = RoleEnum.Teacher.ToString()},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.Employee), RoleName = RoleEnum.Employee.ToString()},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.Student), RoleName = RoleEnum.Student.ToString()},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.Parent), RoleName = RoleEnum.Parent.ToString()},
                        new Role {RoleId = Convert.ToInt32(RoleEnum.Guardian), RoleName = RoleEnum.Guardian.ToString()}

                    };
        roleList.ForEach(item => context.Role.Add(item));
        context.SaveChanges();

        // Create Default User.
        var userList = new List<User>
                    {
                        new User {UserId = Convert.ToInt32(UserEnum.SuperAdmin), UserName = UserEnum.SuperAdmin.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now},
                        new User {UserId = Convert.ToInt32(UserEnum.Admin), UserName = UserEnum.Admin.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now},
                        new User {UserId = Convert.ToInt32(UserEnum.HeadTeacher), UserName = UserEnum.HeadTeacher.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now},
                        new User {UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher), UserName = UserEnum.AssistantHeadTeacher.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now},
                        new User {UserId = Convert.ToInt32(UserEnum.Teacher), UserName = UserEnum.Teacher.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now},
                        new User {UserId = Convert.ToInt32(UserEnum.Employee), UserName = UserEnum.Employee.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now},
                        new User {UserId = Convert.ToInt32(UserEnum.Student), UserName = UserEnum.Student.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now},
                        new User {UserId = Convert.ToInt32(UserEnum.Parent), UserName = UserEnum.Parent.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now},
                        new User {UserId = Convert.ToInt32(UserEnum.Guardian), UserName = UserEnum.Guardian.ToString(), Password = Password.GetBytes("admin123456"),  Email = "schemaplug@gmail.com", LastPasswordChangeDate = DateTime.Now}

                    };
        userList.ForEach(item => context.User.Add(item));
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

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Admin), UserId = Convert.ToInt32(UserEnum.Admin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.HeadTeacher), UserId = Convert.ToInt32(UserEnum.Admin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.AssistantHeadTeacher), UserId = Convert.ToInt32(UserEnum.Admin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Teacher), UserId = Convert.ToInt32(UserEnum.Admin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Employee), UserId = Convert.ToInt32(UserEnum.Admin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.Admin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Parent), UserId = Convert.ToInt32(UserEnum.Admin)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Guardian), UserId = Convert.ToInt32(UserEnum.Admin)},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.HeadTeacher), UserId = Convert.ToInt32(UserEnum.HeadTeacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.AssistantHeadTeacher), UserId = Convert.ToInt32(UserEnum.HeadTeacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Teacher), UserId = Convert.ToInt32(UserEnum.HeadTeacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Employee), UserId = Convert.ToInt32(UserEnum.HeadTeacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.HeadTeacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Parent), UserId = Convert.ToInt32(UserEnum.HeadTeacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Guardian), UserId = Convert.ToInt32(UserEnum.HeadTeacher)},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.AssistantHeadTeacher), UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Teacher), UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Employee), UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Parent), UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Guardian), UserId = Convert.ToInt32(UserEnum.AssistantHeadTeacher)},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Teacher), UserId = Convert.ToInt32(UserEnum.Teacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.Teacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Parent), UserId = Convert.ToInt32(UserEnum.Teacher)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Guardian), UserId = Convert.ToInt32(UserEnum.Teacher)},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Employee), UserId = Convert.ToInt32(UserEnum.Employee)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.Employee)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Parent), UserId = Convert.ToInt32(UserEnum.Employee)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Guardian), UserId = Convert.ToInt32(UserEnum.Employee)},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.Student)},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.Parent)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Parent), UserId = Convert.ToInt32(UserEnum.Parent)},

                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Student), UserId = Convert.ToInt32(UserEnum.Guardian)},
                        new UserRole {RoleId = Convert.ToInt32(RoleEnum.Guardian), UserId = Convert.ToInt32(UserEnum.Guardian)}

                    };
        userRoleList.ForEach(item => context.UserRole.Add(item));
        context.SaveChanges();

        // Create Default AppRight.
        var appRightList = new List<AppRight>
                    {
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.Add), Name = RightEnum.Add.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.Edit), Name = RightEnum.Edit.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.Details), Name = RightEnum.Details.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.Delete), Name = RightEnum.Delete.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.DeleteBulk), Name = RightEnum.DeleteBulk.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.Archive), Name = RightEnum.Archive.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.ArchiveBulk), Name = RightEnum.ArchiveBulk.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.Remove), Name = RightEnum.Remove.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.RemoveBulk), Name = RightEnum.RemoveBulk.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.Assign), Name = RightEnum.Assign.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.Approve), Name = RightEnum.Approve.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.SendEmail), Name = RightEnum.SendEmail.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.SendEmailBulk), Name = RightEnum.SendEmailBulk.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.SendSMS), Name = RightEnum.SendSMS.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.SendSMSBulk), Name = RightEnum.SendSMSBulk.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.ImportExcel), Name = RightEnum.ImportExcel.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.ImportCsv), Name = RightEnum.ImportCsv.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.ExportExcel), Name = RightEnum.ExportExcel.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.ExportCsv), Name = RightEnum.ExportCsv.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.ImportVCard), Name = RightEnum.ImportVCard.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.ExportVCard), Name = RightEnum.ExportVCard.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.ExportVCardBulk), Name = RightEnum.ExportVCardBulk.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.Call), Name = RightEnum.Call.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.Print), Name = RightEnum.Print.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.Download), Name = RightEnum.Download.ToString()},
                        new AppRight {AppRightId = Convert.ToInt32(RightEnum.Upload), Name = RightEnum.Upload.ToString()}

                    };
        appRightList.ForEach(item => context.AppRight.Add(item));
        context.SaveChanges();

        // Create Default AppRightPermission.
        var appRightPermissionList = new List<AppRightPermission>
                    {
                        #region SuperAdmin
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Add), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Edit), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Details), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Delete), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.DeleteBulk), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Archive), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ArchiveBulk), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Remove), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.RemoveBulk), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Assign), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Approve), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.SendEmail), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.SendEmailBulk), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.SendSMS), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.SendSMSBulk), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ImportExcel), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ImportCsv), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ExportExcel), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ExportCsv), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ImportVCard), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ExportVCard), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ExportVCardBulk), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Call), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Print), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Download), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Upload), RoleId = Convert.ToInt32(RoleEnum.SuperAdmin)},
	                    #endregion
                        
                        #region Admin
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Add), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Edit), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Details), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Archive), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ArchiveBulk), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Remove), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.RemoveBulk), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Assign), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Approve), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.SendEmail), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.SendEmailBulk), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.SendSMS), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.SendSMSBulk), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ImportExcel), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ImportCsv), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ExportExcel), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ExportCsv), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ImportVCard), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ExportVCard), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.ExportVCardBulk), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Call), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Print), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Download), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        new AppRightPermission {AppRightId = Convert.ToInt32(RightEnum.Upload), RoleId = Convert.ToInt32(RoleEnum.Admin)},
                        #endregion

                    };
        appRightPermissionList.ForEach(item => context.AppRightPermission.Add(item));
        context.SaveChanges();

        // Create Default AppDefaultSetting.
        var appDefaultSettingList = new List<AppDefaultSetting>
                    {
                        new AppDefaultSetting {AppDefaultSettingId = Convert.ToInt32(DefaultSettingEnum.PageSize), Name = "Page Size", Key = DefaultSettingEnum.PageSize.ToString(), Value = "20", Description = "Grid, List View Default Page Size"},
                        new AppDefaultSetting {AppDefaultSettingId = Convert.ToInt32(DefaultSettingEnum.Version), Name = "Application Version", Key = DefaultSettingEnum.Version.ToString(), Value = "1.0", Description = "Application Version"},
                        new AppDefaultSetting {AppDefaultSettingId = Convert.ToInt32(DefaultSettingEnum.CacheTimeout), Name = "Cache Timeout", Key = DefaultSettingEnum.CacheTimeout.ToString(), Value = "5", Description = "Cache Timeout"},
                        new AppDefaultSetting {AppDefaultSettingId = Convert.ToInt32(DefaultSettingEnum.SessionTimeout), Name = "Session Timeout", Key = DefaultSettingEnum.SessionTimeout.ToString(), Value = "5", Description = "Session Timeout"},
                        new AppDefaultSetting {AppDefaultSettingId = Convert.ToInt32(DefaultSettingEnum.SendEmailPerMinute), Name = "Send Email Per Minute", Key = DefaultSettingEnum.SendEmailPerMinute.ToString(), Value = "30", Description = "Send Email Per Minute"},
                        new AppDefaultSetting {AppDefaultSettingId = Convert.ToInt32(DefaultSettingEnum.SendSMSPerMinute), Name = "Send SMS Per Minute", Key = DefaultSettingEnum.SendSMSPerMinute.ToString(), Value = "30", Description = "Send SMS Per Minute"}
                    };
        appDefaultSettingList.ForEach(item => context.AppDefaultSetting.Add(item));
        context.SaveChanges();

        // Create Default AppInformation.
        var appInformationList = new List<AppInformation>
                    {
                        new AppInformation {AppInformationId = Convert.ToInt32(ApplicationInformationEnum.HeaderTitle), Name = "Application Title", Key = ApplicationInformationEnum.HeaderTitle.ToString(), Value = "", Description = "Application Title"},
                        new AppInformation {AppInformationId = Convert.ToInt32(ApplicationInformationEnum.HeaderText), Name = "Application Header Text", Key = ApplicationInformationEnum.HeaderText.ToString(), Value = "", Description = "Application Header Text"},
                        new AppInformation {AppInformationId = Convert.ToInt32(ApplicationInformationEnum.HeaderUrl), Name = "Application Header Url", Key = ApplicationInformationEnum.HeaderUrl.ToString(), Value = "", Description = "Application Header Url"},
                        new AppInformation {AppInformationId = Convert.ToInt32(ApplicationInformationEnum.MetaAuthor), Name = "Head Meta Author", Key = ApplicationInformationEnum.MetaAuthor.ToString(), Value = "", Description = "Head Meta Author"},
                        new AppInformation {AppInformationId = Convert.ToInt32(ApplicationInformationEnum.MetaKeywords), Name = "Head Meta Keywords", Key = ApplicationInformationEnum.MetaKeywords.ToString(), Value = "", Description = "Head Meta Keywords"},
                        new AppInformation {AppInformationId = Convert.ToInt32(ApplicationInformationEnum.MetaDescription), Name = "Head Meta Description", Key = ApplicationInformationEnum.MetaDescription.ToString(), Value = "", Description = "Head Meta Description"},
                        new AppInformation {AppInformationId = Convert.ToInt32(ApplicationInformationEnum.FooterText), Name = "Application Footer Text", Key = ApplicationInformationEnum.FooterText.ToString(), Value = "", Description = "Application Footer Text"},
                        new AppInformation {AppInformationId = Convert.ToInt32(ApplicationInformationEnum.FooterUrl), Name = "Application Footer Url", Key = ApplicationInformationEnum.FooterUrl.ToString(), Value = "", Description = "Application Footer Url"}
                    };
        appInformationList.ForEach(item => context.AppInformation.Add(item));
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
