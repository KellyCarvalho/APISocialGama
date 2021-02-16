CREATE DATABASE GRUPO 3;
USE GRUPO 3;
--Criação do Database


--Criação das tabelas
CREATE TABLE dbo.Genero (
   Id int IDENTITY(1,1) NOT NULL,
   Descricao varchar(50) NOT NULL,
   CONSTRAINT PK_Genero_Id PRIMARY KEY CLUSTERED (Id)
)

CREATE TABLE dbo.Usuario (
	Id int IDENTITY(1,1) NOT NULL,
	GeneroId int NOT NULL,
	Nome varchar(250) NOT NULL,
	Email varchar(100) NOT NULL,
	Senha varchar(200) NOT NULL,
	DataNascimento DateTime NOT NULL,
	Foto varchar(max) NOT NULL
	CONSTRAINT PK_Usuario_Id PRIMARY KEY CLUSTERED (Id)
)

ALTER TABLE dbo.Usuario
   ADD CONSTRAINT FK_Usuario_Genero FOREIGN KEY (GeneroId)
      REFERENCES dbo.Genero (Id)


CREATE TABLE dbo.Postagem (
   Id int IDENTITY(1,1) NOT NULL,
   UsuarioId int NOT NULL,
   Texto varchar(250) NOT NULL,
   Foto varchar(max),
   Criacao DateTime NOT NULL,
   CONSTRAINT PK_Postagem_Id PRIMARY KEY CLUSTERED (Id)
)


ALTER TABLE dbo.Postagem
   ADD CONSTRAINT FK_Postagem_Usuario FOREIGN KEY (UsuarioId)
      REFERENCES dbo.Usuario (Id)
	  
	  
CREATE TABLE dbo.Comentario (
   Id int IDENTITY(1,1) NOT NULL,
   UsuarioId int NOT NULL,
   PostagemId int NOT NULL,
   Texto varchar(250) NOT NULL,
   Criacao DateTime NOT NULL,
   CONSTRAINT PK_Comentario_Id PRIMARY KEY CLUSTERED (Id)
)

ALTER TABLE dbo.Comentario
   ADD CONSTRAINT FK_Comentario_Usuario FOREIGN KEY (UsuarioId)
      REFERENCES dbo.Usuario (Id)
	  
ALTER TABLE dbo.Comentario
   ADD CONSTRAINT FK_Comentario_Postagem FOREIGN KEY (PostagemId)
      REFERENCES dbo.Postagem (Id)
	  
CREATE TABLE dbo.Curtidas (
   Id int IDENTITY(1,1) NOT NULL,
   UsuarioId int NOT NULL,
   PostagemId int NOT NULL,
   CONSTRAINT PK_Curtidas_Id PRIMARY KEY CLUSTERED (Id)
)

ALTER TABLE dbo.Curtidas
   ADD CONSTRAINT FK_Curtidas_Usuario FOREIGN KEY (UsuarioId)
      REFERENCES dbo.Usuario (Id)
	  
ALTER TABLE dbo.Curtidas
   ADD CONSTRAINT FK_Curtidas_Postagem FOREIGN KEY (PostagemId)
      REFERENCES dbo.Postagem (Id)

CREATE TABLE dbo.Album (
   Id int IDENTITY(1,1) NOT NULL,
   UsuarioId int NOT NULL,
   PostagemId int NOT NULL,
   CONSTRAINT PK_Album_Id PRIMARY KEY CLUSTERED (Id)
   )

   ALTER TABLE dbo.Album
   ADD CONSTRAINT FK_Album_Usuario FOREIGN KEY (UsuarioId)
      REFERENCES dbo.Usuario (Id)
	  
ALTER TABLE dbo.Album
   ADD CONSTRAINT FK_Album_Postagem FOREIGN KEY (PostagemId)
      REFERENCES dbo.Postagem (Id)


INSERT INTO Genero VALUES ('Masculino'); 

delete Usuario;
	  
INSERT INTO Usuario (GeneroId, Nome, Email, Senha, DataNascimento, Foto) VALUES (1, 'Raquel', '12raquel07@gmail.com','123456', '07/02/2002', '');
INSERT INTO Postagem values (4,'flor.jpg','12-02-2021');
INSERT INTO Postagem values (5,'flor.jpg','12-02-2021');
insert into Album values(4,1);
insert into Album(UsuarioId,PostagemId) values(5,2);

SELECT u.Id, u.Nome,  u.Email, u.Senha, u.DataNascimento, u.Foto, g.Id as GeneroId, g.Descricao
                                FROM 
	                                Usuario u
                                INNER JOIN 
	                                Genero g ON g.Id = u.GeneroId
                          
SELECT a.Id, a.PostagemId, a.UsuarioId
FROM Album a
INNER JOIN 
Postagem p ON p.Id=a.PostagemId
INNER JOIN 
Usuario u ON u.Id= a.UsuarioId
	WHERE u.Id=5

SELECT p.Id,u.Id
FROM Postagem p 
INNER JOIN 
Usuario u ON u.Id= p.UsuarioId
WHERE u.Id=4;



