// See https://aka.ms/new-console-template for more information
using NHibernate.Util;
using usingNHibernate;
using usingNHibernate.Configurations;



getEmployees();
//createStudent();


void getEmployees()
{
    var sessionFactory = new NHConfiguration().GetSessionFactory();
    var session = sessionFactory.OpenSession();
    var transaction = session.BeginTransaction();

    var employee = session.Query<Employee>().FirstOrDefault(e => e.Name == "Türkay");

    Console.WriteLine($"{employee.Name} isimli çalışanın adresş:   {employee.ResidentAddress.AddressLine1} {employee.ResidentAddress.City}   {employee.ResidentAddress.Country}");


}



static void createStudent()
{
    var sessionFactory = new NHConfiguration().GetSessionFactory();
    var session = sessionFactory.OpenSession();
    var transaction = session.BeginTransaction();
    Console.Write("Yeni öğrenci adı :>");
    string name = Console.ReadLine();
    Console.Write("Yeni öğrencinin soyadı :>");
    string lastName = Console.ReadLine();
    Student student = new Student { FirstName = name, LastName = lastName };
    session.Save(student);
    transaction.Commit();

    Console.WriteLine($"{student.FirstName} isimli öğrenci eklend. Id'si");
    Console.WriteLine(student.Id);


    using var readingTx = session.BeginTransaction();
    var students = session.CreateCriteria<Student>().List<Student>();
    students.ForEach(student => Console.WriteLine($"{student.FirstName} {student.LastName}"));

    var studentWithSpecId = session.Get<Student>(3);
    Console.WriteLine($"Get ile gelen öğrenci: {studentWithSpecId.FirstName} {studentWithSpecId.LastName} ");
    studentWithSpecId.LastName = "Boz";
    session.Update(studentWithSpecId);
    Console.WriteLine("Bir öğrencinin soyadı güncellendi. Tüm veriler tekrar çekiliyor:");
    Console.WriteLine("......................................");
    students.ForEach(student => Console.WriteLine($"{student.FirstName} {student.LastName}"));

    session.Delete(studentWithSpecId);
    Console.WriteLine("Öğrenci silindikten sonra...");
    Console.WriteLine("......................................");
    students.ForEach(student => Console.WriteLine($"{student.FirstName} {student.LastName}"));


    readingTx.Commit();

}