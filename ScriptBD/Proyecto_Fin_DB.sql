USE [master]
GO
/****** Object:  Database [Proyecto_Fin_Hibrido]    Script Date: 7/13/2021 6:03:02 PM ******/
CREATE DATABASE [Proyecto_Fin_Hibrido2]
 
 use [Proyecto_Fin_Hibrido2]
 

CREATE TABLE [dbo].[Empleado](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Cedula] [varchar](11) NOT NULL,
	[Nombre] [varchar](70) NOT NULL,
	[Salario] [float] NOT NULL,
	[DepartamentoID] [int] NOT NULL,
	[PuestoID] [int] NOT NULL,
	[NominaID] [int] NOT NULL
)

CREATE TABLE [dbo].[Departamento](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [varchar](50) NOT NULL
)

CREATE TABLE [dbo].[Puesto](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [varchar](50) NOT NULL
)

CREATE TABLE [dbo].[Nomina](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [varchar](50) NOT NULL
)

CREATE TABLE [dbo].[Usuario](
	[id][int] IDENTITY(1,1) PRIMARY KEY,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Roles] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Estado] [bit] NOT NULL
)

INSERT INTO [dbo].[Usuario] VALUES ('ADMIN','12345','admin','admin@email.com','Manuel','López',1);
INSERT INTO [dbo].[Usuario] VALUES ('USUARIO','password','empleado','empleado@email.com','Benjamín','Ortiz',1);


ALTER TABLE [dbo].[Empleado] WITH CHECK ADD  CONSTRAINT [FK_Empleado_Departamento] FOREIGN KEY([DepartamentoID])
REFERENCES [dbo].[Departamento] ([id])

ALTER TABLE [dbo].[Empleado] WITH CHECK ADD  CONSTRAINT [FK_Empleado_Puesto] FOREIGN KEY([PuestoID])
REFERENCES [dbo].[Puesto] ([id])

ALTER TABLE [dbo].[Empleado] WITH CHECK ADD  CONSTRAINT [FK_Empleado_Nomina] FOREIGN KEY([NominaID])
REFERENCES [dbo].[Nomina] ([id])
