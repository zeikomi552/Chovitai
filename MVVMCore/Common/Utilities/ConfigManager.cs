using Microsoft.Win32;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCore.Common.Utilities
{
    public class ConfigManager<T> : ModelBase
    {
        #region コンフィグディレクトリ
        /// <summary>
        /// コンフィグディレクトリ
        /// </summary>
        string ConfigDirectory = "Conf";
        #endregion

        #region コンフィグファイル名
        /// <summary>
        /// コンフィグファイル名
        /// </summary>
        string ConfigFileName = "Setting.conf";
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="config_dir_name">コンフィグディレクトリ名</param>
        /// <param name="config_file_name">コンフィグファイル名</param>
        public ConfigManager(string config_dir_name, string config_file_name, T item)
        {
            this.ConfigDirectory = config_dir_name;
            this.ConfigFileName = config_file_name;
            this.Item = item;
        }
        #endregion

        #region Configファイル用フォルダパス
        /// <summary>
        /// Configファイル用フォルダパス
        /// </summary>
        public string ConfigDir
        {
            get
            {
                string path = PathManager.GetApplicationFolder();
                string config_dir = Path.Combine(path, ConfigDirectory);
                PathManager.CreateDirectory(config_dir);
                return config_dir;
            }
        }
        #endregion

        #region コンフィグファイルパス
        /// <summary>
        /// コンフィグファイルパス
        /// </summary>
        public string ConfigFile
        {
            get
            {
                return Path.Combine(ConfigDir, ConfigFileName);
            }
        }
        #endregion

        #region 要素[Item]プロパティ
        /// <summary>
        /// 要素[Item]プロパティ用変数
        /// </summary>
        T _Item;
        /// <summary>
        /// 要素[Item]プロパティ
        /// </summary>
        public T Item
        {
            get
            {
                return _Item;
            }
            set
            {
                if (_Item == null || !_Item.Equals(value))
                {
                    _Item = value;
                    NotifyPropertyChanged("Item");
                }
            }
        }
        #endregion

        #region ファイルの保存処理
        /// <summary>
        /// ファイルの保存処理
        /// </summary>
        public void SaveXML()
        {
            XMLUtil.Seialize<T>(this.ConfigFile, this.Item);
        }
        #endregion

        #region ファイルの保存処理
        /// <summary>
        /// ファイルの保存処理
        /// </summary>
        /// <param name="flter">フィルタ条件</param>
        /// <returns>true:保存成功 false:保存先が選ばれなかった</returns>
        public bool SaveXML(string flter)
        {
            // ダイアログのインスタンスを生成
            var dialog = new SaveFileDialog();

            // ファイルの種類を設定
            dialog.Filter = flter;

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                XMLUtil.Seialize<T>(dialog.FileName, this.Item);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region ロード処理
        /// <summary>
        /// ロード処理
        /// </summary>
        /// <returns>ファイルのロード処理</returns>
        public bool LoadXML()
        {
            if (File.Exists(this.ConfigFile))
            {
                this.Item = XMLUtil.Deserialize<T>(this.ConfigFile);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region ファイルのロード処理
        /// <summary>
        /// ファイルのロード処理
        /// </summary>
        /// <param name="flter">フィルター</param>
        /// <returns>true:読み込み成功 false:ファイルが選ばれなかった</returns>
        public bool LoadXML(string flter)
        {
            // ダイアログのインスタンスを生成
            var dialog = new OpenFileDialog();

            // ファイルの種類を設定
            dialog.Filter = flter;

            // ダイアログを表示する
            if (dialog.ShowDialog() == true)
            {
                this.Item = XMLUtil.Deserialize<T>(dialog.FileName);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        /// <summary>
        /// JSONでの保存処理
        /// </summary>
        /// <exception cref="Exception">ファイル保存に失敗</exception>
        public void SaveJSON()
        {
            JSONUtil.SerializeFromFile<T>(this.Item, this.ConfigFile);
        }

        #region JSONファイルの読み込み
        /// <summary>
        /// JSONファイルの読み込み
        /// </summary>
        public void LoadJSON()
        {
            if (File.Exists(this.ConfigFile))
            {
                this.Item = JSONUtil.DeserializeFromFile<T>(this.ConfigFile);
            }
        }
        #endregion
    }

}
