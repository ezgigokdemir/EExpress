using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EExpress.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Lütfen adınızı giriniz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lütfen ürün tanımı giriniz.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Lütfen fiyat bilgisi giriniz.")]
        public float Price { get; set; }
        
        public string imageUrl { get; set; }
        public bool? isActive { get; set; } = true;
        //public int Stock { get; set; }

        public int Stock { get; set; }

        [Display(Name = "Marka Adı")]
        public int BrandId { get; set; }

        [ForeignKey("BrandId")]
        public virtual Brand Brand { get; set; }

        [Display(Name = "Kategori Adı")]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        
    }
}
