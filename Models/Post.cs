using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAppCheck.Areas.Identity.Data;

namespace WebAppCheck.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Column(TypeName ="nvarchar(50)")]
        public string PostTitle { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        public string PostContent { get; set; }

        public DateTime CreatedDate { get; set; }
        //public List<Comment> Comments { get; set; }


        public string UserID { get; set; }
        public WebAppUser User { get; set; }
    }
}
