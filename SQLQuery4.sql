create proc sp_Insert
@empname varchar(20),
@empsal float
as
begin
insert into EmployeeTbl(empName,empSal)values(@empname,@empsal)
end
