using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Linq;

namespace ProjectPortalService.Repository
{
    public class TableRepository<T> : ITableRepository<T> where T : TableEntity, new()
    {
        private CloudTable _tableRef;

        public TableRepository(string connectionString, string tableName)
        {
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            _tableRef = tableClient.GetTableReference(tableName);

            // Create the table if it doesn't exist.
            _tableRef.CreateIfNotExists();
        }



        /// <summary>
        /// Insert single entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(T entity)
        {
            try
            {
                TableOperation insertOperation = TableOperation.Insert(entity);
                _tableRef.Execute(insertOperation);
                return true;
            }
            catch (Exception)
            {
                //todo: log
                return false;
            }
        }

        /// <summary>
        /// Insert single entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(List<T> entities)
        {
            try
            {
                TableBatchOperation insertBatchOperation = new TableBatchOperation();
                foreach (var entity in entities)
                {
                    insertBatchOperation.Insert(entity);
                }

                _tableRef.ExecuteBatch(insertBatchOperation);
                return true;
            }
            catch (Exception)
            {
                //todo: log
                return false;
            }
        }


        /// <summary>
        /// Get from partition
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<T> Get(string partitionKey)
        {
            try
            {
                List<T> entities = new List<T>();

                TableQuery<T> query = new TableQuery<T>()
                    .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
                entities = _tableRef.ExecuteQuery(query).ToList();

                return entities;
            }
            catch (Exception)
            {
                //todo: log
                return null;
            }
        }


        public T Get(string partitionKey, string rowKey)
        {
            try
            {
                T entity = new T();

                TableQuery<T> query = new TableQuery<T>().Where(
                TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                TableOperators.And,
                TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey)));

                var tableSet = _tableRef.ExecuteQuery(query).ToList();

                if (tableSet.Count >= 1)
                {
                    return tableSet.First();
                }

                return null;
            }
            catch (Exception)
            {
                //todo: log
                return null;
            }
        }


        /// <summary>
        /// Insert or merge
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertOrMerge(T entity)
        {
            try
            {
                // Create the InsertOrReplace TableOperation.
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(entity);

                // Execute the operation.
                _tableRef.Execute(insertOrMergeOperation);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Insert single entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool InsertOrMerge(List<T> entities)
        {
            try
            {
                TableBatchOperation insertBatchOperation = new TableBatchOperation();
                foreach (var entity in entities)
                {
                    insertBatchOperation.InsertOrMerge(entity);
                }

                _tableRef.ExecuteBatch(insertBatchOperation);
                return true;
            }
            catch (Exception)
            {
                //todo: log
                return false;
            }
        }
    }
}