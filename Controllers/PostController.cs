using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppCheck.Data;
using WebAppCheck.Models;

namespace WebAppCheck.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly WebDbContext _context;

        public PostController(WebDbContext context)
        {
            _context = context;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
              return _context.UserPosts != null ? 
                          View(await _context.UserPosts.ToListAsync()) :
                          Problem("Entity set 'WebDbContext.UserPosts'  is null.");
        }

        // GET: Post/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UserPosts == null)
            {
                return NotFound();
            }

            var posts = await _context.UserPosts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // GET: Post/Create
        public IActionResult Create()
        {
            return View(new Posts());
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId,PostTitle,PostContent,CreatedDate")] Posts posts)
        {
            if (ModelState.IsValid)
            {
                posts.CreatedDate = DateTime.Now;
                _context.Add(posts);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(posts);
        }

        // GET: Post/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UserPosts == null)
            {
                return NotFound();
            }

            var posts = await _context.UserPosts.FindAsync(id);
            if (posts == null)
            {
                return NotFound();
            }
            return View(posts);
        }

        // POST: Post/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PostId,PostTitle,PostContent,CreatedDate")] Posts posts)
        {
            if (id != posts.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posts);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostsExists(posts.PostId))
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
            return View(posts);
        }

        // GET: Post/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UserPosts == null)
            {
                return NotFound();
            }

            var posts = await _context.UserPosts
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // POST: Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UserPosts == null)
            {
                return Problem("Entity set 'WebDbContext.UserPosts'  is null.");
            }
            var posts = await _context.UserPosts.FindAsync(id);
            if (posts != null)
            {
                _context.UserPosts.Remove(posts);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostsExists(int id)
        {
          return (_context.UserPosts?.Any(e => e.PostId == id)).GetValueOrDefault();
        }
    }
}
