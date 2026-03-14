using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewHorizons.Models;

namespace NewHorizons.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentsController : Controller
    {
        private readonly NewHorizonsContext _context;

        public CommentsController(NewHorizonsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var comments = await _context.Comments
                .Include(c => c.Author)
                .Include(c => c.Post)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return View(comments);
        }

        public async Task<IActionResult> Details(int id)
        {
            var comment = await _context.Comments
                .Include(c => c.Author)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.CommentId == id);

            if (comment == null)
                return NotFound();

            return View(comment);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var comment = await _context.Comments
                .Include(c => c.Author)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(c => c.CommentId == id);

            if (comment == null)
                return NotFound();

            return View(comment);
        }

        public async Task<IActionResult> Hidden()
        {
            var hiddenComments = await _context.Comments
                .Include(c => c.Author)
                .Include(c => c.Post)
                .Where(c => c.IsHidden)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return View(hiddenComments);
        }

        public async Task<IActionResult> ToggleHidden(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
                return NotFound();

            comment.IsHidden = !comment.IsHidden;

            await _context.SaveChangesAsync();

            return RedirectToAction("Moderation");
        }

        public async Task<IActionResult> Moderation()
        {
            var recentComments = await _context.Comments
                .Include(c => c.Author)
                .Include(c => c.Post)
                .Where(c => !c.IsHidden)
                .OrderByDescending(c => c.CreatedAt)
                .Take(50)
                .ToListAsync();

            return View(recentComments);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Comment comment)
        {
            if (id != comment.CommentId)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(comment);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _context.Comments
                .Include(c => c.Author)
                .FirstOrDefaultAsync(c => c.CommentId == id);

            if (comment == null)
                return NotFound();

            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment != null)
            {
                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}