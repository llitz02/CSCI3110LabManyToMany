using CSCI3110LabManyToMany.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CSCI3110LabManyToMany.Services;

public class DbAuthorRepository : IAuthorRepository
{
    private readonly ApplicationDbContext _db;

    public DbAuthorRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ICollection<Author>> ReadAllAsync()
    {
        return await _db.Authors
            .Include(a => a.BookAuthors)
                .ThenInclude(ba => ba.Book)
            .ToListAsync();
    }

    public async Task<Author?> ReadAsync(int id)
    {
        return await _db.Authors
            .Include(a => a.BookAuthors)
                .ThenInclude(ba => ba.Book)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
}
