create proc sp_Update
@empid int,
@empname varchar(20),
@empsal float
as 
begin
update EmployeeTbl set empName=@empname,empSal=@empsal where empId=@empid
end