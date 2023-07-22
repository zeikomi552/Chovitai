using MVVMCore.Common.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MVVMCore.Common.Utilities
{
    public static class ScrollbarTopRow
    {
        /// <summary>
        /// ListViewのスクロールバーを先頭へ移動する
        /// </summary>
        /// <param name="lv">ListView</param>
        public static void TopRow4ListView(ListView lv)
        {
            // スクロールビューワの取得
            ScrollViewer scrollViewer = VisualTreeHelperWrapper.GetScrollViewer(lv) as ScrollViewer;

            // nullチェック
            if (scrollViewer != null && lv.Items.Count > 0)
            {
                // トップへ移動
                lv.ScrollIntoView(lv.Items[0]);
            }
        }

        /// <summary>
        /// DataGridのスクロールバーを先頭へ移動する
        /// </summary>
        /// <param name="dg">DataGridオブジェクト</param>
        public static void TopRow4DataGrid(DataGrid dg)
        {
            // スクロールビューワの取得
            ScrollViewer scrollViewer = VisualTreeHelperWrapper.GetScrollViewer(dg) as ScrollViewer;

            // nullチェック
            if (scrollViewer != null && dg.Items.Count > 0)
            {
                // トップへ移動
                dg.ScrollIntoView(dg.Items[0]);
            }
        }
    }
}
