
using System;
using Microsoft.WindowsAzure.Storage.Table;


namespace ProjectPortalService.Data
{
    public class CommentTable: TableEntity
    {
        public CommentTable()
        {

        }
        public CommentTable(string commentSection,string commentId)
        {
            this.PartitionKey = commentSection;
            this.RowKey = commentId;
            this.ParentCommentSection = commentSection;
        }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string CommentContent { get; set; }

        public DateTime CommentTime { get; set; }

        public string ParentCommentSection { get; set; }

        public string HeadIcon { get; set; }
    }
}