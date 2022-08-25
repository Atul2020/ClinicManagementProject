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

insert into doctors values('123456','Rohith','Kumar','M','General','12:00','3:00')
insert into doctors values('456789','Jason','Jackson','M','Internal Medicine','1:30','5:30')
insert into doctors values('352345','Tony','Stark','M','Pediatrics','16:00','17:00')
insert into doctors values('897653','Amy','Shelly','F','Orthopedics','10:00','12:00')
insert into doctors values('237856','Amanda','Dolly','F','Ophthalmology','09:00','11:00')

select * from doctors;


create table Patients (patient_id int primary key, firstname varchar(30) constraint check_firstname_for_patient check(firstname not like '%[^a-zA-Z0-9]%'),
lastname varchar(30) constraint verify_lastname_for_patient check(lastname not like '%[^a-zA-Z0-9]%'), sex varchar(10),
age int constraint check_age_patient check(age between 0 and 121), dob date)

select * from patients