using System;
using Microsoft.WindowsAzure.Storage.Table;


namespace ProjectPortalService.Data
{
    public class CommentSectionTable : TableEntity

    {
        public CommentSectionTable()
        {
        }
        public CommentSectionTable(string commentSectionId, string blobName)
        {
            this.PartitionKey = commentSectionId;
            this.RowKey = blobName;
            this.IsCommentSection = true;

        }

        public bool IsCommentSection { get; set; }
        public string CommentSectionName { get; set; }
    }
}