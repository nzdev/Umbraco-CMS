using Umbraco.Cms.Core.Composing;

namespace Umbraco.Cms.Core.WebAssets
{
    public class CustomBackOfficeAssetsCollectionBuilder :
        InstancesOrderedCollectionBuilderBase<CustomBackOfficeAssetsCollectionBuilder, CustomBackOfficeAssetsCollection, IAssetFile>
    {
        protected override CustomBackOfficeAssetsCollectionBuilder This => this;
    }
}
