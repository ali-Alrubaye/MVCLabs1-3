using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace labTwo.Models
{
    public class AlbumViewModel
    {
        public AlbumViewModel()
        {
            this.PhotoAlbV = new HashSet<PhotoViewModel>();

        }

        public Guid AlbumId { get; set; }
        [Required]
        public string AlbumName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AlbumDate { get; set; }
        public string AlbumComment { get; set; }

        public virtual ICollection<PhotoViewModel> PhotoAlbV { get; set; }

    }
}