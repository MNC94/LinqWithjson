// See https://aka.ms/new-console-template for more information

using System.Transactions;

namespace LinqJoingConcepts;

internal class Program
{
    static void Main(string[] args)
    {


        List<Employee>li=new List<Employee>();


        li.Add(new Employee
        { Id = 1, age = 19, name = "Ritesh", gender = "M" });
        li.Add(new Employee
        { Id = 2, age = 20, name = "sujit", gender = "M" });
        li.Add(new Employee
        { Id = 3, age = 23, name = "Kabir", gender = "F" });
        li.Add(new Employee
        { Id = 4, age = 3, name = "mantu", gender = "F" });
        li.Add(new Employee
        { Id = 5, age = 24, name = "Kamlesh", gender = "M" });
        li.Add(new Employee
        { Id = 6, age = 28, name = "Manoj", gender = "M" });


        List<Department> Deli = new List<Department>();
        Deli.Add(new Department
        { id = 2, Departments = "IT", Location = "Bangalore" });
        Deli.Add(new Department
        { id = 8, Departments = "IT", Location = "Bangalore" });
        Deli.Add(new Department
        { id = 3, Departments = "HR", Location = "Bangalore" });
        Deli.Add(new Department
        { id = 7, Departments = "HR", Location = "Bangalore" });
        Deli.Add(new Department
        { id = 6, Departments = "Account", Location = "Bangalore" });

        var Innerjoin = from a in li join b in Deli on a.Id equals b.id select new { a.Id, a.name, b.Location, b.Departments };

        var leftjoin = from emp in li
                       join dept in Deli on emp.Id equals dept.id into temp
                       from C in temp.DefaultIfEmpty()
                       select new
                       { EmployeeId = emp.Id,
                         EmployeeName = emp.name,
                           Department = (C != null) ? C.Departments : "NULL"
                       };


        var Rightjoin = from dept in Deli 
                        join emp in li  on dept.id equals  emp.Id into temp
                       from C in temp.DefaultIfEmpty()
                       select new
                       {
                           EmployeeId = (C!=null)?C.Id:0,
                           deptid = dept.id,
                           EmployeeName = (C != null) ? C.name : "NULL",
                           Department = dept.Departments
                       };



        var Cross = from First in li
                    from     sce in Deli
                        select new
                        {
                            EmployeeName= First.name,
                            Department = sce.Departments
                        };




        //Group Employees by Department using Method Syntax
        var GroupJoinMS = Department.GetAllDepartments(). //Outer Data Source i.e. Departments
            GroupJoin( //Performing Group Join with Inner Data Source
                Employee.GetAllEmployees(), //Inner Data Source
                dept => dept.id, //Outer Key Selector  i.e. the Common Property
                emp => emp.Id, //Inner Key Selector  i.e. the Common Property
                (dept, emp) => new { dept, emp } //Projecting the Result to an Anonymous Type
            );




        //Printing the Result set
        //Outer Foreach is for Each department
        foreach (var item in GroupJoinMS)
        {
            Console.WriteLine("Department :" + item.dept.Departments);
            //Inner Foreach loop for each employee of a Particular department
            foreach (var employee in item.emp)
            {
                Console.WriteLine("  EmployeeID : " + employee.Id + " , Name : " + employee.name);
            }
        }

        Console.ReadLine();


        //Console.WriteLine("Hello, World!");

        //  Student[] std =
        // {
        //new Student() { StudentID = 1, StudentName = "John", Age = 18 },
        //new Student() { StudentID = 2, StudentName = "Steve",  Age = 21 },
        //new Student() { StudentID = 3, StudentName = "Bill",  Age = 25 },
        //new Student(){ StudentID=4,StudentName="",Age=30},
        //new Student() { StudentID = 5, StudentName = "Bill",  Age = 25 }
        // };

        //  Student[] students = StudentExtension.where(std, delegate (Student std)
        //  {
        //      return std.Age > 12 && std.Age < 20;
        //  });

        //  Student bill = std.Where(s => s.StudentName == "Bill").FirstOrDefault();

        //  var studenybyresult = from s in std orderby s.StudentID descending select s;

        //var whereResult = from s in std where s.Age>12 && s.Age<20 orderby s.StudentID descending select s;
    }
    

    delegate bool FindStudent(Student std);

    class StudentExtension
    {
        public static Student[] where(Student[] stdArray, FindStudent del)
        {
            int i = 0;
            Student[] result = new Student[10];
            foreach (Student std in stdArray)
                if (del(std))
                {
                    result[i] = std;
                    i++;
                }

            return result;
        }
    }

    public class Employee
    {
          public int Id { get; set; }
          public string name { get; set; }
          public int age { get; set; }
          public string gender { get; set; }


        public static List<Employee> GetAllEmployees()
        {
            return new List<Employee>()
            {
                new Employee { Id = 1, name = "Preety"},
                new Employee { Id = 2, name = "Priyanka"},
                new Employee { Id = 3, name = "Anurag"},
                new Employee { Id = 4, name = "Pranaya"},
                new Employee { Id = 5, name = "Hina"},
                new Employee { Id = 6, name = "Sambit"},
                new Employee { Id = 7, name = "Happy"},
                new Employee { Id = 8, name = "Tarun"},
                new Employee { Id = 9, name = "Santosh"},
                new Employee { Id = 10, name = "Raja"},
                new Employee { Id = 11, name = "Ramesh"}
            };
        }
    }


    public class Department
    {
        public int id { get; set; }

        public string Departments { get; set; }

        public string Location { get; set; }


        public static List<Department> GetAllDepartments()
        {
            return new List<Department>()
            {
            new Department
            { id = 2, Departments = "IT", Location = "Bangalore" },
           new Department
            { id = 8, Departments = "IT", Location = "Bangalore" },
           new Department
            { id = 3, Departments = "HR", Location = "Bangalore" },
            new Department
            { id = 7, Departments = "HR", Location = "Bangalore" },
            new Department
            { id = 6, Departments = "Account", Location = "Bangalore" }
            };
        }
    }




}


