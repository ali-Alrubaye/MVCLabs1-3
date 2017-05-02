using System;
using System.ComponentModel.DataAnnotations;

namespace labTwo.Models
{
    public class PhotoViewModel
    {
        public Guid PhotoId { get; set; }

        public string PhotoName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PhotoDate { get; set; }

        public string PhotoComment { get; set; }
        public string PhotoPath { get; set; }
        public Guid AlbumId { get; set; }
        public virtual AlbumViewModel AlbumPhV { get; set; }

    }
}