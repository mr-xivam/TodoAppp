using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers;

public class TodoController : Controller
{
    private readonly AppDbContext _context;

    public TodoController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Todo
    public async Task<IActionResult> Index()
    {
        var items = await _context.TodoItems
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
        return View(items);
    }

    // GET: /Todo/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Todo/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TodoItem item)
    {
        if (ModelState.IsValid)
        {
            item.CreatedAt = DateTime.UtcNow;
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(item);
    }

    // POST: /Todo/Toggle/5
    [HttpPost]
    public async Task<IActionResult> Toggle(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item == null) return NotFound();

        item.IsCompleted = !item.IsCompleted;
        item.CompletedAt = item.IsCompleted ? DateTime.UtcNow : null;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // POST: /Todo/Delete/5
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.TodoItems.FindAsync(id);
        if (item != null)
        {
            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
