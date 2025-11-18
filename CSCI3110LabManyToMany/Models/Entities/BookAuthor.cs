namespace CSCI3110LabManyToMany.Models.Entities;
public class BookAuthor
{
    
    public int Id { get; set; } 
    
    public Book Book { get; set; }
    public Author Author { get; set; }
}