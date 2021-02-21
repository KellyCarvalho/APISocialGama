


USE GRUPO3;
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
      REFERENCES dbo.Usuario (Id);
	  
ALTER TABLE dbo.Comentario
   ADD CONSTRAINT FK_Comentario_Postagem FOREIGN KEY (PostagemId)
      REFERENCES dbo.Postagem (Id);
	  
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
      REFERENCES dbo.Postagem (Id);

	 

CREATE TABLE dbo.Convite (
	Id int IDENTITY(1,1) NOT NULL,
	IdUsuario int NOT NULL,
	IdUsuarioConvidado int NOT NULL,
	Status_Convite int NOT NULL,
	Mensagem varchar(250),
	CONSTRAINT PK_Convite_Id PRIMARY KEY CLUSTERED (Id),	
)
drop table Convite;
ALTER TABLE dbo.Convite
   ADD CONSTRAINT FK_Usuario FOREIGN KEY (IdUsuario)
      REFERENCES dbo.Usuario (Id)

	  ALTER TABLE dbo.Convite
   ADD CONSTRAINT FK_Usuario_Convidado FOREIGN KEY (IdUsuarioConvidado)
      REFERENCES dbo.Usuario (Id)

drop table Amigos;
CREATE TABLE dbo.Amigos (
	Id int IDENTITY(1,1) NOT NULL,
	UsuarioId int NOT NULL,
	UsuarioAmigoId int NOT NULL,
	Pendencia int NOT NULL,
	CONSTRAINT PK_Amigos_Id PRIMARY KEY CLUSTERED (Id),	
)

ALTER TABLE dbo.Amigos
   ADD CONSTRAINT FK_Usuario_Amigos FOREIGN KEY (UsuarioId)
      REFERENCES dbo.Usuario (Id)

	  ALTER TABLE dbo.Amigos
   ADD CONSTRAINT FK_Usuario_Amigo_Convidado FOREIGN KEY (UsuarioAmigoId)
      REFERENCES dbo.Usuario (Id);
-----------------------------------------------------------------------------------	 

INSERT INTO Amigos Values(4,1,1);
 select *from Amigos;
  select *from usuario;
INSERT INTO Genero VALUES ('Masculino'); 
INSERT INTO Genero VALUES ('Feminino');
INSERT INTO Genero VALUES ('Não-binárie/Não binárie/Nãobinárie'); 
INSERT INTO Genero VALUES ('Genderqueer/Gênero queer');
INSERT INTO Genero VALUES ('Agênero'); 
INSERT INTO Genero VALUES ('Gênero-fluido'); 
INSERT INTO Genero VALUES ('Homem não-binárie'); 
INSERT INTO Genero VALUES ('Mulher não-binárie');
INSERT INTO Genero VALUES ('Demigênero'); 
INSERT INTO Genero VALUES ('Andrógine'); 
INSERT INTO Genero VALUES ('Neutrois'); 
INSERT INTO Genero VALUES ('Transfeminine'); 
INSERT INTO Genero VALUES ('Transmasculine');
INSERT INTO Genero VALUES ('Demigênero'); 
INSERT INTO Genero VALUES ('Andrógine'); 
INSERT INTO Genero VALUES ('Neutrois'); 
INSERT INTO Genero VALUES ('Prefiro Não declarar'); 

select *from Genero;

select *from Usuario;
select *from Amigos;
select*from Postagem;

INSERT INTO Postagem VALUES (3,'Boa tarde',null,'2021-02-19T18:20:17.636Z'); 

SELECT p.Foto
FROM Postagem p
INNER JOIN	Amigos a
ON a.UsuarioAmigoId=p.UsuarioId
WHERE a.UsuarioId=6 and a.UsuarioAmigoId=7 and Pendencia=0;