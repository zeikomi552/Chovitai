﻿using Chovitai.Common;
using MVVMCore.BaseClass;
using MVVMCore.Common.Utilities;
using System;

namespace Chovitai.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        #region デバッグフラグ[DebugF]プロパティ
        /// <summary>
        /// デバッグフラグ[DebugF]プロパティ
        /// </summary>
        public bool DebugF
        {
            get
            {
                return GblValues.Instance.Config!.Item.DebugF;
            }
        }
        #endregion


        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Init(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ShowMessage.ShowErrorOK(ex.Message, "Error");
            }
        }
        #endregion
        #region クローズ処理
        /// <summary>
        /// クローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void Close(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
