using Chovitai.Models.CvsModel;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static Chovitai.Models.CvsImage.CvsImageM;

namespace Chovitai.Models.CvsImage
{
    public class CvsImageExM : ModelBase
    {
        #region Element of Image Items[Items]プロパティ
        /// <summary>
        /// Element of Image Items[Items]プロパティ用変数
        /// </summary>
        ModelList<CvsItem> _Items = new ModelList<CvsItem>();
        /// <summary>
        /// Element of Image Items[Items]プロパティ
        /// </summary>
        public ModelList<CvsItem> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                if (_Items == null || !_Items.Equals(value))
                {
                    _Items = value;
                    NotifyPropertyChanged("Items");
                }
            }
        }
        #endregion

        #region Metadata[Metadata]プロパティ
        /// <summary>
        /// Metadata[Metadata]プロパティ用変数
        /// </summary>
        CvsMetadataM _Metadata = new ();
        /// <summary>
        /// Metadata[Metadata]プロパティ
        /// </summary>
        [JsonPropertyName("metadata")]
        public CvsMetadataM Metadata
        {
            get
            {
                return _Metadata;
            }
            set
            {
                if (_Metadata == null || !_Metadata.Equals(value))
                {
                    _Metadata = value;
                    NotifyPropertyChanged("Metadata");
                }
            }
        }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model">モデル</param>
        public CvsImageExM(CvsImageM model)
        {
            // set request items
            Items = new ModelList<CvsItem>(model.Items);

            // set request metadata
            Metadata = model.Metadata;
        }
        #endregion

        #region json row data[Rowdata]プロパティ
        /// <summary>
        /// json row data[Rowdata]プロパティ用変数
        /// </summary>
        string _Rowdata = string.Empty;
        /// <summary>
        /// json row data[Rowdata]プロパティ
        /// </summary>
        public string Rowdata
        {
            get
            {
                return _Rowdata;
            }
            set
            {
                if (_Rowdata == null || !_Rowdata.Equals(value))
                {
                    _Rowdata = value;
                    NotifyPropertyChanged("Rowdata");
                }
            }
        }
        #endregion

        #region request url[RequestURL]プロパティ
        /// <summary>
        /// request url[RequestURL]プロパティ用変数
        /// </summary>
        string _RequestURL = string.Empty;
        /// <summary>
        /// request url[RequestURL]プロパティ
        /// </summary>
        public string RequestURL
        {
            get
            {
                return _RequestURL;
            }
            set
            {
                if (_RequestURL == null || !_RequestURL.Equals(value))
                {
                    _RequestURL = value;
                    NotifyPropertyChanged("RequestURL");
                }
            }
        }
        #endregion
    }
}
