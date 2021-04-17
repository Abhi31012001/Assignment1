
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using lab4.Data;
using lab4.Models;
using lab4.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

/*student name- Abhi Patel;
 * 
 * Student No:040978822;
 * 
 partner Name -Meet Patel;

Student no: 040979409

Assignment 1

Lab Instructor - Aamir Rad 

*/
namespace lab4.Controllers
{
    public class CommunitiesController : Controller
    {
        private readonly SchoolCommunityContext _context;

        public CommunitiesController(SchoolCommunityContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string ID)
        {
            CommunityViewModel communityViewModel = new CommunityViewModel();

            communityViewModel.Communities = await _context.Communities
                   .Include(i => i.CommunityMemberships)
                        .ThenInclude(i => i.Student)
                  .AsNoTracking()
                  .OrderBy(i => i.Title)
                  .ToListAsync();

            if (ID != null)
            {
                ViewData["CommunityID"] = ID;
                communityViewModel.CommunityMemberships = communityViewModel.Communities.Where(i => i.ID == ID).Single().CommunityMemberships;
            }

            return View(communityViewModel);
        }

       
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var community = await _context.Communities
                .FirstOrDefaultAsync(m => m.ID == id);
            if (community == null)
            {
                return NotFound();
            }

            return View(community);
        }

        
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Budget")] Community community)
        {
            if (ModelState.IsValid)
            {
                _context.Add(community);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(community);
        }

    
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var community = await _context.Communities.FindAsync(id);
            if (community == null)
            {
                return NotFound();
            }
            return View(community);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,Title,Budget")] Community community)
        {
            if (id != community.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(community);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommunityExists(community.ID))
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
            return View(community);
        }
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var community = await _context.Communities
                .FirstOrDefaultAsync(m => m.ID == id);
            if (community == null)
            {
                return NotFound();
            }

            ViewBag.advertisementsExist = _context.AdvertisementCommunity.Where(m => m.CommunityID == id).Count() != 0;

            return View(community);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var community = await _context.Communities.FindAsync(id);
            _context.Communities.Remove(community);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommunityExists(string id)
        {
            return _context.Communities.Any(e => e.ID == id);
        }

    }
}
