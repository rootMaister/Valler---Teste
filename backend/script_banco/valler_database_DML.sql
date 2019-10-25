--Produto - Mateus       - OK
insert into Produto      (id_categoria, id_usuario, nome_produto, descricao) 
values                   (1,2,'Fanta', 'Bebida 2L'), 
                         (1,2,'Coca', 'Bebida 1L'),
                         (2,3,'Batata', 'Alimento fresco'); 

--Usuario - Mariana      OK
insert into Usuario      (id_tipo_usuario,nome_razao_social, email,senha,documento) 
values                   (1,'Valler-adm','adm@valler.com','23751923','232312342124'),
                         (2,'João Almeida','Joao_almeida@gmail.com','353455344','435625463243'),
                         (3,'EXTRA','extra@extra.com.br','3423453','32434343325');

--Tipo_Usuario - Mariana  - OK
insert into Tipo_Usuario (tipo) 
values                   ('ADM'),
                         ('Comum'),
                         ('Fornecedor');

--Categoria - Pietra    -OK
INSERT INTO Categoria    (categoria)  
values                   ('Bebidas'),
                         ('Alimentos'),
                         ('Frutas');

--Telefone - Pietra      -OK
INSERT INTO Telefone     (id_usuario,telefone) 
values                   (1, '4653-3466'), 
                         (2, '2254-2735'),
                         (3, '2752-3543');

-- Endereco - Vinicius   -OK
INSERT INTO Endereco     (id_usuario, rua, numero, bairro, cidade, uf, cep)
VALUES                   (1, 'Getulio', 731, 'Jardim Belval', 'Barueri', 'SP', '731'),
                         (2, 'Papagaio', 32, 'America', 'Jandira', 'RJ', '543'), 
                         (1, 'Franca', 81, 'Auvorada', 'Quero', 'PQ', '10');

--Oferta - Vinicius      -OK

INSERT INTO Oferta       (id_produto, titulo, data_oferta, data_vencimento, preco, quantidade, imagem)
VALUES                   (4, 'Fanta Laranja 2L', '12/09/2019', '20/09/2019', 'R$ 2,60', '1', 'Fanta_laranja.jpg' ),
                         (5, 'Coca Zero 2L', '30/08/2019', '05/09/2019', 'R$3,40' , '2' , 'Coca_Cola_zero.jpg'),
                         (6, 'Batata', '10/10/2019', '30/10/2019', 'R$6,00', '1', 'Batata.jpg');

--Reserva - Vinicius    - OK
INSERT INTO Reserva (id_oferta, id_usuario, quantidade_reserva, cronometro, status_reserva)
VALUES                   ('4',  2, '3', '15H', 'aguardando'),
                         ('5',  2, '2', '14H', 'aguardando'),
                         ('6',  3, '5', '12H', 'finalizado');


