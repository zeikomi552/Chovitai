using Chovitai.Models.CvsImage;
using Chovitai.Models.CvsModel;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Chovitai.Models.CvsModel.CvsModelM.CvsModelVersions;

namespace Chovitai.Models
{
    public class PromptCountCollectionM : ModelBase
    {
        public PromptCountCollectionM()
        {
            
        }

        #region プロンプト要素[PromptItems]プロパティ
        /// <summary>
        /// プロンプト要素[PromptItems]プロパティ用変数
        /// </summary>
        ModelList<PromptCountM> _PromptItems = new ModelList<PromptCountM>();
        /// <summary>
        /// プロンプト要素[PromptItems]プロパティ
        /// </summary>
        public ModelList<PromptCountM> PromptItems
        {
            get
            {
                return _PromptItems;
            }
            set
            {
                if (_PromptItems == null || !_PromptItems.Equals(value))
                {
                    _PromptItems = value;
                    NotifyPropertyChanged("PromptItems");
                }
            }
        }
        #endregion

        #region ネガティブプロンプト情報[NegativePromptItems]プロパティ
        /// <summary>
        /// ネガティブプロンプト情報[NegativePromptItems]プロパティ用変数
        /// </summary>
        ModelList<PromptCountM> _NegativePromptItems = new ModelList<PromptCountM>();
        /// <summary>
        /// ネガティブプロンプト情報[NegativePromptItems]プロパティ
        /// </summary>
        public ModelList<PromptCountM> NegativePromptItems
        {
            get
            {
                return _NegativePromptItems;
            }
            set
            {
                if (_NegativePromptItems == null || !_NegativePromptItems.Equals(value))
                {
                    _NegativePromptItems = value;
                    NotifyPropertyChanged("NegativePromptItems");
                }
            }
        }
        #endregion

        #region プロンプトリストの初期化
        /// <summary>
        /// プロンプトリストの初期化
        /// </summary>
        /// <param name="cvsimg">CIVITAI用イメージ要素</param>
        public void InitItems(CvsImageExM cvsimg)
        {
            // nullチェック
            if (cvsimg != null && cvsimg.Items != null && cvsimg.Items.Items != null)
            {
                // リストに変換
                var list = cvsimg.Items.Items.ToList<CvsImageM.CvsItem>();

                // プロンプトリストの初期化
                CreatePromptItems(list, false);

                // ネガティブプロンプトリストの初期化
                CreatePromptItems(list, true);
            }
        }
        #endregion

        #region プロンプトリストの初期化
        /// <summary>
        /// プロンプトリストの初期化
        /// </summary>
        /// <param name="cvsimg">CIVITAI用イメージ要素</param>
        public void InitItems(CvsImage.DisplayImageM cvsimg)
        {
            // リストに変換
            var list = cvsimg.FilteredImages.ToList<CvsImageM.CvsItem>();

            // プロンプトリストの初期化
            CreatePromptItems(list, false);

            // ネガティブプロンプトリストの初期化
            CreatePromptItems(list, true);

        }
        #endregion

        #region プロンプトリストの初期化
        /// <summary>
        /// プロンプトリストの初期化
        /// </summary>
        /// <param name="cvsimg">CIVITAI用イメージ要素</param>
        public void InitItems(CvsImageM.CvsItem cvsimg)
        {
            // リストに変換
            var list = new List<CvsImageM.CvsItem>();
            list.Add(cvsimg);

            // プロンプトリストの初期化
            CreatePromptItems(list, false);

            // ネガティブプロンプトリストの初期化
            CreatePromptItems(list, true);

        }
        #endregion

        #region プロンプトリストの初期化
        /// <summary>
        /// プロンプトリストの初期化
        /// </summary>
        /// <param name="cvsimg">CIVITAI用イメージ要素</param>
        public void InitItems(CvsModelM.CvsModelVersions.CvsImages cvsimg)
        {
            // リストに変換
            var list = new List<CvsModelM.CvsModelVersions.CvsImages>();
            list.Add(cvsimg);

            // プロンプトリストの初期化
            CreatePromptItems(list, false);

            // ネガティブプロンプトリストの初期化
            CreatePromptItems(list, true);

        }
        #endregion

        #region プロンプトリストの初期化
        /// <summary>
        /// プロンプトリストの初期化
        /// </summary>
        /// <param name="cvsimg">CIVITAI用イメージ要素</param>
        public void InitItems(List<CvsModelM.CvsItem> cvsimg)
        {
            // プロンプトリストの初期化
            CreatePromptItems(cvsimg, false);

            // ネガティブプロンプトリストの初期化
            CreatePromptItems(cvsimg, true);
        }
        #endregion

        #region プロンプトリストの初期化
        /// <summary>
        /// プロンプトリストの初期化
        /// </summary>
        /// <param name="cvsimg">CIVITAI用イメージ要素</param>
        public void InitItems(CvsModelExM cvsimg)
        {
            if (cvsimg != null && cvsimg.Items != null && cvsimg.Items.Items != null)
            {
                var list_items = cvsimg.Items.Items.ToList<CvsModelM.CvsItem>();
                InitItems(list_items);
            }
        }
        #endregion

        #region プロンプトリストの初期化
        /// <summary>
        /// プロンプトリストの初期化
        /// </summary>
        /// <param name="cvsimg">CIVITAI用イメージ要素</param>
        public void InitItems(CvsModelM.CvsItem cvsimg)
        {
            var list = new List<CvsModelM.CvsItem>();
            list.Add(cvsimg);
            InitItems(list);
        }
        #endregion

        #region Promptリストの初期化
        /// <summary>
        /// Promptリストの初期化
        /// </summary>
        /// <param name="cvsimg">Civitai用イメージ</param>
        /// <param name="negative_prompt_f">false:Promptのセット true:ネガティブプロンプトのセット</param>
        public void CreatePromptItems(List<CvsModelM.CvsModelVersions.CvsImages> cvsimg, bool negative_prompt_f = false)
        {
            // ディクショナリで管理
            Dictionary<string, int> prompt_dic = new Dictionary<string, int>();

            // イメージを一通り回す
            foreach (var item in cvsimg)
            {
                // メタ情報のnullチェック
                if (item.Meta == null)
                    continue;

                // >の後は何故か,で区切られてないことが多いので,を追加
                string prompt = negative_prompt_f ? item.Meta.NegativPrompt.Replace(">", ">,") : item.Meta.Prompt.Replace(">", ">,");

                // 分割
                string[] prompt_list = prompt.Split(",");

                // プロンプトリストを回す
                foreach (var pitem in prompt_list)
                {
                    // (や)を外す
                    string key = pitem.Trim().Replace("(", "").Replace(")", "").ToLower();

                    // プロンプトが登録されていない場合無視
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }

                    // 既に登録済みかを確認
                    if (prompt_dic.ContainsKey(key))
                    {
                        prompt_dic[key]++;  // カウントアップ
                    }
                    else
                    {
                        prompt_dic.Add(key, 1); // 初期登録
                    }
                }
            }

            // ソート
            var tmp = (from x in prompt_dic
                       select new PromptCountM { Prompt = x.Key, Count = x.Value }).OrderByDescending(x => x.Count).ThenBy(x => x.Prompt).ToList();

            if (negative_prompt_f)
            {
                // 要素にセット
                this.NegativePromptItems.Items = new ObservableCollection<PromptCountM>(tmp);
            }
            else
            {
                // 要素にセット
                this.PromptItems.Items = new ObservableCollection<PromptCountM>(tmp);
            }
        }
        #endregion

        #region Promptリストの初期化
        /// <summary>
        /// Promptリストの初期化
        /// </summary>
        /// <param name="cvsimg">Civitai用イメージ</param>
        /// <param name="negative_prompt_f">false:Promptのセット true:ネガティブプロンプトのセット</param>
        public void CreatePromptItems(List<CvsImageM.CvsItem> cvsimg, bool negative_prompt_f = false)
        {
            // ディクショナリで管理
            Dictionary<string, int> prompt_dic = new Dictionary<string, int>();

            // イメージを一通り回す
            foreach (var item in cvsimg)
            {
                // メタ情報のnullチェック
                if (item.Meta == null)
                    continue;

                // >の後は何故か,で区切られてないことが多いので,を追加
                string prompt = negative_prompt_f ? item.Meta.NegativePrompt.Replace(">", ">,") : item.Meta.Prompt.Replace(">", ">,");

                // 分割
                string[] prompt_list = prompt.Split(",");

                // プロンプトリストを回す
                foreach (var pitem in prompt_list)
                {
                    // (や)を外す
                    string key = pitem.Trim().Replace("(", "").Replace(")","").ToLower();

                    // プロンプトが登録されていない場合無視
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }

                    // 既に登録済みかを確認
                    if (prompt_dic.ContainsKey(key))
                    {
                        prompt_dic[key]++;  // カウントアップ
                    }
                    else
                    {
                        prompt_dic.Add(key, 1); // 初期登録
                    }
                }
            }

            // ソート
            var tmp = (from x in prompt_dic
                       select new PromptCountM { Prompt = x.Key, Count = x.Value }).OrderByDescending(x => x.Count).ThenBy(x=>x.Prompt).ToList();

            if (negative_prompt_f)
            {
                // 要素にセット
                this.NegativePromptItems.Items = new ObservableCollection<PromptCountM>(tmp);
            }
            else
            {
                // 要素にセット
                this.PromptItems.Items = new ObservableCollection<PromptCountM>(tmp);
            }
        }
        #endregion

        #region Promptリストの初期化
        /// <summary>
        /// Promptリストの初期化
        /// </summary>
        /// <param name="cvsimg">Civitai用イメージ</param>
        /// <param name="negative_prompt_f">false:Promptのセット true:ネガティブプロンプトのセット</param>
        public void CreatePromptItems(List<CvsModelM.CvsItem> cvsmodel, bool negative_prompt_f = false)
        {
            // ディクショナリで管理
            Dictionary<string, int> prompt_dic = new Dictionary<string, int>();
            List<string> id_list = new List<string>();
            var image_list = new CvsModel.DisplayImageM();

            List<CvsImages> tmp_img = new List<CvsImages>();

            // モデルバージョン分イメージをリストにセット
            foreach (var cvs in cvsmodel)
            {
                foreach (var model_ver in cvs.ModelVersions)
                {
                    // イメージをリストにセット
                    tmp_img.AddRange(model_ver.Images);
                }
            }

            // イメージをセットする
            image_list.SetImages2(new ObservableCollection<CvsImages>(tmp_img));

            // イメージを一通り回す
            foreach (var item in image_list.FilteredImages)
            {
                // メタ情報のnullチェック
                if (item.Meta == null)
                    continue;

                // 既に確認した内容ならば飛ばす
                if (id_list.Contains(item.Url))
                    continue;
                else
                    id_list.Add(item.Url);

                // >の後は何故か,で区切られてないことが多いので,を追加
                string prompt = negative_prompt_f ? item.Meta.NegativPrompt.Replace(">", ">,") : item.Meta.Prompt.Replace(">", ">,");

                // 分割
                string[] prompt_list = prompt.Split(",");

                // プロンプトリストを回す
                foreach (var pitem in prompt_list)
                {
                    // (や)を外す
                    string key = pitem.Trim().Replace("(", "").Replace(")", "").ToLower();

                    // プロンプトが登録されていない場合無視
                    if (string.IsNullOrEmpty(key))
                    {
                        continue;
                    }

                    // 既に登録済みかを確認
                    if (prompt_dic.ContainsKey(key))
                    {
                        prompt_dic[key]++;  // カウントアップ
                    }
                    else
                    {
                        prompt_dic.Add(key, 1); // 初期登録
                    }
                }
            }

            // ソート
            var tmp = (from x in prompt_dic
                       select new PromptCountM { Prompt = x.Key, Count = x.Value }).OrderByDescending(x => x.Count).ThenBy(x => x.Prompt).ToList();

            if (negative_prompt_f)
            {
                // 要素にセット
                this.NegativePromptItems.Items = new ObservableCollection<PromptCountM>(tmp);
            }
            else
            {
                // 要素にセット
                this.PromptItems.Items = new ObservableCollection<PromptCountM>(tmp);
            }
        }
        #endregion

        public string AllPrompt
        {
            get
            {
                string prompt = string.Empty;
                foreach (var tmp in this.PromptItems.Items)
                {
                    if (string.IsNullOrEmpty(prompt))
                    {
                        prompt = prompt + tmp.Prompt;
                    }
                    else
                    {
                        prompt = prompt + "," + tmp.Prompt;
                    }
                }
                return prompt;
            }
        }

        public string AllNegativePrompt
        { 
            get
            {
                string negative = string.Empty;
                foreach (var tmp in this.NegativePromptItems.Items)
                {
                    if (string.IsNullOrEmpty(negative))
                    {
                        negative = negative + tmp.Prompt;
                    }
                    else
                    {
                        negative = negative + "," + tmp.Prompt;
                    }
                }
                return negative;
            }
        }


    }
}
