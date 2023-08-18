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
        _students.Add(this);
    }

    public static List<Student> All()
    {
        return _students;
    }
}