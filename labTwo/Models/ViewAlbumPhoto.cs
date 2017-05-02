using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datalager.Models;

namespace labTwo.Models
{
    public class ViewAlbumPhoto
    {
        public IList<Album> Photos { get; set; }
        public IList<Photo> Albums { get; set; }
    }
}