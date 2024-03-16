using MVVMCore.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chovitai.Models.A1111
{
    public class RedirectMessageM : ModelBase
    {
        #region リダイレクトメッセージ[OutputMessage]プロパティ
        /// <summary>
        /// リダイレクトメッセージ[OutputMessage]プロパティ
        /// WebUIのコンソール上に出力されたメッセージ
        /// </summary>
        public string OutputMessage
        {
            get
            {
                return this.MessageList.ToString() + this.AddMessage;
            }
        }
        #endregion

        #region 追加メッセージ[AddMessage]プロパティ
        /// <summary>
        /// 追加メッセージ[AddMessage]プロパティ用変数
        /// </summary>
        string _AddMessage = string.Empty;
        /// <summary>
        /// 追加メッセージ[AddMessage]プロパティ
        /// </summary>
        public string AddMessage
        {
            get
            {
                return _AddMessage;
            }
            set
            {
                if (_AddMessage == null || !_AddMessage.Equals(value))
                {
                    _AddMessage = value;
                    NotifyPropertyChanged("AddMessage");
                }
            }
        }
        #endregion

        #region 追加メッセージ[MessageList]プロパティ
        /// <summary>
        /// 追加メッセージ[MessageList]プロパティ用変数
        /// </summary>
        StringBuilder _MessageList = new StringBuilder();
        /// <summary>
        /// 追加メッセージ[MessageList]プロパティ
        /// </summary>
        public StringBuilder MessageList
        {
            get
            {
                return _MessageList;
            }
            set
            {
                if (_MessageList == null || !_MessageList.Equals(value))
                {
                    _MessageList = value;
                    NotifyPropertyChanged("MessageList");
                }
            }
        }
        #endregion

        #region this.AddMessage
        /// <summary>
        /// c
        /// </summary>
        /// <param name="message">追加メッセージ</param>
        public void Add(string message)
        {

            if (!message.Contains("Total progress:"))
            {
                this.MessageList.AppendLine(message);

                if (this.AddMessage.Contains("Total progress: 100%"))
                {
                    this.MessageList.AppendLine(this.AddMessage);
                }
                this.AddMessage = string.Empty;
            }
            else
            {
                this.AddMessage = message;
            }
            NotifyPropertyChanged("OutputMessage");

        }
        #endregion
    }
}
