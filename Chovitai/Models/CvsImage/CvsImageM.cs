using Chovitai.Common;
using Chovitai.Common.Commands;
using Chovitai.Common.Enums;
using Chovitai.Models.CvsModel;
using Chovitai.ViewModels;
using Chovitai.Views;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using MVVMCore.Common.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace Chovitai.Models.CvsImage
{
    public class CvsImageM : ModelBase
    {
        #region GET Model Endpoint[Endpoint]プロパティ
        /// <summary>
        /// GET Model Endpoint[Endpoint]プロパティ用変数
        /// </summary>
        public const string Endpoint = "https://civitai.com/api/v1/images";
        #endregion
        public class CvsItem : ModelBase
        {
            public CvsItem()
            {
                MouseDoubleClickCommand = new DelegateCommand(OpenURL);
            }

            public static DelegateCommand? MouseDoubleClickCommand { get; private set; }

            #region ブックマーク[IsBookmark]プロパティ
            /// <summary>
            /// ブックマーク[IsBookmark]プロパティ用変数
            /// </summary>
            bool _IsBookmark = false;
            /// <summary>
            /// ブックマーク[IsBookmark]プロパティ
            /// </summary>
            [JsonPropertyName("IsBookmark")]
            public bool IsBookmark
            {
                get
                {
                    return _IsBookmark;
                }
                set
                {
                    if (!_IsBookmark.Equals(value))
                    {
                        _IsBookmark = value;
                        NotifyPropertyChanged("IsBookmark");
                    }
                }
            }
            #endregion

            #region InnerClass
            public class CvsImageMeta : ModelBase
            {
                #region [ENSD]プロパティ
                /// <summary>
                /// [ENSD]プロパティ用変数
                /// </summary>
                string _ENSD = string.Empty;
                /// <summary>
                /// [ENSD]プロパティ
                /// </summary>
                [JsonPropertyName("ENSD")]
                public string ENSD
                {
                    get
                    {
                        return _ENSD;
                    }
                    set
                    {
                        if (_ENSD == null || !_ENSD.Equals(value))
                        {
                            _ENSD = value;
                            NotifyPropertyChanged("ENSD");
                        }
                    }
                }
                #endregion

                #region [Size]プロパティ
                /// <summary>
                /// [Size]プロパティ用変数
                /// </summary>
                string _Size = string.Empty;
                /// <summary>
                /// [Size]プロパティ
                /// </summary>
                [JsonPropertyName("Size")]
                public string Size
                {
                    get
                    {
                        return _Size;
                    }
                    set
                    {
                        if (_Size == null || !_Size.Equals(value))
                        {
                            _Size = value;
                            NotifyPropertyChanged("Size");
                        }
                    }
                }
                #endregion

                #region [Seed]プロパティ
                /// <summary>
                /// [Seed]プロパティ用変数
                /// </summary>
                object? _Seed = null;
                /// <summary>
                /// [Seed]プロパティ
                /// </summary>
                [JsonPropertyName("seed")]
                public object? Seed
                {
                    get
                    {
                        return _Seed;
                    }
                    set
                    {
                        if (_Seed == null || !_Seed.Equals(value))
                        {
                            _Seed = value;
                            NotifyPropertyChanged("Seed");
                        }
                    }
                }
                #endregion

                #region [Model]プロパティ
                /// <summary>
                /// [Model]プロパティ用変数
                /// </summary>
                string _Model = string.Empty;
                /// <summary>
                /// [Model]プロパティ
                /// </summary>
                [JsonPropertyName("Model")]
                public string Model
                {
                    get
                    {
                        return _Model;
                    }
                    set
                    {
                        if (_Model == null || !_Model.Equals(value))
                        {
                            _Model = value;
                            NotifyPropertyChanged("Model");
                        }
                    }
                }
                #endregion

                #region [Steps]プロパティ
                /// <summary>
                /// [Steps]プロパティ用変数
                /// </summary>
                int _Steps = 0;
                /// <summary>
                /// [Steps]プロパティ
                /// </summary>
                [JsonPropertyName("steps")]
                public int Steps
                {
                    get
                    {
                        return _Steps;
                    }
                    set
                    {
                        if (!_Steps.Equals(value))
                        {
                            _Steps = value;
                            NotifyPropertyChanged("Steps");
                        }
                    }
                }
                #endregion

                #region [Prompt]プロパティ
                /// <summary>
                /// [Prompt]プロパティ用変数
                /// </summary>
                string _Prompt = string.Empty;
                /// <summary>
                /// [Prompt]プロパティ
                /// </summary>
                [JsonPropertyName("prompt")]
                public string Prompt
                {
                    get
                    {
                        return _Prompt;
                    }
                    set
                    {
                        if (_Prompt == null || !_Prompt.Equals(value))
                        {
                            _Prompt = value;
                            NotifyPropertyChanged("Prompt");
                        }
                    }
                }
                #endregion

                #region [Sampler]プロパティ
                /// <summary>
                /// [Sampler]プロパティ用変数
                /// </summary>
                string _Sampler = string.Empty;
                /// <summary>
                /// [Sampler]プロパティ
                /// </summary>
                [JsonPropertyName("sampler")]
                public string Sampler
                {
                    get
                    {
                        return _Sampler;
                    }
                    set
                    {
                        if (_Sampler == null || !_Sampler.Equals(value))
                        {
                            _Sampler = value;
                            NotifyPropertyChanged("Sampler");
                        }
                    }
                }
                #endregion

                #region [CfgScale]プロパティ
                /// <summary>
                /// [CfgScale]プロパティ用変数
                /// </summary>
                object _CfgScale = new object();
                /// <summary>
                /// [CfgScale]プロパティ
                /// </summary>
                [JsonPropertyName("cfgScale")]
                public object CfgScale
                {
                    get
                    {
                        return _CfgScale;
                    }
                    set
                    {
                        if (_CfgScale == null || !_CfgScale.Equals(value))
                        {
                            _CfgScale = value;
                            NotifyPropertyChanged("CfgScale");
                        }
                    }
                }
                #endregion

                #region [Clip_skip]プロパティ
                /// <summary>
                /// [Clip_skip]プロパティ用変数
                /// </summary>
                object _Clip_skip = string.Empty;
                /// <summary>
                /// [Clip_skip]プロパティ
                /// </summary>
                [JsonPropertyName("Clip skip")]
                public object Clip_skip
                {
                    get
                    {
                        return _Clip_skip;
                    }
                    set
                    {
                        if (_Clip_skip == null || !_Clip_skip.Equals(value))
                        {
                            _Clip_skip = value;
                            NotifyPropertyChanged("Clip_skip");
                        }
                    }
                }
                #endregion

                #region [Model_hash]プロパティ
                /// <summary>
                /// [Model_hash]プロパティ用変数
                /// </summary>
                string _Model_hash = string.Empty;
                /// <summary>
                /// [Model_hash]プロパティ
                /// </summary>
                [JsonPropertyName("Model hash")]
                public string Model_hash
                {
                    get
                    {
                        return _Model_hash;
                    }
                    set
                    {
                        if (_Model_hash == null || !_Model_hash.Equals(value))
                        {
                            _Model_hash = value;
                            NotifyPropertyChanged("Model_hash");
                        }
                    }
                }
                #endregion

                #region [Hires_upscale]プロパティ
                /// <summary>
                /// [Hires_upscale]プロパティ用変数
                /// </summary>
                string _Hires_upscale = string.Empty;
                /// <summary>
                /// [Hires_upscale]プロパティ
                /// </summary>
                [JsonPropertyName("Hires upscale")]
                public string Hires_upscale
                {
                    get
                    {
                        return _Hires_upscale;
                    }
                    set
                    {
                        if (_Hires_upscale == null || !_Hires_upscale.Equals(value))
                        {
                            _Hires_upscale = value;
                            NotifyPropertyChanged("Hires_upscale");
                        }
                    }
                }
                #endregion

                #region [Hires_upscaler]プロパティ
                /// <summary>
                /// [Hires_upscaler]プロパティ用変数
                /// </summary>
                string _Hires_upscaler = string.Empty;
                /// <summary>
                /// [Hires_upscaler]プロパティ
                /// </summary>
                [JsonPropertyName("Hires upscaler")]
                public string Hires_upscaler
                {
                    get
                    {
                        return _Hires_upscaler;
                    }
                    set
                    {
                        if (_Hires_upscaler == null || !_Hires_upscaler.Equals(value))
                        {
                            _Hires_upscaler = value;
                            NotifyPropertyChanged("Hires_upscaler");
                        }
                    }
                }
                #endregion

                #region [NegativePrompt]プロパティ
                /// <summary>
                /// [NegativePrompt]プロパティ用変数
                /// </summary>
                string _NegativePrompt = string.Empty;
                /// <summary>
                /// [NegativePrompt]プロパティ
                /// </summary>
                [JsonPropertyName("negativePrompt")]
                public string NegativePrompt
                {
                    get
                    {
                        return _NegativePrompt;
                    }
                    set
                    {
                        if (_NegativePrompt == null || !_NegativePrompt.Equals(value))
                        {
                            _NegativePrompt = value;
                            NotifyPropertyChanged("NegativePrompt");
                        }
                    }
                }
                #endregion

                #region [ControlNet_Model]プロパティ
                /// <summary>
                /// [ControlNet_Model]プロパティ用変数
                /// </summary>
                string _ControlNet_Model = String.Empty;
                /// <summary>
                /// [ControlNet_Model]プロパティ
                /// </summary>
                [JsonPropertyName("ControlNet Model")]
                public string ControlNet_Model
                {
                    get
                    {
                        return _ControlNet_Model;
                    }
                    set
                    {
                        if (_ControlNet_Model == null || !_ControlNet_Model.Equals(value))
                        {
                            _ControlNet_Model = value;
                            NotifyPropertyChanged("ControlNet_Model");
                        }
                    }
                }
                #endregion

                #region [ControlNet_Module]プロパティ
                /// <summary>
                /// [ControlNet_Module]プロパティ用変数
                /// </summary>
                string _ControlNet_Module = String.Empty;
                /// <summary>
                /// [ControlNet_Module]プロパティ
                /// </summary>
                [JsonPropertyName("ControlNet Module")]
                public string ControlNet_Module
                {
                    get
                    {
                        return _ControlNet_Module;
                    }
                    set
                    {
                        if (_ControlNet_Module == null || !_ControlNet_Module.Equals(value))
                        {
                            _ControlNet_Module = value;
                            NotifyPropertyChanged("ControlNet_Module");
                        }
                    }
                }
                #endregion

                #region [ControlNet_Weight]プロパティ
                /// <summary>
                /// [ControlNet_Weight]プロパティ用変数
                /// </summary>
                string _ControlNet_Weight = string.Empty;
                /// <summary>
                /// [ControlNet_Weight]プロパティ
                /// </summary>
                [JsonPropertyName("ControlNet Weight")]
                public string ControlNet_Weight
                {
                    get
                    {
                        return _ControlNet_Weight;
                    }
                    set
                    {
                        if (_ControlNet_Weight == null || !_ControlNet_Weight.Equals(value))
                        {
                            _ControlNet_Weight = value;
                            NotifyPropertyChanged("ControlNet_Weight");
                        }
                    }
                }
                #endregion

                #region [ControlNet_Enabled]プロパティ
                /// <summary>
                /// [ControlNet_Enabled]プロパティ用変数
                /// </summary>
                string _ControlNet_Enabled = string.Empty;
                /// <summary>
                /// [ControlNet_Enabled]プロパティ
                /// </summary>
                [JsonPropertyName("ControlNet Enabled")]
                public string ControlNet_Enabled
                {
                    get
                    {
                        return _ControlNet_Enabled;
                    }
                    set
                    {
                        if (_ControlNet_Enabled == null || !_ControlNet_Enabled.Equals(value))
                        {
                            _ControlNet_Enabled = value;
                            NotifyPropertyChanged("ControlNet_Enabled");
                        }
                    }
                }
                #endregion

                #region [Denoising_strength]プロパティ
                /// <summary>
                /// [Denoising_strength]プロパティ用変数
                /// </summary>
                string _Denoising_strength = string.Empty;
                /// <summary>
                /// [Denoising_strength]プロパティ
                /// </summary>
                [JsonPropertyName("Denoising strength")]
                public string Denoising_strength
                {
                    get
                    {
                        return _Denoising_strength;
                    }
                    set
                    {
                        if (_Denoising_strength == null || !_Denoising_strength.Equals(value))
                        {
                            _Denoising_strength = value;
                            NotifyPropertyChanged("Denoising_strength");
                        }
                    }
                }
                #endregion

                #region [ControlNet_Guidance_Strength]プロパティ
                /// <summary>
                /// [ControlNet_Guidance_Strength]プロパティ用変数
                /// </summary>
                string _ControlNet_Guidance_Strength = string.Empty;
                /// <summary>
                /// [ControlNet_Guidance_Strength]プロパティ
                /// </summary>
                [JsonPropertyName("ControlNet Guidance Strength")]
                public string ControlNet_Guidance_Strength
                {
                    get
                    {
                        return _ControlNet_Guidance_Strength;
                    }
                    set
                    {
                        if (_ControlNet_Guidance_Strength == null || !_ControlNet_Guidance_Strength.Equals(value))
                        {
                            _ControlNet_Guidance_Strength = value;
                            NotifyPropertyChanged("ControlNet_Guidance_Strength");
                        }
                    }
                }
                #endregion
            }
            #endregion

            #region The id of the image[Id]プロパティ
            /// <summary>
            /// The id of the image[Id]プロパティ用変数
            /// </summary>
            int _Id = 0;
            /// <summary>
            /// The id of the image[Id]プロパティ
            /// </summary>
            [JsonPropertyName("id")]
            public int Id
            {
                get
                {
                    return _Id;
                }
                set
                {
                    if (!_Id.Equals(value))
                    {
                        _Id = value;
                        NotifyPropertyChanged("Id");
                    }
                }
            }
            #endregion
           
            #region The url of the image at it's source resolution[Url]プロパティ
            /// <summary>
            /// The url of the image at it's source resolution[Url]プロパティ用変数
            /// </summary>
            string _Url = string.Empty;
            /// <summary>
            /// The url of the image at it's source resolution[Url]プロパティ
            /// </summary>
            [JsonPropertyName("url")]
            public string Url
            {
                get
                {
                    return _Url;
                }
                set
                {
                    if (_Url == null || !_Url.Equals(value))
                    {
                        _Url = value;
                        NotifyPropertyChanged("Url");
                    }
                }
            }
            #endregion

            #region The blurhash of the image[Hash]プロパティ
            /// <summary>
            /// The blurhash of the image[Hash]プロパティ用変数
            /// </summary>
            string _Hash = string.Empty;
            /// <summary>
            /// The blurhash of the image[Hash]プロパティ
            /// </summary>
            [JsonPropertyName("hash")]
            public string Hash
            {
                get
                {
                    return _Hash;
                }
                set
                {
                    if (_Hash == null || !_Hash.Equals(value))
                    {
                        _Hash = value;
                        NotifyPropertyChanged("Hash");
                    }
                }
            }
            #endregion

            #region The blurhash of the image[Width]プロパティ
            /// <summary>
            /// The blurhash of the image[Width]プロパティ用変数
            /// </summary>
            int _Width = 0;
            /// <summary>
            /// The blurhash of the image[Width]プロパティ
            /// </summary>
            [JsonPropertyName("width")]
            public int Width
            {
                get
                {
                    return _Width;
                }
                set
                {
                    if (!_Width.Equals(value))
                    {
                        _Width = value;
                        NotifyPropertyChanged("Width");
                    }
                }
            }
            #endregion

            #region  The height of the image[Height]プロパティ
            /// <summary>
            ///  The height of the image[Height]プロパティ用変数
            /// </summary>
            int _Height = 0;
            /// <summary>
            ///  The height of the image[Height]プロパティ
            /// </summary>
            [JsonPropertyName("height")]
            public int Height
            {
                get
                {
                    return _Height;
                }
                set
                {
                    if (!_Height.Equals(value))
                    {
                        _Height = value;
                        NotifyPropertyChanged("Height");
                    }
                }
            }
            #endregion

            #region If the image has any mature content labels[Nsfw]プロパティ
            /// <summary>
            /// If the image has any mature content labels[Nsfw]プロパティ用変数
            /// </summary>
            bool _Nsfw = false;
            /// <summary>
            /// If the image has any mature content labels[Nsfw]プロパティ
            /// </summary>
            [JsonPropertyName("nsfw")]
            public bool Nsfw
            {
                get
                {
                    return _Nsfw;
                }
                set
                {
                    if (!_Nsfw.Equals(value))
                    {
                        _Nsfw = value;
                        NotifyPropertyChanged("Nsfw");
                    }
                }
            }
            #endregion

            #region The NSFW level of the image[NsfwLevel]プロパティ
            /// <summary>
            /// The NSFW level of the image[NsfwLevel]プロパティ用変数
            /// </summary>
            string _NsfwLevel = string.Empty;
            /// <summary>
            /// The NSFW level of the image[NsfwLevel]プロパティ
            /// </summary>
            [JsonPropertyName("nsfwLevel")]
            public string NsfwLevel
            {
                get
                {
                    return _NsfwLevel;
                }
                set
                {
                    if (_NsfwLevel == null || !_NsfwLevel.Equals(value))
                    {
                        _NsfwLevel = value;
                        NotifyPropertyChanged("NsfwLevel");
                    }
                }
            }
            #endregion

            #region The date the image was posted[CreatedAt]プロパティ
            /// <summary>
            /// The date the image was posted[CreatedAt]プロパティ用変数
            /// </summary>
            DateTime _CreatedAt = DateTime.MinValue;
            /// <summary>
            /// The date the image was posted[CreatedAt]プロパティ
            /// </summary>
            [JsonPropertyName("createdAt")]
            public DateTime CreatedAt
            {
                get
                {
                    return _CreatedAt;
                }
                set
                {
                    if (!_CreatedAt.Equals(value))
                    {
                        _CreatedAt = value;
                        NotifyPropertyChanged("CreatedAt");
                    }
                }
            }
            #endregion

            #region The ID of the post the image belongs to[PostId]プロパティ
            /// <summary>
            /// The ID of the post the image belongs to[PostId]プロパティ用変数
            /// </summary>
            int _PostId = 0;
            /// <summary>
            /// The ID of the post the image belongs to[PostId]プロパティ
            /// </summary>
            [JsonPropertyName("postId")]
            public int PostId
            {
                get
                {
                    return _PostId;
                }
                set
                {
                    if (!_PostId.Equals(value))
                    {
                        _PostId = value;
                        NotifyPropertyChanged("PostId");
                    }
                }
            }
            #endregion

            #region The ID of the post the image belongs to[Meta]プロパティ
            /// <summary>
            /// The ID of the post the image belongs to[Meta]プロパティ用変数
            /// </summary>
            CvsImageMeta _Meta = new CvsImageMeta();
            /// <summary>
            /// The ID of the post the image belongs to[Meta]プロパティ
            /// </summary>
            [JsonPropertyName("meta")]
            public CvsImageMeta Meta
            {
                get
                {
                    return _Meta;
                }
                set
                {
                    if (_Meta == null || !_Meta.Equals(value))
                    {
                        _Meta = value;
                        NotifyPropertyChanged("Meta");
                    }
                }
            }
            #endregion

            #region The username of the creator[Username]プロパティ
            /// <summary>
            /// The username of the creator[Username]プロパティ用変数
            /// </summary>
            string _Username = string.Empty;
            /// <summary>
            /// The username of the creator[Username]プロパティ
            /// </summary>
            [JsonPropertyName("username")]
            public string Username
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

            #region ImageのCivitaiURLL
            /// <summary>
            /// ImageのCivitaiURLL
            /// </summary>
            public string ImageURL
            {
                get
                {
                    return $"https://civitai.com/images/{this.Id}";
                }
            }
            #endregion

            #region URLを開く
            /// <summary>
            /// URLを開く
            /// </summary>
            public void OpenURL()
            {
                try
                {
                    var startInfo = new System.Diagnostics.ProcessStartInfo(ImageURL);
                    startInfo.UseShellExecute = true;
                    System.Diagnostics.Process.Start(startInfo);
                }
                catch (Exception ex)
                {
                    ShowMessage.ShowErrorOK(ex.Message, "Error");
                }
            }
            #endregion

            #region プロンプトツールを開く処理
            /// <summary>
            /// プロンプトツールを開く処理
            /// </summary>
            public void OpenPromptTool()
            {
                var tmp = new PromptCountCollectionM();
                tmp.InitItems(this);

                var wnd = new PromptWindowV();
                var vm = wnd.DataContext as PromptWindowVM;
                vm!.PromptItems = tmp;

                if (wnd.ShowDialog() == true)
                {

                }
            }
            #endregion

            #region ブックマークへ追加
            /// <summary>
            /// ブックマークへ追加
            /// </summary>
            public void ChangeBookmark()
            {
                try
                {
                    var model_bookmark = GblValues.Instance.ImageBookmark;

                    // nullチェック
                    if (model_bookmark != null && model_bookmark.ImageBookmarkConf != null
                        && model_bookmark.ImageBookmarkConf.Item != null && model_bookmark.ImageBookmarkConf.Item.Items != null)
                    {
                        var model_bookmark_items = model_bookmark.ImageBookmarkConf.Item.Items;
                        var check = from x in model_bookmark_items
                                    where x.Id.Equals(Id)
                                    select x;

                        // BookmarkOn
                        if (IsBookmark)
                        {
                            // 存在しなければ
                            if (!check.Any())
                            {
                                // ブックマークの追加
                                model_bookmark_items.Add(this);
                            }
                        }
                        else
                        {
                            // ブックマークの削除
                            model_bookmark_items.Remove(check.First());
                        }
                        // JSON形式で保存
                        model_bookmark.ImageBookmarkConf.SaveJSON();
                    }
                }
                catch (Exception ex)
                {
                    ShowMessage.ShowErrorOK(ex.Message, "Error");
                }
            }
            #endregion
        }

        #region Element of Image Items[Items]プロパティ
        /// <summary>
        /// Element of Image Items[Items]プロパティ用変数
        /// </summary>
        ObservableCollection<CvsItem> _Items = new ObservableCollection<CvsItem>();
        /// <summary>
        /// Element of Image Items[Items]プロパティ
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
    }
}
