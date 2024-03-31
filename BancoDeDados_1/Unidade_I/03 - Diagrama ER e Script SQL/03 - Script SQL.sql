create database sis2024;
use sis2024;

create table imagens(
	imagens_id int auto_increment not null,
    primary key (imagens_id),
    
    imagens longblob
);

create table categoria(
	categoria_id int auto_increment not null,
    primary key (categoria_id),
    
    categoria enum('apartamento','casa','sitio')
);

create table localidade(
	localidade_id int auto_increment not null,
    primary key (localidade_id),
    
    localidade enum('rural','urbano')
);

create table negocio(
	negocio_id int auto_increment not null,
    primary key (negocio_id),
    
    negocio enum('locacao','venda')
);

create table item (
	item_id int auto_increment not null,
    primary key (item_id),
    nome varchar(100) not null,
    valor float not null,
    n_comodos int not null,
    data_criacao datetime not null,
    id_imagens int not null,
    id_categoria int not null,
    id_localidade int not null,
    id_negocio int not null,
    
    foreign key (id_imagens) references imagens(imagens_id),
    foreign key (id_categoria) references categoria(categoria_id),
    foreign key (id_localidade) references localidade(localidade_id),
    foreign key (id_negocio) references negocio(negocio_id)
);
