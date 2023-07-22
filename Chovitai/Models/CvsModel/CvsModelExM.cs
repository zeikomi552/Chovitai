using Chovitai.Common.Utilities;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;
using static Chovitai.Models.CvsModel.CvsModelM;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace Chovitai.Models.CvsModel
{
    public class CvsModelExM : ModelBase
    {
        public CvsModelExM(CvsModelM model)
        {
            // set request items
            Items = new ModelList<CvsItem>(model.Items);

            // set request metadata
            Metadata = model.Metadata;
        }

        #region json result of items[Items]プロパティ
        /// <summary>
        /// json result of items[Items]プロパティ用変数
        /// </summary>
        ModelList<CvsItem> _Items = new ModelList<CvsItem>();
        /// <summary>
        /// json result of items[Items]プロパティ
        /// </summary>
        [JsonPropertyName("items")]
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


        #region マークダウンの出力処理
        /// <summary>
        /// マークダウンの出力処理
        /// </summary>
        public void MarkdownOutput1()
        {
            // ダイアログのインスタンスを生成
            var dialog = new SaveFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "マークダウン (*.md)|*.md";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                StringBuilder sb = new StringBuilder();

                int rank = 1;
                foreach (var item in Items)
                {
                    sb.AppendLine($"## {rank++}位 {item.Id} {item.Name}");
                    sb.AppendLine($"");
                    sb.AppendLine($"- Creator : {item.Creator.Username}");
                    sb.AppendLine($"- AllowCommercialUse : {item.AllowCommercialUse}");
                    sb.AppendLine($"- AllowNoCredit : {item.AllowNoCredit}");
                    sb.AppendLine($"- Nsfw : {item.Nsfw}");
                    sb.AppendLine($"- URL : https://civitai.com/models/{item.Id}");
                    //sb.AppendLine($"- DownloadCount : {item.Stats.DownloadCount}");
                    //sb.AppendLine($"- CommentCount:{item.Stats.CommentCount}");
                    //sb.AppendLine($"- FavoriteCount:{item.Stats.FavoriteCount}");
                    //sb.AppendLine($"- RatingCount:{item.Stats.RatingCount}");
                    //sb.AppendLine($"- Rating:{item.Stats.Rating}");
                    sb.AppendLine($"");
                    foreach (var modelver in item.ModelVersions)
                    {
                        sb.AppendLine($"### ver : {modelver.Name}");
                        sb.AppendLine($"");
                        sb.AppendLine($"- Create At {modelver.CreatedAt}");
                        sb.AppendLine($"- ModelVersionURL https://civitai.com/models/{item.Id}?modelVersionId={modelver.Id}");
                        sb.AppendLine($"- [Model Download]({modelver.DownloadUrl})");
                        sb.AppendLine($"");
                        int count = 0;
                        foreach (var image in modelver.Images)
                        {
                            sb.AppendLine($"");
                            //sb.AppendLine($"{image.Nsfw}");

                            if (image.Meta != null && (image.Nsfw.Equals("None") || image.Nsfw.Equals("Soft")))
                            {
                                sb.AppendLine($"```");
                                sb.AppendLine($"Prompt : {image.Meta.Prompt}");
                                sb.AppendLine($"");
                                sb.AppendLine($"Negative Prompt : {image.Meta.NegativPrompt}");
                                sb.AppendLine($"```");
                                sb.AppendLine($"");
                                sb.AppendLine($"");
                                sb.AppendLine($"<img alt=\"{image.Url}\" src=\"{image.Url}\" width=\"20%\">");
                                sb.AppendLine($"");
                                if (count++ > 2) break;
                                //break;
                            }
                        }
                        sb.AppendLine($"");
                        break;
                    }
                    sb.AppendLine($"");
                }

                // ファイル出力処理
                File.WriteAllText(dialog.FileName, sb.ToString());
            }
        }
        #endregion

        #region マークダウンの出力処理
        /// <summary>
        /// マークダウンの出力処理
        /// </summary>
        public void MarkdownOutput2()
        {
            // ダイアログのインスタンスを生成
            var dialog = new SaveFileDialog();

            // ファイルの種類を設定
            dialog.Filter = "マークダウン (*.md)|*.md";

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"## CIVITAIモデルダウンロードランキング");
                sb.AppendLine($"データ取得日 : {DateTime.Today.ToString("yyyy/MM/dd")}");
                sb.AppendLine($"※各リンクはCIVITAIへのログインが必要です");
                sb.AppendLine($"");
                sb.AppendLine($"CIVITAI");
                sb.AppendLine($"https://civitai.com/");
                sb.AppendLine($"");
                sb.AppendLine($"REST API");
                sb.AppendLine($"{RequestURL}");
                sb.AppendLine($"");


                sb.AppendLine($"|<center>順位</center><center>(DL数)</center>|モデルID / 作者名<br>モデル名|モデルタイプ<br>NSFW<br>商用利用|");
                sb.AppendLine($"|---|---|---|");


                int rank = 1;
                foreach (var item in Items)
                {
                    sb.AppendLine($"|<center>{rank++}位</center><center>({item.Stats.DownloadCount})</center>" +
                        $"|{item.Id} / [{item.Creator.Username}](https://civitai.com/user/{item.Creator.Username}/models)<br>[{item.Name.Replace("|", "\\|")}](https://civitai.com/models/{item.Id})" +
                        $"| {item.Type}<br>{(item.Nsfw ? "NSFW" : "-")}<br>{item.AllowCommercialUse}|");
                }

                // ファイル出力処理
                File.WriteAllText(dialog.FileName, sb.ToString());
            }
        }
        #endregion


    }
}
