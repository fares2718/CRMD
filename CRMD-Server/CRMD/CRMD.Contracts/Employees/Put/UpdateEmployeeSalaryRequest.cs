namespace CRMD.Contracts.Employees.Put;

public record UpdateEmployeeSalaryRequest(int Id, decimal newSalary);
