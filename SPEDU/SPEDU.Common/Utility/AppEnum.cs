namespace SPEDU.Common.Utility
{
    public enum RoleEnum : int
    {
        SuperAdmin = 1,
        Admin = 2,
        HeadTeacher = 3,
        AssistantHeadTeacher = 4,
        Teacher = 5,
        Employee = 6,
        Student = 7,
        Parent = 8,
        Guardian = 9
    }

    public enum UserEnum : int
    {
        SuperAdmin = 1,
        Admin = 2,
        HeadTeacher = 3,
        AssistantHeadTeacher = 4,
        Teacher = 5,
        Employee = 6,
        Student = 7,
        Parent = 8,
        Guardian = 9
    }

    public enum RightEnum : int
    {
        Add = 1,
        Edit = 2,
        Details = 3,
        Delete = 4,
        DeleteBulk = 5,
        Archive = 6,
        ArchiveBulk = 7,
        Remove = 8,
        RemoveBulk = 9,
        Assign = 10,
        Approve = 11,
        SendEmail = 12,
        SendEmailBulk = 13,
        SendSMS = 14,
        SendSMSBulk = 15,
        ImportExcel = 16,
        ImportCsv = 17,
        ExportExcel = 18,
        ExportCsv = 19,
        ImportVCard = 20,
        ExportVCard = 21,
        ExportVCardBulk = 22,
        Call = 23,
        Print = 24,
        Download = 25,
        Upload = 26
    }

    public enum DefaultSettingEnum : int
    {
        PageSize = 1,
        Version = 2,
        CacheTimeout = 3,
        SessionTimeout = 4,
        SendEmailPerMinute = 5,
        SendSMSPerMinute = 6
    }

    public enum ApplicationInformationEnum : int
    {
        HeaderTitle = 1,
        HeaderText = 2,
        HeaderUrl = 3,
        MetaAuthor = 4,
        MetaKeywords = 5,
        MetaDescription = 6,
        FooterText = 7,
        FooterUrl = 8
    }
}
