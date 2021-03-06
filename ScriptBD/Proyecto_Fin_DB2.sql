USE [master]
GO
/****** Object:  Database [Proyecto_Fin_Hibrido2]    Script Date: 8/3/2021 6:42:41 PM ******/
CREATE DATABASE [Proyecto_Fin_Hibrido2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Proyecto_Fin_Hibrido2', FILENAME = N'X:\SQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\Proyecto_Fin_Hibrido2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Proyecto_Fin_Hibrido2_log', FILENAME = N'X:\SQL\MSSQL15.SQLEXPRESS\MSSQL\DATA\Proyecto_Fin_Hibrido2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Proyecto_Fin_Hibrido2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET ARITHABORT OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET  MULTI_USER 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET QUERY_STORE = OFF
GO
USE [Proyecto_Fin_Hibrido2]
GO
/****** Object:  Table [dbo].[AsientoContable]    Script Date: 8/3/2021 6:42:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AsientoContable](
	[id] [int] NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[Cuentadb] [int] NOT NULL,
	[Cuentacr] [int] NOT NULL,
	[Monto] [float] NOT NULL,
	[Contabilizado] [bit] NOT NULL,
	[Estado] [char](1) NOT NULL,
	[Fecha] [date] NOT NULL,
 CONSTRAINT [PK_AsientoContable] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuenta]    Script Date: 8/3/2021 6:42:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuenta](
	[id] [int] NOT NULL,
	[Descripcion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Cuenta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departamento]    Script Date: 8/3/2021 6:42:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departamento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 8/3/2021 6:42:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Cedula] [varchar](11) NOT NULL,
	[Nombre] [varchar](70) NOT NULL,
	[Salario] [float] NOT NULL,
	[DepartamentoID] [int] NOT NULL,
	[PuestoID] [int] NOT NULL,
	[NominaID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Login]    Script Date: 8/3/2021 6:42:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Login](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [nvarchar](50) NOT NULL,
	[Contrasena] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Nomina]    Script Date: 8/3/2021 6:42:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nomina](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Puesto]    Script Date: 8/3/2021 6:42:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Puesto](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoDeduccion]    Script Date: 8/3/2021 6:42:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoDeduccion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[DependeSalario] [bit] NOT NULL,
	[Estado] [bit] NOT NULL,
	[Automatico] [bit] NOT NULL,
	[Porcentual] [bit] NOT NULL,
 CONSTRAINT [PK__TipoDedu__3213E83F48366D4A] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoIngreso]    Script Date: 8/3/2021 6:42:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoIngreso](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[DependeSalario] [bit] NOT NULL,
	[Estado] [bit] NOT NULL,
	[Automatico] [bit] NOT NULL,
	[Porcentual] [bit] NOT NULL,
 CONSTRAINT [PK__TipoIngr__3213E83FBC0F1F49] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoNomina]    Script Date: 8/3/2021 6:42:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoNomina](
	[id] [int] NOT NULL,
	[TipoNomina] [nvarchar](50) NOT NULL,
	[Periodo] [nvarchar](50) NOT NULL,
	[EmpleadoID] [int] NOT NULL,
 CONSTRAINT [PK_TipoNomina] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transaccion]    Script Date: 8/3/2021 6:42:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaccion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[EmpleadoID] [int] NOT NULL,
	[fecha] [date] NOT NULL,
	[IoD] [bit] NOT NULL,
	[TipoIngreso] [varchar](20) NULL,
	[TipoDeduccion] [varchar](20) NULL,
	[Contabilizado] [bit] NOT NULL,
	[Monto] [float] NOT NULL,
 CONSTRAINT [PK_Transaccion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AsientoContable]  WITH CHECK ADD  CONSTRAINT [FK_AsientoContable_Cuenta] FOREIGN KEY([Cuentadb])
REFERENCES [dbo].[Cuenta] ([id])
GO
ALTER TABLE [dbo].[AsientoContable] CHECK CONSTRAINT [FK_AsientoContable_Cuenta]
GO
ALTER TABLE [dbo].[AsientoContable]  WITH CHECK ADD  CONSTRAINT [FK_AsientoContable_Cuenta1] FOREIGN KEY([Cuentacr])
REFERENCES [dbo].[Cuenta] ([id])
GO
ALTER TABLE [dbo].[AsientoContable] CHECK CONSTRAINT [FK_AsientoContable_Cuenta1]
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_Empleado_Departamento] FOREIGN KEY([DepartamentoID])
REFERENCES [dbo].[Departamento] ([id])
GO
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_Empleado_Departamento]
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_Empleado_Nomina] FOREIGN KEY([NominaID])
REFERENCES [dbo].[Nomina] ([id])
GO
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_Empleado_Nomina]
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_Empleado_Puesto] FOREIGN KEY([PuestoID])
REFERENCES [dbo].[Puesto] ([id])
GO
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_Empleado_Puesto]
GO
ALTER TABLE [dbo].[TipoNomina]  WITH CHECK ADD  CONSTRAINT [FK_TipoNomina_Empleado] FOREIGN KEY([EmpleadoID])
REFERENCES [dbo].[Empleado] ([id])
GO
ALTER TABLE [dbo].[TipoNomina] CHECK CONSTRAINT [FK_TipoNomina_Empleado]
GO
ALTER TABLE [dbo].[Transaccion]  WITH CHECK ADD  CONSTRAINT [FK_Transaccion_Empleado] FOREIGN KEY([EmpleadoID])
REFERENCES [dbo].[Empleado] ([id])
GO
ALTER TABLE [dbo].[Transaccion] CHECK CONSTRAINT [FK_Transaccion_Empleado]
GO
USE [master]
GO
ALTER DATABASE [Proyecto_Fin_Hibrido2] SET  READ_WRITE 
GO
