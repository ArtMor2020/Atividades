-- Creating symposium

insert into symposium ( id, title, info ) values ( 1, 'Simposio de Tecnologia', 'Descrição' );

-- Addign people

insert into person ( name, birthdate ) values ( 'Joao', '12 05 1958' );
insert into person ( name, birthdate ) values ( 'Pedro', '12 05 1589' );
insert into person ( name, birthdate ) values ( 'Alexandre', '12 05 2003' );
insert into person ( name, birthdate ) values ( 'Alissa', '12 05 1955' );
insert into person ( name, birthdate ) values ( 'Maria', '12 05 1988' );
insert into person ( name, birthdate ) values ( 'Mario', '12 05 1952' );
insert into person ( name, birthdate ) values ( 'Joana', '12 05 1978' );
insert into person ( name, birthdate ) values ( 'Bianca', '12 05 1998' );
insert into person ( name, birthdate ) values ( 'Marco', '12 05 1928' );
insert into person ( name, birthdate ) values ( 'Paula', '12 05 1988' );

-- Creating courses

insert into course ( id, course, info, presenter_id ) values ( 1, 'Computação - Basica', 'Descrição basica', 1 );
insert into course ( id, course, info, presenter_id ) values ( 2, 'Computação- Avancada', 'Descrição avancada', 2 );

-- Adding courses to symposium

insert into symposium_courses ( symposium_id, course_id ) values ( 1, 1 );
insert into symposium_courses ( symposium_id, course_id ) values ( 1, 2 );

-- Creating themes

insert into theme ( id, theme ) values ( 1, 'Artificial Inteligence' );
insert into theme ( id, theme ) values ( 2, 'Hardware' );
insert into theme ( id, theme ) values ( 3, 'Software' );

-- Creating cientific commisions

insert into cientific_commision ( id, commision_name, theme_id ) values ( 1, 'Commision of AI', 1 );
insert into cientific_commision ( id, commision_name, theme_id ) values ( 2, 'Commision of Hardware', 2 );
insert into cientific_commision ( id, commision_name, theme_id ) values ( 3, 'Commision of Software', 3 );

-- Creating Articles 

insert into article ( id, article, info, theme_id ) values ( 1, 'History of AI', 'Info of History of AI', 1 );
insert into article ( id, article, info, theme_id ) values ( 2, 'AI Today', 'Info of AI Today', 1 );
insert into article ( id, article, info, theme_id ) values ( 3, 'History of Hardware', 'Info of History of Hardware', 2 );
insert into article ( id, article, info, theme_id ) values ( 4, 'Hardware Today', 'Info of Hardware Today', 2 );
insert into article ( id, article, info, theme_id ) values ( 5, 'Software Today', 'Info of Software Today', 3 );

-- Adding articles to symposium

insert into symposium_articles ( symposium_id, article_id ) values ( 1, 1 );
insert into symposium_articles ( symposium_id, article_id ) values ( 1, 2 );
insert into symposium_articles ( symposium_id, article_id ) values ( 1, 3 );
insert into symposium_articles ( symposium_id, article_id ) values ( 1, 4 );
insert into symposium_articles ( symposium_id, article_id ) values ( 1, 5 );

-- Adding article presentations

insert into article_presentation ( article_id, info ) values ( 1, 'Presentation of History of AI');
insert into article_presentation ( article_id, info ) values ( 2, 'Presentation of AI Today');
insert into article_presentation ( article_id, info ) values ( 3, 'Presentation of History of Hardware');
insert into article_presentation ( article_id, info ) values ( 4, 'Presentation of Hardware Today');
insert into article_presentation ( article_id, info ) values ( 5, 'Presentation of Software Today');

-- Adding articles to cientific commision

insert into cientific_commision_articles ( cientific_commision_id, article_id ) values ( 1, 1 );
insert into cientific_commision_articles ( cientific_commision_id, article_id ) values ( 1, 2 );

insert into cientific_commision_articles ( cientific_commision_id, article_id ) values ( 2, 3 );
insert into cientific_commision_articles ( cientific_commision_id, article_id ) values ( 2, 4 );

insert into cientific_commision_articles ( cientific_commision_id, article_id ) values ( 3, 5 );

-- Adding article presenters

insert into article_presenters ( article_id, person_id ) values ( 1, 1);
insert into article_presenters ( article_id, person_id ) values ( 2, 2);
insert into article_presenters ( article_id, person_id ) values ( 3, 3);
insert into article_presenters ( article_id, person_id ) values ( 4, 4);
insert into article_presenters ( article_id, person_id ) values ( 5, 5);

-- Adding managers to symposium

insert into symposium_managers ( symposium_id, person_id ) values ( 1, 1);
insert into symposium_managers ( symposium_id, person_id ) values ( 1, 2);
insert into symposium_managers ( symposium_id, person_id ) values ( 1, 3);

-- Adding board members to cientific commisions

insert into cientific_commision_board_members (	cientific_commision_id, person_id ) values (1, 1);
insert into cientific_commision_board_members (	cientific_commision_id, person_id ) values (1, 2);
insert into cientific_commision_board_members (	cientific_commision_id, person_id ) values (1, 3);

insert into cientific_commision_board_members (	cientific_commision_id, person_id ) values (2, 4);
insert into cientific_commision_board_members (	cientific_commision_id, person_id ) values (2, 5);
insert into cientific_commision_board_members (	cientific_commision_id, person_id ) values (2, 6);

insert into cientific_commision_board_members (	cientific_commision_id, person_id ) values (3, 7);
insert into cientific_commision_board_members (	cientific_commision_id, person_id ) values (3, 8);
insert into cientific_commision_board_members (	cientific_commision_id, person_id ) values (3, 9);

-- Adding audience to courses

insert into course_audience ( course_id, person_id ) values ( 1, 1);
insert into course_audience ( course_id, person_id ) values ( 1, 2);
insert into course_audience ( course_id, person_id ) values ( 1, 3);
insert into course_audience ( course_id, person_id ) values ( 1, 4);
insert into course_audience ( course_id, person_id ) values ( 1, 5);

insert into course_audience ( course_id, person_id ) values ( 2, 6);
insert into course_audience ( course_id, person_id ) values ( 2, 7);
insert into course_audience ( course_id, person_id ) values ( 2, 8);
insert into course_audience ( course_id, person_id ) values ( 2, 9);
insert into course_audience ( course_id, person_id ) values ( 2, 10);

-- adding audience to articles

insert into article_presentation_audience ( article_presentation_id, person_id ) values (1, 1);
insert into article_presentation_audience ( article_presentation_id, person_id ) values (1, 2);
insert into article_presentation_audience ( article_presentation_id, person_id ) values (1, 3);

insert into article_presentation_audience ( article_presentation_id, person_id ) values (2, 2);
insert into article_presentation_audience ( article_presentation_id, person_id ) values (2, 3);
insert into article_presentation_audience ( article_presentation_id, person_id ) values (2, 4);

insert into article_presentation_audience ( article_presentation_id, person_id ) values (3, 3);
insert into article_presentation_audience ( article_presentation_id, person_id ) values (3, 4);
insert into article_presentation_audience ( article_presentation_id, person_id ) values (3, 5);

insert into article_presentation_audience ( article_presentation_id, person_id ) values (4, 4);
insert into article_presentation_audience ( article_presentation_id, person_id ) values (4, 5);
insert into article_presentation_audience ( article_presentation_id, person_id ) values (4, 6);

insert into article_presentation_audience ( article_presentation_id, person_id ) values (5, 5);
insert into article_presentation_audience ( article_presentation_id, person_id ) values (5, 6);
insert into article_presentation_audience ( article_presentation_id, person_id ) values (5, 7);

-- Adding articles to approval list

insert into article_commision_aproval_list ( cientific_commision_id, article_id, is_aproved) values ( 1, 1, 1);
insert into article_commision_aproval_list ( cientific_commision_id, article_id, is_aproved) values ( 1, 2, 1);
insert into article_commision_aproval_list ( cientific_commision_id, article_id, is_aproved) values ( 2, 3, 1);
insert into article_commision_aproval_list ( cientific_commision_id, article_id, is_aproved) values ( 2, 4, 1);
insert into article_commision_aproval_list ( cientific_commision_id, article_id, is_aproved) values ( 3, 5, 1);