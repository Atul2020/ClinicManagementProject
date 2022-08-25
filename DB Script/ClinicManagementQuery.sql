create database ClinicManagement
use  ClinicManagement

create table Users(username varchar(10) unique constraint verify_username check(username not like '%[^a-zA-Z0-9]%') 
, firstname varchar(30),lastname varchar(30),password varchar(30) constraint verify_password check(password like '%@%'))

insert into Users values('Atulacc','Atul','Lakkapragada','07@atul')
insert into Users values('Deepakacc','Deepak','Kumar','11@deepak')
insert into Users values('Santoacc','Santo','Brighton','19@santo')
insert into Users values('Basidcc','Basid','Mohammed','29@basid')

select * from Users;
truncate table users

create table Doctors (doctor_id int primary key, firstname varchar(30) constraint verify_firstname check(firstname not like '%[^a-zA-Z0-9]%'), 
lastname varchar(30) constraint verify_lastname check(lastname not like '%[^a-zA-Z0-9]%'), 
sex varchar(10), specialization varchar(30), visiting_hours_from time,visiting_hours_to time)
