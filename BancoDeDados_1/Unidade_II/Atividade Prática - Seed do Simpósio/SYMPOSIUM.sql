create database symposium;
use symposium;

-- --------------------------------------------------------------------------------------------------- MAIN TABLES

create table person
(
	id int auto_increment not null primary key,
    name varchar(255) not null,
    birthdate char(10) not null
);

create table symposium
(
	id int auto_increment not null primary key,
    title varchar(255) not null,
    info longtext not null
);

create table course
(
	id int auto_increment not null primary key,
    course varchar(255) not null,
    info longtext not null,
    
    presenter_id int not null,
    constraint fk_course_presenter_id foreign key (presenter_id) references person(id)
);

create table theme
(
	id int auto_increment not null primary key,
    theme varchar(255) not null
);

create table cientific_commision
(
	id int auto_increment not null primary key,
    commision_name varchar(255) not null,
    
    theme_id int not null,
    constraint fk_cientific_commision_theme_id foreign key (theme_id) references theme(id)
);

create table article
(
	id int auto_increment not null primary key,
    article varchar(255) not null,
    info longtext not null,
    
    theme_id int not null,
    constraint fk_article_theme_id foreign key (theme_id) references theme(id)
);

-- --------------------------------------------------------------------------------------------------- LISTS

create table symposium_managers
(
	symposium_id int not null,
	person_id int not null,
    
    constraint fk_symposium_managers_symposium_id foreign key (symposium_id) references symposium(id),
    constraint fk_symposium_managers_person_id foreign key (person_id) references person(id),
    
    primary key (symposium_id, person_id)
);

create table symposium_courses
(
	symposium_id int not null,
    course_id int not null,
    
	constraint fk_symposium_courses_symposium_id foreign key (symposium_id) references symposium(id),
    constraint fk_symposium_courses_course_id foreign key (course_id) references course(id),
    
    primary key (symposium_id, course_id)
);

create table symposium_articles
(
	symposium_id int not null,
    article_id int not null,
	
    constraint fk_symposium_articles_symposium_id foreign key (symposium_id) references symposium(id),
    constraint fk_symposium_articles_article_id foreign key (article_id) references article(id),
    
    primary key (symposium_id, article_id)
);

create table course_audience
(
	course_id int not null,
    person_id int not null,
    
	constraint fk_course_audience_course_id foreign key (course_id) references course(id),
    constraint fk_course_audience_person_id foreign key (person_id) references person(id),
    
    primary key (course_id, person_id)
);

create table cientific_commision_board_members
(
	cientific_commision_id int not null,
    person_id int not null,
    
    constraint fk_cientific_commision_board_members_cientific_commision_id 
		foreign key (cientific_commision_id) references cientific_commision(id),
	constraint fk_cientific_commision_board_members_person_id
		foreign key (person_id) references person(id),
        
	primary key (cientific_commision_id, person_id)
);

create table cientific_commision_articles
(
	cientific_commision_id int not null,
    article_id int not null,
    
	constraint fk_cientific_commision_articles_cientific_commision_id 
		foreign key (cientific_commision_id) references cientific_commision(id),
	constraint fk_cientific_commision_articles_article_id
		foreign key (article_id) references article(id),
	
    primary key (cientific_commision_id, article_id)
);

create table article_presenters
(
	article_id int not null,
    person_id int not null,
    
    constraint fk_article_presenters_article_id foreign key (article_id) references article(id),
    constraint fk_article_presenters_person_id foreign key (person_id) references person(id),
    
    primary key (article_id, person_id)
);

create table article_audience
(
	article_id int not null,
    person_id int not null,
    
    constraint fk_article_audience_article_id foreign key (article_id) references article(id),
    constraint fk_article_audience_person_id foreign key (person_id) references person(id),
    
    primary key (article_id, person_id)
);