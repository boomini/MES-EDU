using DC_POPUP;
using DC00_assm;
using DC00_WinForm;
using Infragistics.Win.UltraWinGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KFQS_Form
{
    public partial class AP_ProductPlan : DC00_WinForm.BaseMDIChildForm
    {
        //그리드를 셋팅할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtil = new UltraGridUtil();
        // 공장 변수 입력
        private String plantCode = LoginInfo.PlantCode;
        public AP_ProductPlan()
        {
            InitializeComponent();
        }

        private void AP_ProductPlan_Load(object sender, EventArgs e)
        {
            // 그리드를 셋팅한다.
            try
            {
                _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",       "공장",         true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANNO",          "계획번호",     true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",        "생산품목",     true, GridColDataType_emu.VarChar,    300, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANQTY",         "계획수량",     true, GridColDataType_emu.Double,     100, 120, Infragistics.Win.HAlign.Right,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",        "단위",         true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE",  "작업장",       true, GridColDataType_emu.VarChar,    200, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CHK",             "확정",         true, GridColDataType_emu.CheckBox,   100, 120, Infragistics.Win.HAlign.Center, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO",         "작업지시번호", true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ORDERDATE",       "확정일시",     true, GridColDataType_emu.DateTime24, 120, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ORDERWORKER",     "확정자",       true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ORDERCLOSEFLAG",  "지시종료여부", true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER",           "등록자",       true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",        "등록일시",     true, GridColDataType_emu.DateTime24, 120, 120, Infragistics.Win.HAlign.Center, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITOR",          "수정자",       true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE",        "수정일시",     true, GridColDataType_emu.DateTime24, 120, 120, Infragistics.Win.HAlign.Center, true, false);
                _GridUtil.SetInitUltraGridBind(grid1);

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();



                #region ▶ COMBOBOX ◀
                dtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                dtTemp = _Common.Standard_CODE("UNITCODE");     //단위
                UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE", dtTemp, "CODE_ID", "CODE_NAME");

                dtTemp = _Common.Standard_CODE("YESNO");     // 지시 종료 여부
                UltraGridUtil.SetComboUltraGrid(this.grid1, "ORDERCLOSEFLAG", dtTemp, "CODE_ID", "CODE_NAME");
                Common.FillComboboxMaster(this.cboOrderCloseFlag, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");


                dtTemp = _Common.GET_Workcenter_Code();     //작업장
                Common.FillComboboxMaster(this.cboWorkercenterCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERCODE", dtTemp, "CODE_ID", "CODE_NAME");

                // 품목코드 
                //FP  : 완제품
                //OM  : 외주가공품
                //R/M : 원자재
                //S/M : 부자재(H / W)
                //SFP : 반제품
                dtTemp = _Common.GET_ItemCodeFERT_Code("FERT");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMCODE", dtTemp, "CODE_ID", "CODE_NAME");

                #endregion


            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
            
        }
        public override void DoInquire()
        {
            base.DoInquire();
            DBHelper helper = new DBHelper(false);
            _GridUtil.Grid_Clear(grid1);
            try
            {
                _GridUtil.Grid_Clear(grid1);
                string sPlantCode        = Convert.ToString(cboPlantCode_H.Value);
                string sWorkercenterCode = Convert.ToString(cboWorkercenterCode_H.Value);
                string sOrderno          = Convert.ToString(txtOrderNo_H.Text);
                string sOrderCloseFlag   = Convert.ToString(cboWorkercenterCode_H.Value);


                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("03AP_ProductPlan_S1", CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE",        sPlantCode, DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("WORKCENTERCODE", sWorkercenterCode, DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ORDERNO",          sOrderno, DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ORDERCLOSEFLAG",   sOrderCloseFlag, DbType.String, ParameterDirection.Input));
                this.ClosePrgForm();
                if (dtTemp.Rows.Count > 0)
                {
                    grid1.DataSource = dtTemp;
                    grid1.DataBinds(dtTemp);
                }
                else
                {
                    _GridUtil.Grid_Clear(grid1);
                    ShowDialog("조회할 데이터가 없습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                }



            }
            catch (Exception ex)
            {
                // ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                helper.Close();
            }
        }
        public override void DoNew()
        {
            base.DoNew();
            try
            {
                this.grid1.InsertRow();
                this.grid1.SetDefaultValue("PLANTCODE", this.plantCode);

                grid1.ActiveRow.Cells["PLANNO"].Activation = Activation.NoEdit;
                grid1.ActiveRow.Cells["CHK"].Activation = Activation.NoEdit;
                grid1.ActiveRow.Cells["ORDERWORKER"].Activation = Activation.NoEdit;
                grid1.ActiveRow.Cells["MAKER"].Activation = Activation.NoEdit;
                grid1.ActiveRow.Cells["MAKEDATE"].Activation = Activation.NoEdit;
                grid1.ActiveRow.Cells["EDITOR"].Activation = Activation.NoEdit;
                grid1.ActiveRow.Cells["EDITDATE"].Activation = Activation.NoEdit;

            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString());
            }

        }
        public override void DoDelete()
        {

            base.DoDelete();
            if (Convert.ToString(grid1.ActiveRow.Cells["CHK"].Value) == "1")
            {
                ShowDialog("작업지시 확정 내역을 취소후 삭제하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            this.grid1.DeleteRow();
            

        }
        public override void DoSave()
        {

            //그리드에 표현된 내용을 소스 바인딩에 포함한다.
            this.grid1.UpdateData();
            DataTable dt = new DataTable();
            dt = grid1.chkChange();

            DBHelper helper = new DBHelper("", true);

            try
            {
                this.Focus();

                if (this.ShowDialog("C:Q00009") == System.Windows.Forms.DialogResult.Cancel)
                {
                    CancelProcess = true;
                    return;
                }

                base.DoSave();

                foreach (DataRow drRow in dt.Rows)
                {
                    switch (drRow.RowState)
                    {
                        case DataRowState.Deleted:
                            #region 삭제
                            drRow.RejectChanges();
                            helper.ExecuteNoneQuery("03AP_ProductPlan_D1", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE", plantCode, DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("PLANNO", drRow["PLANNO"], DbType.String, ParameterDirection.Input)
                                                                    );

                            #endregion
                            break;
                        case DataRowState.Added:
                            #region 추가
                            string sErrorMsg = string.Empty;
                            if (Convert.ToString(drRow["ITEMCODE"]) == "")
                            {
                                sErrorMsg += "품목 ";
                            }
                            if (Convert.ToString(drRow["PLANQTY"]) == "")
                            {
                                sErrorMsg += "수량 ";
                            }
                            if (Convert.ToString(drRow["WORKCENTERCODE"]) == "")
                            {
                                sErrorMsg += "작업장 ";
                            }
                            if (sErrorMsg != "")
                            {
                                this.ClosePrgForm();
                                ShowDialog(sErrorMsg + "을 입력하지 않았습니다", DialogForm.DialogType.OK);
                                return;
                            }
                            helper.ExecuteNoneQuery("03AP_ProductPlan_I1", CommandType.StoredProcedure
                                                  , helper.CreateParameter("PLANTCODE", drRow["PLANTCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("ITEMCODE", drRow["ITEMCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("PLANQTY", Convert.ToString(drRow["PLANQTY"]).Replace(",", ""), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("UNITCODE", drRow["UNITCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("WORKCENTERCODE", drRow["WORKCENTERCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("MAKER", LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                                  );

                            #endregion
                            break;
                        case DataRowState.Modified:
                            #region 수정
                            string sOrderFalg = string.Empty;
                            if (Convert.ToString(drRow["CHK"]).ToUpper() == "1") sOrderFalg = "Y";
                            else sOrderFalg = "N";

                            helper.ExecuteNoneQuery("03AP_ProductPlan_U1", CommandType.StoredProcedure
                                                  , helper.CreateParameter("PLANTCODE", drRow["PLANTCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("PLANNO", drRow["PLANNO"].ToString(), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("ORDERFLAG", sOrderFalg, DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("EDITOR", LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                                  );


                            #endregion
                            break;
                    }
                }
                if (helper.RSCODE != "S")
                {
                    this.ClosePrgForm();
                    helper.Rollback();
                    this.ShowDialog(helper.RSMSG, DialogForm.DialogType.OK);
                    return;
                }
                helper.Commit();
                this.ClosePrgForm();
                this.ShowDialog("R00002", DialogForm.DialogType.OK);    // 데이터가 저장 되었습니다.
                DoInquire();
            }
            catch (Exception ex)
            {
                CancelProcess = true;
                helper.Rollback();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            //바코드 발행
            if (grid1.ActiveRow == null) return; //선택된 행이 없을 경우 종료
            DataRow drRow = ((DataTable)this.grid1.DataSource).NewRow();
            drRow["ITEMCODE"] = Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value);
            drRow["ITEMNAME"] = Convert.ToString(this.grid1.ActiveRow.Cells["ITEMNAME"].Value);
            drRow["CUSTNAME"] = Convert.ToString(this.grid1.ActiveRow.Cells["CUSTNAME"].Value);
            drRow["STOCKQTY"] = Convert.ToString(this.grid1.ActiveRow.Cells["STOCKQTY"].Value);
            drRow["MATLOTNO"] = Convert.ToString(this.grid1.ActiveRow.Cells["MATLOTNO"].Value);
            drRow["UNITCODE"] = Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value);

            //바코드 디자인 선언
            Report_LotBacode repBarCode = new Report_LotBacode();
            //레포트 북 선언
            Telerik.Reporting.ReportBook repBook = new Telerik.Reporting.ReportBook();
            //바코드 디자이너에 데이터 등록
            repBarCode.DataSource = drRow;
            //레포트 북에 디자이너 등록
            repBook.Reports.Add(repBarCode);

            //미리보기 창 활성화
            ReportViewer repViewer = new ReportViewer(repBook, 1);
            repViewer.ShowDialog();
        }
    }
}
