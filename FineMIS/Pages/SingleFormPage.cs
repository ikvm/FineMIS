using System;
using System.Collections.Generic;
using FineUI;
using System.Web.UI;
using OutOfMemory;
using PetaPoco;

namespace FineMIS.Pages
{
    public abstract class SingleFormPage : PageBase, ISinglePageBase
    {
        #region 属性

        /// <summary>
        /// 主表单实例
        /// </summary>
        public abstract Form MainForm { get; }

        protected ACTION Action { get; set; }

        protected long Id { get; set; }

        #endregion

        #region 页面初始化
        protected override void OnInit(EventArgs e)
        {
            Action = (ACTION)Request["action"].ToInt32();
            Id = Request["id"].ToInt64();
            base.OnInit(e);
            InitForm();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                Session[FORCE_REFRESH] = false;
            }
        }
        #endregion

        #region 每个页面可能需要实现的方法
        /// <summary>
        /// 初始化表单
        /// </summary>
        protected abstract void InitForm();

        /// <summary>
        /// 保存表单数据（需要每个页面自行实现）
        /// </summary>
        protected virtual void SaveForm()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region 常用按钮事件
        /// <summary>
        /// 点击保存并关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnSaveAndClose_Click(object sender, EventArgs e)
        {
            try
            {
                SaveForm();
                // 回发主页面
                switch (Action)
                {
                    case ACTION.INSERT:
                        // 新增
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference(ACTION.INSERT.ToString() + "|1"));
                        break;
                    case ACTION.UPDATE:
                        // 编辑
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference(ACTION.UPDATE.ToString() + "|1"));
                        break;
                    case ACTION.DETAIL:
                        // 查看
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message, "保存失败", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 点击保存并继续按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnSaveAndContinue_Click(object sender, EventArgs e)
        {
            try
            {
                SaveForm();
                ResetControl(MainForm);
                Session[FORCE_REFRESH] = true;
                Notify.Show("保存成功,请继续添加!", null, NotifyIcon.Success);
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message, "保存失败", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 点击重置按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.Reset();
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message, "重置失败", MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 点击关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                // 如果已经修改过数据，那么回发时应当刷新表格
                PageContext.RegisterStartupScript(Session[FORCE_REFRESH].ToBoolean()
                    ? ActiveWindow.GetHidePostBackReference(FORCE_REFRESH)
                    : ActiveWindow.GetHideReference());
            }
            catch (Exception ex)
            {
                Alert.ShowInTop(ex.Message, "关闭失败", MessageBoxIcon.Error);
            }
        }
        #endregion

        #region 表单页面可能弹出新的页面（如：选择列表界面）
        /// <summary>
        /// 获得弹出窗体地址
        /// </summary>
        /// <param name="action"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public virtual string GetWindowUrl(ACTION action, int rowIndex = 0)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获得弹出窗体尺寸
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public virtual System.Drawing.Size GetWindowSize(ACTION action = ACTION.NONE)
        {
            return new System.Drawing.Size(600, 350);
        }
        /// <summary>
        /// 弹出窗口关闭
        /// </summary>
        /// <param name="argument"></param>
        public virtual void ProcessArgument(string argument)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}