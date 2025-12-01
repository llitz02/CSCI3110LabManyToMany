using System.ComponentModel.DataAnnotations;

namespace CSCI3110LabManyToMany.Models.Entities;

public class Book
{
    public int Id { get; set; }
    [StringLength(256)]
    public string Title { get; set; } = String.Empty;
    public int PublicationYear { get; set; }
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
}

