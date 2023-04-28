/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Name]
      ,[Model]
      ,[Status]
  FROM [CarWashTbl].[dbo].[CarModeltbl]