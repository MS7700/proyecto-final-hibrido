USE [master]
GO
/****** Object:  Database [Proyecto_Fin_Hibrido]    Script Date: 7/13/2021 6:03:02 PM ******/
CREATE DATABASE [Proyecto_Fin_Hibrido3]
 
 use [Proyecto_Fin_Hibrido3]
 

CREATE TABLE [dbo].[Empleado](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Cedula] [varchar](11) NOT NULL,
	[Nombre] [varchar](70) NOT NULL,
	[Salario] [float] NOT NULL,
	[DepartamentoID] [int] NOT NULL,
	[PuestoID] [int] NOT NULL,
	[TipoNominaID] [int] NOT NULL
)

CREATE TABLE [dbo].[Departamento](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [varchar](50) NOT NULL
)

CREATE TABLE [dbo].[Puesto](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [varchar](50) NOT NULL
)

CREATE TABLE [dbo].[TipoNomina](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [varchar](50) NOT NULL
)

CREATE TABLE [dbo].[TipoDeduccion](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](50) NOT NULL,
	[Automatico] [bit] NOT NULL,
	[Porcentual] [bit] NOT NULL,
	[Cantidad] [float] NULL,
	[Estado] [bit] NOT NULL
)

CREATE TABLE [dbo].[TipoIngreso](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](50) NOT NULL,
	[Automatico] [bit] NOT NULL,
	[Porcentual] [bit] NOT NULL,
	[Cantidad] [float] NULL,
	[Estado] [bit] NOT NULL
)

CREATE TABLE [dbo].[Cuenta](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [nvarchar](50) NOT NULL
)

CREATE TABLE [dbo].[AsientoContable](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Fecha] [date] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[Cuentadb] [int] NOT NULL,
	[Cuentacr] [int] NOT NULL,
	[Monto] [float] NOT NULL,
	[Contabilizado] [bit] NOT NULL,
	[Estado] [char](1) NOT NULL
	)

	CREATE TABLE [dbo].[Transaccion](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[EmpleadoID] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Tipo] [char](1) NOT NULL,
	[TipoIngresoID] [int] NULL,
	[TipoDeduccionID] [int] NULL,
	[Monto] [float] NOT NULL,
	[Contabilizado] [bit] NOT NULL
)

CREATE TABLE [dbo].[Nomina](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Fecha] [date] NOT NULL,
	[Periodo] [nvarchar](50) NOT NULL,
	[TipoNominaID] [int] NOT NULL,
	[Contabilizado] [bit] NOT NULL
)

CREATE TABLE [dbo].[NominaResumen](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[NominaID] [int] NOT NULL,
	[EmpleadoID] [int] NOT NULL,
	[SueldoBruto] [float] NOT NULL,
	[SueldoDevengado] [float] NOT NULL
)

CREATE TABLE [dbo].[NominaDetalle](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[NominaResumenID] [int] NOT NULL,
	[TransaccionID] [int] NOT NULL,
	[Tipo] [varchar](9) NOT NULL,
	[Descripción] [nvarchar](50) NOT NULL,
	[Monto] [float] NOT NULL
)

ALTER TABLE [dbo].[Empleado] WITH CHECK ADD  CONSTRAINT [FK_Empleado_Departamento] FOREIGN KEY([DepartamentoID])
REFERENCES [dbo].[Departamento] ([id])

ALTER TABLE [dbo].[Empleado] WITH CHECK ADD  CONSTRAINT [FK_Empleado_Puesto] FOREIGN KEY([PuestoID])
REFERENCES [dbo].[Puesto] ([id])

ALTER TABLE [dbo].[Empleado] WITH CHECK ADD  CONSTRAINT [FK_Empleado_TipoNomina] FOREIGN KEY([TipoNominaID])
REFERENCES [dbo].[TipoNomina] ([id])

ALTER TABLE [dbo].[AsientoContable]  WITH CHECK ADD  CONSTRAINT [FK_AsientoContable_Cuenta] FOREIGN KEY([Cuentadb])
REFERENCES [dbo].[Cuenta] ([id])

ALTER TABLE [dbo].[AsientoContable]  WITH CHECK ADD  CONSTRAINT [FK_AsientoContable_Cuenta1] FOREIGN KEY([Cuentacr])
REFERENCES [dbo].[Cuenta] ([id])

ALTER TABLE [dbo].[Transaccion]  WITH CHECK ADD  CONSTRAINT [FK_Transaccion_Empleado] FOREIGN KEY([EmpleadoID])
REFERENCES [dbo].[Empleado] ([id])

ALTER TABLE [dbo].[Transaccion]  WITH CHECK ADD  CONSTRAINT [FK_Transaccion_TipoIngreso] FOREIGN KEY([TipoIngresoID])
REFERENCES [dbo].[TipoIngreso] ([id])

ALTER TABLE [dbo].[Transaccion]  WITH CHECK ADD  CONSTRAINT [FK_Transaccion_TipoDeduccion] FOREIGN KEY([TipoDeduccionID])
REFERENCES [dbo].[TipoDeduccion] ([id])

ALTER TABLE [dbo].[Nomina] WITH CHECK ADD  CONSTRAINT [FK_Nomina_TipoNomina] FOREIGN KEY([TipoNominaID])
REFERENCES [dbo].[TipoNomina] ([id])

ALTER TABLE [dbo].[NominaResumen]  WITH CHECK ADD  CONSTRAINT [FK_NominaResumen_Nomina] FOREIGN KEY([NominaID])
REFERENCES [dbo].[Nomina] ([id])

ALTER TABLE [dbo].[NominaResumen]  WITH CHECK ADD  CONSTRAINT [FK_NominaResumen_Empleado] FOREIGN KEY([EmpleadoID])
REFERENCES [dbo].[Empleado] ([id])

ALTER TABLE [dbo].[NominaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_NominaDetalle_Nomina] FOREIGN KEY([NominaResumenID])
REFERENCES [dbo].[NominaResumen] ([id])

ALTER TABLE [dbo].[NominaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_NominaDetalle_Transaccion] FOREIGN KEY([TransaccionID])
REFERENCES [dbo].[Transaccion] ([id])