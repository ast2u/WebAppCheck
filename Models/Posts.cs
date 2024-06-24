using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebAppCheck.Models
{
    public class Posts
    {
        [Key]
        public int PostId { get; set; }

        private string _postTitle;

        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "You need a title for your Post.")]
        [DisplayName("Post Title")]
        [MaxLength(50, ErrorMessage = "Maximum 50 characters only.")]
        public string PostTitle {
            get => _postTitle;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _postTitle = char.ToUpper(value[0]) + value.Substring(1);
                }
                else
                {
                    _postTitle = value;
                }
            }
        }

        [Column(TypeName = "nvarchar(300)")]
        [DisplayName("Post Content")]
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(300, ErrorMessage = "Maximum 300 characters only.")]
        public string PostContent { get; set; }

        [DisplayFormat(DataFormatString ="{0:MM/dd/yyyy}")]
        public DateTime CreatedDate { get; set; }


    }
}
