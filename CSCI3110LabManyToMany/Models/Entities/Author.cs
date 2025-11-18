using System.ComponentModel.DataAnnotations;

namespace CSCI3110LabManyToMany.Models.Entities;

public class Author
{
    public int Id { get; set; }
    [StringLength(128)]
    public string? FirstName { get; set; }
    [StringLength(128)]
    public string LastName { get; set; } = String.Empty;

    public ICollection<BookAuthor> BookAuthors { get; set;
}
}
