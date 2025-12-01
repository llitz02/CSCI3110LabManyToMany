using CSCI3110LabManyToMany.Models;
using CSCI3110LabManyToMany.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSCI3110LabManyToMany.Controllers;

public class AuthorController : Controller
{
    private readonly IAuthorRepository _authorRepo;

    public AuthorController(IAuthorRepository authorRepo)
    {
        _authorRepo = authorRepo;
    }

    public async Task<IActionResult> Index()
    {
        var allAuthors = await _authorRepo.ReadAllAsync();
        var authorDetailsCollection = allAuthors.Select(a => new AuthorDetailsVM
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName,
            NumberOfBooks = a.BookAuthors?.Count ?? 0
        }).ToList();
        return View(authorDetailsCollection);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var allAuthors = await _authorRepo.ReadAllAsync();
        // This is a workaround to avoid circular references
        foreach (var author in allAuthors)
        {
            if (author.BookAuthors == null) continue;
            foreach (var bookAuthor in author.BookAuthors)
            {
                bookAuthor.Book = null;
                bookAuthor.Author = null;
            }
        }
        return Json(allAuthors);
    }
}
