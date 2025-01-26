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
        
        #region QuestionText
        /// <summary>
        ///   Question
        /// </summary>
        [PXDBString(4000)]
        [PXUIField(DisplayName="Question")]
        public string QuestionText { get; set; }
        /// <exclude/>
        public abstract class questionText : BqlString.Field<questionText> { }
        #endregion
         
        #region ResultSample
        /// <summary>
        ///   Result Sample Text
        /// </summary>
        [PXDBString(4000)]
        [PXUIField(DisplayName="Result Sample Text")]
        public string ResultSample { get; set; }
        /// <exclude/>
        public abstract class resultSample : BqlString.Field<resultSample> { }
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