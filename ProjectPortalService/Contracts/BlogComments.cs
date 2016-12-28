using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ProjectPortalService.Contracts
{
    public class BlogCommentBase
    {
        [Required(ErrorMessage = "BlogCommentId is required")]
        public Guid BlogCommentID { get; set; }

        [Required(ErrorMessage = "BlogName is required")]
        public string BlogCommentName { get; set; }

        [Required(ErrorMessage ="UserName is required")]
        public string UserName { get; set; }

        public string Email { get; set; }

        [Required(ErrorMessage ="Comment cannot be empty")]
        public string CommentContent { get; set; }

        public String CommentTime { get; set; }

        public Uri HeadIcon { get; set; }
    }

    public class SubBlogComment : BlogCommentBase
    {
        public Guid ParentBlogCommentId { get; set; } 
    }

    public class BlogComment: BlogCommentBase
    {
        public List<SubBlogComment> SubBlogCommentList { get; set; } 
    }

  
}
