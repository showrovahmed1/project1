using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleEmployeeManagementSystem
{
    // Employee Class
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public int VacationDays { get; set; } = 20; // Default vacation days
        public bool IsActive { get; set; } = true;

        public void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Position: {Position}");
            Console.WriteLine($"Department: {Department}");
            Console.WriteLine($"Salary: ${Salary}");
            Console.WriteLine($"Hire Date: {HireDate:yyyy-MM-dd}");
            Console.WriteLine($"Vacation Days: {VacationDays}");
            Console.WriteLine($"Status: {(IsActive ? "Active" : "Inactive")}");
            Console.WriteLine("-----------------------------------");
        }
    }

    // Payroll Class
    class Payroll
    {
        public int PayrollId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime PayDate { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Overtime { get; set; }
        public decimal Bonus { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetSalary => BasicSalary + Overtime + Bonus - Deductions;
    }

    // Vacation Request Class
    class VacationRequest
    {
        public int RequestId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; } = "Pending";
        public int Days => (EndDate - StartDate).Days + 1;
    }

    // Main Program
    class Program
    {
        // Data Storage
        private static List<Employee> employees = new List<Employee>();
        private static List<Payroll> payrolls = new List<Payroll>();
        private static List<VacationRequest> vacationRequests = new List<VacationRequest>();
        private static int nextEmployeeId = 1;
        private static int nextPayrollId = 1;
        private static int nextVacationId = 1;

        static void Main(string[] args)
        {
            // Add some sample data
            InitializeSampleData();

            Console.Title = "Employee Management System";
            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("══════════════════════════════════════");
                Console.WriteLine("     EMPLOYEE MANAGEMENT SYSTEM");
                Console.WriteLine("══════════════════════════════════════");
                Console.WriteLine("1. Employee Management");
                Console.WriteLine("2. Payroll Management");
                Console.WriteLine("3. Vacation Management");
                Console.WriteLine("4. Exit");
                Console.WriteLine("══════════════════════════════════════");
                Console.Write("Select option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        EmployeeMenu();
                        break;
                    case "2":
                        PayrollMenu();
                        break;
                    case "3":
                        VacationMenu();
                        break;
                    case "4":
                        Console.WriteLine("\nThank you for using EMS!");
                        return;
                    default:
                        Console.WriteLine("Invalid option! Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Employee Management Menu
        static void EmployeeMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("══════════════════════════════════════");
                Console.WriteLine("       EMPLOYEE MANAGEMENT");
                Console.WriteLine("══════════════════════════════════════");
                Console.WriteLine("1. Add New Employee");
                Console.WriteLine("2. Update Employee");
                Console.WriteLine("3. Delete Employee");
                Console.WriteLine("4. Search Employee");
                Console.WriteLine("5. Display All Employees");
                Console.WriteLine("6. Add Vacation Days");
                Console.WriteLine("7. Back to Main Menu");
                Console.WriteLine("══════════════════════════════════════");
                Console.Write("Select option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        UpdateEmployee();
                        break;
                    case "3":
                        DeleteEmployee();
                        break;
                    case "4":
                        SearchEmployee();
                        break;
                    case "5":
                        DisplayAllEmployees();
                        break;
                    case "6":
                        AddVacationDays();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        // Payroll Management Menu
        static void PayrollMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("══════════════════════════════════════");
                Console.WriteLine("       PAYROLL MANAGEMENT");
                Console.WriteLine("══════════════════════════════════════");
                Console.WriteLine("1. Process Payroll");
                Console.WriteLine("2. View Payroll History");
                Console.WriteLine("3. Back to Main Menu");
                Console.WriteLine("══════════════════════════════════════");
                Console.Write("Select option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ProcessPayroll();
                        break;
                    case "2":
                        ViewPayrollHistory();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        // Vacation Management Menu
        static void VacationMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("══════════════════════════════════════");
                Console.WriteLine("       VACATION MANAGEMENT");
                Console.WriteLine("══════════════════════════════════════");
                Console.WriteLine("1. Request Vacation");
                Console.WriteLine("2. View Vacation Requests");
                Console.WriteLine("3. Approve/Reject Vacation");
                Console.WriteLine("4. Back to Main Menu");
                Console.WriteLine("══════════════════════════════════════");
                Console.Write("Select option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RequestVacation();
                        break;
                    case "2":
                        ViewVacationRequests();
                        break;
                    case "3":
                        ApproveRejectVacation();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        // Employee Operations
        static void AddEmployee()
        {
            Console.Clear();
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("         ADD NEW EMPLOYEE");
            Console.WriteLine("══════════════════════════════════════");

            try
            {
                Employee emp = new Employee();
                emp.Id = nextEmployeeId++;

                Console.Write("Enter Name: ");
                emp.Name = Console.ReadLine();

                Console.Write("Enter Position: ");
                emp.Position = Console.ReadLine();

                Console.Write("Enter Department: ");
                emp.Department = Console.ReadLine();

                Console.Write("Enter Salary: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal salary))
                    emp.Salary = salary;
                else
                {
                    Console.WriteLine("Invalid salary!");
                    return;
                }

                Console.Write("Enter Hire Date (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime hireDate))
                    emp.HireDate = hireDate;
                else
                {
                    Console.WriteLine("Invalid date!");
                    return;
                }

                employees.Add(emp);
                Console.WriteLine($"\n✓ Employee added successfully! ID: {emp.Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void UpdateEmployee()
        {
            Console.Clear();
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("        UPDATE EMPLOYEE");
            Console.WriteLine("══════════════════════════════════════");

            Console.Write("Enter Employee ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var emp = employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    Console.WriteLine("\nCurrent Details:");
                    emp.DisplayInfo();

                    Console.WriteLine("\nEnter new details (press Enter to skip):");

                    Console.Write($"Name [{emp.Name}]: ");
                    var name = Console.ReadLine();
                    if (!string.IsNullOrEmpty(name)) emp.Name = name;

                    Console.Write($"Position [{emp.Position}]: ");
                    var position = Console.ReadLine();
                    if (!string.IsNullOrEmpty(position)) emp.Position = position;

                    Console.Write($"Department [{emp.Department}]: ");
                    var dept = Console.ReadLine();
                    if (!string.IsNullOrEmpty(dept)) emp.Department = dept;

                    Console.Write($"Salary [{emp.Salary}]: ");
                    var salary = Console.ReadLine();
                    if (!string.IsNullOrEmpty(salary) && decimal.TryParse(salary, out decimal newSalary))
                        emp.Salary = newSalary;

                    Console.WriteLine("\n✓ Employee updated successfully!");
                }
                else
                {
                    Console.WriteLine("Employee not found!");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID!");
            }
        }

        static void DeleteEmployee()
        {
            Console.Clear();
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("        DELETE EMPLOYEE");
            Console.WriteLine("══════════════════════════════════════");

            Console.Write("Enter Employee ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var emp = employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    Console.WriteLine("\nEmployee Details:");
                    emp.DisplayInfo();

                    Console.Write("\nAre you sure? (Y/N): ");
                    if (Console.ReadLine().ToUpper() == "Y")
                    {
                        emp.IsActive = false; // Soft delete
                        Console.WriteLine("✓ Employee marked as inactive!");
                    }
                    else
                    {
                        Console.WriteLine("Operation cancelled.");
                    }
                }
                else
                {
                    Console.WriteLine("Employee not found!");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID!");
            }
        }

        static void SearchEmployee()
        {
            Console.Clear();
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("        SEARCH EMPLOYEE");
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("1. Search by ID");
            Console.WriteLine("2. Search by Name");
            Console.WriteLine("══════════════════════════════════════");
            Console.Write("Select option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter Employee ID: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        var emp = employees.FirstOrDefault(e => e.Id == id);
                        if (emp != null)
                        {
                            Console.WriteLine("\nSearch Result:");
                            emp.DisplayInfo();
                        }
                        else
                        {
                            Console.WriteLine("Employee not found!");
                        }
                    }
                    break;

                case "2":
                    Console.Write("Enter Name (or part of name): ");
                    string searchName = Console.ReadLine().ToLower();
                    var results = employees.Where(e => e.Name.ToLower().Contains(searchName)).ToList();
                    
                    if (results.Any())
                    {
                        Console.WriteLine($"\nFound {results.Count} employee(s):");
                        foreach (var emp in results)
                        {
                            emp.DisplayInfo();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No employees found!");
                    }
                    break;

                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }

        static void DisplayAllEmployees()
        {
            Console.Clear();
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("        ALL EMPLOYEES");
            Console.WriteLine("══════════════════════════════════════");

            if (!employees.Any(e => e.IsActive))
            {
                Console.WriteLine("No active employees found!");
                return;
            }

            var activeEmployees = employees.Where(e => e.IsActive).ToList();
            
            Console.WriteLine($"Total Active Employees: {activeEmployees.Count}\n");
            Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-15} {4,-10} {5,-10}", 
                "ID", "Name", "Department", "Position", "Salary", "Vacation Days");
            Console.WriteLine(new string('-', 80));

            foreach (var emp in activeEmployees)
            {
                Console.WriteLine("{0,-5} {1,-20} {2,-15} {3,-15} {4,-10:C} {5,-10}", 
                    emp.Id, 
                    emp.Name, 
                    emp.Department, 
                    emp.Position, 
                    emp.Salary, 
                    emp.VacationDays);
            }
        }

        static void AddVacationDays()
        {
            Console.Clear();
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("        ADD VACATION DAYS");
            Console.WriteLine("══════════════════════════════════════");

            Console.Write("Enter Employee ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var emp = employees.FirstOrDefault(e => e.Id == id);
                if (emp != null)
                {
                    Console.WriteLine($"\nEmployee: {emp.Name}");
                    Console.WriteLine($"Current Vacation Days: {emp.VacationDays}");
                    
                    Console.Write("\nEnter days to add: ");
                    if (int.TryParse(Console.ReadLine(), out int days) && days > 0)
                    {
                        emp.VacationDays += days;
                        Console.WriteLine($"\n✓ Added {days} vacation days!");
                        Console.WriteLine($"New total: {emp.VacationDays} days");
                    }
                    else
                    {
                        Console.WriteLine("Invalid number of days!");
                    }
                }
                else
                {
                    Console.WriteLine("Employee not found!");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID!");
            }
        }

        // Payroll Operations
        static void ProcessPayroll()
        {
            Console.Clear();
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("        PROCESS PAYROLL");
            Console.WriteLine("══════════════════════════════════════");

            Console.Write("Enter Employee ID: ");
            if (int.TryParse(Console.ReadLine(), out int empId))
            {
                var emp = employees.FirstOrDefault(e => e.Id == empId);
                if (emp != null && emp.IsActive)
                {
                    Payroll payroll = new Payroll
                    {
                        PayrollId = nextPayrollId++,
                        EmployeeId = emp.Id,
                        EmployeeName = emp.Name,
                        PayDate = DateTime.Now,
                        BasicSalary = emp.Salary
                    };

                    Console.WriteLine($"\nEmployee: {emp.Name}");
                    Console.WriteLine($"Basic Salary: ${emp.Salary}");

                    Console.Write("Enter Overtime Amount: $");
                    if (decimal.TryParse(Console.ReadLine(), out decimal overtime))
                        payroll.Overtime = overtime;

                    Console.Write("Enter Bonus Amount: $");
                    if (decimal.TryParse(Console.ReadLine(), out decimal bonus))
                        payroll.Bonus = bonus;

                    Console.Write("Enter Deductions Amount: $");
                    if (decimal.TryParse(Console.ReadLine(), out decimal deductions))
                        payroll.Deductions = deductions;

                    payrolls.Add(payroll);

                    Console.WriteLine("\n══════════════════════════════════════");
                    Console.WriteLine("           PAYROLL SLIP");
                    Console.WriteLine("══════════════════════════════════════");
                    Console.WriteLine($"Employee: {payroll.EmployeeName}");
                    Console.WriteLine($"Pay Date: {payroll.PayDate:yyyy-MM-dd}");
                    Console.WriteLine($"Basic Salary: ${payroll.BasicSalary}");
                    Console.WriteLine($"Overtime: ${payroll.Overtime}");
                    Console.WriteLine($"Bonus: ${payroll.Bonus}");
                    Console.WriteLine($"Deductions: ${payroll.Deductions}");
                    Console.WriteLine($"Net Salary: ${payroll.NetSalary}");
                    Console.WriteLine("══════════════════════════════════════");
                    Console.WriteLine("\n✓ Payroll processed successfully!");
                }
                else
                {
                    Console.WriteLine("Employee not found or inactive!");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID!");
            }
        }

        static void ViewPayrollHistory()
        {
            Console.Clear();
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("        PAYROLL HISTORY");
            Console.WriteLine("══════════════════════════════════════");

            if (!payrolls.Any())
            {
                Console.WriteLine("No payroll records found!");
                return;
            }

            Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-15} {4,-15}", 
                "Pay ID", "Employee", "Pay Date", "Basic Salary", "Net Salary");
            Console.WriteLine(new string('-', 80));

            foreach (var payroll in payrolls.OrderByDescending(p => p.PayDate))
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-15:yyyy-MM-dd} {3,-15:C} {4,-15:C}", 
                    payroll.PayrollId, 
                    payroll.EmployeeName, 
                    payroll.PayDate, 
                    payroll.BasicSalary, 
                    payroll.NetSalary);
            }

            Console.WriteLine("\n══════════════════════════════════════");
            Console.WriteLine($"Total Records: {payrolls.Count}");
            Console.WriteLine($"Total Paid: ${payrolls.Sum(p => p.NetSalary):C}");
        }

        // Vacation Operations
        static void RequestVacation()
        {
            Console.Clear();
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("        REQUEST VACATION");
            Console.WriteLine("══════════════════════════════════════");

            Console.Write("Enter Employee ID: ");
            if (int.TryParse(Console.ReadLine(), out int empId))
            {
                var emp = employees.FirstOrDefault(e => e.Id == empId);
                if (emp != null && emp.IsActive)
                {
                    Console.WriteLine($"\nEmployee: {emp.Name}");
                    Console.WriteLine($"Available Vacation Days: {emp.VacationDays}");

                    VacationRequest request = new VacationRequest
                    {
                        RequestId = nextVacationId++,
                        EmployeeId = emp.Id,
                        EmployeeName = emp.Name
                    };

                    Console.Write("\nEnter Start Date (YYYY-MM-DD): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
                        request.StartDate = startDate;
                    else
                    {
                        Console.WriteLine("Invalid date!");
                        return;
                    }

                    Console.Write("Enter End Date (YYYY-MM-DD): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                        request.EndDate = endDate;
                    else
                    {
                        Console.WriteLine("Invalid date!");
                        return;
                    }

                    if (request.Days > emp.VacationDays)
                    {
                        Console.WriteLine($"\n✗ Not enough vacation days!");
                        Console.WriteLine($"Requested: {request.Days}, Available: {emp.VacationDays}");
                        return;
                    }

                    Console.Write("Enter Reason: ");
                    request.Reason = Console.ReadLine();

                    vacationRequests.Add(request);

                    Console.WriteLine("\n✓ Vacation request submitted!");
                    Console.WriteLine($"Request ID: {request.RequestId}");
                    Console.WriteLine($"Status: {request.Status}");
                }
                else
                {
                    Console.WriteLine("Employee not found or inactive!");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID!");
            }
        }

        static void ViewVacationRequests()
        {
            Console.Clear();
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("        VACATION REQUESTS");
            Console.WriteLine("══════════════════════════════════════");

            if (!vacationRequests.Any())
            {
                Console.WriteLine("No vacation requests found!");
                return;
            }

            Console.WriteLine("{0,-10} {1,-20} {2,-15} {3,-15} {4,-10} {5,-10}", 
                "Req ID", "Employee", "Start Date", "End Date", "Days", "Status");
            Console.WriteLine(new string('-', 85));

            foreach (var request in vacationRequests.OrderByDescending(v => v.StartDate))
            {
                Console.WriteLine("{0,-10} {1,-20} {2,-15:yyyy-MM-dd} {3,-15:yyyy-MM-dd} {4,-10} {5,-10}", 
                    request.RequestId, 
                    request.EmployeeName, 
                    request.StartDate, 
                    request.EndDate, 
                    request.Days, 
                    request.Status);
            }
        }

        static void ApproveRejectVacation()
        {
            Console.Clear();
            Console.WriteLine("══════════════════════════════════════");
            Console.WriteLine("    APPROVE/REJECT VACATION");
            Console.WriteLine("══════════════════════════════════════");

            // Show pending requests
            var pendingRequests = vacationRequests.Where(v => v.Status == "Pending").ToList();
            
            if (!pendingRequests.Any())
            {
                Console.WriteLine("No pending vacation requests!");
                return;
            }

            Console.WriteLine("Pending Requests:\n");
            foreach (var request in pendingRequests)
            {
                Console.WriteLine($"ID: {request.RequestId}, Employee: {request.EmployeeName}");
                Console.WriteLine($"Dates: {request.StartDate:yyyy-MM-dd} to {request.EndDate:yyyy-MM-dd}");
                Console.WriteLine($"Days: {request.Days}, Reason: {request.Reason}");
                Console.WriteLine("-----------------------------------");
            }

            Console.Write("\nEnter Request ID to process: ");
            if (int.TryParse(Console.ReadLine(), out int reqId))
            {
                var request = vacationRequests.FirstOrDefault(v => v.RequestId == reqId);
                if (request != null && request.Status == "Pending")
                {
                    var emp = employees.FirstOrDefault(e => e.Id == request.EmployeeId);
                    
                    Console.WriteLine($"\nProcess vacation for: {request.EmployeeName}");
                    Console.WriteLine($"Requested Days: {request.Days}");
                    Console.WriteLine($"Available Days: {emp.VacationDays}");

                    Console.WriteLine("\n1. Approve");
                    Console.WriteLine("2. Reject");
                    Console.Write("Select option: ");

                    string choice = Console.ReadLine();
                    
                    switch (choice)
                    {
                        case "1":
                            if (request.Days <= emp.VacationDays)
                            {
                                emp.VacationDays -= request.Days;
                                request.Status = "Approved";
                                Console.WriteLine("\n✓ Vacation approved!");
                                Console.WriteLine($"Remaining vacation days: {emp.VacationDays}");
                            }
                            else
                            {
                                Console.WriteLine("\n✗ Cannot approve - not enough vacation days!");
                            }
                            break;

                        case "2":
                            request.Status = "Rejected";
                            Console.WriteLine("\n✗ Vacation rejected!");
                            break;

                        default:
                            Console.WriteLine("Invalid option!");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Request not found or already processed!");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID!");
            }
        }

        // Initialize Sample Data
        static void InitializeSampleData()
        {
            employees.Add(new Employee
            {
                Id = nextEmployeeId++,
                Name = "MOHON MD MIRAJ KAKA",
                Position = "Software Developer",
                Department = "IT",
                Salary = 60000,
                HireDate = new DateTime(2022, 1, 15),
                VacationDays = 15
            });

            employees.Add(new Employee
            {
                Id = nextEmployeeId++,
                Name = "RAKIBUL ISLAM BAPPY",
                Position = "HR Manager",
                Department = "HR",
                Salary = 75000,
                HireDate = new DateTime(2021, 5, 20),
                VacationDays = 20
            });

            employees.Add(new Employee
            {
                Id = nextEmployeeId++,
                Name = "BODY JIHAD",
                Position = "Sales Executive",
                Department = "Sales",
                Salary = 55000,
                HireDate = new DateTime(2023, 3, 10),
                VacationDays = 18
            });

            Console.WriteLine("✓ Sample data loaded (3 employees)");
        }
    }
}
