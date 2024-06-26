CREATE DATABASE serpensantedb

USE serpensantedb

CREATE TABLE Professor(
    Matricula INT NOT NULL IDENTITY(4400,1),
    Nome NVARCHAR(60) NOT NULL,
    DataNasc DATE NOT NULL,
    Telefone NVARCHAR(20) NOT NULL,
    Email NVARCHAR(50) UNIQUE NOT NULL,
    Ativo BIT DEFAULT(0)

    CONSTRAINT PK_PROFESSOR PRIMARY KEY(Matricula)
)

CREATE TABLE Aluno(
    Matricula INT NOT NULL IDENTITY(7700, 1),
    Nome NVARCHAR(60) NOT NULL,
    DataNasc DATE NOT NULL,
    Telefone NVARCHAR(20) NOT NULL,
    Email NVARCHAR(50) UNIQUE NOT NULL,
    Ativo BIT DEFAULT(0)

    CONSTRAINT PK_ALUNO PRIMARY KEY(Matricula)
)

CREATE TABLE Materia(

    Id INT NOT NULL IDENTITY(1,1),
    Nome NVARCHAR(30) NOT NULL,
    Tipo TINYINT  NOT NULL

    CONSTRAINT PK_MATERIA PRIMARY KEY(Id)
)


CREATE TABLE Aula(
    Id INT NOT NULL IDENTITY(1,1),
    Titulo NVARCHAR(160) NOT NULL UNIQUE,
    Duracao DATETIME NOT NULL, 
    linkURL NVARCHAR(80) NULL,
    ProfessorId INT NOT NULL 

    CONSTRAINT PK_AULA PRIMARY KEY (Id),
    CONSTRAINT FK_PROFESSOR FOREIGN KEY (ProfessorId) REFERENCES Professor(Matricula)
)

CREATE TABLE Curso(

    Id INT NOT NULL IDENTITY(1,1),
    Nome NVARCHAR(80) NOT NULL UNIQUE,
    Duracao DATETIME NOT NULL,
    Descricao TEXT NOT NULL,
    linkURL NVARCHAR(30) NULL,
    Ativo BIT DEFAULT(0),
    CriadoEm DATETIME NOT NULL,
    CodMateria INT NOT NULL,

    CONSTRAINT PK_CURSO PRIMARY KEY (Id),
    CONSTRAINT FK_MATERIA FOREIGN KEY (CodMateria) REFERENCES Materia(id)

)

CREATE TABLE AlunoCurso(

    AlunoId INT NOT NULL,
    CursoId INT NOT NULL,
    DtInicio DATETIME,
    DtFinal DATETIME,
    Progresso INT

    CONSTRAINT PK_ALUNOCURSO PRIMARY KEY (AlunoId, CursoId),
    CONSTRAINT FK_ALUNOCURSO_CURSO FOREIGN KEY (CursoId) REFERENCES Curso(Id),
    CONSTRAINT FK_ALUNOCURSO_ALUNO FOREIGN KEY (AlunoId) REFERENCES Aluno(Matricula)
)

CREATE TABLE Acesso (
    Id INT NOT NULL IDENTITY(1,1),
    Nome VARCHAR(20) NOT NULL

    CONSTRAINT PK_ROLE PRIMARY KEY (Id)
)



ALTER TABLE Professor ADD PasswordHash NVARCHAR(160) NULL
ALTER TABLE Aluno ADD PasswordHash NVARCHAR(160) NULL

