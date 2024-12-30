using TrisGPOI.Core.Collection.Interfaces;
using TrisGPOI.Database.Collection;

namespace TrisGPOI.Core.Collection
{
    public static class CollectionListManager
    {
        public static string[] getList()
        {
            return CollectionList.collectionList;
        }
    }
}
