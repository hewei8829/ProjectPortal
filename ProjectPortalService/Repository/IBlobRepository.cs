using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectPortalService.Repository
{
    public interface IBlobRepository
    {
        bool Upload(string content, string fileName);
        bool Upload(byte[] content, string fileName);
        string DownLoad(string containerName, string fileName);
    }
}