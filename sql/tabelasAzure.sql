


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
INSERT INTO Convite VALUES(1,2,0,'Seja meu amigo(a)');
INSERT INTO Amigos Values(1,2,1);
 select *from Amigos;
  select *from usuario;
INSERT INTO Genero VALUES ('Masculino'); 
INSERT INTO Genero VALUES ('Feminino'); 


								SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
                                FROM
										Amigos
                                WHERE
										UsuarioId= 1 and Pendencia =0;

SELECT c.Id,c.IdUsuario,c.IdUsuarioConvidado,c.Status_Convite,c.Mensagem,u.Nome

FROM Convite c

INNER JOIN Usuario u ON U.Id = c.IdUsuario

where u.id=1;





UPDATE Amigos SET Pendencia=0 WHERE UsuarioId=1 and UsuarioAmigoId=2;

								UPDATE Convite
                                SET Status_Convite=0
                                WHERE IdUsuarioConvidado=2;





INSERT INTO Convite VALUES(2,1,0,'Seja meu amigo(a)');
select *from usuario;
select *from Convite;
						  SELECT Id,
                                 IdUsuario,
                                 IdUsuarioConvidado,                             
                                 Mensagem,
                                 Status_Convite
                                 From Convite
                                 Where 
                                 IdUsuarioConvidado=2;

						SELECT	 Id,
                                 IdUsuario,
                                 IdusuarioConvidado,                               
                                 Mensagem,
                                 Status_Convite
                                 From Convite
                                 Where 
                                 IdusuarioConvidado=2;

						SELECT	 Id,
                                 IdUsuario,
                                 IdusuarioConvidado,                                
                                 Mensagem,
                                 Status_Convite
                                 From Convite
                                 Where 
                                 IdUsuario=1;




  select *from amigos; 
    select *from usuario;  
  delete amigos;


  SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
                                FROM
										Amigos
                                WHERE
										UsuarioAmigoId=3 and Pendencia=0;