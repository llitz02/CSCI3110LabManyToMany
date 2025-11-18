namespace CSCI3110LabManyToMany.Models;

public class AuthorDetailsVM
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string LastName { get; set; } = String.Empty;
    public int NumberOfBooks { get; set; }
}
