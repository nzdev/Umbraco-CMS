using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Umbraco.Web.PublishedCache.NuCache
{
    internal static class CacheKeys
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string DraftOrPub(bool previewing)
        {
            return previewing ? "D:" : "P:";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string LangId(string culture)
        {
            return culture != null ? ("-L:" + culture) : string.Empty;
        }

        public static string PublishedContentChildren(Guid contentUid, bool previewing)
        {
            return "NC.C.C[" + DraftOrPub(previewing) + ":" + contentUid + "]";
        }

        public static string ContentCacheRoots(bool previewing)
        {
            return "C.CC.R[" + DraftOrPub(previewing) + "]";
        }

        public static string MediaCacheRoots(bool previewing)
        {
            return "N.MC.R[" + DraftOrPub(previewing) + "]";
        }

        public static string PublishedContentAsPreviewing(Guid contentUid)
        {
            return "N.C.AP[" + contentUid + "]";
        }

        public static string ProfileName(int userId)
        {
            return "NuCache.Profile.Name[" + userId + "]";
        }

        public static string PropertyCacheValues(Guid contentUid, string typeAlias, bool previewing)
        {
            return (previewing ? "N.PC.D" : "N.PC.P") + contentUid + ":" + typeAlias;
        }

        // routes still use int id and not Guid uid, because routable nodes must have
        // a valid ID in the database at that point, whereas content and properties
        // may be virtual (and not in umbracoNode).

        public static string ContentCacheRouteByContent(int id, bool previewing, string culture)
        {
            return (previewing ? "N.CC.RBCD["  : "N.CC.RBCP[" ) + id + LangId(culture);
        }

        public static string ContentCacheContentByRoute(string route, bool previewing, string culture)
        {
            return (previewing ? "N.CC.CBRD[" : "N.CC.CBRP[") + route + LangId(culture);
        }

        //public static string ContentCacheRouteByContentStartsWith()
        //{
        //    return "NuCache.ContentCache.RouteByContent[";
        //}

        //public static string ContentCacheContentByRouteStartsWith()
        //{
        //    return "NuCache.ContentCache.ContentByRoute[";
        //}

        public static string MemberCacheMember(string name, bool previewing, object p)
        {
            return "NuCache.MemberCache." + name + "[" + DraftOrPub(previewing) + p + "]";
        }
    }
}
