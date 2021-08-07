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

DROP TABLE TipoDeduccion;
DROP TABLE TipoIngreso;

-- Creating table 'TipoDeduccion'
CREATE TABLE [dbo].[TipoDeduccion] (
    [id] int PRIMARY KEY IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [Automatico] bit  NOT NULL,
    [Porcentual] bit  NOT NULL,
    [Cantidad] float  NULL,
    [Estado] bit  NOT NULL
);
GO

-- Creating table 'TipoIngreso'
CREATE TABLE [dbo].[TipoIngreso] (
    [id] int  PRIMARY KEY  IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [Automatico] bit  NOT NULL,
    [Porcentual] bit  NOT NULL,
    [Cantidad] float  NULL,
    [Estado] bit  NOT NULL
);
GO

-- Creating table 'Transaccion'
CREATE TABLE [dbo].[Transaccion] (
    [id] int  PRIMARY KEY  IDENTITY(1,1) NOT NULL,
    [EmpleadoID] int  NOT NULL,
    [Fecha] datetime  NOT NULL,
    [Tipo] varchar(9)  NOT NULL,
    [TipoIngresoID] int  NULL,
    [TipoDeduccionID] int  NULL,
    [Monto] float  NOT NULL,
    [Contabilizado] bit  NOT NULL
);
GO


ALTER TABLE [dbo].[Empleado] WITH CHECK ADD  CONSTRAINT [FK_Empleado_Departamento] FOREIGN KEY([DepartamentoID])
REFERENCES [dbo].[Departamento] ([id])

ALTER TABLE [dbo].[Empleado] WITH CHECK ADD  CONSTRAINT [FK_Empleado_Puesto] FOREIGN KEY([PuestoID])
REFERENCES [dbo].[Puesto] ([id])

ALTER TABLE [dbo].[Empleado] WITH CHECK ADD  CONSTRAINT [FK_Empleado_Nomina] FOREIGN KEY([NominaID])
REFERENCES [dbo].[Nomina] ([id])

-- Creating foreign key on [EmpleadoID] in table 'Transaccion'
ALTER TABLE [dbo].[Transaccion]
ADD CONSTRAINT [FK_Transaccion_Empleado]
    FOREIGN KEY ([EmpleadoID])
    REFERENCES [dbo].[Empleado]
        ([id])

		-- Creating foreign key on [TipoIngresoID] in table 'Transaccion'
ALTER TABLE [dbo].[Transaccion]
ADD CONSTRAINT [FK_Transaccion_TipoIngreso]
    FOREIGN KEY ([TipoIngresoID])
    REFERENCES [dbo].[TipoIngreso]
        ([id])

		ALTER TABLE [dbo].[Transaccion]
ADD CONSTRAINT [FK_Transaccion_TipoDeduccion]
    FOREIGN KEY ([TipoDeduccionID])
    REFERENCES [dbo].[TipoDeduccion]
        ([id])