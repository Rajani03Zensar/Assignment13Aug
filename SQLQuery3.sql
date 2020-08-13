create proc sp_SearchEmp
@empid int
as
begin
select empName,empSal from EmployeeTbl where empId=@empid
end

select * from BookTable