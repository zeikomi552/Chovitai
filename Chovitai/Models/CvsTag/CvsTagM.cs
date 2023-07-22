using Chovitai.Models.CvsModel;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Chovitai.Models.CvsTag
{
    public class CvsTagM : ModelBase
    {
        #region GET Model Endpoint[Endpoint]プロパティ
        /// <summary>
        /// GET Model Endpoint[Endpoint]プロパティ用変数
        /// </summary>
        public const string Endpoint = "https://civitai.com/api/v1/tags";
        #endregion

        public class CvsItem : ModelBase
        {
            #region The name of the tag[Name]プロパティ
            /// <summary>
            /// The name of the tag[Name]プロパティ用変数
            /// </summary>
            string _Name = string.Empty;
            /// <summary>
            /// The name of the tag[Name]プロパティ
            /// </summary>
            [JsonPropertyName("name")]
            public string Name
            {
                get
                {
                    return _Name;
                }
                set
                {
                    if (_Name == null || !_Name.Equals(value))
                    {
                        _Name = value;
                        NotifyPropertyChanged("Name");
                    }
                }
            }
            #endregion

            #region The amount of models linked to this tag[ModelCount]プロパティ
            /// <summary>
            /// The amount of models linked to this tag[ModelCount]プロパティ用変数
            /// </summary>
            int _ModelCount = 0;
            /// <summary>
            /// The amount of models linked to this tag[ModelCount]プロパティ
            /// </summary>
            [JsonPropertyName("modelCount")]
            public int ModelCount
            {
                get
                {
                    return _ModelCount;
                }
                set
                {
                    if (!_ModelCount.Equals(value))
                    {
                        _ModelCount = value;
                        NotifyPropertyChanged("ModelCount");
                    }
                }
            }
            #endregion

            #region Url to get all models from this tag[Link]プロパティ
            /// <summary>
            /// Url to get all models from this tag[Link]プロパティ用変数
            /// </summary>
            string _Link = string.Empty;
            /// <summary>
            /// Url to get all models from this tag[Link]プロパティ
            /// </summary>
            [JsonPropertyName("link")]
            public string Link
            {
                get
                {
                    return _Link;
                }
                set
                {
                    if (_Link == null || !_Link.Equals(value))
                    {
                        _Link = value;
                        NotifyPropertyChanged("Link");
                    }
                }
            }
            #endregion
        }


        #region json result of items[Items]プロパティ
        /// <summary>
        /// json result of items[Items]プロパティ用変数
        /// </summary>
        ObservableCollection<CvsItem> _Items = new ObservableCollection<CvsItem>();
        /// <summary>
        /// json result of items[Items]プロパティ
        /// </summary>
        [JsonPropertyName("items")]
        public ObservableCollection<CvsItem> Items
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

        #region 選択アイテム[SelectedItem]プロパティ
        /// <summary>
        /// 選択アイテム[SelectedItem]プロパティ用変数
        /// </summary>
        CvsItem _SelectedItem = new CvsItem();
        /// <summary>
        /// 選択アイテム[SelectedItem]プロパティ
        /// </summary>
        public CvsItem SelectedItem
        {
            get
            {
                return _SelectedItem;
            }
            set
            {
                if (_SelectedItem == null || !_SelectedItem.Equals(value))
                {
                    _SelectedItem = value;
                    NotifyPropertyChanged("SelectedItem");
                }
            }
        }
        #endregion


        #region Metadata[Metadata]プロパティ
        /// <summary>
        /// Metadata[Metadata]プロパティ用変数
        /// </summary>
        CvsMetadataM _Metadata = new CvsMetadataM();
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
