using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheArtOfDev.HtmlRenderer.WinForms;
using TicketSystem.Application.Contract;
using TicketSystem.Context;
using TicketSystem.Models;
using DrawingImage = System.Drawing.Image;

namespace TicketSystem.Infrastructure
{
    public class TicketRepository : Repository<Ticket, int>, ITicketRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Images");

        public TicketRepository(ApplicationContext applicationContext, IUnitOfWork unitOfWork) : base(applicationContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Ticket> CreateTicketWithMobileNumber(string mobileNumber, string htmlImage)
        {

            var users = await _unitOfWork.Users.GetAllAsync();
            var user = await users
                .Include(u => u.ticket)
                .FirstOrDefaultAsync(u => u.MobileNumber == mobileNumber);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            
            if (user.ticket != null)
            {
                throw new Exception("User already has a ticket");
            }

            
            var imagePath = SaveHtmlImageToFile(htmlImage);

           
            var ticket = new Ticket
            {
                ImagePath = imagePath,
                UserId = user.Id,
                User = user,
                CreatedDate = DateTime.Now
            };

            
            await _unitOfWork.Tickets.CreateAsync(ticket);
            await _unitOfWork.SaveChangesAsync();

            return ticket;
        }

        public async Task<IQueryable<Ticket>> GetAllTicketsNumberDesc()
        {
            var tickets = await _unitOfWork.Tickets.GetAllAsync(); 
            return tickets.OrderByDescending(t => t.Id).AsQueryable();
        }

        public async Task<Ticket> GetTicketByMobileNumber(string mobileNumber)
        {
            var tickets = await _unitOfWork.Tickets.GetAllAsync(); 
            return await tickets
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.User.MobileNumber == mobileNumber);
        }

        private string SaveHtmlImageToFile(string htmlImage)
        {
            using (var img = RenderHtmlToImage(htmlImage))
            {
                string fileName = $"{Guid.NewGuid()}.jpg";
                string filePath = Path.Combine(_imageDirectory, fileName);

                if (!Directory.Exists(_imageDirectory))
                {
                    Directory.CreateDirectory(_imageDirectory);
                }

                using (var memoryStream = new MemoryStream())
                {
                    img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg); // Save to memory stream first
                    memoryStream.Seek(0, SeekOrigin.Begin); // Rewind the stream

                    using (var imageSharp = SixLabors.ImageSharp.Image.Load<Rgba32>(memoryStream))
                    {
                        imageSharp.Save(filePath, new JpegEncoder());
                    }
                }

                return filePath;
            }
        }

        private DrawingImage RenderHtmlToImage(string html)
        {
            return HtmlRender.RenderToImage(html, 800, 600); 
        }
    }
}
