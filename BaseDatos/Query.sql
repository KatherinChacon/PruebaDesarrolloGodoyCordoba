CREATE DATABASE BD_GodoyCordoba

go

USE BD_GodoyCordoba

CREATE TABLE Sesion(
	IdSesion int primary key identity,	
	Correo varchar(100),
	Contrasena varchar(100)
)

CREATE TABLE Usuario(
	IdUsuario int primary key identity,
	Nombre varchar(100),
	Apellidos varchar(100),
	Cedula bigint,
	Correo varchar(100),
	Fecha_Acceso datetime
)

SELECT * FROM Sesion

SELECT * FROM Usuario

INSERT INTO Usuario (Nombre, Apellidos, Cedula, Correo, Fecha_Acceso) 
	VALUES ('Katherin', 'Chacon Valbuena', 1023969643, 'katherin.chacon@hotmail.com', GETDATE())
		
-- Campo con toquen para configurar JWT (Minimo debe ser de 32)
SELECT NEWID()
