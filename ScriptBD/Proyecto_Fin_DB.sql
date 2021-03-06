USE [master]
GO
/****** Object:  Database [Proyecto_Fin_Hibrido]    Script Date: 7/13/2021 6:03:02 PM ******/
CREATE DATABASE [Proyecto_Fin_Hibrido3]
 
 use [Proyecto_Fin_Hibrido3]
 GO

CREATE TABLE [dbo].[Empleado](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Cedula] [varchar](11) NOT NULL,
	[Nombre] [varchar](70) NOT NULL,
	[Salario] [float] NOT NULL,
	[DepartamentoID] [int] NOT NULL,
	[PuestoID] [int] NOT NULL,
	[TipoNominaID] [int] NOT NULL,
	Estado [bit] NOT NULL
);

CREATE TABLE [dbo].[Departamento](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [varchar](50) NOT NULL
);

CREATE TABLE [dbo].[Puesto](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [varchar](50) NOT NULL
);

CREATE TABLE [dbo].[TipoNomina](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Descripcion] [varchar](50) NOT NULL
);

CREATE TABLE [dbo].[TipoDeduccion](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](50) NOT NULL,
	[Automatico] [bit] NOT NULL,
	[Porcentual] [bit] NOT NULL,
	[Cantidad] [float] NULL,
	[Estado] [bit] NOT NULL
);

CREATE TABLE [dbo].[TipoIngreso](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Nombre] [nvarchar](50) NOT NULL,
	[Automatico] [bit] NOT NULL,
	[Porcentual] [bit] NOT NULL,
	[Cantidad] [float] NULL,
	[Estado] [bit] NOT NULL
);

CREATE TABLE [dbo].[Cuenta](
	[id] [int] PRIMARY KEY,
	[Descripcion] [nvarchar](50) NOT NULL
);

CREATE TABLE [dbo].[AsientoContable](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[ContabilidadID] [int] NULL,
	[Fecha] [date] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[Auxiliar] [int] NOT NULL,
	[Cuentadb] [int] NOT NULL,
	[Cuentacr] [int] NOT NULL,
	[Monto] [float] NOT NULL,
	[Contabilizado] [bit] NOT NULL,
	[Estado] [char](1) NOT NULL
	);
	--ALTER TABLE [dbo].[AsientoContable] ADD [ContabilidadID] [int] NULL;
	
CREATE TABLE [dbo].[Transaccion](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[EmpleadoID] [int] NOT NULL,
	[Fecha] [date] NOT NULL,
	[Tipo] [varchar](9) NOT NULL,
	[TipoIngresoID] [int] NULL,
	[TipoDeduccionID] [int] NULL,
	[Monto] [float] NOT NULL,
	[Contabilizado] [bit] NOT NULL
);

CREATE TABLE [dbo].[Nomina](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[Fecha] [date] NOT NULL,
	[Periodo] [nvarchar](50) NOT NULL,
	[TipoNominaID] [int] NOT NULL,
	[MontoTotal] [float] NULL,
	[Contabilizado] [bit] NOT NULL,
	[AsientoContableID] [int] NULL,
);
--ALTER TABLE [dbo].[Nomina] ADD [MontoTotal] [float] NULL;
--ALTER TABLE [dbo].[Nomina] ADD [AsientoContableID] [int] NULL;

CREATE TABLE [dbo].[NominaResumen](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[NominaID] [int] NOT NULL,
	[EmpleadoID] [int] NOT NULL,
	[SueldoBruto] [float] NOT NULL,
	[SueldoDevengado] [float] NOT NULL
);

CREATE TABLE [dbo].[NominaDetalle](
	[id] [int] IDENTITY(1,1) PRIMARY KEY,
	[NominaResumenID] [int] NOT NULL,
	[TransaccionID] [int] NOT NULL,
	[Tipo] [varchar](9) NOT NULL,
	[Descripción] [nvarchar](50) NOT NULL,
	[Monto] [float] NOT NULL
);

CREATE TABLE [dbo].[Usuario](
	[id][int] IDENTITY(1,1) PRIMARY KEY,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[Roles] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Apellido] [varchar](50) NOT NULL,
	[Estado] [bit] NOT NULL
);


--Foreign keys
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

ALTER TABLE [dbo].[Nomina] WITH CHECK ADD  CONSTRAINT [FK_Nomina_AsientoContable] FOREIGN KEY([AsientoContableID])
REFERENCES [dbo].[AsientoContable] ([id]) ON DELETE SET NULL;

ALTER TABLE [dbo].[NominaResumen]  WITH CHECK ADD  CONSTRAINT [FK_NominaResumen_Nomina] FOREIGN KEY([NominaID])
REFERENCES [dbo].[Nomina] ([id]) ON DELETE CASCADE;

ALTER TABLE [dbo].[NominaResumen]  WITH CHECK ADD  CONSTRAINT [FK_NominaResumen_Empleado] FOREIGN KEY([EmpleadoID])
REFERENCES [dbo].[Empleado] ([id])

ALTER TABLE [dbo].[NominaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_NominaDetalle_Nomina] FOREIGN KEY([NominaResumenID])
REFERENCES [dbo].[NominaResumen] ([id]) ON DELETE CASCADE;

ALTER TABLE [dbo].[NominaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_NominaDetalle_Transaccion] FOREIGN KEY([TransaccionID])
REFERENCES [dbo].[Transaccion] ([id])

--DEFAULTS VALUES

--Departamento
insert into Departamento values('TI');
insert into Departamento values('Recursos Humanos');

--Puesto
insert into Puesto values('Programador');
insert into Puesto values('Auxiliar');

--TipoNomina
insert into TipoNomina values('Quincenal');
insert into TipoNomina values('Mensual');

--Empleado
insert into Empleado values('0012223334','Pedro Álvarez',15000,1,1,1,1);
insert into Empleado values('7776665559','Martha Ramírez',25000,2,2,2,1);
insert into Empleado values('40200390074','Ricardo Pérez',35000,1,1,2,1);

--TipoDeduccion
insert into TipoDeduccion values('AFP',1,1,2.87,1);
insert into TipoDeduccion values('SFS',1,1,3.04,1);

--TipoIngreso
insert into TipoIngreso values('Hora extra',1,1,0.92,1);
insert into TipoIngreso values('Bonificación',0,0,null,1);

--Cuenta
insert into Cuenta values(70,'Salarios y Sueldos Empleados');
insert into Cuenta values(71,'Gastos de Nomina Empresa');

--AsientoContable
insert into AsientoContable values('20210815','202108',2,71,70,15000,0,'R');

--Transaccion
insert into Transaccion values(1,'20210815','Deducción',null,1,430.5,1);
insert into Transaccion values(1,'20210815','Deducción',null,2,456,1);

--Nomina
insert into Nomina values('20210815','202108',1,1,1);

--NominaResumen
insert into NominaResumen values(1,1,15000,14113.5);

--NominaDetalle
insert into NominaDetalle values(1,1,'Deducción','AFP',430.5);
insert into NominaDetalle values(1,2,'Deducción','SFS',456);

--Usuario
INSERT INTO [dbo].[Usuario] VALUES ('ADMIN','12345','admin','admin@email.com','Manuel','López',1);
INSERT INTO [dbo].[Usuario] VALUES ('USUARIO','password','empleado','empleado@email.com','Benjamín','Ortiz',1);
