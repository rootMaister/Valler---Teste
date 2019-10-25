CREATE TABLE Endereco (
	id_endereco INT IDENTITY PRIMARY KEY NOT NULL,
	id_usuario INT FOREIGN KEY REFERENCES Usuario (id_usuario),
	rua VARCHAR (255) NOT NULL,
	numero VARCHAR (255) NOT NULL,
	bairro VARCHAR (255) NOT NULL,
	cidade VARCHAR (255) NOT NULL,
	uf VARCHAR (255) NOT NULL, 
	cep VARCHAR (255) NOT NULL
);      

CREATE TABLE Oferta (
	id_oferta INT IDENTITY PRIMARY KEY NOT NULL,
	id_produto INT FOREIGN KEY REFERENCES Produto (id_produto),
	titulo VARCHAR (255) NOT NULL,
	data_oferta VARCHAR (255) NOT NULL,
	data_vencimento VARCHAR (255) NOT NULL,
	preco VARCHAR (255) NOT NULL,
	quantidade VARCHAR (255) NOT NULL,
	imagem VARCHAR (255) NOT NULL
);

CREATE TABLE Reserva (
	id_reserva INT IDENTITY PRIMARY KEY NOT NULL,
	id_oferta INT FOREIGN KEY REFERENCES Oferta (id_oferta),
	id_usuario INT FOREIGN KEY REFERENCES Usuario (id_usuario),
	quantidade_reserva VARCHAR (255) NOT NULL,
	cronometro VARCHAR (255) NOT NULL,
	statuss VARCHAR (255) NOT NULL
);