using Chovitai.Common.Converters;
using Chovitai.Common.Enums;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.TextFormatting;
using System.Windows.Threading;
using static Chovitai.Models.CvsModel.CvsModelM.CvsModelVersions;

namespace Chovitai.Models.CvsModel
{
    public class DisplayImageM : ModelBase
    {
        #region イメージフィルタ用[ImageFilter]プロパティ
        /// <summary>
        /// イメージフィルタ用[ImageFilter]プロパティ用変数
        /// </summary>
        ImageNsfwEnum _ImageFilter = ImageNsfwEnum.None;
        /// <summary>
        /// イメージフィルタ用[ImageFilter]プロパティ
        /// </summary>
        public ImageNsfwEnum ImageFilter
        {
            get
            {
                return _ImageFilter;
            }
            set
            {
                if (!_ImageFilter.Equals(value))
                {
                    _ImageFilter = value;
                    NotifyPropertyChanged("ImageFilter");
                }
            }
        }
        #endregion

        #region This model images[Images]プロパティ
        /// <summary>
        /// This model images[Images]プロパティ用変数
        /// </summary>
        ObservableCollection<CvsImages> _Images = new ObservableCollection<CvsImages>();
        /// <summary>
        /// This model images[Images]プロパティ
        /// </summary>
        private ObservableCollection<CvsImages> Images
        {
            get
            {
                return _Images;
            }
            set
            {
                if (_Images == null || !_Images.Equals(value))
                {
                    _Images = value;
                    NotifyPropertyChanged("Images");
                }
            }
        }
        #endregion

        #region This model Filtered images[FilteredImages]プロパティ
        /// <summary>
        /// This model Filtered images[FilteredImages]プロパティ用変数
        /// </summary>
        ObservableCollection<CvsImages> _FilteredImages = new ObservableCollection<CvsImages>();
        /// <summary>
        /// This model Filtered images[FilteredImages]プロパティ
        /// </summary>
        public ObservableCollection<CvsImages> FilteredImages
        {
            get
            {
                return _FilteredImages;
            }
            set
            {
                if (_FilteredImages == null || !_FilteredImages.Equals(value))
                {
                    _FilteredImages = value;
                    NotifyPropertyChanged("FilteredImages");
                }
            }
        }
        #endregion

        #region Selected image[SelectedImage]プロパティ
        /// <summary>
        /// Selected image[SelectedImage]プロパティ用変数
        /// </summary>
        CvsImages _SelectedImage = new CvsImages();
        /// <summary>
        /// Selected image[SelectedImage]プロパティ
        /// </summary>
        public CvsImages SelectedImage
        {
            get
            {
                return _SelectedImage;
            }
            set
            {
                if (_SelectedImage == null || !_SelectedImage.Equals(value))
                {
                    _SelectedImage = value;
                    NotifyPropertyChanged("SelectedImage");
                }
            }
        }
        #endregion

        #region イメージのリストをセットする
        /// <summary>
        /// イメージのリストをセットする(フィルタ処理は非同期)
        /// </summary>
        /// <param name="images">イメージリスト</param>
        public void SetImages(ObservableCollection<CvsImages> images)
        {
            Images = images;   // イメージのセット

            // フィルタのリフレッシュ
            RefreshFilter();
        }
        #endregion

        #region イメージのリストをセットする(フィルタ処理は同期的に行う)
        /// <summary>
        /// イメージのリストをセットする(フィルタ処理は同期的に行う)
        /// </summary>
        /// <param name="images">イメージ</param>
        public void SetImages2(ObservableCollection<CvsImages> images)
        {
            Images = images;   // イメージのセット

            var tmp = (from x in Images
                       where ImageNsfwEnumToVisibilityConverter.Convert(x.Nsfw)
                       select x).ToList<CvsImages>();

            this.FilteredImages = new ObservableCollection<CvsImages>(tmp);

            // フィルター後のイメージが1個以上ある場合
            if (this.FilteredImages.Any())
            {
                // 最初のものを選択する
                this.SelectedImage = this.FilteredImages.First();
            }
        }
        #endregion

        static int Counter = 0;

        #region イメージフィルターのリフレッシュ
        /// <summary>
        /// イメージフィルターのリフレッシュ
        /// </summary>
        public void RefreshFilter()
        {
            Counter++;

            int own_counter = Counter;

            Task.Run(() =>
            {
                // レスポンス向上のため、連続で関数が呼ばれた時、一瞬またせる
                System.Threading.Thread.Sleep(100);

                // 100ミリ以内の連続呼び出しは破棄（最後のみ残す）
                if (Counter != own_counter)
                {
                    //Debug.WriteLine(string.Format("{0} {1}", Counter, own_counter));
                    return;
                }

                // イメージフィルターのセット
                ImageNsfwEnumToVisibilityConverter.ImageFilter = this.ImageFilter;

                // 表示する画像のリスト作成
                var tmp = (from x in Images
                           where ImageNsfwEnumToVisibilityConverter.Convert(x.Nsfw)
                           select x).ToList<CvsImages>();

                  Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                    new Action(() =>
                    {
                        this.FilteredImages = new ObservableCollection<CvsImages>(tmp);

                        // フィルター後のイメージが1個以上ある場合
                        if (this.FilteredImages.Any())
                        {
                            // 最初のものを選択する
                            this.SelectedImage = this.FilteredImages.First();
                        }
                    }));
            });

        }
        #endregion

        #region 1つ以上あるかどうかを判定する
        /// <summary>
        /// 1つ以上あるかどうかを判定する
        /// </summary>
        public bool Any
        {
            get
            {
                return FilteredImages.Any();
            }
        }
        #endregion

        #region 最初のアイテムを選択する
        /// <summary>
        /// 最初のアイテムを選択する
        /// </summary>
        public void SetFirst()
        {
            if (FilteredImages.Count > 0)
            {
                SelectedImage = FilteredImages.ElementAt(0);
            }
        }
        #endregion
    }
}
