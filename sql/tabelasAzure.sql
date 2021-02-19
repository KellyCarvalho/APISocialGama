


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

INSERT INTO Amigos Values(3,1,1);
 select *from Amigos;
  select *from usuario;
INSERT INTO Genero VALUES ('Masculino'); 
INSERT INTO Genero VALUES ('Feminino'); 
INSERT INTO Postagem VALUES (3,'Boa tarde',null,'2021-02-19T18:20:17.636Z'); 


								SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
                                FROM
										Amigos
                                WHERE
										UsuarioId= 1 and Pendencia =0;






  delete amigos;

  select *from amigos; 
    select *from usuario;  



										SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
										FROM
										Amigos
										WHERE
										UsuarioAmigoId=3 and Pendencia=0;

select p.Id, p.UsuarioId, a.UsuarioAmigoId
From Postagem p
inner join Amigos a on a.UsuarioAmigoId=p.UsuarioId
where p.UsuarioId=3;
SELECT *FROM Usuario;
select *from Postagem;
select *from Amigos;

select p.Id, p.UsuarioId,p.Foto,p.Texto,p.Criacao
from postagem p
inner join Amigos a on a.UsuarioAmigoId=p.UsuarioId;

SELECT p.Id,
p.UsuarioId,
p.Texto,
p.Foto,
p.Criacao
FROM 
Postagem p
INNER JOIN Amigos a 
on a.UsuarioAmigoId=p.UsuarioId
where a.UsuarioAmigoId=3 and a.Pendencia=0;


SELECT p.Id,
p.UsuarioId,
p.Texto,
p.Foto,
p.Criacao
FROM 
Postagem p
INNER JOIN Amigos a 
on a.UsuarioAmigoId=p.UsuarioId
where a.UsuarioAmigoId=p.UsuarioId;

SELECT a.UsuarioId, p.Id,p.Foto,p.Texto,p.Criacao
FROM 
Amigos b
INNER JOIN Amigos a 
on a.UsuarioAmigoId=b.UsuarioId
INNER JOIN Postagem p
on p.UsuarioId=b.UsuarioAmigoId
where .UsuarioId=1
order by  p.Criacao desc;

DELETE 
FROM
Amigos
WHERE 
UsuarioAmigoId=3 and UsuarioId=1;
delete Amigos;

select * from Amigos;


										SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
										FROM
										Amigos
										WHERE
										UsuarioAmigoId=1 and Pendencia=0 and UsuarioId=2;

										SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
										FROM
										Amigos
										WHERE
										UsuarioAmigoId=3 and Pendencia=0 and UsuarioId=1

										SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
                                FROM
										Amigos
                                WHERE
										UsuarioAmigoId=3 and Pendencia=1;

										SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
                                FROM
										Amigos
                                WHERE
										UsuarioAmigoId=3 and Pendencia=0 and UsuarioId=1

										SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
										FROM
										Amigos
										WHERE
										UsuarioAmigoId=3 and Pendencia=1 and UsuarioId=1;

										SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
										FROM
										Amigos
										WHERE
										UsuarioAmigoId=3 and Pendencia=1 and UsuarioId=1;

										SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
										FROM
										Amigos
										WHERE
										UsuarioAmigoId=1 and Pendencia=1 and UsuarioId=3

										SELECT Id,
										UsuarioId,
                                        UsuarioAmigoId,
										Pendencia
										FROM
										Amigos
										WHERE
										UsuarioAmigoId='{friendId}' and Pendencia=1 and UsuarioId='{userId}'
select *from usuario;
SELECT u.Id,
u.Nome,
u.Email,
u.DataNascimento,
u.Foto
FROM 
Usuario u
INNER JOIN Amigos a 
on a.UsuarioAmigoId=u.Id
Inner Join Genero g
on u.GeneroId=g.Id
where a.UsuarioId=2 and a.UsuarioAmigoId=1;




										