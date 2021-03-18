using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RentApp.Models
{
    public class RentItems
    {
        [Key]
        public int RentItemId { get; set; }

        [DisplayName("Utrustning")]
        [Required(ErrorMessage = "Vänligen ange utrustning som tillkommit")]
        public string Name { get; set; }

        [DisplayName("Tillgänglig")]
        public bool Available { get; set; }

        [ForeignKey("TypeOfEquipment")]
        public int EquipmentId { get; set; }

        [DisplayName("Kategori")]
        public TypeOfEquipment TypeOfEquipment { get; set; }

        [ForeignKey("ImageModal")]
        public int ImageId { get; set; }

        public ImageModal ImageModal { get; set; }

        [NotMapped]
        public Rating Rating { get; set; }

        [DisplayName("Omdömen")]
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<Rent> Rented { get; set; }

    }

    public class ImageModal
    {
        [Key]
        public int ImageId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Ladda upp bild")]
        public IFormFile ImageFile { get; set; }

        public ICollection<RentItems> RentItems { get; set; }
    }

    public class TypeOfEquipment
    {
        [Key]
        public int EquipmentId { get; set; }

        [DisplayName("Utrustning")]
        [Required(ErrorMessage = "Vänligen ange typ av utrustning")]
        public string Name { get; set; }
        public ICollection<RentItems> RentItems { get; set; }
    }
    public class Rent
    {
        [Key]
        public int RentId { get; set; }

        [DisplayName("Förnamn")]
        [MinLength(3, ErrorMessage = "Namn måste vara minst tre tecken")]
        [Required(ErrorMessage = "Vänligen ange ditt förnamn för att hyra utrustning")]
        public string FirstName { get; set; }

        [DisplayName("Efternamn")]
        [MinLength(3, ErrorMessage = "Namn måste vara minst tre tecken")]
        [Required(ErrorMessage = "Vänligen ange ditt efternamn för att hyra utrustning")]
        public string LastName { get; set; }

        [DisplayName("Mobilnummer")]
        [Required(ErrorMessage = "Vänligen ange ditt mobilnummer för att hyra utrustning")]
        public int TelNum { get; set; }

        [DisplayName("Lånedatum")]
        public DateTime RentDate { get; set; }

        [ForeignKey("RentItems")]
        public int RentItemId { get; set; }
        public RentItems RentItems { get; set; }
    }
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [DisplayName("Recension")]
        [Required(ErrorMessage = "Vänligen ange text för din recension")]
        public string Opinion { get; set; }

        [ForeignKey("RentItems")]
        public int RentItemId { get; set; }
        public RentItems RentItems { get; set; }


    }
}
