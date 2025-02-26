USE [master]
GO
/****** Object:  Database [ProyectoDevel]    Script Date: 01/06/2024 05:16:17 a. m. ******/
CREATE DATABASE [ProyectoDevel]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ProyectoDevel', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ProyectoDevel.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ProyectoDevel_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ProyectoDevel_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ProyectoDevel] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ProyectoDevel].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ProyectoDevel] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ProyectoDevel] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ProyectoDevel] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ProyectoDevel] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ProyectoDevel] SET ARITHABORT OFF 
GO
ALTER DATABASE [ProyectoDevel] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [ProyectoDevel] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ProyectoDevel] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ProyectoDevel] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ProyectoDevel] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ProyectoDevel] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ProyectoDevel] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ProyectoDevel] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ProyectoDevel] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ProyectoDevel] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ProyectoDevel] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ProyectoDevel] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ProyectoDevel] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ProyectoDevel] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ProyectoDevel] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ProyectoDevel] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ProyectoDevel] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ProyectoDevel] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ProyectoDevel] SET  MULTI_USER 
GO
ALTER DATABASE [ProyectoDevel] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ProyectoDevel] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ProyectoDevel] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ProyectoDevel] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ProyectoDevel] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ProyectoDevel] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ProyectoDevel] SET QUERY_STORE = ON
GO
ALTER DATABASE [ProyectoDevel] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ProyectoDevel]
GO
/****** Object:  Table [dbo].[Encuesta]    Script Date: 01/06/2024 05:16:17 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Encuesta](
	[idEncuesta] [int] IDENTITY(1,1) NOT NULL,
	[idUsuarioCre] [int] NULL,
	[titulo] [varchar](40) NULL,
	[descripcion] [varchar](150) NULL,
PRIMARY KEY CLUSTERED 
(
	[idEncuesta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Opcion]    Script Date: 01/06/2024 05:16:17 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Opcion](
	[idOpcion] [int] IDENTITY(1,1) NOT NULL,
	[idPreg] [int] NULL,
	[texto] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[idOpcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pregunta]    Script Date: 01/06/2024 05:16:17 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pregunta](
	[idPregunta] [int] IDENTITY(1,1) NOT NULL,
	[idEncuesta] [int] NULL,
	[descripcion] [varchar](200) NULL,
	[requerido] [char](1) NULL,
	[tipocampo] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[idPregunta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RespuestaEnc]    Script Date: 01/06/2024 05:16:17 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RespuestaEnc](
	[idResEn] [int] IDENTITY(1,1) NOT NULL,
	[idEncuesta] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idResEn] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RespuestaPre]    Script Date: 01/06/2024 05:16:17 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RespuestaPre](
	[idResPre] [int] IDENTITY(1,1) NOT NULL,
	[idResEnc] [int] NULL,
	[idPreg] [int] NULL,
	[respuesta] [varchar](200) NULL,
	[idOpcion] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[idResPre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 01/06/2024 05:16:17 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Pass] [varchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Encuesta]  WITH CHECK ADD FOREIGN KEY([idUsuarioCre])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Opcion]  WITH CHECK ADD FOREIGN KEY([idPreg])
REFERENCES [dbo].[Pregunta] ([idPregunta])
GO
ALTER TABLE [dbo].[Pregunta]  WITH CHECK ADD FOREIGN KEY([idEncuesta])
REFERENCES [dbo].[Encuesta] ([idEncuesta])
GO
ALTER TABLE [dbo].[RespuestaEnc]  WITH CHECK ADD FOREIGN KEY([idEncuesta])
REFERENCES [dbo].[Encuesta] ([idEncuesta])
GO
ALTER TABLE [dbo].[RespuestaPre]  WITH CHECK ADD FOREIGN KEY([idOpcion])
REFERENCES [dbo].[Opcion] ([idOpcion])
GO
ALTER TABLE [dbo].[RespuestaPre]  WITH CHECK ADD FOREIGN KEY([idPreg])
REFERENCES [dbo].[Pregunta] ([idPregunta])
GO
ALTER TABLE [dbo].[RespuestaPre]  WITH CHECK ADD FOREIGN KEY([idResEnc])
REFERENCES [dbo].[RespuestaEnc] ([idResEn])
GO
/****** Object:  StoredProcedure [dbo].[sp_Registro]    Script Date: 01/06/2024 05:16:17 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_Registro](
@Nombre varchar(50),
@Apellido varchar(50),
@Email varchar(50), 
@Pass varchar(200), 
@Registro bit output, 
@Mensaje varchar(100) output
)
as
begin
	If(not exists (Select * from Usuario where Email = @Email))
	begin
		insert into Usuario(Nombre, Apellido, Email, Pass) values (@Nombre, @Apellido, @Email,@Pass);
		set @Registro = 1
		set @Mensaje = 'Usuario registrado Exitosamente'
	end
	else
	begin
		set @Registro = 0
		set @Mensaje = 'Error: Correo ya Existente'
	end
end
GO
/****** Object:  StoredProcedure [dbo].[sp_ValidarUsuario]    Script Date: 01/06/2024 05:16:17 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[sp_ValidarUsuario](
@Email varchar (100),
@Pass varchar(100)
)
as
begin
	if(exists(SELECT * FROM Usuario where Email = @Email AND Pass = @Pass))
		select IdUsuario From Usuario Where Email = @Email AND Pass = @Pass
	else
		Select '0'
end
GO
USE [master]
GO
ALTER DATABASE [ProyectoDevel] SET  READ_WRITE 
GO
