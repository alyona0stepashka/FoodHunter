using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using FH.Models.StaticModels;

namespace FH.Models.Models
{
    public class FileModel: BaseEntity
    {
        public string Name { get; set; }

        public string Extension { get; set; }

        public string Path { get; set; }

        [ForeignKey("Feedback")]
        public int? FeedbackId { get; set; }

        //---------------

        public virtual Feedback Feedback { get; set; }

        //[ForeignKey("PhotoAlbum")]
        //public int? PhotoAlbumId { get; set; }

        // [ForeignKey("Message")]
        //public int? MessageId { get; set; }

        //public virtual PhotoAlbum PhotoAlbum { get; set; }

        //public virtual ChatMessage Message { get; set; }
    }
}
