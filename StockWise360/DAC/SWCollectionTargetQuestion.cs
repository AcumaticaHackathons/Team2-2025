using System;
using PX.Data;
using PX.Data.BQL;
using PX.Data.BQL.Fluent;
using PX.Data.ReferentialIntegrity.Attributes;

namespace StockWise360.DAC
{
    [PXCacheName("SW Collection Target Question")]
    public class SWCollectionTargetQuestion : PXBqlTable, IBqlTable
    {
        public class PK : PrimaryKeyOf<SWCollectionTargetQuestion>.By<collectionTargetQuestionID>
        {
            public static SWCollectionTargetQuestion Find(PXGraph graph, int? collectionTargetQuestionID) => FindBy(graph, collectionTargetQuestionID);
        }

        #region CollectionTargetQuestionID
        /// <summary>
        ///   Question ID
        /// </summary>
        [PXDBIdentity(IsKey = true)]
        [PXUIField(DisplayName="Question ID", Enabled = false)]
        public int? CollectionTargetQuestionID { get; set; }
        /// <exclude/>
        public abstract class collectionTargetQuestionID : BqlInt.Field<collectionTargetQuestionID> { }
        #endregion
        
        #region CollectionTargetID
        /// <summary>
        ///   Collection Target ID
        /// </summary>
        [PXDBInt]
        [PXDBDefault(typeof(SWCollectionTarget.collectionTargetID))]
        [PXParent(typeof(SelectFrom<SWCollectionTarget>
            .Where<SWCollectionTarget.collectionTargetID.IsEqual<collectionTargetID.FromCurrent>>))]
        [PXUIField(DisplayName="Collection Target ID", Enabled = false)]
        public int? CollectionTargetID { get; set; }
        /// <exclude/>
        public abstract class collectionTargetID : BqlInt.Field<collectionTargetID> { }
        #endregion      
        
        #region LineNbr
        /// <summary>
        ///   Custom LineNbr field.
        ///   The default LineNbr for SOLine increments by 2 (e.g., 1, 3, 5, 7).
        ///   This customization ensures a sequential numbering (e.g., 1, 2, 3, 4).
        /// </summary>
        [PXDBInt]
        [PXLineNbr(typeof(SWCollectionTarget.lineCntr))]
        [PXDefault(PersistingCheck = PXPersistingCheck.Nothing)]
        [PXUIField(DisplayName="Line Nbr.", Enabled = false, Visible = true)]
        public int? LineNbr { get; set; }
        /// <exclude/>
        public abstract class lineNbr : PX.Data.BQL.BqlInt.Field<lineNbr> { }
        #endregion
        
        #region ThumbnailURL
        /// <summary>
        ///   ThumbnailURL
        /// </summary>
        [PXString]
        [PXUIField(DisplayName="Image", Enabled = false)]
        public string ThumbnailURL { get; set; }
        /// <exclude />
        public abstract class thumbnailURL : IBqlField { }
        #endregion
        
        #region JsonResult
        /// <summary>
        ///   Json Result
        /// </summary>
        [PXDBString]
        [PXUIField(DisplayName="Json Result")]
        public string JsonResult { get; set; }
        /// <exclude/>
        public abstract class jsonResult : PX.Data.BQL.BqlString.Field<jsonResult> { }
        #endregion
        
        #region ItemID
        /// <summary>
        ///   Item ID
        /// </summary>
        [PXDBString]
        [PXUIField(DisplayName="Item ID")]
        public string ItemID { get; set; }
        /// <exclude/>
        public abstract class itemID : PX.Data.BQL.BqlString.Field<itemID> { }
        #endregion  
        
        #region Manufacturer
        /// <summary>
        ///   Manufacturer
        /// </summary>
        [PXDBString]
        [PXUIField(DisplayName="Manufacturer")]
        public string Manufacturer { get; set; }
        /// <exclude/>
        public abstract class manufacturer : PX.Data.BQL.BqlString.Field<manufacturer> { }
        #endregion
        
        #region Information
        /// <summary>
        ///   Information
        /// </summary>
        [PXDBString]
        [PXUIField(DisplayName="Information")]
        public string Information { get; set; }
        /// <exclude/>
        public abstract class information : PX.Data.BQL.BqlString.Field<information> { }
        #endregion
        
        #region Description
        /// <summary>
        ///   Description
        /// </summary>
        [PXDBString]
        [PXUIField(DisplayName="Description")]
        public string Description { get; set; }
        /// <exclude/>
        public abstract class description : PX.Data.BQL.BqlString.Field<description> { }
        #endregion
        
        #region Vendors
        /// <summary>
        ///   Vendors
        /// </summary>
        [PXDBString]
        [PXUIField(DisplayName="Vendors")]
        public string Vendors { get; set; }
        /// <exclude/>
        public abstract class vendors : PX.Data.BQL.BqlString.Field<vendors> { }
        #endregion
        
        #region Use
        /// <summary>
        ///   Use
        /// </summary>
        [PXDBString]
        [PXUIField(DisplayName="Use")]
        public string Use { get; set; }
        /// <exclude/>
        public abstract class use : PX.Data.BQL.BqlString.Field<use> { }
        #endregion
        
        #region Lead
        /// <summary>
        ///   Lead
        /// </summary>
        [PXDBString]
        [PXUIField(DisplayName="Lead")]
        public string Lead { get; set; }
        /// <exclude/>
        public abstract class lead : PX.Data.BQL.BqlString.Field<lead> { }
        #endregion
        
        #region Noteid
        /// <summary>
        /// Note ID establishes a globally unique identifier and facilitates attachments
        /// </summary>
        [PXNote]
        public virtual Guid? Noteid { get; set; }
        /// <summary>
        /// Note ID establishes a globally unique identifier and facilitates attachments
        /// </summary>
        public abstract class noteid : BqlType<IBqlGuid, Guid>.Field<noteid> { }
        #endregion
        
        #region Audit fields
        
        #region CreatedByID
        /// <summary>
        ///   CreatedByID Audit Field
        /// </summary>
        [PXDBCreatedByID]
        public virtual Guid? CreatedByID { get; set; }
        /// <exclude/>
        public abstract class createdByID : BqlType<IBqlGuid, Guid>.Field<createdByID> { }
        #endregion

        #region CreatedByScreenID
        /// <summary>
        ///   CreatedByScreenID Audit Field
        /// </summary>
        [PXDBCreatedByScreenID]
        public virtual string CreatedByScreenID { get; set; }
        /// <exclude/>
        public abstract class createdByScreenID : BqlType<IBqlString, string>.Field<createdByScreenID> { }
        #endregion

        #region CreatedDateTime
        /// <summary>
        ///   CreatedDateTime Audit Field
        /// </summary>
        [PXDBCreatedDateTime]
        [PXUIField(DisplayName = "Created Date Time")]
        public virtual DateTime? CreatedDateTime { get; set; }
        /// <exclude/>
        public abstract class createdDateTime : BqlType<IBqlDateTime, DateTime>.Field<createdDateTime> { }
        #endregion

        #region LastModifiedByID
        /// <summary>
        ///   LastModifiedByID Audit Field
        /// </summary>
        [PXDBLastModifiedByID]
        public virtual Guid? LastModifiedByID { get; set; }
        /// <exclude/>
        public abstract class lastModifiedByID : BqlType<IBqlGuid, Guid>.Field<lastModifiedByID> { }
        #endregion

        #region LastModifiedByScreenID
        /// <summary>
        ///   LastModifiedByScreenID Audit Field
        /// </summary>
        [PXDBLastModifiedByScreenID]
        public virtual string LastModifiedByScreenID { get; set; }
        /// <exclude/>
        public abstract class lastModifiedByScreenID : BqlType<IBqlString, string>.Field<lastModifiedByScreenID> { }
        #endregion
        #region LastModifiedDateTime

        /// <summary>
        ///   LastModifiedDateTime Audit Field
        /// </summary>
        [PXDBLastModifiedDateTime]
        [PXUIField(DisplayName = "Last Modified Date Time")]
        public virtual DateTime? LastModifiedDateTime { get; set; }
        /// <exclude/>
        public abstract class lastModifiedDateTime : BqlType<IBqlDateTime, DateTime>.Field<lastModifiedDateTime> { }
        #endregion

        #region Tstamp
        /// <summary>
        ///   Time Stamp Field used for concurrency control
        /// </summary>
        [PXDBTimestamp]
        [PXUIField(DisplayName = "Tstamp")]
        public virtual byte[] Tstamp { get; set; }
        /// <exclude/>
        public abstract class tstamp : BqlType<IBqlByteArray, byte[]>.Field<tstamp> { }
        #endregion

        #endregion
    }
}