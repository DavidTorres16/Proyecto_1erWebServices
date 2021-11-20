create schema ejercicioCarlos;

use ejercicioCarlos;

create table Client(
	id int not null primary key auto_increment,
    name varchar(30),
    last_name varchar(30),
    bill_id int
);

create table client_bill(
	id int not null auto_increment primary key,
    client_id int,
    cod int,
    foreign key (client_id) references Client(id)
);

create table bill(
	id int not null auto_increment primary key,
    client_bill_id int,
    description varchar(250),
    value int,
	foreign key (client_bill_id) references client_bill(id)
);


