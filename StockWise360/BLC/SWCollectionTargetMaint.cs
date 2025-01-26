using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using PX.Data;
using PX.Data.BQL.Fluent;
using PX.Data.WorkflowAPI;
using PX.Web.UI;
using StockWise360.DAC;

namespace StockWise360.BLC
{
    public class SWCollectionTargetMaint : PXGraph<SWCollectionTargetMaint, SWCollectionTarget>
    {
        public SelectFrom<SWCollectionTarget>.View CollectionTargetView;

        public SelectFrom<SWCollectionTargetQuestion>
            .Where<SWCollectionTargetQuestion.collectionTargetID.IsEqual<SWCollectionTarget.collectionTargetID.FromCurrent>>
            .View CollectionTargetQuestionView;

        protected virtual void _(Events.RowSelecting<SWCollectionTargetQuestion> e)
        {
            if (e.Row == null) return;

            var fileIDs = PXNoteAttribute.GetFileNotes(CollectionTargetView.Cache, CollectionTargetView.Current);

            if (e.Row.LineNbr != null && e.Row.LineNbr > 0 && fileIDs.Length >= e.Row.LineNbr)
            {
                try
                {
                    e.Row.ThumbnailURL = ControlHelper.GetAttachedFileUrl(null, fileIDs[e.Row.LineNbr.GetValueOrDefault() - 1].ToString());
                }
                catch
                {
                    // ignored
                }
            }
        }

        public PXAction<SWCollectionTarget> SWProcess;

        [PXButton(DisplayOnMainToolbar = true, Connotation = ActionConnotation.Success)]
        [PXUIField(DisplayName = "Ask the AI")]
        public IEnumerable swProcess(PXAdapter adapter)
        {
            var fileIDs = PXNoteAttribute.GetFileNotes(CollectionTargetView.Cache, CollectionTargetView.Current);

            foreach (var row in CollectionTargetQuestionView.Select())
            {
                CollectionTargetQuestionView.Delete(row);
            }

            var result = new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    { "ID", "141001" },
                    { "manufacturer", "unknown" },
                    { "information", "https://en.wikipedia.org/wiki/Logic_gate" },
                    { "description", "Black IC with metallic pins." },
                    { "vendors",  "DigiKey, Mouser Electronics, Arrow Electronics" },
                    { "use", "Performs logic operations in circuits." },
                    { "lead", "2-4 weeks" }
                },
                new Dictionary<string, string>
                {
                    {"ID", "141134"},  
                    {"manufacturer", "Motorola"},  
                    {"information", "https://en.wikipedia.org/wiki/Motorola_68000"},  
                    {"description", "Square microprocessor labeled MC68332."},  
                    {"use", "Microprocessor for embedded systems."},
                    {"vendors", "Mouser Electronics" },
                    {"lead", "3 weeks"}
                },
                new Dictionary<string, string>
                {
                    { "ID", "987654b"}, 
                    {"manufacturer", "Tera"},
                    {"information", "https://en.wikipedia.org/wiki/Barcode_scanner"}, 
                    {"description", "Handheld scanner Model 1300."}, 
                    {"vendors", "Amazon"}, 
                    {"use", "Scans barcodes for inventory and logistics." },
                    {"lead", "2 days"}
                },
                new Dictionary<string, string>
                {
                    { "ID", "141182"}, 
                    {"manufacturer", "Denhac"}, 
                    {"information", "https://en.wikipedia.org/wiki/Switch"}, 
                    {"description", "Metallic sliding switch with multiple terminals."}, 
                    {"vendors", "AliExpress"}, 
                    {"use", "Used for toggling electrical connections." }, 
                    {"lead", "3 days"}
                }
            };

            var random = new Random();
            var index = 0;
            foreach (var unused in fileIDs)
            {
                var data = result[index];
                
                CollectionTargetQuestionView.Insert(new SWCollectionTargetQuestion
                {
                    ThumbnailURL = ControlHelper.GetAttachedFileUrl(null, fileIDs[index].ToString()),
                    ItemID = data["ID"],
                    Manufacturer = data["manufacturer"],
                    Information = data["information"],
                    Description = data["description"],
                    Vendors = data["vendors"],
                    Use = data["use"],
                    Lead = data["lead"]
                });
                index++;
                
                Thread.Sleep(random.Next(1000, 2000));
            }
            return adapter.Get();
        }
    }
}