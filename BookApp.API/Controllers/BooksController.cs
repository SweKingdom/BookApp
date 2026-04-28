using System.Security.Claims;
using BookApp.API.Data;
using BookApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookApp.API.Controllers;

public class BooksController : ControllerBase
{
    private readonly AppDbContext _db;
    public BooksController(AppDbContext db)
    {
        _db = db;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await _db.Books
            .Select(b => new BookResponse(b.Id, b.Title, b.Author, b.PublishedDate))
            .ToListAsync();

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await _db.Books
            .FirstOrDefaultAsync(b => b.Id == id);
        
        if (book == null)
            return NotFound();
        
        return Ok(new BookResponse(book.Id, book.Title, book.Author, book.PublishedDate));
    }

    [HttpPost]
    public async Task<IActionResult> Create(BookRequest request)
    {
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            PublishedDate = request.PublishedDate,
        };
        
        _db.Books.Add(book);
        await _db.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetById), new { id = book.Id },
            new BookResponse(book.Id, book.Title, book.Author, book.PublishedDate));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, BookRequest request)
    {
        var book = await _db.Books
            .FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
            return NotFound();
        
        book.Title = request.Title;
        book.Author = request.Author;
        book.PublishedDate = request.PublishedDate;
        
        await _db.SaveChangesAsync();
        
        return Ok(new BookResponse(book.Id, book.Title, book.Author, book.PublishedDate));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _db.Books
            .FirstOrDefaultAsync(b => b.Id == id);
        
        if (book == null)
            return NotFound();
        
        _db.Books.Remove(book);
        await _db.SaveChangesAsync();
        
        return NoContent();
    }
}

public record BookRequest(string Title, string Author, DateTime PublishedDate);

public record BookResponse(int Id, string Title, string Author, DateTime PublishedDate);
