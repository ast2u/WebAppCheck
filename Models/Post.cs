using System.ComponentModel;
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
        [Required(ErrorMessage ="You need a title for your Post.")]
        [DisplayName("Post Title")]
        [MaxLength(50,ErrorMessage ="Maximum 50 characters only.")]
        public string PostTitle { get; set; }

        [Column(TypeName = "nvarchar(300)")]
        [DisplayName("Post Content")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(300, ErrorMessage = "Maximum 300 characters only.")]
        public string PostContent { get; set; }

        public DateTime CreatedDate { get; set; }
        //public List<Comment> Comments { get; set; }


        public string UserID { get; set; }
        public WebAppUser User { get; set; }
    }
}
