using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Text;


namespace ProjectPortalService.Repository
{
    public class BlobRepository :IBlobRepository
    {
        private CloudBlobContainer _blobContainerRef;

        public BlobRepository(string connectionString, string containerName)
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to the table.
            _blobContainerRef = blobClient.GetContainerReference(containerName);

            // Create the table if it doesn't exist.
            _blobContainerRef.CreateIfNotExists();
        }

        public bool Upload(string content, string fileName)
        {
            try
            {
                CloudBlockBlob blobRef = _blobContainerRef.GetBlockBlobReference(fileName);

                // convert string to stream
                byte[] byteArray = Encoding.UTF8.GetBytes(content);
                //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
                MemoryStream stream = new MemoryStream(byteArray);

                blobRef.UploadFromStream(stream);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Upload(byte[] content, string fileName)
        {
            try
            {
                CloudBlockBlob blobRef = _blobContainerRef.GetBlockBlobReference(fileName);
              
                //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
                MemoryStream stream = new MemoryStream(content);

                blobRef.UploadFromStream(stream);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string DownLoad(string containerName, string fileName)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                CloudBlockBlob blockBlob = _blobContainerRef.GetBlockBlobReference(fileName);
                blockBlob.DownloadToStream(stream);
                StreamReader reader = new StreamReader(stream);
                string content = reader.ReadToEnd();
                return content;
            }
            catch (Exception)
            {
                return null; 
            }                
        }
    }
}