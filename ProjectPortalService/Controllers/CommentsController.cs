using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectPortalService.Contracts;

using System.Web.Http;
using ProjectPortalService.Data;
using System.Configuration;
using Swashbuckle.Swagger.Annotations;
using ProjectPortalService.Repository;
using ProjectPortalService.Utilities;
using Microsoft.WindowsAzure.Storage.Table;



namespace ProjectPortalService.Controllers
{
    public class CommentsController : ApiController
    {
        private string _storageConnectionString;
        private ITableRepository<CommentSectionTable> _commentSectionTableRepository;
        private ITableRepository<CommentTable> _commenTableRepository;


        public CommentsController()
        {
            _storageConnectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
            _commentSectionTableRepository = new TableRepository<CommentSectionTable>(_storageConnectionString, Constants.CommentTableName);
            _commenTableRepository = new TableRepository<CommentTable>(_storageConnectionString, Constants.CommentTableName);

        }
        // GET: api/values
        [HttpGet]
        public IHttpActionResult Get(string Id, bool returnTimeSpan = false)
        {
            var commentSectionTable = _commentSectionTableRepository.Get(Id, Id);
            if (commentSectionTable == null) return NotFound();

            var commentTables = _commenTableRepository.Get(Id).Where(x=>x.PartitionKey!=x.RowKey).OrderByDescending(x=>x.CommentTime);

            var blogCommentList = new List<BlogComment>();
            foreach (var blogCommentTable in commentTables)
            {             
                var blogComment = new BlogComment();
                blogComment.BlogCommentID = Guid.Parse(blogCommentTable.RowKey) ;
                blogComment.UserName = blogCommentTable.UserName;
                blogComment.CommentContent = blogCommentTable.CommentContent;
                blogComment.Email = blogCommentTable.Email;
                if (returnTimeSpan)
                {
                    blogComment.CommentTime = GetTimeSpanString(blogCommentTable.CommentTime);
                }
                else
                {
                    blogComment.CommentTime = blogCommentTable.CommentTime.ToString();
                }
                blogCommentList.Add(blogComment);
            }

            var comment = new CommentSection()
            {
                CommentSectionId = Guid.Parse(commentSectionTable.PartitionKey),
                BlogName = commentSectionTable.CommentSectionName,
                BlogCommentList = blogCommentList
            };

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(comment));
            
        }

        private string GetTimeSpanString(DateTime commentTime)
        {
            var timeSpan = new TimeSpan(0);
            timeSpan = DateTime.UtcNow - commentTime.ToUniversalTime();
            if(timeSpan> new TimeSpan(24,0,0))
            {
                return "More then One day ago";
            }
            else if( timeSpan > new TimeSpan(1,0,0))
            {
                return timeSpan.ToString(@"hh")  + " hours " + timeSpan.ToString(@"mm") + " mins ago";
            }
            else if( timeSpan > new TimeSpan(0,3,0))
            {
                return timeSpan.ToString(@"mm") + " mins ago";
            }
            else
            {
                return "within 3 mins";
            }
        }


        // POST api/values 
        [HttpPost]
        public IHttpActionResult AddCommentSection(CommentSection commentSection)
        {
            var commentTables = new List<CommentTable>();
            //Insert the section table first
            var commentSectionTable = new CommentSectionTable(commentSection.CommentSectionId.ToString(), commentSection.CommentSectionId.ToString());
            commentSectionTable.CommentSectionName = commentSection.BlogName;
            _commentSectionTableRepository.InsertOrMerge(commentSectionTable);


            //add the comment in the section
            foreach (var comment in commentSection.BlogCommentList)
            {
                var commentTable = new CommentTable(commentSection.CommentSectionId.ToString(), comment.BlogCommentID.ToString());
                commentTable.CommentContent = comment.CommentContent;
                commentTable.CommentTime = DateTime.UtcNow;
                commentTable.UserName = comment.UserName;
                commentTable.Email = comment.Email;
                commentTables.Add(commentTable);
            }

            _commenTableRepository.InsertOrMerge(commentTables);
            return Created("commentSection Created",commentSection);
        }


        // POST api/values 
        [HttpPost]
        [Route("~/Api/AddComment")]
        public IHttpActionResult AddComment(string sectionId, BlogComment blogCommentInfo)
        {
            if(_commentSectionTableRepository.Get(sectionId, sectionId) ==null)
            {
                return NotFound();
            }
            else
            {
                var commentTable = new CommentTable(sectionId, Guid.NewGuid().ToString());
                commentTable.UserName = blogCommentInfo.UserName;
                commentTable.ParentCommentSection = sectionId;
                commentTable.Email = blogCommentInfo.Email;
                commentTable.CommentContent = blogCommentInfo.CommentContent;
                commentTable.CommentTime = DateTime.UtcNow;
                _commenTableRepository.InsertOrMerge(commentTable);
                return Created("new comment Created", blogCommentInfo);
            }
        }

        //[HttpPost]
        //public void AddComment(string commentSectionId, BlogComment commentSection)
        //{

        //}

        //[HttpPost]
        //public void AddSubComment(CommentSection commentSection)
        //{
        //}

        [HttpDelete]
        public void DeleteComment(string commentSectionId, string commentId)
        {
        }


    }
}
