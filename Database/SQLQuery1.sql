Create database test

use test
create table LoginDetails(Username varchar(20) Primary key , Password varchar(20))  

insert into LoginDetails values ('jagath','123')
insert into LoginDetails values ('chandra','123')

insert into LoginDetails values('Akhil','123')
insert into LoginDetails values('Shravya','123')
insert into LoginDetails values('Sai','123')
insert into LoginDetails values('Avi','123')
insert into LoginDetails values('Sri','123')
insert into LoginDetails values('Abi','123')
insert into LoginDetails values('Anu','123')
insert into LoginDetails values('Aki','123')
insert into LoginDetails values('vijay','123')


select * from LoginDetails

create table ProcessDetails(Name varchar(20) foreign key references LoginDetails(Username),
ContactNumber varchar(10) primary key,
CreditCardNumber bigint,
ComponentType varchar(30),
ComponentName varchar(30),
Quantity int,
IsPriorityRequest varchar(10),
RequestId int identity(001,1),
ProcessingCharge float,
PackagingAndDeliveryCharge float,
DateofDelivery date);

insert into ProcessDetails values('jagath','1234567890',1234567890123,'Integral','Laptop',12,'Yes',500,345,'2021-11-21')

insert into ProcessDetails values('chandra','9112345678',2345678901231234,'Accessory','Watch',10,'No',300,565,'2021-04-2')
insert into ProcessDetails values('Akhil','9212345678',1234567890123789097,'Integral','Mobile',3,'Yes',200,235,'2021-05-12')
insert into ProcessDetails values('Shravya','8901234567',1234567890123789,'Integral','Powerbank',6,'No',400,985,'2021-06-11')
insert into ProcessDetails values('Sai','8911234560',1234567890123234,'Integral','Desktop',7,'Yes',900,900,'2021-07-05')
insert into ProcessDetails values('Avi','9912345671',3456734567890123,'Accessory','Earpods',12,'No',300,300,'2021-08-08')
insert into ProcessDetails values('Sri','9234567893',8765456567890123,'Integral','Washing Machine',4,'Yes',700,235,'2021-09-23')
insert into ProcessDetails values('Abi','8834567890',8567578167890123,'Integral','Television',9,'No',800,285,'2021-10-30')
insert into ProcessDetails values('Anu','8794567898',456892367890123,'Integral','Speakers',20,'Yes',200,345,'2021-12-13')
insert into ProcessDetails values('Aki','8978567899',987451567890123,'Accessory','Laptop',3,'No',100,130,'2021-06-15')

insert into ProcessDetails(Name,ContactNumber,CreditCardNumber,ComponentType,ComponentName,Quantity,IsPriorityRequest) values('vijay','9876543210' , 7654321098987321,'Integral','Cooler',2,'No')

delete from ProcessDetails where Name='jagath'

select * from ProcessDetails



