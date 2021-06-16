
namespace KFQS_Form
{
    partial class PP_WorkerPerProd
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton1 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
            Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton dateButton2 = new Infragistics.Win.UltraWinSchedule.CalendarCombo.DateButton();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.grid1 = new DC00_Component.Grid(this.components);
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.cboPlantCode_H = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.dtpEnd_H = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
            this.dtpStart_H = new Infragistics.Win.UltraWinSchedule.UltraCalendarCombo();
            this.sLabel4 = new DC00_Component.SLabel();
            this.lblDate = new DC00_Component.SLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.txtWorkerName = new System.Windows.Forms.TextBox();
            this.txtWorkerId = new DC00_Component.SBtnTextEditor();
            ((System.ComponentModel.ISupportInitialize)(this.gbxHeader)).BeginInit();
            this.gbxHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxBody)).BeginInit();
            this.gbxBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantCode_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkerId)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxHeader
            // 
            this.gbxHeader.ContentPadding.Bottom = 2;
            this.gbxHeader.ContentPadding.Left = 2;
            this.gbxHeader.ContentPadding.Right = 2;
            this.gbxHeader.ContentPadding.Top = 4;
            this.gbxHeader.Controls.Add(this.dtpEnd_H);
            this.gbxHeader.Controls.Add(this.dtpStart_H);
            this.gbxHeader.Controls.Add(this.sLabel4);
            this.gbxHeader.Controls.Add(this.lblDate);
            this.gbxHeader.Controls.Add(this.cboPlantCode_H);
            this.gbxHeader.Controls.Add(this.ultraLabel1);
            this.gbxHeader.Controls.Add(this.txtWorkerId);
            this.gbxHeader.Controls.Add(this.ultraLabel3);
            this.gbxHeader.Controls.Add(this.txtWorkerName);
            this.gbxHeader.Size = new System.Drawing.Size(1326, 118);
            // 
            // gbxBody
            // 
            this.gbxBody.ContentPadding.Bottom = 4;
            this.gbxBody.ContentPadding.Left = 4;
            this.gbxBody.ContentPadding.Right = 4;
            this.gbxBody.ContentPadding.Top = 6;
            this.gbxBody.Controls.Add(this.grid1);
            this.gbxBody.Location = new System.Drawing.Point(0, 118);
            this.gbxBody.Size = new System.Drawing.Size(1326, 534);
            // 
            // grid1
            // 
            this.grid1.AutoResizeColumn = true;
            this.grid1.AutoUserColumn = true;
            this.grid1.ContextMenuCopyEnabled = true;
            this.grid1.ContextMenuDeleteEnabled = true;
            this.grid1.ContextMenuExcelEnabled = true;
            this.grid1.ContextMenuInsertEnabled = true;
            this.grid1.ContextMenuPasteEnabled = true;
            this.grid1.DeleteButtonEnable = true;
            appearance37.BackColor = System.Drawing.SystemColors.Window;
            appearance37.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.grid1.DisplayLayout.Appearance = appearance37;
            this.grid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            this.grid1.DisplayLayout.DefaultSelectedBackColor = System.Drawing.Color.Empty;
            appearance38.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance38.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance38.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance38.BorderColor = System.Drawing.SystemColors.Window;
            this.grid1.DisplayLayout.GroupByBox.Appearance = appearance38;
            appearance40.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grid1.DisplayLayout.GroupByBox.BandLabelAppearance = appearance40;
            this.grid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grid1.DisplayLayout.GroupByBox.Hidden = true;
            appearance39.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance39.BackColor2 = System.Drawing.SystemColors.Control;
            appearance39.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance39.ForeColor = System.Drawing.SystemColors.GrayText;
            this.grid1.DisplayLayout.GroupByBox.PromptAppearance = appearance39;
            this.grid1.DisplayLayout.MaxColScrollRegions = 1;
            this.grid1.DisplayLayout.MaxRowScrollRegions = 1;
            appearance43.BackColor = System.Drawing.SystemColors.Window;
            appearance43.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grid1.DisplayLayout.Override.ActiveCellAppearance = appearance43;
            appearance46.BackColor = System.Drawing.SystemColors.Highlight;
            appearance46.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grid1.DisplayLayout.Override.ActiveRowAppearance = appearance46;
            this.grid1.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.True;
            this.grid1.DisplayLayout.Override.AllowMultiCellOperations = ((Infragistics.Win.UltraWinGrid.AllowMultiCellOperation)(((Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.Copy | Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.Cut) 
            | Infragistics.Win.UltraWinGrid.AllowMultiCellOperation.Paste)));
            this.grid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.grid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance48.BackColor = System.Drawing.SystemColors.Window;
            this.grid1.DisplayLayout.Override.CardAreaAppearance = appearance48;
            appearance44.BorderColor = System.Drawing.Color.Silver;
            appearance44.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.grid1.DisplayLayout.Override.CellAppearance = appearance44;
            this.grid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.grid1.DisplayLayout.Override.CellPadding = 0;
            appearance42.BackColor = System.Drawing.SystemColors.Control;
            appearance42.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance42.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance42.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance42.BorderColor = System.Drawing.SystemColors.Window;
            this.grid1.DisplayLayout.Override.GroupByRowAppearance = appearance42;
            appearance41.TextHAlignAsString = "Left";
            this.grid1.DisplayLayout.Override.HeaderAppearance = appearance41;
            this.grid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance47.BackColor = System.Drawing.SystemColors.Window;
            appearance47.BorderColor = System.Drawing.Color.Silver;
            this.grid1.DisplayLayout.Override.RowAppearance = appearance47;
            this.grid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance45.BackColor = System.Drawing.SystemColors.ControlLight;
            this.grid1.DisplayLayout.Override.TemplateAddRowAppearance = appearance45;
            this.grid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grid1.DisplayLayout.SelectionOverlayBorderThickness = 2;
            this.grid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.grid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid1.EnterNextRowEnable = true;
            this.grid1.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.grid1.Location = new System.Drawing.Point(6, 6);
            this.grid1.Name = "grid1";
            this.grid1.Size = new System.Drawing.Size(1314, 522);
            this.grid1.TabIndex = 0;
            this.grid1.Text = "grid1";
            this.grid1.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            this.grid1.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
            this.grid1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.grid1.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.Location = new System.Drawing.Point(30, 30);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(100, 23);
            this.ultraLabel1.TabIndex = 1;
            this.ultraLabel1.Text = "공장";
            // 
            // cboPlantCode_H
            // 
            this.cboPlantCode_H.Location = new System.Drawing.Point(104, 25);
            this.cboPlantCode_H.Name = "cboPlantCode_H";
            this.cboPlantCode_H.Size = new System.Drawing.Size(227, 35);
            this.cboPlantCode_H.TabIndex = 10;
            // 
            // dtpEnd_H
            // 
            this.dtpEnd_H.DateButtons.Add(dateButton1);
            this.dtpEnd_H.Location = new System.Drawing.Point(1151, 30);
            this.dtpEnd_H.Name = "dtpEnd_H";
            this.dtpEnd_H.NonAutoSizeHeight = 26;
            this.dtpEnd_H.Size = new System.Drawing.Size(145, 32);
            this.dtpEnd_H.TabIndex = 243;
            this.dtpEnd_H.Value = new System.DateTime(2021, 6, 10, 0, 0, 0, 0);
            // 
            // dtpStart_H
            // 
            this.dtpStart_H.DateButtons.Add(dateButton2);
            this.dtpStart_H.Location = new System.Drawing.Point(983, 30);
            this.dtpStart_H.Name = "dtpStart_H";
            this.dtpStart_H.NonAutoSizeHeight = 26;
            this.dtpStart_H.Size = new System.Drawing.Size(145, 32);
            this.dtpStart_H.TabIndex = 242;
            this.dtpStart_H.Value = new System.DateTime(2021, 6, 10, 0, 0, 0, 0);
            // 
            // sLabel4
            // 
            appearance4.FontData.BoldAsString = "False";
            appearance4.FontData.UnderlineAsString = "False";
            appearance4.ForeColor = System.Drawing.Color.Black;
            appearance4.TextHAlignAsString = "Center";
            this.sLabel4.Appearance = appearance4;
            this.sLabel4.BorderStyleInner = Infragistics.Win.UIElementBorderStyle.None;
            this.sLabel4.DbField = null;
            this.sLabel4.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.sLabel4.Location = new System.Drawing.Point(1134, 36);
            this.sLabel4.Name = "sLabel4";
            this.sLabel4.RequireFlag = DC00_Component.SLabel.RequireFlagEnum.NO;
            this.sLabel4.Size = new System.Drawing.Size(17, 26);
            this.sLabel4.TabIndex = 245;
            this.sLabel4.Text = "~";
            // 
            // lblDate
            // 
            appearance3.FontData.BoldAsString = "False";
            appearance3.FontData.UnderlineAsString = "False";
            appearance3.ForeColor = System.Drawing.Color.Black;
            appearance3.TextHAlignAsString = "Right";
            appearance3.TextVAlignAsString = "Middle";
            this.lblDate.Appearance = appearance3;
            this.lblDate.DbField = null;
            this.lblDate.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDate.Location = new System.Drawing.Point(894, 34);
            this.lblDate.Name = "lblDate";
            this.lblDate.RequireFlag = DC00_Component.SLabel.RequireFlagEnum.NO;
            this.lblDate.Size = new System.Drawing.Size(83, 23);
            this.lblDate.TabIndex = 244;
            this.lblDate.Text = "생산일자";
            // 
            // ultraLabel3
            // 
            this.ultraLabel3.Location = new System.Drawing.Point(342, 29);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(68, 23);
            this.ultraLabel3.TabIndex = 249;
            this.ultraLabel3.Text = "작업자";
            // 
            // txtWorkerName
            // 
            this.txtWorkerName.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.txtWorkerName.Location = new System.Drawing.Point(649, 27);
            this.txtWorkerName.Name = "txtWorkerName";
            this.txtWorkerName.Size = new System.Drawing.Size(226, 30);
            this.txtWorkerName.TabIndex = 247;
            // 
            // txtWorkerId
            // 
            appearance1.FontData.BoldAsString = "False";
            appearance1.FontData.UnderlineAsString = "False";
            appearance1.ForeColor = System.Drawing.Color.Black;
            this.txtWorkerId.Appearance = appearance1;
            this.txtWorkerId.btnImgType = DC00_Component.SBtnTextEditor.ButtonImgTypeEnum.Type1;
            this.txtWorkerId.btnWidth = 26;
            this.txtWorkerId.Location = new System.Drawing.Point(416, 25);
            this.txtWorkerId.Name = "txtWorkerId";
            this.txtWorkerId.RequireFlag = DC00_Component.SBtnTextEditor.RequireFlagEnum.NO;
            this.txtWorkerId.RequirePop = DC00_Component.SBtnTextEditor.RequireFlagEnum.NO;
            this.txtWorkerId.Size = new System.Drawing.Size(227, 35);
            this.txtWorkerId.TabIndex = 0;
            // 
            // PP_WorkerPerProd
            // 
            this.ClientSize = new System.Drawing.Size(1326, 652);
            this.Name = "PP_WorkerPerProd";
            this.Text = "작업자 일별 생산 실적";
            this.Load += new System.EventHandler(this.PP_WorkerPerProd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbxHeader)).EndInit();
            this.gbxHeader.ResumeLayout(false);
            this.gbxHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxBody)).EndInit();
            this.gbxBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantCode_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtWorkerId)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DC00_Component.Grid grid1;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cboPlantCode_H;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarCombo dtpEnd_H;
        private Infragistics.Win.UltraWinSchedule.UltraCalendarCombo dtpStart_H;
        private DC00_Component.SLabel sLabel4;
        private DC00_Component.SLabel lblDate;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private System.Windows.Forms.TextBox txtWorkerName;
        private DC00_Component.SBtnTextEditor txtWorkerId;
    }
}
