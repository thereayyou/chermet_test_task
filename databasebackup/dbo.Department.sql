﻿--
-- Script was generated by Devart dbForge Studio for SQL Server, Version 6.5.16.0
-- Product home page: http://www.devart.com/dbforge/sql/studio
-- Script date 03.12.2023 23:14:34
-- Server version: 16.00.1000
--

SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

SET IDENTITY_INSERT MvcDemoDb.dbo.Department ON
GO
INSERT MvcDemoDb.dbo.Department(Id, Name) VALUES (1, 'Manage')
INSERT MvcDemoDb.dbo.Department(Id, Name) VALUES (4, 'HR')
GO
SET IDENTITY_INSERT MvcDemoDb.dbo.Department OFF
GO