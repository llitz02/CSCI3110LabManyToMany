namespace CSCI3110LabManyToMany.Models;

public class BookDetailsVM
{
    public int Id { get; set; }
    public string Title { get; set; } = String.Empty;
    public int PublicationYear { get; set; }
    public int NumberOfAuthors { get; set; }
}
