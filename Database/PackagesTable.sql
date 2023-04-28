/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Name]
      ,[Price]
      ,[Status]
  FROM [CarWashTbl].[dbo].[PackageTable]