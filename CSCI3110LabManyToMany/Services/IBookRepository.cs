
using CSCI3110LabManyToMany.Models.Entities;

namespace CSCI3110LabManyToMany.Services;

public interface IBookRepository
{
    Task<ICollection<Book>> ReadAllAsync();
    Task<Book?> ReadAsync(int id);
    Task<bool> AssignAuthorToBookAsync(int bookId, int authorId);
}
