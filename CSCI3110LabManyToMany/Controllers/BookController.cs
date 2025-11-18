using CSCI3110LabManyToMany.Models;
using CSCI3110LabManyToMany.Services;
using Microsoft.AspNetCore.Mvc;

namespace CSCI3110LabManyToMany.Controllers;

public class BookController : Controller
{
    private readonly IBookRepository _bookRepo;

    public BookController(IBookRepository bookRepo)
    {
        _bookRepo = bookRepo;
    }

    public async Task<IActionResult> Index()
    {
        var allBooks = await _bookRepo.ReadAllAsync();
        var bookDetailsCollection = allBooks.Select(b => new BookDetailsVM
        {
            Id = b.Id,
            Title = b.Title,
            PublicationYear = b.PublicationYear,
            NumberOfAuthors = b.BookAuthors?.Count ?? 0
        }).ToList();
        return View(bookDetailsCollection);
    }
}
