alter proc sp_DeleteEmp
@empid int
as 
begin 
delete from EmployeeTbl where empId=@empid
end