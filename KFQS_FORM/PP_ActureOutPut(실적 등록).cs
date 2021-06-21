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
    public partial class PP_ActureOutPut : DC00_WinForm.BaseMDIChildForm
    {
        //그리드를 셋팅할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtil = new UltraGridUtil();
        // 공장 변수 입력
        private String plantCode = LoginInfo.PlantCode;
        public PP_ActureOutPut()
        {
            InitializeComponent();
        }

        private void PP_ActureOutPut_Load(object sender, EventArgs e)
        {
            // 그리드를 셋팅한다.
            try
            {
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO", "작업지시 번호", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE", "품목코드", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANQTY", "계획수량", true, GridColDataType_emu.Double, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY", "양품수량", true, GridColDataType_emu.Double, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "BADQTY", "불량수량", true, GridColDataType_emu.Double, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE", "단위", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO", "투입LOT", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "COMPONENT", "투입품목", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "COMPONENTQTY", "투입수량", true, GridColDataType_emu.Double, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CUNITCODE", "투입단위", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUSCODE", "가동/비가동 상태", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUS", "가동/비가동 상태", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ERRORFLAG", "고장/정상 상태", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKER", "작업자", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKERNAME", "작업자명", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "STARTDATE", "최초가동 시작시간", true, GridColDataType_emu.DateTime24, 160, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ENDDATE", "작업지시 종료시간", true, GridColDataType_emu.DateTime24, 160, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.SetInitUltraGridBind(grid1);

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();


                
                #region ▶ COMBOBOX ◀
                dtTemp = _Common.Standard_CODE("PLANTCODE");  // 공장
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");


                dtTemp = _Common.GET_Workcenter_Code();  // 작업장
                Common.FillComboboxMaster(this.cboWorkCenterCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERCODE", dtTemp, "CODE_ID", "CODE_NAME");

                // 품목코드 
                //FP  : 완제품
                //OM  : 외주가공품
                //R/M : 원자재
                //S/M : 부자재(H / W)
                //SFP : 반제품


                BizTextBoxManager btbManager = new BizTextBoxManager();
                btbManager.PopUpAdd(txtWorkerId, txtWorkerName, "WORKER_MASTER", new object[] { "", "", "", "", "" });
                #endregion


            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
            
        }
        public override void DoInquire()
        {
            
            DBHelper helper = new DBHelper(false);
            
            try
            {
                base.DoInquire();
                _GridUtil.Grid_Clear(grid1);
                string sPlantCode        = Convert.ToString(cboPlantCode_H.Value);
                string sWorkcentercode   = Convert.ToString(cboWorkCenterCode_H.Value);
                string sStartDate        = string.Format("{0:yyyy-MM-dd}",dtpStart_H.Value);
                string sEndDate          = string.Format("{0:yyyy-MM-dd}",dtpEnd_H.Value);
                string sOrderNo          = Convert.ToString(txtOrderNumber_H.Text);




                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("03PP_ActureOutPut_S33", CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE",      sPlantCode,      DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("WORKCENTERCODE", sWorkcentercode, DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("STARTDATE",      sStartDate,      DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ENDDATE",        sEndDate,        DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ORDERNO",        sOrderNo,        DbType.String, ParameterDirection.Input)
                                           );

                this.ClosePrgForm();
                //this.grid1.DataSource = dtTemp;
                if (dtTemp.Rows.Count > 0)
                {
                    this.grid1.DataSource = dtTemp;
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
        }
        public override void DoDelete()
        {

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
                    if(drRow.RowState == DataRowState.Modified) 
                    {
                        #region 수정
                        string check = drRow["ITEMTYPE"].ToString();
                        if (drRow["ITEMTYPE"].ToString() != "ROH")
                        {
                            helper.Rollback();
                            MessageBox.Show("원자재만 선택 가능합니다.");
                            return;
                        }
                        string sOrderFalg = string.Empty;
                        if (Convert.ToString(drRow["CHK"]).ToUpper() == "1")
                        {

                            helper.ExecuteNoneQuery("03PP_StockPP_U1", CommandType.StoredProcedure
                                                     , helper.CreateParameter("PLANTCODE", drRow["PLANTCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("LotNo", drRow["LOTNO"].ToString(), DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("ItemCode", drRow["ITEMCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("Qty", drRow["STOCKQTY"].ToString(), DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("UnitCode", drRow["UNITCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("WorkerId", LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                                     );
       
                        }
                        else return;
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

        private void btnWorker_Click(object sender, EventArgs e)
        {
            //작업자 등록 시작
            //작업지시가 있을때 validation
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null)
            {
                ShowDialog("작업지시를 선택후 진행 하세요", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }

            string sWorkID = txtWorkerId.Text.ToString();
            if(sWorkID == "")
            {
                ShowDialog("작업자를 선택 후 진행하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            //DB에 등록하기 위한 변수 지정
            string sOrderNo = grid1.ActiveRow.Cells["ORDERNO"].Value.ToString();
            string sWorkCentercode = grid1.ActiveRow.Cells["WORKCENTERCODE"].Value.ToString();

            DBHelper helper = new DBHelper("",true);
            try
            {
                helper.ExecuteNoneQuery("03PP_ActureOutput_I2", CommandType.StoredProcedure,
                                        helper.CreateParameter("PLANTCODE", "1000", DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("WORKER", sWorkID, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("ORDERNO", sOrderNo, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("WORKCENTERCODE", sWorkCentercode, DbType.String, ParameterDirection.Input)
                                        );
                if ( helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch(Exception ex)
            {

                helper.Rollback();
                ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally 
            { 
                helper.Close(); 
            }

        }

        private void btnLotIn_Click(object sender, EventArgs e)
        {
            //LOT 투입
            if (this.grid1.ActiveRow == null) return;
            DBHelper helper = new DBHelper("", true);
            try
            {
                string sItemcode = Convert.ToString(grid1.ActiveRow.Cells["ITEMCODE"].Value);
                string sLotno    = Convert.ToString(txtInLotNO.Text);
                string sWorkercenterCode = Convert.ToString(grid1.ActiveRow.Cells["WORKCENTERCODE"].Value);

                string sOrderno = Convert.ToString(grid1.ActiveRow.Cells["ORDERNO"].Value);
                string sUnitCode = Convert.ToString(grid1.ActiveRow.Cells["UNITCODE"].Value);
                string sInFlag = Convert.ToString(btnLotIn.Text);
                string sWorker = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
                if (sInFlag == "투입")
                {
                    sInFlag = "IN";
                }
                else sInFlag = "OUT";

                helper.ExecuteNoneQuery("03PP_ActureOutput_I1", CommandType.StoredProcedure,
                                        helper.CreateParameter("PLANTCODE",      "1000", DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("ITEMCODE",       sItemcode, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("LOTNO",          sLotno, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("WORKCENTERCODE", sWorkercenterCode, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("ORDERNO",        sOrderno, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("UNITCODE",        sUnitCode, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("INFLAG",         sInFlag, DbType.String, ParameterDirection.Input),
                                        helper.CreateParameter("MAKER",          sWorker, DbType.String, ParameterDirection.Input)
                                        );
                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                }
                helper.Commit();
            }
            catch(Exception ex)
            {
                helper.Rollback();
                //ShowDialog();
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }

        private void grid1_AfterRowActivate(object sender, EventArgs e)
        {
            if (Convert.ToString(this.grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "R")
            {
                btnRunStop.Text = "비가동";
            }
            else btnRunStop.Text = "가동";

            string sMatLotno = Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value);
            if(sMatLotno != "")
            {
                txtInLotNO.Text = sMatLotno;
                btnLotIn.Text = "투입취소";
            }
            else
            {
                txtInLotNO.Text = "";
                btnLotIn.Text = "투입";
            }
            txtWorkerId.Text = Convert.ToString(grid1.ActiveRow.Cells["WORKER"].Value);
            txtWorkerName.Text = Convert.ToString(grid1.ActiveRow.Cells["WORKERNAME"].Value);
        }

        private void btnRunStop_Click(object sender, EventArgs e)
        {
            // 가동/비가동 
            DBHelper helper = new DBHelper("", true);
            try
            {
                string sStatus = "R";
                if (btnRunStop.Text == "비가동") sStatus = "S";
                helper.ExecuteNoneQuery("03PP_ActureOutput_U1", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE",       "1000"                                                              , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("WORKCENTERCODE",  Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ORDERNO",         Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value)       , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ITEMCODE",        Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value)      , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("UNITCODE",        Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value)     , DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("STATUS",          sStatus                                                             , DbType.String, ParameterDirection.Input)
                                                                    );
                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog("정상적으로 등록되었습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                }
                else
                {
                    helper.Rollback();
                    ShowDialog("데이터 등록 중 오류가 발생했습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch(Exception ex)
            {
                ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            //생산 실적 등록
            if(this.grid1.ActiveRow == null)
            {
                ShowDialog("작업지시를 선택하세요", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            double dProdQty = 0;     // 누적 양품수량
            double dErrorQty = 0;    // 누적 불량수량
            double dTProdQty = 0;    // 입력 양품수량
            double dTErrorQty = 0;   // 입력 불량 수량
            double dOrderQty = 0;    // 작업지시 수량    
            double dInQty = 0;       // 투입 LOT 잔량

            string sProdQty = Convert.ToString(this.grid1.ActiveRow.Cells["PRODQTY"].Value).Replace(",", "");
                //숫자열에 ,가 자동으로 찍히기 때문에 숫자로 받아오기 위해 ,제거
            double.TryParse(sProdQty, out dProdQty);

            string sBadQty = Convert.ToString(this.grid1.ActiveRow.Cells["BADQTY"].Value).Replace(",", "");
            double.TryParse(sBadQty, out dErrorQty);

            string sTProdQty = Convert.ToString(txtProduct.Text);
            double.TryParse(sTProdQty, out dTProdQty);

            string sTBadQty = Convert.ToString(txtBad.Text);
            double.TryParse(sTBadQty, out dTErrorQty);

            string sOrderQty = Convert.ToString(this.grid1.ActiveRow.Cells["PLANQTY"].Value).Replace(",", "");
            double.TryParse(sOrderQty, out dOrderQty);

            string sInQty = Convert.ToString(this.grid1.ActiveRow.Cells["COMPONENTQTY"].Value).Replace(",", "");
            double.TryParse(sInQty, out dInQty);

            if(dInQty == 0)
            {
                ShowDialog("투입한 LOT이 존재하지 않습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            if((dTProdQty + dTErrorQty) == 0)
            {
                ShowDialog("실적 수량을 입력하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            if(dOrderQty < (dProdQty + dErrorQty) + (dTProdQty + dTErrorQty))
            {
                ShowDialog("생산수량 및 불량 수량의 합계가 지시수량보다 많습니다.", DC00_WinForm.DialogForm.DialogType.OK);
            }

            DBHelper helper = new DBHelper("", true);
            try
            {
                helper.ExecuteNoneQuery("20PP_ActureOutPut_U2", CommandType.StoredProcedure
                                                              , helper.CreateParameter("PLANTCODE", "1000", DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("WORKCENTERCODE", Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value), DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("ORDERNO", Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value), DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("ITEMCODE", Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value), DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("UNITCODE", Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value), DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("PRODQTY", dTProdQty, DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("ERRORQTY", dTErrorQty, DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("MATLOTNO", Convert.ToString(this.grid1.ActiveRow.Cells["MATLOTNO"].Value), DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("CITEMCODE", Convert.ToString(this.grid1.ActiveRow.Cells["COMPONENT"].Value), DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("CUNITCODE", Convert.ToString(this.grid1.ActiveRow.Cells["CUNITCODE"].Value), DbType.String, ParameterDirection.Input)
                                                              );
                if(helper.RSCODE != "S")
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG);
                    return;

                }
                helper.Commit();
                ShowDialog("생산 실적 등록을 완료 하였습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                DoInquire();
                txtInLotNO.Text = "";
                txtProduct.Text = "";
                txtBad.Text = "";
            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString(), DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }
        }

        private void btnOrderClose_Click(object sender, EventArgs e)
        {
            // 작업지시 종료
            if (grid1.Rows.Count == 0) return;
            if (grid1.ActiveRow == null) return;
            if (Convert.ToString(grid1.ActiveRow.Cells["MATLOTNO"].Value) != "")
            {
                ShowDialog("LOT 투입 취소 후 진행하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            //가동 일 경우 종료 안되도록 확인
            if(Convert.ToString(grid1.ActiveRow.Cells["WORKSTATUSCODE"].Value) == "R")
            {
                ShowDialog("비가동 등록 후 진행하세요", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            DBHelper helper = new DBHelper("", true);
            try
            {
                helper.ExecuteNoneQuery("03PP_ActureOutPut_U3", CommandType.StoredProcedure
                                                              , helper.CreateParameter("PLANTCODE", "1000", DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("WORKCENTERCODE", Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value), DbType.String, ParameterDirection.Input)
                                                              , helper.CreateParameter("ORDERNO", Convert.ToString(this.grid1.ActiveRow.Cells["ORDERNO"].Value), DbType.String, ParameterDirection.Input)
                                                              );
                if(helper.RSCODE != "S")
                {
                    helper.Rollback();
                    ShowDialog(helper.RSMSG);
                    return;
                }
                helper.Commit();
                ShowDialog("상태 등록을 완료 하였습니다", DC00_WinForm.DialogForm.DialogType.OK);
                DoInquire();
                txtInLotNO.Text = "";
                txtProduct.Text = "";
                txtBad.Text = "";
            }
            catch (Exception ex)
            {
                helper.Rollback();
                ShowDialog(ex.ToString(), DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally
            {
                helper.Close();
            }
        }

        private void btnError_Click(object sender, EventArgs e)
        {
             
           DBHelper helper = new DBHelper("", true);
            try
            {
                helper.ExecuteNoneQuery("03PP_ActureOutput_U33", CommandType.StoredProcedure
                                                                    , helper.CreateParameter("PLANTCODE", "1000", DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("WORKCENTERCODE", Convert.ToString(this.grid1.ActiveRow.Cells["WORKCENTERCODE"].Value), DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("ORDDERNO", Convert.ToString(this.grid1.ActiveRow.Cells["ORDDERNO"].Value), DbType.String, ParameterDirection.Input)
                                                                    , helper.CreateParameter("WORKER", LoginInfo.UserID , DbType.String, ParameterDirection.Input)
                                                                    );
                if (helper.RSCODE == "S")
                {
                    helper.Commit();
                    ShowDialog("정상적으로 등록되었습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                }
                else
                {
                    helper.Rollback();
                    ShowDialog("데이터 등록 중 오류가 발생했습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }
    }
}
