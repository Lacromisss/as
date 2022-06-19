using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspPage.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string ImgUrl { get; set; }
        [NotMapped]
        public IFormFile  Photo { get; set; }
    }
}
