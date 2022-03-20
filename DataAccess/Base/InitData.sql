Insert into Category (Code, Name) values (101, 'Book');
Insert into Category (Code, Name) values (102, 'Magazine');
Insert into Category (Code, Name) values (103, 'DVD');

Insert into Item 
(Id, Name, CategoryCode, Author, Description) 
values 
(
1, 'Alice in Wonderland', 101, 'Lewis Carroll', 
'One of the best-known works of Victorian literature, its narrative, structure, characters and imagery have had huge influence on popular culture and literature, especially in the fantasy genre.[1][2] The book has never been out of print and has been translated into 174 languages. Its legacy covers adaptations for screen, radio, art, ballet, opera, musicals, theme parks, board games and video games.[3] Carroll published a sequel in 1871 entitled Through the Looking-Glass and a shortened version for young children, The Nursery "Alice", in 1890.' 
);

Insert into Item 
(Id, Name, CategoryCode, Author, Description) 
values 
(
2, 'Harry Potter', 101, 'J. K. Rowling',
'The novels chronicle the lives of a young wizard, Harry Potter, and his friends Hermione Granger and Ron Weasley, all of whom are students at Hogwarts School of Witchcraft and Wizardry. The main story arc concerns Harry''s struggle against Lord Voldemort, a dark wizard who intends to become immortal, overthrow the wizard governing body known as the Ministry of Magic and subjugate all wizards and Muggles (non-magical people).'
);
