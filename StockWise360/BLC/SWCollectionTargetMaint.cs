using PX.Data;
using PX.Data.BQL.Fluent;
using StockWise360.DAC;

namespace StockWise360.BLC
{
    public class SWCollectionTargetMaint : PXGraph<SWCollectionTargetMaint, SWCollectionTarget>
    {
        public SelectFrom<SWCollectionTarget>.View CollectionTargetView;

        public SelectFrom<SWCollectionTargetQuestion>
            .Where<SWCollectionTargetQuestion.collectionTargetID.IsEqual<SWCollectionTarget.collectionTargetID.FromCurrent>>
            .View CollectionTargetQuestionView;
    }
}