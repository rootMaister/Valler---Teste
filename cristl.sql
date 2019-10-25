CREATE TABLE Telefone(
	id_telefone int identity  PRIMARY KEY,
	id_usuario foreign key references Usuario(id_usuario) not null,
	telefone varchar(15) not null

);

CREATE TABLE Categoria(
	id_categoria int identity PRIMARY KEY,
	categoria varchar(255) not null

);







