
namespace KFQS_Form
{
    partial class MM_StockOUT
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
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.txtLotNO_H = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.grid1 = new DC00_Component.Grid(this.components);
            this.cboPlantCode_H = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.cboItemCode_H = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cboSave_H = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.ultraLabel7 = new Infragistics.Win.Misc.UltraLabel();
            this.cboStorage_H = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.ultraLabel6 = new Infragistics.Win.Misc.UltraLabel();
            this.dtpStart = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            this.dtpEnd = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
            ((System.ComponentModel.ISupportInitialize)(this.gbxHeader)).BeginInit();
            this.gbxHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxBody)).BeginInit();
            this.gbxBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNO_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantCode_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboItemCode_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboSave_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStorage_H)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // gbxHeader
            // 
            this.gbxHeader.ContentPadding.Bottom = 2;
            this.gbxHeader.ContentPadding.Left = 2;
            this.gbxHeader.ContentPadding.Right = 2;
            this.gbxHeader.ContentPadding.Top = 4;
            this.gbxHeader.Controls.Add(this.ultraLabel5);
            this.gbxHeader.Controls.Add(this.dtpEnd);
            this.gbxHeader.Controls.Add(this.dtpStart);
            this.gbxHeader.Controls.Add(this.ultraGroupBox1);
            this.gbxHeader.Controls.Add(this.ultraLabel3);
            this.gbxHeader.Controls.Add(this.cboItemCode_H);
            this.gbxHeader.Controls.Add(this.ultraLabel2);
            this.gbxHeader.Controls.Add(this.cboPlantCode_H);
            this.gbxHeader.Controls.Add(this.txtLotNO_H);
            this.gbxHeader.Controls.Add(this.ultraLabel4);
            this.gbxHeader.Controls.Add(this.ultraLabel1);
            this.gbxHeader.Size = new System.Drawing.Size(1326, 349);
            // 
            // gbxBody
            // 
            this.gbxBody.ContentPadding.Bottom = 4;
            this.gbxBody.ContentPadding.Left = 4;
            this.gbxBody.ContentPadding.Right = 4;
            this.gbxBody.ContentPadding.Top = 6;
            this.gbxBody.Controls.Add(this.grid1);
            this.gbxBody.Location = new System.Drawing.Point(0, 349);
            this.gbxBody.Size = new System.Drawing.Size(1326, 303);
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.Location = new System.Drawing.Point(30, 30);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(100, 23);
            this.ultraLabel1.TabIndex = 1;
            this.ultraLabel1.Text = "공장";
            // 
            // ultraLabel4
            // 
            this.ultraLabel4.Location = new System.Drawing.Point(415, 30);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(100, 23);
            this.ultraLabel4.TabIndex = 6;
            this.ultraLabel4.Text = "작업지시번호";
            // 
            // txtLotNO_H
            // 
            this.txtLotNO_H.Location = new System.Drawing.Point(514, 93);
            this.txtLotNO_H.Name = "txtLotNO_H";
            this.txtLotNO_H.Size = new System.Drawing.Size(330, 35);
            this.txtLotNO_H.TabIndex = 7;
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
            this.grid1.Size = new System.Drawing.Size(1314, 291);
            this.grid1.TabIndex = 0;
            this.grid1.Text = "grid1";
            this.grid1.TextRenderingMode = Infragistics.Win.TextRenderingMode.GDI;
            this.grid1.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
            this.grid1.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.grid1.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // cboPlantCode_H
            // 
            this.cboPlantCode_H.Location = new System.Drawing.Point(104, 25);
            this.cboPlantCode_H.Name = "cboPlantCode_H";
            this.cboPlantCode_H.Size = new System.Drawing.Size(227, 35);
            this.cboPlantCode_H.TabIndex = 10;
            this.cboPlantCode_H.Text = "ultraComboEditor1";
            // 
            // cboItemCode_H
            // 
            this.cboItemCode_H.Location = new System.Drawing.Point(104, 85);
            this.cboItemCode_H.Name = "cboItemCode_H";
            this.cboItemCode_H.Size = new System.Drawing.Size(175, 35);
            this.cboItemCode_H.TabIndex = 12;
            this.cboItemCode_H.Text = "ultraComboEditor1";
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.Location = new System.Drawing.Point(30, 97);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(122, 23);
            this.ultraLabel2.TabIndex = 11;
            this.ultraLabel2.Text = "품목";
            // 
            // ultraLabel3
            // 
            this.ultraLabel3.Location = new System.Drawing.Point(415, 97);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(93, 23);
            this.ultraLabel3.TabIndex = 13;
            this.ultraLabel3.Text = "LOTNO";
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.cboSave_H);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel7);
            this.ultraGroupBox1.Controls.Add(this.cboStorage_H);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel6);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ultraGroupBox1.Location = new System.Drawing.Point(4, 187);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(1318, 158);
            this.ultraGroupBox1.TabIndex = 15;
            this.ultraGroupBox1.Text = "ultraGroupBox1";
            // 
            // cboSave_H
            // 
            this.cboSave_H.Location = new System.Drawing.Point(716, 70);
            this.cboSave_H.Name = "cboSave_H";
            this.cboSave_H.Size = new System.Drawing.Size(227, 35);
            this.cboSave_H.TabIndex = 22;
            this.cboSave_H.Text = "ultraComboEditor1";
            // 
            // ultraLabel7
            // 
            this.ultraLabel7.Location = new System.Drawing.Point(587, 74);
            this.ultraLabel7.Name = "ultraLabel7";
            this.ultraLabel7.Size = new System.Drawing.Size(140, 23);
            this.ultraLabel7.TabIndex = 21;
            this.ultraLabel7.Text = "출고저장위치";
            // 
            // cboStorage_H
            // 
            this.cboStorage_H.Location = new System.Drawing.Point(211, 69);
            this.cboStorage_H.Name = "cboStorage_H";
            this.cboStorage_H.Size = new System.Drawing.Size(227, 35);
            this.cboStorage_H.TabIndex = 20;
            this.cboStorage_H.Text = "ultraComboEditor1";
            // 
            // ultraLabel6
            // 
            this.ultraLabel6.Location = new System.Drawing.Point(128, 74);
            this.ultraLabel6.Name = "ultraLabel6";
            this.ultraLabel6.Size = new System.Drawing.Size(100, 23);
            this.ultraLabel6.TabIndex = 19;
            this.ultraLabel6.Text = "출고창고";
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(531, 30);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(144, 35);
            this.dtpStart.TabIndex = 16;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(720, 30);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(144, 35);
            this.dtpEnd.TabIndex = 17;
            // 
            // ultraLabel5
            // 
            this.ultraLabel5.Location = new System.Drawing.Point(692, 33);
            this.ultraLabel5.Name = "ultraLabel5";
            this.ultraLabel5.Size = new System.Drawing.Size(22, 27);
            this.ultraLabel5.TabIndex = 18;
            this.ultraLabel5.Text = "~";
            // 
            // MM_StockOUT
            // 
            this.ClientSize = new System.Drawing.Size(1326, 652);
            this.Name = "MM_StockOUT";
            this.Text = "생산계획 및 확정";
            this.Load += new System.EventHandler(this.MM_StockOUT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gbxHeader)).EndInit();
            this.gbxHeader.ResumeLayout(false);
            this.gbxHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gbxBody)).EndInit();
            this.gbxBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLotNO_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPlantCode_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboItemCode_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboSave_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboStorage_H)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEnd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtLotNO_H;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private DC00_Component.Grid grid1;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cboPlantCode_H;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cboItemCode_H;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private Infragistics.Win.Misc.UltraLabel ultraLabel5;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor dtpEnd;
        private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor dtpStart;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cboSave_H;
        private Infragistics.Win.Misc.UltraLabel ultraLabel7;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cboStorage_H;
        private Infragistics.Win.Misc.UltraLabel ultraLabel6;
    }
}
