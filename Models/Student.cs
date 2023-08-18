namespace Assignment.Api.Models;

public class Student
{
    private static readonly List<Student> _students = new();
    public string Id { get; set; }
    public string Name { get; set; }

    public Student(string id, string name)
    {
        Id = id;
        Name = name;
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