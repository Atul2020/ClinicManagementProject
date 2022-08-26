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

insert into doctors values('10001','Rohith','Kumar','M','General','12:00','15:00')
insert into doctors values('10002','Jason','Jackson','M','Internal Medicine','13:30','17:30')
insert into doctors values('10003','Tony','Stark','M','Pediatrics','16:00','17:00')
insert into doctors values('10004','Amy','Shelly','F','Orthopedics','10:00','12:00')
insert into doctors values('10005','Amanda','Dolly','F','Ophthalmology','09:00','11:00')

select * from doctors;


create table Patients (patient_id int identity(100,1) primary key, firstname varchar(30) constraint verify_firstname_for_patient check(firstname not like '%[^a-zA-Z0-9]%'),
lastname varchar(30) constraint verify_lastname_for_patient check(lastname not like '%[^a-zA-Z0-9]%'), sex varchar(10),
age int constraint check_age_patient check(age between 0 and 121), dob date)

select * from patients

insert into patients values(1,'Danny','Rose','M',20,'2000/05/12')

create table Appointments (aptID int identity(200,1) primary key,doctor_id int foreign key(doctor_id) 
references doctors(doctor_id),visiting_date date,slottime varchar(30),apt_status varchar(30),patient_id int foreign key references Patients(patient_id));

select * from Appointments


insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10001,'2022-08-26','12:00-13:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10001,'2022-08-26','13:00-14:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10001,'2022-08-26','14:00-15:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10001,'2022-08-27','12:00-13:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10001,'2022-08-27','13:00-14:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10001,'2022-08-27','14:00-15:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10001,'2022-08-28','12:00-13:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10001,'2022-08-28','13:00-14:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10001,'2022-08-28','14:00-15:00','free',Null)


insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10002,'2022-08-26','13:30-14:30','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10002,'2022-08-26','14:30-15:30','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10002,'2022-08-26','15:30-16:30','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10002,'2022-08-26','16:30-17:30','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10002,'2022-08-27','13:30-14:30','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10002,'2022-08-27','14:30-15:30','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10002,'2022-08-27','15:30-16:30','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10002,'2022-08-27','16:30-17:30','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10002,'2022-08-28','13:30-14:30','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10002,'2022-08-28','14:30-15:30','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10002,'2022-08-28','15:30-16:30','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10002,'2022-08-28','16:30-17:30','free',Null)

insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10003,'2022-08-26','16:00-17:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10003,'2022-08-27','16:00-17:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10003,'2022-08-28','16:00-17:00','free',Null)

insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10004,'2022-08-26','10:00-11:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10004,'2022-08-26','11:00-12:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10004,'2022-08-27','10:00-11:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10004,'2022-08-27','11:00-12:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10004,'2022-08-28','10:00-11:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10004,'2022-08-28','11:00-12:00','free',Null)

insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10005,'2022-08-26','11:00-12:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10005,'2022-08-26','11:00-12:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10005,'2022-08-27','11:00-12:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10005,'2022-08-27','11:00-12:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10005,'2022-08-28','11:00-12:00','free',Null)
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(10005,'2022-08-28','11:00-12:00','free',Null)


