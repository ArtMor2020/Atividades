drop database if exists filetags;

create database filetags;
use filetags;

-- Tables

create table tags(
	id int not null auto_increment primary key,
    name varchar(255) not null,
    description varchar(255) null
);

create table files(
	id int not null auto_increment primary key,
	name varchar(255) not null,
    path varchar(255) not null,
    type varchar(255) not null
);

-- Lists

create table fileTags(
	fileId int not null,
	tagId int not null,
    foreign key (fileId) references files(id),
    foreign key (tagId) references tags(id)
);