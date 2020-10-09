using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.PublishedCache.NuCache.LiteDb
{
    public class LiteDbContentSettings
    {
        public ConnectionString ConnectionString { get; set; }
        public string CollectionName { get; set; }
    }
}
