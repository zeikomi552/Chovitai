using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMCore.Common.Utilities
{
    public class ShowMessage
    {
        /// <summary>
        /// エラーメッセージの表示処理(OKボタンのみ)
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="caption">キャプション</param>
        public static void ShowErrorOK(string message, string caption)
        {
            // エラーメッセージの表示
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// 情報メッセージの表示処理(OKボタンのみ)
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="caption">キャプション</param>
        public static void ShowNoticeOK(string message, string caption)
        {
            // エラーメッセージの表示
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 確認メッセージの表示処理(YesNo)
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="caption">キャプション</param>
        /// <returns>選択結果</returns>
        public static MessageBoxResult ShowQuestionYesNo(string message, string caption)
        {
            // エラーメッセージの表示
            return MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);

        }

    }
}
