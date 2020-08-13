create database DontNetTraining
use DontNetTraining

create table EmployeeTbl(
   empId int primary key identity (1,1),
   empName varchar(20),
   empSal float
);
insert into EmployeeTbl values('Rajani',24655.98)
insert into EmployeeTbl values('Shreya',27888.98)
insert into EmployeeTbl values('Shriya',55666.98)
insert into EmployeeTbl values('Ratnesh',65751.98)
insert into EmployeeTbl values('Ravi',65323.98)

select * from EmployeeTbl