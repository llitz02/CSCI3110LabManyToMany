
using CSCI3110LabManyToMany.Models.Entities;

namespace CSCI3110LabManyToMany.Services;

public interface IAuthorRepository
{
    Task<ICollection<Author>> ReadAllAsync();
    Task<Author?> ReadAsync(int id);
}
