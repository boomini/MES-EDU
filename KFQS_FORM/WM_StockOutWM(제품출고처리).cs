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
    public partial class WM_StockOutWM : DC00_WinForm.BaseMDIChildForm
    {
        //그리드를 셋팅할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtil = new UltraGridUtil();
        // 공장 변수 입력
        private String plantCode = LoginInfo.PlantCode;
        public WM_StockOutWM()
        {
            InitializeComponent();
        }

        private void WM_StockOutWM_Load(object sender, EventArgs e)
        {
            // 그리드를 셋팅한다.
            try
            {
                _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "CHK",       "출고 등록",  true, GridColDataType_emu.CheckBox, 120, 120, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장",       true, GridColDataType_emu.VarChar,  120, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "SHIPNO",    "상차번호",   true, GridColDataType_emu.VarChar,  300, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "SHIPDATE",  "상차일자",   true, GridColDataType_emu.VarChar,  100, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CARNO",     "차량번호",   true, GridColDataType_emu.VarChar,  100, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CUSTCODE",  "거래처코드", true, GridColDataType_emu.VarChar,  100, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "CUSTNAME",  "거래처명",   true, GridColDataType_emu.VarChar,  200, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKER",    "상차자",     true, GridColDataType_emu.VarChar,  100, 120, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "TRADINGNO", "명세서번호", true, GridColDataType_emu.VarChar,  100, 120, Infragistics.Win.HAlign.Left, true, false);

                _GridUtil.InitColumnUltraGrid(grid1, "TRADINGDATE", "출고일자", true, GridColDataType_emu.VarChar,       100, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",    "등록일자", true, GridColDataType_emu.DateTime24,    100, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER",       "등록자",   true, GridColDataType_emu.VarChar,       100, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE",    "수정일시", true, GridColDataType_emu.DateTime24,    160, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "EDITOR",      "수정자",   true, GridColDataType_emu.VarChar,       100, 120, Infragistics.Win.HAlign.Left, true, false);

                _GridUtil.SetInitUltraGridBind(grid1);

                _GridUtil.InitializeGrid(this.grid2, true, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid2, "PLANTCODE", "공장",     true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left,   false, false);
                _GridUtil.InitColumnUltraGrid(grid2, "SHIPNO",    "상차번호", true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left,   false, false);
                _GridUtil.InitColumnUltraGrid(grid2, "SHIPSEQ",   "상차순번", true, GridColDataType_emu.VarChar, 80, 120, Infragistics.Win.HAlign.Center,  true,  false);
                _GridUtil.InitColumnUltraGrid(grid2, "LOTNO",     "LOTNO",    true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left,   true,  false);
                _GridUtil.InitColumnUltraGrid(grid2, "ITEMCODE",  "품목",     true, GridColDataType_emu.VarChar, 200, 120, Infragistics.Win.HAlign.Left,   true,  false);
                _GridUtil.InitColumnUltraGrid(grid2, "ITEMNAME",  "품명",     true, GridColDataType_emu.VarChar, 150, 120, Infragistics.Win.HAlign.Center, true,  true);
                _GridUtil.InitColumnUltraGrid(grid2, "SHIPQTY",   "상차수량", true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left,   true,  false);
                _GridUtil.InitColumnUltraGrid(grid2, "UNITCODE",  "단위",     true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left,   true,  false);
                _GridUtil.SetInitUltraGridBind(grid1);
                Common _Common = new Common();
                DataTable dtTemp = new DataTable();



               
                dtTemp = _Common.Standard_CODE("PLANTCODE");  // 공장
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

                dtTemp = _Common.Standard_CODE("UNITCODE");  // 단위
                UltraGridUtil.SetComboUltraGrid(this.grid2, "UNITCODE", dtTemp, "CODE_ID", "CODE_NAME");

                BizTextBoxManager btbManager = new BizTextBoxManager();
                btbManager.PopUpAdd(txtCustCode_H, txtCustName_H, "CUST_MASTER", new object[] { cboPlantCode_H, "", "", "" });


                cboPlantCode_H.Value = plantCode;
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
            _GridUtil.Grid_Clear(grid2);
            try
            {
                _GridUtil.Grid_Clear(grid1);
                _GridUtil.Grid_Clear(grid2);
                string sPlantCode = Convert.ToString(cboPlantCode_H.Value);
                string sCustCode  = Convert.ToString(txtCustCode_H.Text);
                string sCarNo     = Convert.ToString(txtCarNO.Text);
                string sShipNo    = Convert.ToString(txtShipNO.Text);
                string sStartDate = string.Format("{0:yyyy-MM-dd}", dtpStart_H.Value);
                string sEndDate   = string.Format("{0:yyyy-MM-dd}", dtpEnd_H.Value);





                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("03WM_StockOutWM_S1", CommandType.StoredProcedure
                                           , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                           , helper.CreateParameter("CUSTCODE",  sCustCode, DbType.String, ParameterDirection.Input)
                                           , helper.CreateParameter("STARTDATE", sStartDate, DbType.String, ParameterDirection.Input)
                                           , helper.CreateParameter("ENDDATE",   sEndDate, DbType.String, ParameterDirection.Input)
                                           , helper.CreateParameter("CARNO",     sCarNo, DbType.String, ParameterDirection.Input)
                                           , helper.CreateParameter("SHIPNO",    sShipNo, DbType.String, ParameterDirection.Input)
                                         );

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
        }
        public override void DoDelete()
        {

        }
        public override void DoSave()
        {

            this.grid1.UpdateData();
            DataTable dt = grid1.chkChange();
            if (dt == null)
                return;
            string sCarNo = Convert.ToString(dt.Rows[0]["CARNO"]);
            int ChkCount = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToString(dt.Rows[i]["CHK"]) != "1") continue;
                if (sCarNo != Convert.ToString(dt.Rows[i]["CARNO"]))
                {
                    ShowDialog("차량 번호가 동일하지 않은 출고 등록 및 거래명세서는 발행 할 수 없습니다.", DialogForm.DialogType.OK);
                    return;
                }
                ChkCount += 1;
            }
            if (ChkCount == 0)
            {
                ShowDialog("선택된 출고 내역이 없습니다.", DialogForm.DialogType.OK);
                return;
            }
            DBHelper helper = new DBHelper("", true);
            try
            {
                // 동일한 차량 번호만 선택 하였는지 확인!

                if (this.ShowDialog("선택하신 내역을 출고 등록 하시겠습니까 ?") == System.Windows.Forms.DialogResult.Cancel) return;

                string sTradingNo = string.Empty;
                foreach (DataRow drRow in dt.Rows)
                {
                    switch (drRow.RowState)
                    {
                        case DataRowState.Deleted:
                            #region 삭제 
                            #endregion
                            break;
                        case DataRowState.Added:
                            #region 추가

                            #endregion
                            break;
                        case DataRowState.Modified:
                            #region 수정 
                            if (Convert.ToString(drRow["CHK"]) != "1") continue;
                            helper.ExecuteNoneQuery("00WM_StockOutWM_U1", CommandType.StoredProcedure
                                                  , helper.CreateParameter("PLANTCODE", Convert.ToString(drRow["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("SHIPNO", Convert.ToString(drRow["SHIPNO"]), DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("TRADINGNO", sTradingNo, DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("MAKER", LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                                  );

                            if (helper.RSCODE == "S")
                            {
                                sTradingNo = helper.RSMSG;
                            }
                            else break;
                            #endregion
                            break;
                    }
                    if (helper.RSCODE != "S") break;
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
                this.ShowDialog("데이터가 저장 되었습니다.", DialogForm.DialogType.OK);
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

        private void grid1_AfterRowActivate(object sender, EventArgs e)
        {
            DBHelper helper = new DBHelper(false);

            try
            {

                string sPlantCode = Convert.ToString(grid1.ActiveRow.Cells["PLANTCODE"].Value);
                string sShipNo = Convert.ToString(grid1.ActiveRow.Cells["SHIPNO"].Value);



                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("03WM_StockOutWM_S2", CommandType.StoredProcedure
                                           , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                           , helper.CreateParameter("SHIPNO", sShipNo, DbType.String, ParameterDirection.Input)
                                         );

                this.grid2.DataSource = dtTemp;
                
               



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
    }
}

