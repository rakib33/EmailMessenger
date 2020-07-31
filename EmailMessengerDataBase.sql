USE [EmDb]
GO
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_dbo.UserRole_dbo.User_UserId]
GO
ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_dbo.UserRole_dbo.Role_RoleId]
GO
ALTER TABLE [dbo].[UserLogin] DROP CONSTRAINT [FK_dbo.UserLogin_dbo.User_UserId]
GO
ALTER TABLE [dbo].[UserClaim] DROP CONSTRAINT [FK_dbo.UserClaim_dbo.User_UserId]
GO
ALTER TABLE [dbo].[ProjectGroups] DROP CONSTRAINT [FK_dbo.ProjectGroups_dbo.Project_Project_Id]
GO
ALTER TABLE [dbo].[ProjectGroups] DROP CONSTRAINT [FK_dbo.ProjectGroups_dbo.Group_Group_Id]
GO
ALTER TABLE [dbo].[Project] DROP CONSTRAINT [FK_dbo.Project_dbo.MailTemplate_MailTemplateId]
GO
ALTER TABLE [dbo].[MailTracker] DROP CONSTRAINT [FK_dbo.MailTracker_dbo.Project_ProjectId]
GO
ALTER TABLE [dbo].[MailTemplateText] DROP CONSTRAINT [FK_dbo.MailTemplateText_dbo.MailTemplate_MailTemplateId]
GO
ALTER TABLE [dbo].[Group] DROP CONSTRAINT [FK_dbo.Group_dbo.ServerConnect_ServerConnectId]
GO
ALTER TABLE [dbo].[Attachment] DROP CONSTRAINT [FK_dbo.Attachment_dbo.Project_ProjectId]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[UserRole]
GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[UserLogin]
GO
/****** Object:  Table [dbo].[UserClaim]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[UserClaim]
GO
/****** Object:  Table [dbo].[User]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[User]
GO
/****** Object:  Table [dbo].[ServerConnect]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[ServerConnect]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[Role]
GO
/****** Object:  Table [dbo].[ProjectGroups]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[ProjectGroups]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[Project]
GO
/****** Object:  Table [dbo].[MailTracker]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[MailTracker]
GO
/****** Object:  Table [dbo].[MailTemplateText]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[MailTemplateText]
GO
/****** Object:  Table [dbo].[MailTemplate]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[MailTemplate]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[Group]
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[Attachment]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 7/31/2020 8:15:34 PM ******/
DROP TABLE [dbo].[__MigrationHistory]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 7/31/2020 8:15:34 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Attachment]    Script Date: 7/31/2020 8:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attachment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AttachFileName] [nvarchar](max) NOT NULL,
	[AttachFilePath] [nvarchar](max) NOT NULL,
	[ProjectId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Attachment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Group]    Script Date: 7/31/2020 8:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Size] [int] NOT NULL,
	[ServerConnectId] [int] NOT NULL,
	[ConditionQuery] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Group] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MailTemplate]    Script Date: 7/31/2020 8:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailTemplate](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TemplateName] [nvarchar](150) NOT NULL,
	[TemplateFilePath] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.MailTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MailTemplateText]    Script Date: 7/31/2020 8:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailTemplateText](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Order] [int] NOT NULL,
	[TemplateDataOrder] [nvarchar](max) NOT NULL,
	[TemplateData] [nvarchar](max) NOT NULL,
	[MailTemplateId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.MailTemplateText] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MailTracker]    Script Date: 7/31/2020 8:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailTracker](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MailTo] [nvarchar](max) NOT NULL,
	[MailSendTime] [datetime] NOT NULL,
	[ProjectId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.MailTracker] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Project]    Script Date: 7/31/2020 8:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [nvarchar](max) NOT NULL,
	[Status] [bit] NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[TimeInterval] [int] NULL,
	[IntervalOption] [nvarchar](max) NULL,
	[ExpiredTime] [datetime] NOT NULL,
	[ProjectTemplatePath] [nvarchar](max) NULL,
	[MailTemplateId] [int] NULL,
 CONSTRAINT [PK_dbo.Project] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProjectGroups]    Script Date: 7/31/2020 8:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectGroups](
	[Project_Id] [int] NOT NULL,
	[Group_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ProjectGroups] PRIMARY KEY CLUSTERED 
(
	[Project_Id] ASC,
	[Group_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 7/31/2020 8:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServerConnect]    Script Date: 7/31/2020 8:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServerConnect](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServerType] [nvarchar](max) NOT NULL,
	[ConnectionName] [nvarchar](max) NOT NULL,
	[ServerHostNameIP] [nvarchar](max) NOT NULL,
	[ServerUserName] [nvarchar](max) NOT NULL,
	[ServerPassword] [nvarchar](max) NOT NULL,
	[DatabaseName] [nvarchar](max) NOT NULL,
	[TableName] [nvarchar](max) NOT NULL,
	[EmailFieldName] [nvarchar](max) NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.ServerConnect] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 7/31/2020 8:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserClaim]    Script Date: 7/31/2020 8:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.UserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 7/31/2020 8:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.UserLogin] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 7/31/2020 8:15:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.UserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[__MigrationHistory] ([MigrationId], [ContextKey], [Model], [ProductVersion]) VALUES (N'202007310528250_InitialCreate', N'EmailMessenger.Models.SqlDbContext', 0x1F8B0800000000000400ED1D6B6FDCB8F17B81FE07613FDD15BE5D3F9AC3D5B0EFE0B3E33BA371EC669D43BF19F48AB6D5E8B191B439A7457F593FF427F52F94D493EF87C495B4411020F08AE470381C0E87C3E1CCFFFEF3DF939F5EA2D0FB04D32C48E2D3D9C17C7FE6C17895F841FC743ADBE48FDFFD30FBE9C73FFEE1E4B51FBD78BFD5F58E703DD432CE4E67CF79BE3E5E2CB2D5338C40368F82559A64C9633E5F25D102F8C9E2707FFF2F8B8383054420660896E79DBCDBC47910C1E207FA799EC42BB8CE3720BC4E7C1866D57754B22CA07A6F4104B33558C1D3D9EB0804E135CC32183FC1745E36987967610010324B183ECE3C10C7490E7284EAF1FB0C2EF334899F966BF40184779FD710D57B046106AB211CB7D54D47B37F8847B3681BD6A0569B2C4F224B8007471579166CF34E449E35E443047C8D089D7FC6A32E8888E8777DBF84299A7144F418AEF299C7767A7C1EA6B88184D67316C29E47D7DB6BF804B113FEB7E79D6FC27C93C2D3186EF214847BDEEDE6210C567F859FEF920F303E8D376138F37E06192C3BC6D33867D05C288775E5C3E2D3BB2484AA215D37943BCBD66F613EAF1BCE4B90972902F77B927E989310F73CE376EDE80F4D477F74F0F078F4C3ABEF817FF4FD9FE1D12B7202D158513DEA03FA749B266B9822DCE063337E9A42A8D2826DD83423DA54F4CE53B4E067DE35787983E6307F46A2E0F087997719BC40BFFE52AD99F77180E4036A94A71BF4F32D9A3AF010C2A67CA1EC13FFAFE8F5F0D5F74E7A7D0B3E054FC5D433FD2379902271F10E864569F61CAC2B7623E7FBBEAA76992611FE4DF357597ABF4C36E90A0F269156B903E913647977D132AF114B6350EED9BA863A7DD6C698F2EC2DAC8A07D46525D45D0CBD1A6A7CB7DBAF31C79DADD768F20AD6C214B1DF1A1800AE76862F581E16047220100D7A413BE9639046B019E5CF09623F105BE37C0BB20CC903FF57903D2B50477F3A407D09579B14B1E93207D17AEBBDDD3E27317CBB891E30F70FD797B3A9B9FB3DB904AB3C495FC7B8556F786F92D5876493BF8EFD0B90C3F7F9AA06887FDE0591390027E89CAD5648965C226686FE79820E1135C0AB383F3AB4068765D4D80AC979088248AC9130D2F4BEAEDA6A25E21A9C6622A926D24E54A8BE499E82D80CD5BAAA1CD5B28616D5AA9A2DAA189819A6554D39A245052D9E652D67FA5E3143EE15BE02ECF435BE7E9BB74C1610645C2209097F81314C9118F36F419EC3346E67C0446E8CA12C14D3873BDDFADE54F4F41B0837AEBBEAB41A0A21E07E351460A7BF1A0A34D1E74F818FB5128363505D198137AA2F3E61E9D71C83D9D0CB811AE6D09D0F23038C97CB2F69B259AB0F6C659B7951139FCCCADF3B7326DBBE582F28A3D1074D659E25375DC06C9506EBD2E63B74E7CBE09FB09F224D996AF553A6D97B92D80F3025FEB681A96A6577DCEAA44A23FAF90F84BF586FAC0AEF0B26A11446BAA4D1016B4D9129AE154953A4182BB800B302306BD46FD1131473DAACA84E2F5DB61AB49140AAEA7E154982BE2ADA8C239496680C9BACEF991D4149F3D254C09A0E6C8D1BA8112239E252103253A06E5837BA71256035D6B697759042DFC9982B06B883D11A2D7C881868FB26B76B108475871CB39BCAAD5A4EF611A5AC9892485A5394C8712911A32BF2E891E55224A94ABD84298D8E8144251B7C15AB224952114723570F5EED6F43AED6BD5F06A1AB053DAABAD965FDDDC11789A2C5D692AE4665454E0D53D7B6D5CA8C54452B3122531BB5687612239802D6A20437FA2A4E047DDDA484FDA3DBA1A7A631521400056E38A140A2307CEF4AA5A3F3ED8A76CB77246D5825C04C36755AC616DE5BD51AE6BCB5BE2E60E121DB9165DDDEDC81E7058D62A4535E31F65F93AC38655EDD8E8581C165EC56FBAFBD1A46D0C690C07D00994E1BDD96D8C775C7E9BA704BB90C60E83BEABF9BD7539E83D57304633369DA56FF2A4A057D95E4C1479B7178AAED7F9CA355A5B7BB53611A53AAC899A1E1C5FBA61AE1C9C095F26E0C7C95FEA68A14AC3EE8BC07C9E34559FFEB6A12F455D02719471B5FC2D8A90D739825417094684D088AC5CA3B53C766559C6559B20A0AC404AF132ADF727AA8AF63DFD33B9AB76F23E8E70ED7687504D81F097D399D1DCC58BEBF892F600873E89DADCA4729E7205B019FA7381A906F815863B3E0116B9DD669E4FEC4F58916234C7123803D5433B4BC8338E7576E10AF823508B554625A1AAE7A3CF6A60FB6E402AED152401D6A2961D2B9D8471D23D0F4C34C8A8E42270B82E3D48C28712994CDB9CEBFB09D77CE757C109ED438364AF8B272AEDB0A63AA29360073AA49628280F4BDC5180C5A39929A3200EB553A350665DC59250C5AF9BB0DC2A034C54660509A243BC7A0A5FFB0E9FC33CEC453634FDA8B79F86D5D49AE117893A2C7E45993B957974DBAEC92BD9DED466FD64DB214A880852A0F4525488BC1AA0DF732340DADF82DD6FCA599394D0C2F28C59D6D5502745A9C46C31960911ACDA1091EEC95D3A88BD6887DD50E284E16701F1EDD9FCF0FDC6C042A6C066031159D7786B3447EA1B2D9573A89DAC9702D5C015331779A53937C8A510CC08C8AB931E99DF3CD1E476DE62DD552A55461B6261466E2D6C69C1F15F66EBDF81C9F11E5E80FA11C4BE7C5A473C2083C0A038AECC24AC54D662466B6C2FAB2C3522D94989777810915F80FA5FD89E7667A6C58DE0FA036396A01D35A227F0C2F1EF047B1131E3A4356176559757BC3721206BA8439FDD6B9BD8B10DE1170EC4803610FF82A80AD114003B48AA2C301E28C1D16C8D58FE095D855165E0BB0F5837525D8CA2EA7015BBBB573A02AED49D3BC752DE5003422410382543F4570681DDE0258E5B5AB04589E92354029A544045111FC4B00AEDD9A84EC46A80A26832DA58B749CB5C46740112280673132A81451511E7A8A954D461784CD681AA1C0C938A3FB3C028E402EB0BB0A3D7003A2C8E25AF08431B9AE32B9101210A89A0C058134974B1222D583714EA55A42E9A924BA33B1B935E94525E6864342A57A30CEA954F1A89E4802BBBD85E5BE1789682BFB96171BFBD88AA78CCA6C6C62382606D0EE5D0A72484CC504981AD7DE83D77877F3B4B0B02A77B02B134314ECAA0A9299599225E05D72918E7E7A6BA6B93DB32B5F0D4D1BE1FB6B9E345A739CB1414EBB528C4D7004245623EB2F9905BE8902B1ACB10B995A86889150CAA04A2CCB6D4106ACD7551AB1AE691219A4B25418DB2A58A66F145B9DA8915827FA91A576976B8EC34DD9C9A28CF35C7D38594802429F5C83F53A889F8800D1D5176F5946873EFF6E691F33392A612C56D4E6C71EDE9B9EF224054F9029C5EF387C7819A4595EBBF3CFBC733FE2AA51877FC961A4EE4A7856E2A7AE3EA4D4CDF0DF8483BD3680B3C0705281BA4423C50BA4183494490BBEBD8723778310A402D7D9F324DC44B1DC1654B46E62807845102A51D86A12CE4580DFB346410C109E2290270B66449CED8723A30D9DAF32FCF7CDE337628253987F3B41720B5B938FA24828E4777368EC332712225B668B23F97889C7942CB585DC3E4AE2E1B665B650DBA7463CD4B6CC1C2AFD80885A1854893944E25110098EF86C0E8B7DE5430264CB065FB9060B7682EB95138F63CAC6BABCDDB88CB635D901DC76AE2833B7FD54A99B6F47B2F2EB605BDCDF716E784384BB796A6E0FBACF951C848CE2B55F1E497399AF9E1C4AEDBA4F4291B9F38F367732FBB1ED7CB19734F6D3A585B09DD5558554E7B6196B1844546E0E1851660E950E9C4EC2A44B6CF4092A3A3AAD4E504516589231D02924C9824EF0241415D7B0D056B8A8E794D2C2959A4316C43F27410B8A3BC016E0CC96994315844827010B8ACD618BB561951E3C81FD4B7A67D467032B2F98FBED601218DB118A6E3640220035757E6B3F5BC2AA424C73C0AAEF936428E9F55A1F862A5D0BFA319404865CFE50D19B69F1A30C392D87794B8664A644BC2A24B51C9E1DDB8EC41CD28B335B86281D44EC9940D26E3B928488924C02213E5B182EC83874D4A1952CB050878A48C69416547CB135D010DEB2BC8546E14A2B87CB063566CC5F54D96418BB31F2F7676DC9458601734B5B6E87BD6FC988BB8C046B0B2C18AA0AA24BF151F5CD0A4A1D449701547FB6D096A930BA94A64C95984364E3EB52D467CA2CCE6064185DEA00461658CF2C1D4A5730C3740573F8ECA30F12B4EE41C8684B9CBE0CEFBFCE5517FF068B5DDD7C3B2B9E0E064BAD07AAC41E621B854804B52DDDFE863901EE2ABD5CDC72D81D77896ACD656210DBE1B42AB02709A0FA64CF5B44A050117311C5DD60CBC17EF1329174207160476DBD8F3B9850158DB7C3A36CFC36CA78C4947581CACB43B6CC7A476707AC78E331AE24ACFD6F1C09C1CA11BDA3FC93B5DE0E5BD581CC380190D84269C392B1B0DA92DD6621DA834A78246BDC790DCF5D4D7DC9E94A6841C05E616272312EC03C858C38A806267E8C45746E8D97F4399A115EA5C7A41D56ACDF1B3FB19CFB1B5BA561ABEA4BF3BB717FAB5CCF289FB86250D8C3AD184C56B9C1B1BE68659599579BE1D0D1F17396C3688E2BCC971FC3F330C09B4C53E11AC4C123CCF232BCE2EC70FFE070E69D8501C84A2FC5CACBEE987DBD66E476777084DDEEA01F2DD8E6F6CE7B184A96F954F4475DD06C7AE60608141960D26A4341760A5B4C06AD8E3F8174F50CD26F22F0F2ADA340D4FD60CA824BBB80CA068C7601930D02DD0FA628B0733F885CB0E67EE0C40198A5304D920731EE4634443E41A29BEC8772279E215677C7D1F12045932078207C15FBF0E574F6AFA2D571E1B75DAC2EFC79CFBBCADEC7C1C70D2AB843D3E5FD9B4F67ED96E662671C33BA4BC365F155C57E36FA69A2F3749A53F5EAEFF765D33DAF38471F7BFB0C2DBBCC703D8A0ED8944D7B60631E105DE5B7B3530BAAF2FB5140651684517A37C6FFA784FE10E4F6819129AF1F07C297F1F9710091F2F9710BCF090979E79EEEB004DE3C25301FFDCC99C8D7268365BD78BAA32670DB2134CB0EA98BBB6D3575CB11B71A81D7CCCE6AF0D3DA9BB8ECF2BD163A9F41DE025C8F2CF11D3863C712AC3BDB1D6FF9FCE9CE608FC9DA9649D3BF0C59C2252FEF7974E53344F63C5F1349C6BB6C5A9CEF0D01CA80A998E63477394950BE0DF126F47DD95916A53C689CF01495A0BA8B5EC525A716A97A9D9353337C6EB26D8A5355F7DA8905E9A83B8F52E822E3004771F647D3C54DB796ADED1EEB50119C77A8C5A825433BEE039166EE7A318B52289B6FF364EB06EF43CD89A248C7EC28D7F270BBA323B6E3FD5476761FA0F2DB76D105A4C9727B1AD7050970FB411C42ACF5B2F449DC4B7696B5C4D907FB4DA238A3603F98847F811D4F340DB7C10E52BF909DE5073A7F5EFFC5CCE6C4EBAB484D8C01E4DE20661CA0F4E8E0ABCB1C2DF46C4376D4897EF73DCF802DEA76DDD7ED1CCC9EB3348353C92CD8C61516E0336042C12173082AA22198DE817AE324BEB24B1D388164578200D1123E1B3041E0D0BC267BB86C71373FFD34801363B62A6CB884D9064CF63734B3C91E354F9CD9AC52FA4D8CD7C6DA3F47E634E32D744209FA84DED37C3CDBC19382701854790B78340CD341F5C9003660B62FD9EB7BCB63CE088C247277672300EBF3787D814C643EA543B390EA8DC150D9907A65CF1C2771A53C247ACFAC9DBD33628E91F972777211D6026B7B6C26CC3DD96933DD51E6B0D93027C11336F92999F8A6C424322583C81EC30DD51123E982D13AE724F35D4C1B10676AB925C7D7A9C96454E4C9D1229DE5D4F569E58B77A936BD139921C7E71F2A0519BB379A26A39C3A07A99FB78FCD42CC3BDEC62B90CDFFC04E61F5E85A243BF519F0CA17BCA733FF015F679697457649F2E89B1DAE3FBA58D45DA7449246792445BD196799E48C6E5C875C0D597F56A3AB6E0994C3ABEAC8FAEB98A7D22C4DA5AC538B2C96F2249622E056F92D55E92D45C09B420D78FA8CC0F541178B3AEA951CD32837A6AE57A30C9AA4C2C0333C5128EAAD5B464C5D424CE9B8241933259C21E33CBA58C12392F4716324E814A6F71349358D415F00452A1FA79C90934ED4A54E032C25092578256195DD13657BF9379D1085DA1824A181DD13655BE9369D906488A523BED2D166D99444CE119D3408FC257BA0C2DCCE37166FD2BD075EA788D30E5C1C9A8737AE10980B511E65D0A3E4139526AF549244A699880320BAE307C71942FB13C262214D25F9A7F06828482CA51AB6E1329A4662CFAEC28E5782D9A8898ED6BCEBAC9D5D072CD0C6B9887EE643B6C8C8C9871E3B59BCDBC4D8F7B9FC7501B3E0A90571D246936A81D675AEE2C7A436E23018D55558DF6B98031FE4E02CCD8347B0CA51310E8810C44FB3264364F400FDABF86693AF37391A328C1E42EA7536B604A9FA2FD28ED2389F94CFF0321743406806D85DFC26FE7913847E83F7A5C0E35702029B982AE7783C973976927FFADC407A9BC486802AF23596B12602F44DBC049F6017DC904EF5063E81D5E7DBE6B9BA0C887E2268B29F5C04E029055156C168DBA39F8887FDE8E5C7FF033D2AC00CCCD00000, N'6.1.1-30610')
ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Attachment_dbo.Project_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_dbo.Attachment_dbo.Project_ProjectId]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Group_dbo.ServerConnect_ServerConnectId] FOREIGN KEY([ServerConnectId])
REFERENCES [dbo].[ServerConnect] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_dbo.Group_dbo.ServerConnect_ServerConnectId]
GO
ALTER TABLE [dbo].[MailTemplateText]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MailTemplateText_dbo.MailTemplate_MailTemplateId] FOREIGN KEY([MailTemplateId])
REFERENCES [dbo].[MailTemplate] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MailTemplateText] CHECK CONSTRAINT [FK_dbo.MailTemplateText_dbo.MailTemplate_MailTemplateId]
GO
ALTER TABLE [dbo].[MailTracker]  WITH CHECK ADD  CONSTRAINT [FK_dbo.MailTracker_dbo.Project_ProjectId] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[Project] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MailTracker] CHECK CONSTRAINT [FK_dbo.MailTracker_dbo.Project_ProjectId]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Project_dbo.MailTemplate_MailTemplateId] FOREIGN KEY([MailTemplateId])
REFERENCES [dbo].[MailTemplate] ([Id])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_dbo.Project_dbo.MailTemplate_MailTemplateId]
GO
ALTER TABLE [dbo].[ProjectGroups]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProjectGroups_dbo.Group_Group_Id] FOREIGN KEY([Group_Id])
REFERENCES [dbo].[Group] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectGroups] CHECK CONSTRAINT [FK_dbo.ProjectGroups_dbo.Group_Group_Id]
GO
ALTER TABLE [dbo].[ProjectGroups]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProjectGroups_dbo.Project_Project_Id] FOREIGN KEY([Project_Id])
REFERENCES [dbo].[Project] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProjectGroups] CHECK CONSTRAINT [FK_dbo.ProjectGroups_dbo.Project_Project_Id]
GO
ALTER TABLE [dbo].[UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserClaim_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaim] CHECK CONSTRAINT [FK_dbo.UserClaim_dbo.User_UserId]
GO
ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserLogin_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_dbo.UserLogin_dbo.User_UserId]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRole_dbo.Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_dbo.UserRole_dbo.Role_RoleId]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRole_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_dbo.UserRole_dbo.User_UserId]
GO
