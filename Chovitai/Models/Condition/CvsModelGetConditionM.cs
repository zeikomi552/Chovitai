using Chovitai.Common.Enums;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Models.Condition
{
    public class CvsModelGetConditionM : ModelBase
    {
        #region The number of results to be returned per page. This can be a number between 1 and 200. By default, each page will return 100 results[Limit]プロパティ
        /// <summary>
        /// The number of results to be returned per page. This can be a number between 1 and 200. By default, each page will return 100 results[Limit]プロパティ用変数
        /// </summary>
        int _Limit = 100;
        /// <summary>
        /// The number of results to be returned per page. This can be a number between 1 and 200. By default, each page will return 100 results[Limit]プロパティ
        /// </summary>
        public int Limit
        {
            get
            {
                return _Limit;
            }
            set
            {
                if (!_Limit.Equals(value))
                {
                    _Limit = value;
                    NotifyPropertyChanged("Limit");
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

        #region Search query to filter models by name[Query ]プロパティ
        /// <summary>
        /// Search query to filter models by name[Query ]プロパティ用変数
        /// </summary>
        string? _Query = null;
        /// <summary>
        /// Search query to filter models by name[Query ]プロパティ
        /// </summary>
        public string? Query
        {
            get
            {
                return _Query;
            }
            set
            {
                if (_Query == null || !_Query.Equals(value))
                {
                    _Query = value;
                    NotifyPropertyChanged("Query");
                }
            }
        }
        #endregion

        #region Search query to filter models by tag[Tag ]プロパティ
        /// <summary>
        /// Search query to filter models by tag[Tag ]プロパティ用変数
        /// </summary>
        string? _Tag = null;
        /// <summary>
        /// Search query to filter models by tag[Tag ]プロパティ
        /// </summary>
        public string? Tag
        {
            get
            {
                return _Tag;
            }
            set
            {
                if (_Tag == null || !_Tag.Equals(value))
                {
                    _Tag = value;
                    NotifyPropertyChanged("Tag");
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

        #region The type of model you want to filter with. If none is specified, it will return all types[Types]プロパティ
        /// <summary>
        /// The type of model you want to filter with. If none is specified, it will return all types[Types]プロパティ用変数
        /// </summary>
        ModelTypeEnum? _Types = ModelTypeEnum.Empty;
        /// <summary>
        /// The type of model you want to filter with. If none is specified, it will return all types[Types]プロパティ
        /// </summary>
        public ModelTypeEnum? Types
        {
            get
            {
                return _Types;
            }
            set
            {
                if (_Types == null || !_Types.Equals(value))
                {
                    _Types = value;
                    NotifyPropertyChanged("Types");
                }
            }
        }
        #endregion

        #region The order in which you wish to sort the results[Sort]プロパティ
        /// <summary>
        /// The order in which you wish to sort the results[Sort]プロパティ用変数
        /// </summary>
        ModelSortEnum? _Sort = ModelSortEnum.Empty;
        /// <summary>
        /// The order in which you wish to sort the results[Sort]プロパティ
        /// </summary>
        public ModelSortEnum? Sort
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

        #region The rating you wish to filter the models with. If none is specified, it will return models with any rating[Rating]プロパティ
        /// <summary>
        /// The rating you wish to filter the models with. If none is specified, it will return models with any rating[Rating]プロパティ用変数
        /// </summary>
        decimal? _Rating = null;
        /// <summary>
        /// The rating you wish to filter the models with. If none is specified, it will return models with any rating[Rating]プロパティ
        /// </summary>
        public decimal? Rating
        {
            get
            {
                return _Rating;
            }
            set
            {
                if (_Rating == null || !_Rating.Equals(value))
                {
                    _Rating = value;
                    NotifyPropertyChanged("Rating");
                }
            }
        }
        #endregion

        #region Filter to favorites of the authenticated user (this requires an API token or session cookie)[Favorites]プロパティ
        /// <summary>
        /// Filter to favorites of the authenticated user (this requires an API token or session cookie)[Favorites]プロパティ用変数
        /// </summary>
        bool? _Favorites = null;
        /// <summary>
        /// Filter to favorites of the authenticated user (this requires an API token or session cookie)[Favorites]プロパティ
        /// </summary>
        public bool? Favorites
        {
            get
            {
                return _Favorites;
            }
            set
            {
                if (_Favorites == null || !_Favorites.Equals(value))
                {
                    _Favorites = value;
                    NotifyPropertyChanged("Favorites");
                }
            }
        }
        #endregion

        #region Filter to hidden models of the authenticated user (this requires an API token or session cookie)[Hidden]プロパティ
        /// <summary>
        /// Filter to hidden models of the authenticated user (this requires an API token or session cookie)[Hidden]プロパティ用変数
        /// </summary>
        bool? _Hidden = null;
        /// <summary>
        /// Filter to hidden models of the authenticated user (this requires an API token or session cookie)[Hidden]プロパティ
        /// </summary>
        public bool? Hidden
        {
            get
            {
                return _Hidden;
            }
            set
            {
                if (_Hidden == null || !_Hidden.Equals(value))
                {
                    _Hidden = value;
                    NotifyPropertyChanged("Hidden");
                }
            }
        }
        #endregion

        #region Only include the primary file for each model (This will use your preferred format options if you use an API token or session cookie)[PrimaryFileOnly]プロパティ
        /// <summary>
        /// Only include the primary file for each model (This will use your preferred format options if you use an API token or session cookie)[PrimaryFileOnly]プロパティ用変数
        /// </summary>
        bool? _PrimaryFileOnly = null;
        /// <summary>
        /// Only include the primary file for each model (This will use your preferred format options if you use an API token or session cookie)[PrimaryFileOnly]プロパティ
        /// </summary>
        public bool? PrimaryFileOnly
        {
            get
            {
                return _PrimaryFileOnly;
            }
            set
            {
                if (_PrimaryFileOnly == null || !_PrimaryFileOnly.Equals(value))
                {
                    _PrimaryFileOnly = value;
                    NotifyPropertyChanged("PrimaryFileOnly");
                }
            }
        }
        #endregion


        #region Filter to models that require or don't require crediting the creator[AllowNoCredit ]プロパティ
        /// <summary>
        /// Filter to models that require or don't require crediting the creator[AllowNoCredit ]プロパティ用変数
        /// </summary>
        bool? _AllowNoCredit = null;
        /// <summary>
        /// Filter to models that require or don't require crediting the creator[AllowNoCredit ]プロパティ
        /// </summary>
        public bool? AllowNoCredit
        {
            get
            {
                return _AllowNoCredit;
            }
            set
            {
                if (_AllowNoCredit == null || !_AllowNoCredit.Equals(value))
                {
                    _AllowNoCredit = value;
                    NotifyPropertyChanged("AllowNoCredit");
                }
            }
        }
        #endregion

        #region Filter to models that allow or don't allow creating derivatives[AllowDerivatives ]プロパティ
        /// <summary>
        /// Filter to models that allow or don't allow creating derivatives[AllowDerivatives ]プロパティ用変数
        /// </summary>
        bool? _AllowDerivatives = null;
        /// <summary>
        /// Filter to models that allow or don't allow creating derivatives[AllowDerivatives ]プロパティ
        /// </summary>
        public bool? AllowDerivatives
        {
            get
            {
                return _AllowDerivatives;
            }
            set
            {
                if (_AllowDerivatives == null || !_AllowDerivatives.Equals(value))
                {
                    _AllowDerivatives = value;
                    NotifyPropertyChanged("AllowDerivatives");
                }
            }
        }
        #endregion

        #region Filter to models that allow or don't allow derivatives to have a different license[AllowDifferentLicenses ]プロパティ
        /// <summary>
        /// Filter to models that allow or don't allow derivatives to have a different license[AllowDifferentLicenses ]プロパティ用変数
        /// </summary>
        bool? _AllowDifferentLicenses = null;
        /// <summary>
        /// Filter to models that allow or don't allow derivatives to have a different license[AllowDifferentLicenses ]プロパティ
        /// </summary>
        public bool? AllowDifferentLicenses
        {
            get
            {
                return _AllowDifferentLicenses;
            }
            set
            {
                if (_AllowDifferentLicenses == null || !_AllowDifferentLicenses.Equals(value))
                {
                    _AllowDifferentLicenses = value;
                    NotifyPropertyChanged("AllowDifferentLicenses");
                }
            }
        }
        #endregion

        #region Filter to models based on their commercial permissions[AllowCommercialUse ]プロパティ
        /// <summary>
        /// Filter to models based on their commercial permissions[AllowCommercialUse ]プロパティ用変数
        /// </summary>
        ModelAllowCommercialUseEnum? _AllowCommercialUse = ModelAllowCommercialUseEnum.Empty;
        /// <summary>
        /// Filter to models based on their commercial permissions[AllowCommercialUse ]プロパティ
        /// </summary>
        public ModelAllowCommercialUseEnum? AllowCommercialUse
        {
            get
            {
                return _AllowCommercialUse;
            }
            set
            {
                if (_AllowCommercialUse == null || !_AllowCommercialUse.Equals(value))
                {
                    _AllowCommercialUse = value;
                    NotifyPropertyChanged("AllowCommercialUse");
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
                if (this.Page.HasValue) query += $"&page={this.Page.Value}";
                if (!string.IsNullOrEmpty(this.Query)) query += $"&query={this.Query}";
                if (!string.IsNullOrEmpty(this.Tag)) query += $"&tag={this.Tag}";
                if (!string.IsNullOrEmpty(this.Username)) query += $"&username={this.Username}";
                if (this.Types.HasValue && !this.Types.Equals(ModelTypeEnum.Empty)) query += $"&types={this.Types.Value.ToString().Replace("_","+")}";
                if (this.Sort.HasValue && !this.Sort.Equals(ModelSortEnum.Empty)) query += $"&sort={this.Sort.Value.ToString().Replace("_", "+")}";
                if (this.Period.HasValue && !this.Period.Equals(ModelPeriodEnum.Empty)) query += $"&period={this.Period.Value}";
                if (this.Rating.HasValue) query += $"&rating={this.Rating.Value}";
                if (this.Favorites.HasValue) query += $"&favorites={this.Favorites.Value}";
                if (this.Hidden.HasValue) query += $"&hidden={this.Hidden.Value}";
                if (this.PrimaryFileOnly.HasValue) query += $"&primaryFileOnly={this.PrimaryFileOnly.Value}";
                if (this.AllowNoCredit.HasValue) query += $"&allowNoCredit={this.AllowNoCredit.Value}";
                if (this.AllowDerivatives.HasValue) query += $"&allowDerivatives={this.AllowDerivatives.Value}";
                if (this.AllowDifferentLicenses.HasValue) query += $"&allowDifferentLicenses={this.AllowDifferentLicenses.Value}";
                if (this.AllowCommercialUse.HasValue && !this.AllowCommercialUse.Equals(ModelAllowCommercialUseEnum.Empty)) query += $"&allowCommercialUse={this.AllowCommercialUse.Value}";
                if (this.Nsfw.HasValue) query += $"&nsfw={this.Nsfw.Value}";

                return "?" + query;

            }
        }
        #endregion



    }
}
