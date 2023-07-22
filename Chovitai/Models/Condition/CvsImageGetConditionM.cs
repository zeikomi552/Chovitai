using Chovitai.Common.Enums;
using Chovitai.Models.CvsImage;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Models.Condition
{
    public class CvsImageGetConditionM : ModelBase
    {
        #region The number of results to be returned per page. This can be a number between 1 and 200. By default, each page will return 100 results[Limit]プロパティ
        /// <summary>
        /// The number of results to be returned per page. This can be a number between 1 and 200. By default, each page will return 100 results[Limit]プロパティ用変数
        /// </summary>
        int? _Limit = 100;
        /// <summary>
        /// The number of results to be returned per page. This can be a number between 1 and 200. By default, each page will return 100 results[Limit]プロパティ
        /// </summary>
        public int? Limit
        {
            get
            {
                return _Limit;
            }
            set
            {
                if (_Limit == null || !_Limit.Equals(value))
                {
                    _Limit = value;
                    NotifyPropertyChanged("Limit");
                }
            }
        }
        #endregion

        #region The ID of a post to get images from[PostId]プロパティ
        /// <summary>
        /// The ID of a post to get images from[PostId]プロパティ用変数
        /// </summary>
        Int64? _PostId = null;
        /// <summary>
        /// The ID of a post to get images from[PostId]プロパティ
        /// </summary>
        public Int64? PostId
        {
            get
            {
                return _PostId;
            }
            set
            {
                if (_PostId == null || !_PostId.Equals(value))
                {
                    _PostId = value;
                    NotifyPropertyChanged("PostId");
                }
            }
        }
        #endregion

        #region The ID of a model to get images from (model gallery)[ModelId]プロパティ
        /// <summary>
        /// The ID of a model to get images from (model gallery)[ModelId]プロパティ用変数
        /// </summary>
        Int64? _ModelId = null;
        /// <summary>
        /// The ID of a model to get images from (model gallery)[ModelId]プロパティ
        /// </summary>
        public Int64? ModelId
        {
            get
            {
                return _ModelId;
            }
            set
            {
                if (_ModelId == null || !_ModelId.Equals(value))
                {
                    _ModelId = value;
                    NotifyPropertyChanged("ModelId");
                }
            }
        }
        #endregion

        #region The ID of a model version to get images from (model gallery filtered to version)[ModelVersionId]プロパティ
        /// <summary>
        /// The ID of a model version to get images from (model gallery filtered to version)[ModelVersionId]プロパティ用変数
        /// </summary>
        Int64? _ModelVersionId = null;
        /// <summary>
        /// The ID of a model version to get images from (model gallery filtered to version)[ModelVersionId]プロパティ
        /// </summary>
        public Int64? ModelVersionId
        {
            get
            {
                return _ModelVersionId;
            }
            set
            {
                if (_ModelVersionId == null || !_ModelVersionId.Equals(value))
                {
                    _ModelVersionId = value;
                    NotifyPropertyChanged("ModelVersionId");
                }
            }
        }
        #endregion

        #region The page from which to start fetching models[Page ]プロパティ
        /// <summary>
        /// The page from which to start fetching models[Page ]プロパティ用変数
        /// </summary>
        int? _Page = 1;
        /// <summary>
        /// The page from which to start fetching models[Page ]プロパティ
        /// </summary>
        public int? Page
        {
            get
            {
                return _Page;
            }
            set
            {
                if (_Page == null || !_Page.Equals(value))
                {
                    _Page = value;
                    NotifyPropertyChanged("Page");
                }
            }
        }
        #endregion

        #region カーソルリスト[CursorList]プロパティ
        /// <summary>
        /// カーソルリスト[CursorList]プロパティ用変数
        /// </summary>
        Dictionary<int, string> _CursorList = new Dictionary<int, string>();
        /// <summary>
        /// カーソルリスト[CursorList]プロパティ
        /// </summary>
        public Dictionary<int, string> CursorList
        {
            get
            {
                return _CursorList;
            }
            set
            {
                if (_CursorList == null || !_CursorList.Equals(value))
                {
                    _CursorList = value;
                    NotifyPropertyChanged("CursorList");
                }
            }
        }
        #endregion

        #region Search query to filter models by user[Username ]プロパティ
        /// <summary>
        /// Search query to filter models by user[Username ]プロパティ用変数
        /// </summary>
        string? _Username = null;
        /// <summary>
        /// Search query to filter models by user[Username ]プロパティ
        /// </summary>
        public string? Username
        {
            get
            {
                return _Username;
            }
            set
            {
                if (_Username == null || !_Username.Equals(value))
                {
                    _Username = value;
                    NotifyPropertyChanged("Username");
                }
            }
        }
        #endregion

        #region The time frame in which the models will be sorted[Period]プロパティ
        /// <summary>
        /// The time frame in which the models will be sorted[Period]プロパティ用変数
        /// </summary>
        ModelPeriodEnum? _Period = ModelPeriodEnum.Empty;
        /// <summary>
        /// The time frame in which the models will be sorted[Period]プロパティ
        /// </summary>
        public ModelPeriodEnum? Period
        {
            get
            {
                return _Period;
            }
            set
            {
                if (_Period == null || !_Period.Equals(value))
                {
                    _Period = value;
                    NotifyPropertyChanged("Period");
                }
            }
        }
        #endregion

        #region If false, will return safer images and hide models that don't have safe images[Nsfw]プロパティ
        /// <summary>
        /// If false, will return safer images and hide models that don't have safe images[Nsfw]プロパティ用変数
        /// </summary>
        bool? _Nsfw = null;
        /// <summary>
        /// If false, will return safer images and hide models that don't have safe images[Nsfw]プロパティ
        /// </summary>
        public bool? Nsfw
        {
            get
            {
                return _Nsfw;
            }
            set
            {
                if (_Nsfw == null || !_Nsfw.Equals(value))
                {
                    _Nsfw = value;
                    NotifyPropertyChanged("Nsfw");
                }
            }
        }
        #endregion

        #region The order in which you wish to sort the results[Sort]プロパティ
        /// <summary>
        /// The order in which you wish to sort the results[Sort]プロパティ用変数
        /// </summary>
        ModelSortEnum2? _Sort = null;
        /// <summary>
        /// The order in which you wish to sort the results[Sort]プロパティ
        /// </summary>
        public ModelSortEnum2? Sort
        {
            get
            {
                return _Sort;
            }
            set
            {
                if (_Sort == null || !_Sort.Equals(value))
                {
                    _Sort = value;
                    NotifyPropertyChanged("Sort");
                }
            }
        }
        #endregion



        #region GET Condition[GetConditionQuery]プロパティ
        /// <summary>
        /// GET Condition[GetConditionQuery]プロパティ用変数
        /// </summary>
        string _GetConditionQuery = string.Empty;
        /// <summary>
        /// GET Condition[GetConditionQuery]プロパティ
        /// </summary>
        public string GetConditionQuery
        {
            get
            {
                string query = string.Empty;

                query += $"limit={this.Limit}";
                if (this.PostId.HasValue) query += $"&postId={this.PostId.Value}";
                if (this.ModelId.HasValue) query += $"&modelId={this.ModelId.Value}";
                if (this.ModelVersionId.HasValue) query += $"&modelVersionId={this.ModelVersionId.Value}";
                if (!string.IsNullOrEmpty(this.Username)) query += $"&username={this.Username}";
                if (this.Period.HasValue && !this.Period.Equals(ModelPeriodEnum.Empty)) query += $"&period={this.Period.Value}";
                if (this.Nsfw.HasValue) query += $"&nsfw={this.Nsfw.Value}";
                if (this.Sort.HasValue && !this.Sort.Equals(ModelSortEnum2.Empty)) query += $"&sort={this.Sort.Value.ToString().Replace("_", "+")}";
                //if (this.Page.HasValue) query += $"&page={this.Page.Value}";

                return "?" + query;
            }
        }
        #endregion

        /// <summary>
        /// カーソルのクリア
        /// </summary>
        public void CursorClear()
        {
            this.CursorList.Clear();
        }

        /// <summary>
        /// カーソルの追加
        /// </summary>
        /// <param name="img">イメージモデル</param>
        public void AddCursor(CvsImageExM img)
        {
            // nullチェック
            if (img != null && img.Metadata != null && img.Metadata.NextCursor != null)
            {
                // 次のカーソルのセット
                this.CursorList.Add(img.Metadata.NextCursor.Value, img.Metadata.NextPage);
            }
        }
        public void RemoveLastCursor()
        {
            if (this.CursorList.Count > 0)
            {
                this.CursorList.Remove(this.CursorList.LastOrDefault().Key);
            }
        }
    }
}
