using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ab_comeia_todolist.Data;
using ab_comeia_todolist.Models;
using Microsoft.AspNetCore.Authorization;

namespace ab_comeia_todolist.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Todos
                .AsNoTracking()
                .Where(t => t.User == User.Identity!.Name)
                .ToListAsync()
            );
        }

        // GET : Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            if (todo.User != User.Identity!.Name)
            {
                return NotFound();
            }

            return View(todo);
        }

        // GET : Home/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST : Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,Tagname,Description,UpTo,Done,CreatedAt,LastUpdateDate,User")] Todo todo)
        {
            if (ModelState.IsValid)
            {
                todo.CreatedAt = DateTime.Now;
                todo.User = User.Identity!.Name!;

                _context.Add(todo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(todo);
        }

        // GET : Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            if (todo.User != User.Identity!.Name)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST : Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Tagname,Description,UpTo,Done,CreatedAt,LastUpdateDate,User")] Todo todo)
        {
            if (id != todo.Id)
            {
                return NotFound();
            }

            Todo originalData = (await _context.Todos.FirstOrDefaultAsync(t => t.Id == id))!;
            if (originalData.User != User.Identity!.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    todo.CreatedAt = originalData.CreatedAt;
                    todo.User = User.Identity.Name;
                    todo.LastUpdateDate = DateTime.Now;

                    _context.Entry(originalData).CurrentValues.SetValues(todo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoExists(todo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(todo);
        }

        // GET : Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todo = await _context.Todos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (todo == null)
            {
                return NotFound();
            }

            if (todo.User != User.Identity!.Name)
            {
                return NotFound();
            }

            return View(todo);
        }

        // POST : Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null || todo.User != User.Identity!.Name) return NotFound();
            
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TodoExists(int id)
        {
            return _context.Todos.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}