IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [AspNetRoles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

CREATE TABLE [Jobs] (
    [Id] uniqueidentifier NOT NULL,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    CONSTRAINT [PK_Jobs] PRIMARY KEY ([Id])
);

CREATE TABLE [Shifts] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [StartTime] time NOT NULL,
    [EndTime] time NOT NULL,
    [StartBreak] time NOT NULL,
    [EndBreak] time NOT NULL,
    CONSTRAINT [PK_Shifts] PRIMARY KEY ([Id])
);

CREATE TABLE [Sites] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [Latitude] nvarchar(max) NULL,
    [Longitude] nvarchar(max) NULL,
    CONSTRAINT [PK_Sites] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUsers] (
    [Id] uniqueidentifier NOT NULL,
    [FName] nvarchar(max) NULL,
    [SName] nvarchar(max) NULL,
    [JobId] uniqueidentifier NOT NULL,
    [ManagerId] uniqueidentifier NOT NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUsers_AspNetUsers_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AspNetUsers_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Jobs] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [JobPerformanceMetrics] (
    [Id] uniqueidentifier NOT NULL,
    [MetricName] nvarchar(max) NULL,
    [PerformanceScore] int NOT NULL,
    [JobId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_JobPerformanceMetrics] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_JobPerformanceMetrics_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Jobs] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [JobQualifications] (
    [Id] uniqueidentifier NOT NULL,
    [Qualification] nvarchar(max) NULL,
    [JobId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_JobQualifications] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_JobQualifications_Jobs_JobId] FOREIGN KEY ([JobId]) REFERENCES [Jobs] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] uniqueidentifier NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserRoles] (
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserTokens] (
    [UserId] uniqueidentifier NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Attendences] (
    [Id] uniqueidentifier NOT NULL,
    [Date] date NOT NULL,
    [CheckIn] datetime2 NULL,
    [CheckOut] datetime2 NULL,
    [EmployeeId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Attendences] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Attendences_AspNetUsers_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [EmployeeSchedules] (
    [Id] uniqueidentifier NOT NULL,
    [EmployeeId] uniqueidentifier NOT NULL,
    [ShiftId] uniqueidentifier NOT NULL,
    [SiteId] uniqueidentifier NOT NULL,
    [Date] date NOT NULL,
    CONSTRAINT [PK_EmployeeSchedules] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_EmployeeSchedules_AspNetUsers_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeeSchedules_Shifts_ShiftId] FOREIGN KEY ([ShiftId]) REFERENCES [Shifts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_EmployeeSchedules_Sites_SiteId] FOREIGN KEY ([SiteId]) REFERENCES [Sites] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Requests] (
    [Id] uniqueidentifier NOT NULL,
    [RequestType] int NOT NULL,
    [Status] int NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [EmployeeId] int NOT NULL,
    [EmployeeId1] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Requests] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Requests_AspNetUsers_EmployeeId1] FOREIGN KEY ([EmployeeId1]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [OvertimeRequests] (
    [RequestId] uniqueidentifier NOT NULL,
    [StartTime] time NOT NULL,
    [EndTime] time NOT NULL,
    CONSTRAINT [PK_OvertimeRequests] PRIMARY KEY ([RequestId]),
    CONSTRAINT [FK_OvertimeRequests_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [RemoteWorkRequests] (
    [RequestId] uniqueidentifier NOT NULL,
    [Date] date NOT NULL,
    CONSTRAINT [PK_RemoteWorkRequests] PRIMARY KEY ([RequestId]),
    CONSTRAINT [FK_RemoteWorkRequests_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [SickLeaveRequests] (
    [RequestId] uniqueidentifier NOT NULL,
    [LeaveHours] decimal(18,2) NOT NULL,
    [EndDate] datetime2 NOT NULL,
    CONSTRAINT [PK_SickLeaveRequests] PRIMARY KEY ([RequestId]),
    CONSTRAINT [FK_SickLeaveRequests_Requests_RequestId] FOREIGN KEY ([RequestId]) REFERENCES [Requests] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

CREATE INDEX [IX_AspNetUsers_JobId] ON [AspNetUsers] ([JobId]);

CREATE INDEX [IX_AspNetUsers_ManagerId] ON [AspNetUsers] ([ManagerId]);

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

CREATE INDEX [IX_Attendences_EmployeeId] ON [Attendences] ([EmployeeId]);

CREATE INDEX [IX_EmployeeSchedules_EmployeeId] ON [EmployeeSchedules] ([EmployeeId]);

CREATE INDEX [IX_EmployeeSchedules_ShiftId] ON [EmployeeSchedules] ([ShiftId]);

CREATE INDEX [IX_EmployeeSchedules_SiteId] ON [EmployeeSchedules] ([SiteId]);

CREATE INDEX [IX_JobPerformanceMetrics_JobId] ON [JobPerformanceMetrics] ([JobId]);

CREATE INDEX [IX_JobQualifications_JobId] ON [JobQualifications] ([JobId]);

CREATE INDEX [IX_Requests_EmployeeId1] ON [Requests] ([EmployeeId1]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260825040903_initail', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [RefreshTokens] (
    [Id] uniqueidentifier NOT NULL,
    [Token] nvarchar(max) NOT NULL,
    [ExpiresAt] datetime2 NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [RevokedAt] datetime2 NULL,
    [EmployeeId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_RefreshTokens] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RefreshTokens_AspNetUsers_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_RefreshTokens_EmployeeId] ON [RefreshTokens] ([EmployeeId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260825041753_AddRefreshToken', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [AspNetUsers] ADD [DepartmentId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

CREATE TABLE [Department] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Department] PRIMARY KEY ([Id])
);

CREATE INDEX [IX_AspNetUsers_DepartmentId] ON [AspNetUsers] ([DepartmentId]);

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Department_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Department] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260825045935_AddDepartment', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_AspNetUsers_ManagerId];

ALTER TABLE [AspNetUsers] DROP CONSTRAINT [FK_AspNetUsers_Department_DepartmentId];

ALTER TABLE [Department] DROP CONSTRAINT [PK_Department];

EXEC sp_rename N'[Department]', N'Departments', 'OBJECT';

DECLARE @var nvarchar(max);
SELECT @var = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AspNetUsers]') AND [c].[name] = N'ManagerId');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [AspNetUsers] DROP CONSTRAINT ' + @var + ';');
ALTER TABLE [AspNetUsers] ALTER COLUMN [ManagerId] uniqueidentifier NULL;

ALTER TABLE [Departments] ADD CONSTRAINT [PK_Departments] PRIMARY KEY ([Id]);

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_AspNetUsers_ManagerId] FOREIGN KEY ([ManagerId]) REFERENCES [AspNetUsers] ([Id]);

ALTER TABLE [AspNetUsers] ADD CONSTRAINT [FK_AspNetUsers_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260825075137_SetManagerOptional', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
DECLARE @var1 nvarchar(max);
SELECT @var1 = QUOTENAME([d].[name])
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[RefreshTokens]') AND [c].[name] = N'RevokedAt');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [RefreshTokens] DROP CONSTRAINT ' + @var1 + ';');
ALTER TABLE [RefreshTokens] DROP COLUMN [RevokedAt];

ALTER TABLE [RefreshTokens] ADD [IsRevoked] bit NOT NULL DEFAULT CAST(0 AS bit);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260825121300_EditRefeshToken', N'10.0.11');

COMMIT;
GO

BEGIN TRANSACTION;
CREATE TABLE [PasswordResetOtps] (
    [Id] uniqueidentifier NOT NULL,
    [EmployeeId] uniqueidentifier NOT NULL,
    [OtpHash] nvarchar(max) NOT NULL,
    [ExpiresAt] datetime2 NOT NULL,
    [IsUsed] bit NOT NULL,
    [FailedAttempts] int NOT NULL,
    CONSTRAINT [PK_PasswordResetOtps] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PasswordResetOtps_AspNetUsers_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [PasswordResetSessions] (
    [Id] uniqueidentifier NOT NULL,
    [EmployeeId] uniqueidentifier NOT NULL,
    [ResetToken] nvarchar(max) NOT NULL,
    [ExpiresAt] datetime2 NOT NULL,
    [IsUsed] bit NOT NULL,
    CONSTRAINT [PK_PasswordResetSessions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_PasswordResetSessions_AspNetUsers_EmployeeId] FOREIGN KEY ([EmployeeId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_PasswordResetOtps_EmployeeId] ON [PasswordResetOtps] ([EmployeeId]);

CREATE INDEX [IX_PasswordResetSessions_EmployeeId] ON [PasswordResetSessions] ([EmployeeId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260827081548_resetPasswordModels', N'10.0.11');

COMMIT;
GO

