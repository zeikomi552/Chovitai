using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace MVVMCore.Common.Wrapper
{
    public static class VisualTreeHelperWrapper
    {
        /// <summary>
        /// VisualTreeを親側にたどって、
        /// 指定した型の要素を探す
        /// </summary>
        public static T FindAncestor<T>(this DependencyObject depObj)
            where T : DependencyObject
        {
            while (depObj != null)
            {
                if (depObj is T target)
                {
                    return target;
                }
                depObj = VisualTreeHelper.GetParent(depObj);
            }
            return null;
        }

        #region 最上位のWindowの取得処理
        /// <summary>
        /// 最上位のWindowの取得処理
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public static DependencyObject GetWindow<T>(object sender)
        {
            DependencyObject depobj = (DependencyObject)sender;
            while (true)
            {
                depobj = VisualTreeHelper.GetParent(depobj);

                if (depobj is T)
                {
                    return depobj;
                }
                else if (depobj == null)
                {
                    return null;
                }
            }
        }
        #endregion

        #region スクロールビューワーの取得処理
        /// <summary>
        /// スクロールビューワーの取得処理
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static DependencyObject GetScrollViewer(DependencyObject o)
        {
            if (o is ScrollViewer)
            { return o; }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(o); i++)
            {
                var child = VisualTreeHelper.GetChild(o, i);

                var result = GetScrollViewer(child);
                if (result == null)
                {
                    continue;
                }
                else
                {
                    return result;
                }
            }

            return null;
        }
        #endregion
    }
}
