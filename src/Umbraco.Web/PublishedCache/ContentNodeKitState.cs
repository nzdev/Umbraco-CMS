using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco.Web.PublishedCache
{
    [Flags]
    public enum ContentNodeKitLoadState : short
    {
        [Display(Name = "None", Description = "No data loaded")]
        None = 0,
        [Display(Name = "Routing Properties Loaded", Description = "Routing Properties Loaded")]
        RoutingPropertiesLoaded = 1,
        [Display(Name = "All Draft Properties Loaded", Description = "All Draft Properties Loaded")]
        AllDraftPropertiesLoaded = 4,
        [Display(Name = "All Published Properties Loaded", Description = "All Published Properties Loaded")]
        AllPublishedPropertiesLoaded = 8,
        [Display(Name = "All", Description = "All data loaded")]
        All = 16,

    }
}
