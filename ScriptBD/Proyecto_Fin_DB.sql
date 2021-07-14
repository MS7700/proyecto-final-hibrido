USE [master]
GO
/****** Object:  Database [Proyecto_Fin_Hibrido]    Script Date: 7/13/2021 6:03:02 PM ******/
CREATE DATABASE [Proyecto_Fin_Hibrido]
 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET ARITHABORT OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET  MULTI_USER 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET QUERY_STORE = OFF
GO
USE [Proyecto_Fin_Hibrido]
GO
/****** Object:  Table [dbo].[Asiento_Contable]    Script Date: 7/13/2021 6:03:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asiento_Contable](
	[IdAsiento] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NOT NULL,
	[IdEmpleado] [int] NOT NULL,
	[Cuenta] [varchar](50) NOT NULL,
	[TipoMovimiento] [char](2) NOT NULL,
	[FechaAsiento] [date] NOT NULL,
	[MontoAsiento] [float] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Asiento_Contable] PRIMARY KEY CLUSTERED 
(
	[IdAsiento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleado]    Script Date: 7/13/2021 6:03:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleado](
	[IdEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[Cedula] [varchar](11) NOT NULL,
	[Nombre] [varchar](70) NOT NULL,
	[Salario] [float] NOT NULL,
	[IdDepartamento] [int] NOT NULL,
	[IdPuesto] [int] NOT NULL,
	[IdNomina] [int] NOT NULL,
 CONSTRAINT [PK_Empleado] PRIMARY KEY CLUSTERED 
(
	[IdEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departamento](
	[IdDepartamento] [int] IDENTITY(1,1) NOT NULL,
	[Departamento] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Departamento] PRIMARY KEY CLUSTERED 
(
	[IdDepartamento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Puesto](
	[IdPuesto] [int] IDENTITY(1,1) NOT NULL,
	[Puesto] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Puesto] PRIMARY KEY CLUSTERED 
(
	[IdPuesto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Nomina]    Script Date: 7/13/2021 6:03:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nomina](
	[IdNomina] [int] IDENTITY(1,1) NOT NULL,
	[Nomina] [varchar](50) NOT NULL
 CONSTRAINT [PK_Nomina] PRIMARY KEY CLUSTERED 
(
	[IdNomina] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistroTransacciones]    Script Date: 7/13/2021 6:03:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistroTransacciones](
	[IdTransaccion] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpleado] [int] NOT NULL,
	[IdTipo] [int] NOT NULL,
	[TipoTransaccion] [varchar](50) NOT NULL,
	[Fecha] [date] NOT NULL,
	[Monto] [float] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_RegistroTransacciones] PRIMARY KEY CLUSTERED 
(
	[IdTransaccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[REL_TIPOS]    Script Date: 7/13/2021 6:03:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[REL_TIPOS](
	[IdTipo] [int] IDENTITY(1,1) NOT NULL,
	[IdDeduccion] [int] NULL,
	[IdIngreso] [int] NULL,
 CONSTRAINT [PK_REL_TIPOS] PRIMARY KEY CLUSTERED 
(
	[IdTipo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoDeduccion]    Script Date: 7/13/2021 6:03:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoDeduccion](
	[IdDeduccion] [int] NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[DependeSalario] [bit] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_TipoDeduccion] PRIMARY KEY CLUSTERED 
(
	[IdDeduccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoIngreso]    Script Date: 7/13/2021 6:03:02 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoIngreso](
	[IdIngreso] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[DependeSalario] [bit] NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_TipoIngreso] PRIMARY KEY CLUSTERED 
(
	[IdIngreso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Asiento_Contable]  WITH CHECK ADD  CONSTRAINT [FK_Asiento_Contable_Empleado] FOREIGN KEY([IdEmpleado])
REFERENCES [dbo].[Empleado] ([IdEmpleado])
GO
ALTER TABLE [dbo].[Asiento_Contable] CHECK CONSTRAINT [FK_Asiento_Contable_Empleado]
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_Empleado_Nomina] FOREIGN KEY([IdNomina])
REFERENCES [dbo].[Nomina] ([IdNomina])
GO
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_Empleado_Nomina]
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_Empleado_Puesto] FOREIGN KEY([IdPuesto])
REFERENCES [dbo].[Puesto] ([IdPuesto])
GO
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_Empleado_Puesto]
GO
ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_Empleado_Departamento] FOREIGN KEY([IdDepartamento])
REFERENCES [dbo].[Departamento] ([IdDepartamento])
GO
ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_Empleado_Departamento]
GO
ALTER TABLE [dbo].[RegistroTransacciones]  WITH CHECK ADD  CONSTRAINT [FK_RegistroTransacciones_Empleado] FOREIGN KEY([IdEmpleado])
REFERENCES [dbo].[Empleado] ([IdEmpleado])
GO
ALTER TABLE [dbo].[RegistroTransacciones] CHECK CONSTRAINT [FK_RegistroTransacciones_Empleado]
GO
ALTER TABLE [dbo].[RegistroTransacciones]  WITH CHECK ADD  CONSTRAINT [FK_RegistroTransacciones_REL_TIPOS] FOREIGN KEY([IdTipo])
REFERENCES [dbo].[REL_TIPOS] ([IdTipo])
GO
ALTER TABLE [dbo].[RegistroTransacciones] CHECK CONSTRAINT [FK_RegistroTransacciones_REL_TIPOS]
GO
ALTER TABLE [dbo].[REL_TIPOS]  WITH CHECK ADD  CONSTRAINT [FK_REL_TIPOS_TipoDeduccion] FOREIGN KEY([IdDeduccion])
REFERENCES [dbo].[TipoDeduccion] ([IdDeduccion])
GO
ALTER TABLE [dbo].[REL_TIPOS] CHECK CONSTRAINT [FK_REL_TIPOS_TipoDeduccion]
GO
ALTER TABLE [dbo].[REL_TIPOS]  WITH CHECK ADD  CONSTRAINT [FK_REL_TIPOS_TipoIngreso] FOREIGN KEY([IdIngreso])
REFERENCES [dbo].[TipoIngreso] ([IdIngreso])
GO
ALTER TABLE [dbo].[REL_TIPOS] CHECK CONSTRAINT [FK_REL_TIPOS_TipoIngreso]
GO
USE [master]
GO
ALTER DATABASE [Proyecto_Fin_Hibrido] SET  READ_WRITE 
GO
