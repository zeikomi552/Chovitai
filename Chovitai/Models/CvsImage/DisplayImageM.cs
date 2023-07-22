using Chovitai.Common;
using Chovitai.Common.Converters;
using Chovitai.Common.Enums;
using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Chovitai.Models.CvsImage.CvsImageM;
using static Chovitai.Models.CvsModel.CvsModelM.CvsModelVersions;

namespace Chovitai.Models.CvsImage
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
        ObservableCollection<CvsItem> _Images = new ObservableCollection<CvsItem>();
        /// <summary>
        /// This model images[Images]プロパティ
        /// </summary>
        private ObservableCollection<CvsItem> Images
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
        ObservableCollection<CvsItem> _FilteredImages = new ObservableCollection<CvsItem>();
        /// <summary>
        /// This model Filtered images[FilteredImages]プロパティ
        /// </summary>
        public ObservableCollection<CvsItem> FilteredImages
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
        CvsItem _SelectedImage = new CvsItem();
        /// <summary>
        /// Selected image[SelectedImage]プロパティ
        /// </summary>
        public CvsItem SelectedImage
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
        /// イメージのリストをセットする
        /// </summary>
        /// <param name="images">イメージリスト</param>
        public void SetImages(ObservableCollection<CvsItem> images)
        {
            Images = images;   // イメージのセット

            // フィルタのリフレッシュ
            RefreshFilter();
        }
        #endregion

        #region イメージフィルターのリフレッシュ
        /// <summary>
        /// イメージフィルターのリフレッシュ
        /// </summary>
        public void RefreshFilter()
        {
            ImageNsfwEnumToVisibilityConverter.ImageFilter = this.ImageFilter;

            var tmp = (from x in Images
                       where ImageNsfwEnumToVisibilityConverter.Convert(x.NsfwLevel)
                       select x).ToList<CvsItem>();

            FilteredImages = new ObservableCollection<CvsItem>(tmp);

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
