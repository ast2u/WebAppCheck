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

        // GET: Post/Create
        public IActionResult CreateOrEdit(int id=0)
        {
            if (id == 0)
            {
                return View(new Posts());
            }
            else
            {
                return View(_context.UserPosts.Find(id));
            }
            
        }

        // POST: Post/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("PostId,PostTitle,PostContent,CreatedDate")] Posts posts)
        {
            if (ModelState.IsValid)
            {
                if(posts.PostId == 0)
                {
                    posts.CreatedDate = DateTime.Now;
                    _context.Add(posts);
                }
                else
                {
                    _context.Update(posts);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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

    }
}
