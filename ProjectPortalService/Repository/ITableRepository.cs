using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;


namespace ProjectPortalService.Repository
{
    public interface ITableRepository<T> where T:ITableEntity
    {
        bool Insert(T entity);
        bool Insert(List<T> entities);
        List<T> Get(string partitionKey);
        T Get(string partitionKey, string rowKey);
        bool InsertOrMerge(T entity);
        bool InsertOrMerge(List<T> entities);
    }
}