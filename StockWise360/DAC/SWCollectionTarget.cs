using System;
using PX.Data;
using PX.Data.BQL;
using PX.Data.ReferentialIntegrity.Attributes;
using StockWise360.BLC;

namespace StockWise360.DAC
{
    [PXCacheName("SW Collection Target")]
    [PXPrimaryGraph(typeof(SWCollectionTargetMaint))]
    public class SWCollectionTarget : PXBqlTable, IBqlTable
    {
        public class PK : PrimaryKeyOf<SWCollectionTarget>.By<collectionTargetID>
        {
            public static SWCollectionTarget Find(PXGraph graph, int? collectionTargetID) => FindBy(graph, collectionTargetID);
        }

        #region CollectionTargetID
        /// <summary>
        ///   Collection Target ID
        /// </summary>
        [PXDBIdentity(IsKey = true)]
        [PXUIField(DisplayName="Collection Target ID")]
        public int? CollectionTargetID { get; set; }
        /// <exclude/>
        public abstract class collectionTargetID : BqlInt.Field<collectionTargetID> { }
        #endregion      
        
        #region CollectionName
        /// <summary>
        ///   Name
        /// </summary>
        [PXDBString(100)]
        [PXUIField(DisplayName="Name")]
        public string CollectionName { get; set; }
        /// <exclude/>
        public abstract class collectionName : BqlString.Field<collectionName> { }
        #endregion
        
        #region CollectionPath
        /// <summary>
        ///   Path
        /// </summary>
        [PXDBString(255)]
        [PXUIField(DisplayName="Path")]
        public string CollectionPath { get; set; }
        /// <exclude/>
        public abstract class collectionPath : BqlString.Field<collectionPath> { }
        #endregion
        
        #region MainPrompt
        /// <summary>
        ///   Main Prompt
        /// </summary>
        [PXDBString(4000)]
        [PXUIField(DisplayName="Main Prompt")]
        public string MainPrompt { get; set; }
        /// <exclude/>
        public abstract class mainPrompt : BqlString.Field<mainPrompt> { }
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