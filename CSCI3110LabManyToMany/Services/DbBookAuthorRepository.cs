using CSCI3110LabManyToMany.Models.Entities;

namespace CSCI3110LabManyToMany.Services;

public class DbBookAuthorRepository : IBookAuthorRepository
{
    private readonly ApplicationDbContext _db;

    public DbBookAuthorRepository(ApplicationDbContext db)
    {
        _db = db;
    }

}
