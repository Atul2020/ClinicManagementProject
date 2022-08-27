create database ClinicManagement
use  ClinicManagement

--Creating the Users Table
create table Users(username varchar(10) unique constraint verify_username check(username not like '%[^a-zA-Z0-9]%'), 
firstname varchar(30),
lastname varchar(30),
password varchar(30) constraint verify_password check(password like '%@%'))

--Inserting default Users into the table
insert into Users values('Atulacc','Atul','Lakkapragada','07@atul')
insert into Users values('Dwayneacc','Dwayne','Johnson','11@Ddwayne')

select * from Users;

--Creating the Doctors Table
create table Doctors (doctor_id int primary key,
firstname varchar(30) constraint verify_firstname check(firstname not like '%[^a-zA-Z0-9]%'), 
lastname varchar(30) constraint verify_lastname check(lastname not like '%[^a-zA-Z0-9]%'), 
sex varchar(10),
specialization varchar(30),
visiting_hours_from time,
visiting_hours_to time)

--Inserting default Doctors into the table
insert into doctors values('10001','Rohith','Kumar','M','General','12:00','15:00')
insert into doctors values('10002','Jason','Jackson','M','Internal Medicine','13:30','17:30')
insert into doctors values('10003','Tony','Stark','M','Pediatrics','16:00','17:00')
insert into doctors values('10004','Amy','Shelly','F','Orthopedics','10:00','12:00')
insert into doctors values('10005','Amanda','Dolly','F','Ophthalmology','09:00','11:00')

select * from doctors;

--Creating the Patients Table
create table Patients (patient_id int identity(100,1) primary key,
firstname varchar(30) constraint verify_firstname_for_patient check(firstname not like '%[^a-zA-Z0-9]%'),
lastname varchar(30) constraint verify_lastname_for_patient check(lastname not like '%[^a-zA-Z0-9]%'),
sex varchar(10),
age int constraint check_age_patient check(age between 0 and 121),
dob date)

select * from patients

--Creating the Appointments table
create table Appointments (aptID int identity(200,1) primary key,
doctor_id int foreign key(doctor_id) references doctors(doctor_id),
visiting_date date,
slottime varchar(30),
apt_status varchar(30),
patient_id int foreign key references Patients(patient_id))

select * from Appointments

go
 
 --Stored Procedure to Insert records into Appointments table
create proc insertToAppointments
(
@doctor_id int,
@visiting_date date,
@slottime varchar(30),
@apt_status varchar(30),
@patient_id int)
as
insert into Appointments(doctor_id,visiting_date,slottime,apt_status,patient_id) values(@doctor_id,@visiting_date,@slottime,@apt_status,@patient_id)

--Inserting Default appointments for the next 10 days as the appointment can only be booked for the next 10 days
exec insertToAppointments 10001,'2022-08-29','12:00-13:00','free',Null
exec insertToAppointments 10001,'2022-08-29','13:00-14:00','free',Null
exec insertToAppointments 10001,'2022-08-29','14:00-15:00','free',Null
exec insertToAppointments 10002,'2022-08-29','13:30-14:30','free',Null
exec insertToAppointments 10002,'2022-08-29','14:30-15:30','free',Null
exec insertToAppointments 10002,'2022-08-29','15:30-16:30','free',Null
exec insertToAppointments 10002,'2022-08-29','16:30-17:30','free',Null
exec insertToAppointments 10003,'2022-08-29','16:00-17:00','free',Null
exec insertToAppointments 10004,'2022-08-29','10:00-11:00','free',Null
exec insertToAppointments 10004,'2022-08-29','11:00-12:00','free',Null
exec insertToAppointments 10005,'2022-08-29','09:00-10:00','free',Null
exec insertToAppointments 10005,'2022-08-29','10:00-11:00','free',Null

exec insertToAppointments 10001,'2022-08-30','12:00-13:00','free',Null
exec insertToAppointments 10001,'2022-08-30','13:00-14:00','free',Null
exec insertToAppointments 10001,'2022-08-30','14:00-15:00','free',Null
exec insertToAppointments 10002,'2022-08-30','13:30-14:30','free',Null
exec insertToAppointments 10002,'2022-08-30','14:30-15:30','free',Null
exec insertToAppointments 10002,'2022-08-30','15:30-16:30','free',Null
exec insertToAppointments 10002,'2022-08-30','16:30-17:30','free',Null
exec insertToAppointments 10003,'2022-08-30','16:00-17:00','free',Null
exec insertToAppointments 10004,'2022-08-30','10:00-11:00','free',Null
exec insertToAppointments 10004,'2022-08-30','11:00-12:00','free',Null
exec insertToAppointments 10005,'2022-08-30','09:00-10:00','free',Null
exec insertToAppointments 10005,'2022-08-30','10:00-11:00','free',Null

exec insertToAppointments 10001,'2022-08-31','12:00-13:00','free',Null
exec insertToAppointments 10001,'2022-08-31','13:00-14:00','free',Null
exec insertToAppointments 10001,'2022-08-31','14:00-15:00','free',Null
exec insertToAppointments 10002,'2022-08-31','13:30-14:30','free',Null
exec insertToAppointments 10002,'2022-08-31','14:30-15:30','free',Null
exec insertToAppointments 10002,'2022-08-31','15:30-16:30','free',Null
exec insertToAppointments 10002,'2022-08-31','16:30-17:30','free',Null
exec insertToAppointments 10003,'2022-08-31','16:00-17:00','free',Null
exec insertToAppointments 10004,'2022-08-31','10:00-11:00','free',Null
exec insertToAppointments 10004,'2022-08-31','11:00-12:00','free',Null
exec insertToAppointments 10005,'2022-08-31','09:00-10:00','free',Null
exec insertToAppointments 10005,'2022-08-31','10:00-11:00','free',Null

exec insertToAppointments 10001,'2022-09-01','12:00-13:00','free',Null
exec insertToAppointments 10001,'2022-09-01','13:00-14:00','free',Null
exec insertToAppointments 10001,'2022-09-01','14:00-15:00','free',Null
exec insertToAppointments 10002,'2022-09-01','13:30-14:30','free',Null
exec insertToAppointments 10002,'2022-09-01','14:30-15:30','free',Null
exec insertToAppointments 10002,'2022-09-01','15:30-16:30','free',Null
exec insertToAppointments 10002,'2022-09-01','16:30-17:30','free',Null
exec insertToAppointments 10003,'2022-09-01','16:00-17:00','free',Null
exec insertToAppointments 10004,'2022-09-01','10:00-11:00','free',Null
exec insertToAppointments 10004,'2022-09-01','11:00-12:00','free',Null
exec insertToAppointments 10005,'2022-09-01','09:00-10:00','free',Null
exec insertToAppointments 10005,'2022-09-01','10:00-11:00','free',Null

exec insertToAppointments 10001,'2022-09-02','12:00-13:00','free',Null
exec insertToAppointments 10001,'2022-09-02','13:00-14:00','free',Null
exec insertToAppointments 10001,'2022-09-02','14:00-15:00','free',Null
exec insertToAppointments 10002,'2022-09-02','13:30-14:30','free',Null
exec insertToAppointments 10002,'2022-09-02','14:30-15:30','free',Null
exec insertToAppointments 10002,'2022-09-02','15:30-16:30','free',Null
exec insertToAppointments 10002,'2022-09-02','16:30-17:30','free',Null
exec insertToAppointments 10003,'2022-09-02','16:00-17:00','free',Null
exec insertToAppointments 10004,'2022-09-02','10:00-11:00','free',Null
exec insertToAppointments 10004,'2022-09-02','11:00-12:00','free',Null
exec insertToAppointments 10005,'2022-09-02','09:00-10:00','free',Null
exec insertToAppointments 10005,'2022-09-02','10:00-11:00','free',Null

exec insertToAppointments 10001,'2022-09-03','12:00-13:00','free',Null
exec insertToAppointments 10001,'2022-09-03','13:00-14:00','free',Null
exec insertToAppointments 10001,'2022-09-03','14:00-15:00','free',Null
exec insertToAppointments 10002,'2022-09-03','13:30-14:30','free',Null
exec insertToAppointments 10002,'2022-09-03','14:30-15:30','free',Null
exec insertToAppointments 10002,'2022-09-03','15:30-16:30','free',Null
exec insertToAppointments 10002,'2022-09-03','16:30-17:30','free',Null
exec insertToAppointments 10003,'2022-09-03','16:00-17:00','free',Null
exec insertToAppointments 10004,'2022-09-03','10:00-11:00','free',Null
exec insertToAppointments 10004,'2022-09-03','11:00-12:00','free',Null
exec insertToAppointments 10005,'2022-09-03','09:00-10:00','free',Null
exec insertToAppointments 10005,'2022-09-03','10:00-11:00','free',Null

exec insertToAppointments 10001,'2022-09-04','12:00-13:00','free',Null
exec insertToAppointments 10001,'2022-09-04','13:00-14:00','free',Null
exec insertToAppointments 10001,'2022-09-04','14:00-15:00','free',Null
exec insertToAppointments 10002,'2022-09-04','13:30-14:30','free',Null
exec insertToAppointments 10002,'2022-09-04','14:30-15:30','free',Null
exec insertToAppointments 10002,'2022-09-04','15:30-16:30','free',Null
exec insertToAppointments 10002,'2022-09-04','16:30-17:30','free',Null
exec insertToAppointments 10003,'2022-09-04','16:00-17:00','free',Null
exec insertToAppointments 10004,'2022-09-04','10:00-11:00','free',Null
exec insertToAppointments 10004,'2022-09-04','11:00-12:00','free',Null
exec insertToAppointments 10005,'2022-09-04','09:00-10:00','free',Null
exec insertToAppointments 10005,'2022-09-04','10:00-11:00','free',Null

exec insertToAppointments 10001,'2022-09-05','12:00-13:00','free',Null
exec insertToAppointments 10001,'2022-09-05','13:00-14:00','free',Null
exec insertToAppointments 10001,'2022-09-05','14:00-15:00','free',Null
exec insertToAppointments 10002,'2022-09-05','13:30-14:30','free',Null
exec insertToAppointments 10002,'2022-09-05','14:30-15:30','free',Null
exec insertToAppointments 10002,'2022-09-05','15:30-16:30','free',Null
exec insertToAppointments 10002,'2022-09-05','16:30-17:30','free',Null
exec insertToAppointments 10003,'2022-09-05','16:00-17:00','free',Null
exec insertToAppointments 10004,'2022-09-05','10:00-11:00','free',Null
exec insertToAppointments 10004,'2022-09-05','11:00-12:00','free',Null
exec insertToAppointments 10005,'2022-09-05','09:00-10:00','free',Null
exec insertToAppointments 10005,'2022-09-05','10:00-11:00','free',Null

exec insertToAppointments 10001,'2022-09-06','12:00-13:00','free',Null
exec insertToAppointments 10001,'2022-09-06','13:00-14:00','free',Null
exec insertToAppointments 10001,'2022-09-06','14:00-15:00','free',Null
exec insertToAppointments 10002,'2022-09-06','13:30-14:30','free',Null
exec insertToAppointments 10002,'2022-09-06','14:30-15:30','free',Null
exec insertToAppointments 10002,'2022-09-06','15:30-16:30','free',Null
exec insertToAppointments 10002,'2022-09-06','16:30-17:30','free',Null
exec insertToAppointments 10003,'2022-09-06','16:00-17:00','free',Null
exec insertToAppointments 10004,'2022-09-06','10:00-11:00','free',Null
exec insertToAppointments 10004,'2022-09-06','11:00-12:00','free',Null
exec insertToAppointments 10005,'2022-09-06','09:00-10:00','free',Null
exec insertToAppointments 10005,'2022-09-06','10:00-11:00','free',Null

exec insertToAppointments 10001,'2022-09-07','12:00-13:00','free',Null
exec insertToAppointments 10001,'2022-09-07','13:00-14:00','free',Null
exec insertToAppointments 10001,'2022-09-07','14:00-15:00','free',Null
exec insertToAppointments 10002,'2022-09-07','13:30-14:30','free',Null
exec insertToAppointments 10002,'2022-09-07','14:30-15:30','free',Null
exec insertToAppointments 10002,'2022-09-07','15:30-16:30','free',Null
exec insertToAppointments 10002,'2022-09-07','16:30-17:30','free',Null
exec insertToAppointments 10003,'2022-09-07','16:00-17:00','free',Null
exec insertToAppointments 10004,'2022-09-07','10:00-11:00','free',Null
exec insertToAppointments 10004,'2022-09-07','11:00-12:00','free',Null
exec insertToAppointments 10005,'2022-09-07','09:00-10:00','free',Null
exec insertToAppointments 10005,'2022-09-07','10:00-11:00','free',Null