﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 20.01.2021 11:57:10
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;

namespace DocumentManager.DAL.Model
{
    /// <summary>
    /// Група на вид документ: - входящ - изходящ - вътрешен(напр. кредитно досие)
    /// </summary>
    public partial class DocumentTypeGroup {

        public DocumentTypeGroup()
        {
            this.DocumentTypes = new List<DocumentType>();
            OnCreated();
        }

        /// <summary>
        /// Група на вид документ: - входящ - изходящ - вътрешен(напр. кредитно досие)
        /// </summary>
        public virtual string DocumentTypeGroupCode { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public virtual string Name { get; set; }

        public virtual IList<DocumentType> DocumentTypes { get; set; }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}