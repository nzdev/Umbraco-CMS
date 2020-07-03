﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Web.PublishedCache.NuCache.DataSource
{
    public interface IContentNestedDataSerializer
    {
        ContentNestedData Deserialize(string data);
        string Serialize(ContentNestedData nestedData);
    }
}
