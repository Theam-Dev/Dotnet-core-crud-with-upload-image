using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotnetCoreCrudImage.Data;
using DotnetCoreCrudImage.Models;
using DotnetCoreCrudImage.ViewsModal;

namespace DotnetCoreCrudImage.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public PostsController(ApplicationDbContext context ,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
              return View(await _context.Post.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            var post = await _context.Post.FirstOrDefaultAsync(m => m.Id == id);
            return View(post);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(post);
                Post obj = new Post
                {
                    Title = post.Title,
                    Image = uniqueFileName
                };
                _context.Add(obj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            var post = await _context.Post.FindAsync(id);
            var postView = new PostViewModel()
            {
                Id = post.Id,
                Title = post.Title,
                ExistingImage = post.Image
            };
            return View(postView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PostViewModel post)
        {
            if (ModelState.IsValid)
            {
                var postobj = await _context.Post.FindAsync(post.Id);
                postobj.Title = post.Title;
                if (post.Image != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", postobj.Image.ToString());
                    System.IO.File.Delete(filePath);
                    postobj.Image = ProcessUploadedFile(post);
                }
                else
                {
                    postobj.Image = postobj.Image;
                }
                _context.Update(postobj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            var post = await _context.Post.FirstOrDefaultAsync(m => m.Id == id);
            return View(post);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private string ProcessUploadedFile(PostViewModel model)
        {
            string uniqueFileName = null;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
