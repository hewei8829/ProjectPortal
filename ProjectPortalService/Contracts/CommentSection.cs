using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPortalService.Contracts
{
    public class CommentSection
    {
        public Guid CommentSectionId { get; set; }
        public string BlogName { get; set; }

        public List<BlogComment> BlogCommentList {get;set;}
        
    }
}
