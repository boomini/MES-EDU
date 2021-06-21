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
        //그리드 셋팅 할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtill = new UltraGridUtil();
        //공장 변수 입력
        //private sPlantCode = LoginInfo
        public AP_ProductPlan()
        {
            InitializeComponent();
        }

        private void AP_ProductPlan_Load(object sender, EventArgs e)
        {   // 그리드 셋팅하고 시작한다.
            try
            {
                _GridUtill.InitializeGrid(this.grid1, true, true, false, "", false);
                _GridUtill.InitColumnUltraGrid(grid1, "PLANTCODE"      , "공장"         , true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "PLANNO"         , "계획번호"     , true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMCODE"       , "생산품목"     , true, GridColDataType_emu.VarChar, 300, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "PLANQTY"        , "계획수량"     , true, GridColDataType_emu.Double, 100, 120, Infragistics.Win.HAlign.Right, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "UNITCODE"       , "단위"         , true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKCENTERCODE" , "작업장"       , true, GridColDataType_emu.VarChar, 200, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "CHK"            , "확정"         , true, GridColDataType_emu.CheckBox, 100, 120, Infragistics.Win.HAlign.Center, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "ORDERNO"        , "작업지시번호" , true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ORDERDATE"      , "확정일시"     , true, GridColDataType_emu.DateTime24, 120, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ORDERWORKER"    , "확정자"       , true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ORDERCLOSEFLAG" , "지시종료여부" , true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKER"          , "등록자"       , true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKEDATE"       , "등록일시"     , true, GridColDataType_emu.DateTime24, 120, 120, Infragistics.Win.HAlign.Center, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "EDITOR"         , "수정자"       , true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "EDITDATE"       , "수정일시"     , true, GridColDataType_emu.DateTime24, 120, 120, Infragistics.Win.HAlign.Center, true, false);

                _GridUtill.SetInitUltraGridBind(grid1);

                DataTable rtnDtTemp = new DataTable();
                Common _Common = new Common();

                #region ▶ COMBOBOX ◀
                rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
                Common.FillComboboxMaster(this.cboPlantCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

                rtnDtTemp = _Common.Standard_CODE("UNITCODE");     //단위
                UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

                rtnDtTemp = _Common.Standard_CODE("YESNO");     // 지시 종료 여부
                UltraGridUtil.SetComboUltraGrid(this.grid1, "ORDERCLOSEFLAG", rtnDtTemp, "CODE_ID", "CODE_NAME");
                Common.FillComboboxMaster(this.cboOrderCloseFlag, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");


                rtnDtTemp = _Common.GET_Workcenter_Code();     //작업장
                Common.FillComboboxMaster(this.cboWorkcenterCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

                // 품목코드 
                //FP  : 완제품
                //OM  : 외주가공품
                //R/M : 원자재
                //S/M : 부자재(H / W)
                //SFP : 반제품
                rtnDtTemp = _Common.GET_ItemCodeFERT_Code("FERT");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

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
            _GridUtill.Grid_Clear(grid1);

            DBHelper helper = new DBHelper(false);
            try
            {
                string sPlantcode = Convert.ToString(cboPlantCode_H.Value);
                string sWorkcenterCode = Convert.ToString(cboWorkcenterCode_H.Value);
                string sOrderNo = Convert.ToString(txtOrderNo.Text);
                string sOrderCloseFlag = Convert.ToString(cboOrderCloseFlag.Value);



                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("19AP_ProductPlan_S1", CommandType.StoredProcedure
                                          , helper.CreateParameter("PLANTCODE"       , sPlantcode       , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("WORKCENTERCODE"  , sWorkcenterCode  , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ORDERNO"         , sOrderNo         , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ORDERCLOSEFLAG"  , sOrderCloseFlag  , DbType.String, ParameterDirection.Input));
                this.ClosePrgForm();

                if (dtTemp.Rows.Count > 0) // 데이터가 있으면 바인딩해서 그리드에 출력
                {
                    grid1.DataSource = dtTemp;
                    grid1.DataBinds(dtTemp);
                }
                else
                {
                    _GridUtill.Grid_Clear(grid1);
                    ShowDialog("조회할 데이터가 없습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally { helper.Close(); }
        }
        public override void DoNew()
        {
            base.DoNew();
            try
            {

                this.grid1.InsertRow();
                this.grid1.SetDefaultValue("PLANTCODE", LoginInfo.PlantCode);
                grid1.ActiveRow.Cells["PLANNO"].Activation      = Activation.NoEdit;
                grid1.ActiveRow.Cells["CHK"].Activation         = Activation.NoEdit;
                grid1.ActiveRow.Cells["ORDERNO"].Activation     = Activation.NoEdit;
                grid1.ActiveRow.Cells["ORDERWORKER"].Activation = Activation.NoEdit;

                grid1.ActiveRow.Cells["MAKER"].Activation    = Activation.NoEdit;
                grid1.ActiveRow.Cells["MAKEDATE"].Activation = Activation.NoEdit;
                grid1.ActiveRow.Cells["EDITOR"].Activation   = Activation.NoEdit;
                grid1.ActiveRow.Cells["EDITDATE"].Activation = Activation.NoEdit;

            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
        }
        public override void DoDelete()
        {
            if (Convert.ToString(grid1.ActiveRow.Cells["CHK"].Value) == "1")
            {
                ShowDialog("작업지시 확정 내역을 취소 후 삭제 하세요", DialogForm.DialogType.OK);
                return;
            }
            base.DoDelete();
            grid1.DeleteRow();
        }
        public override void DoSave()
        {
            //그리드에 표현된 내용을 소스 바인딩에 포함한다.
            this.grid1.UpdateData();

            base.DoSave();
            DataTable dtTemp = new DataTable();
            dtTemp = grid1.chkChange();
            if (dtTemp.Rows.Count == 0) return;
            DBHelper helper = new DBHelper("", true);
            try
            {
                this.Focus();

                if (this.ShowDialog("C:Q00009") == System.Windows.Forms.DialogResult.Cancel)  // 저장 하시겠습니까라고 물어보는 구문 
                {
                    CancelProcess = true;
                    return;
                }

                base.DoSave();

                foreach (DataRow drRow in dtTemp.Rows)
                {
                    switch (drRow.RowState)
                    {
                        case DataRowState.Deleted:
                            #region 삭제
                            drRow.RejectChanges();
                            helper.ExecuteNoneQuery("19AP_ProductPlan_D1", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE", LoginInfo.PlantCode , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("PLANNO"   , drRow["PLANNO"]     , DbType.String, ParameterDirection.Input)
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
                            helper.ExecuteNoneQuery("19AP_ProductPlan_I1", CommandType.StoredProcedure
                                                  , helper.CreateParameter("PLANTCODE"      , drRow["PLANTCODE"].ToString()      , DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("ITEMCODE"       , drRow["ITEMCODE"].ToString()       , DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("UNITCODE"       , drRow["UNITCODE"].ToString()       , DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("WORKCENTERCODE" , drRow["WORKCENTERCODE"].ToString() , DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("MAKER"          , LoginInfo.UserID                   , DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("PLANQTY"        , Convert.ToString(drRow["PLANQTY"]).Replace(",", ""), DbType.String, ParameterDirection.Input)
                                                  );

                            #endregion
                            break;
                        case DataRowState.Modified:
                            #region 수정
                            string sOrderFalg = string.Empty;
                            if (Convert.ToString(drRow["CHK"]).ToUpper() == "1") sOrderFalg = "Y";
                            else sOrderFalg = "N";

                            helper.ExecuteNoneQuery("19AP_ProductPlan_U1", CommandType.StoredProcedure
                                                  , helper.CreateParameter("PLANTCODE"   , drRow["PLANTCODE"].ToString() , DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("PLANNO"      , drRow["PLANNO"].ToString()    , DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("ORDERFLAG"   , sOrderFalg                    , DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("EDITOR"      , LoginInfo.UserID              , DbType.String, ParameterDirection.Input)
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
                ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }
    }
}