

using lab4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using lab4.Data;
using lab4.Models.ViewModels;

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
    public class AdvertisementsController : Controller
    {

        private readonly SchoolCommunityContext _context;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string containerName = "advertisements";

        public AdvertisementsController(SchoolCommunityContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }

        public IActionResult Index(string id)
        {
            var community = _context.Communities.Where(m => m.ID == id).FirstOrDefault();
            var adsView = new AdsViewModel();
            adsView.Community = community;
            var adsCommunity = _context.AdvertisementCommunity.Where(m => m.CommunityID == id).Include(m => m.Advertisement);

            adsView.Advertisements = adsCommunity.Select(m => m.Advertisement);

            return View(adsView);
        }

        public IActionResult Create(string id)
        {
            return View(_context.Communities.Find(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(string CommunityID, IFormFile file)
        {

            BlobContainerClient containerClient;
            try
            {
                containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName);
                containerClient.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);
            }
            catch (RequestFailedException)
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }

            try
            {
                
                var blockBlob = containerClient.GetBlobClient(file.FileName);
                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                using (var memoryStream = new MemoryStream())
                {
                   
                    await file.CopyToAsync(memoryStream);

                    
                    memoryStream.Position = 0;

                    await blockBlob.UploadAsync(memoryStream);
                    memoryStream.Close();
                }

                var image = new Advertisement();
                image.Url = blockBlob.Uri.AbsoluteUri;
                image.FileName = file.FileName;
                _context.Advertisements.Add(image);
                _context.SaveChanges();

                var adsCommunity = new AdsCommunity();
                adsCommunity.AdvertisementID = image.Id;
                adsCommunity.CommunityID = CommunityID;

                _context.AdvertisementCommunity.Add(adsCommunity);
                _context.SaveChanges();
            }
            catch (RequestFailedException)
            {
                View("Error");
            }

            return RedirectToAction("Index", new { id = CommunityID });
        }

        

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var image = await _context.Advertisements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (image == null)
            {
                return NotFound();
            }

            return View(image);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var image = await _context.Advertisements.FindAsync(id);
            string communityID = _context.AdvertisementCommunity.Where(m => m.AdvertisementID == id).First().CommunityID;


            BlobContainerClient containerClient;
            try
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }
            catch (RequestFailedException)
            {
                return View("Error");
            }

            try
            {
                var blockBlob = containerClient.GetBlobClient(image.FileName);
                if (await blockBlob.ExistsAsync())
                {
                    await blockBlob.DeleteAsync();
                }

                _context.Advertisements.Remove(image);
                await _context.SaveChangesAsync();

            }
            catch (RequestFailedException)
            {
                return View("Error");
            }

            return RedirectToAction("Index", new { id = communityID });
        }
    }
}
