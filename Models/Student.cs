namespace Assignment.Api.Models;

public class Student
{
    private static readonly List<Student> _students = new();
    public int Id { get; set; }
    public string Name { get; set; }
    public string Npm { get; set; }

    public Student(int id, string name, string npm)
    {
        Id = id;
        Name = name;
        Npm = npm;
    }

    public static List<Student> All()
    {
        return _students;
    }

    public static Student Add(Student student)
    {
        _students.Add(student);
        return student;
    }

    public static void Remove(Student student)
    {
        _students.Remove(student);
    }
}