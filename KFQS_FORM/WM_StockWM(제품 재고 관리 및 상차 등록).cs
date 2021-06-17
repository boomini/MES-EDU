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
    public partial class WM_StockWM : DC00_WinForm.BaseMDIChildForm
    {
        //그리드를 셋팅할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtil = new UltraGridUtil();
        // 공장 변수 입력
        private String plantCode = LoginInfo.PlantCode;
        public WM_StockWM()
        {
            InitializeComponent();
        }

        private void WM_StockWM_Load(object sender, EventArgs e)
        {
            // 그리드를 셋팅한다.
            try
            {
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "SHIPCHECK", "상차등록", true, GridColDataType_emu.CheckBox, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "SHIPFLAG", "상차여부", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE", "품목", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME", "품목명", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "LOTNO", "LOTNO", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WHCODE", "입고창고", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY", "재고수량", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "BASEUNIT", "단위", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "INDATE", "입고일자", true, GridColDataType_emu.Double, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE", "등록일시", true, GridColDataType_emu.DateTime24, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER", "입고자", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.SetInitUltraGridBind(grid1);

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();


                
                #region ▶ COMBOBOX ◀
                dtTemp = _Common.Standard_CODE("PLANTCODE");  // 공장
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                BizTextBoxManager btbManager = new BizTextBoxManager(); //품목
                btbManager.PopUpAdd(txtItemCode_H, txtItemName_H, "ITEM_MASTER", new object[] { "1000", "" });

                dtTemp =_Common.Standard_CODE("YESNO");//상차여부
                Common.FillComboboxMaster(this.cboShip, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

                //작업자
                btbManager.PopUpAdd(txtWorkerId, txtWorkerName, "WORKER_MASTER", new object[] { "", "", "", "", "" });

                //거래처
                btbManager.PopUpAdd(txtCustCode_H, txtCustName_H, "CUST_MASTER", new object[] { cboPlantCode_H, "", "", "" });

                // 품목코드 
                //FP  : 완제품
                //OM  : 외주가공품
                //R/M : 원자재
                //S/M : 부자재(H / W)
                //SFP : 반제품


         
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
                string sStartDate        = string.Format("{0:yyyy-MM-dd}",dtpStart_H.Value);
                string sEndDate          = string.Format("{0:yyyy-MM-dd}",dtpEnd_H.Value);
                string sLotno          = Convert.ToString(txtOrderNumber_H.Text);
                string sItemcode = Convert.ToString(txtItemCode_H.Text);
                string sWorker = Convert.ToString(txtWorkerId.Text);
                string sShipflag = Convert.ToString(cboShip.Value);
                string sCarnum = Convert.ToString(txtCarnum.Text);
                string sCustcode = Convert.ToString(txtCustCode_H.Value);




                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("03WM_StockWM_S1", CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE", sPlantCode,      DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("STARTDATE", sStartDate,      DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ENDDATE",   sEndDate,        DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("LOTNO",     sLotno,          DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ITEMCODE",  sItemcode,       DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("SHIPFLAG",  sShipflag,       DbType.String, ParameterDirection.Input)
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
            string sWorker = Convert.ToString(txtWorkerId.Text);
            string sCarnum = Convert.ToString(txtCarnum.Text);
            string sCustcode = Convert.ToString(txtCustCode_H.Value);
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
                string sPlantcode = "";
                string shipno = "0";
                foreach (DataRow drRow in dt.Rows)
                {
                    sPlantcode = drRow["PLANTCODE"].ToString();

                    helper.ExecuteNoneQuery("03WM_StockWM_U1", CommandType.StoredProcedure
                                                     , helper.CreateParameter("PLANTCODE", drRow["PLANTCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("WORKER", sWorker, DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("LOTNO", drRow["LOTNO"].ToString(), DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("ITEMCODE", drRow["ITEMCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("STOCKQTY", drRow["STOCKQTY"].ToString(), DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("MAKER", LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("SHIPNO", shipno, DbType.String, ParameterDirection.Input)
                                                     );
                    if(helper.RSCODE == "S")
                    {
                        shipno = helper.RSMSG;
                    }
                    else
                    {
                        this.ShowDialog("상차 등록이 실패하였습니다.", DialogForm.DialogType.OK);
                    }

                    
                }
                helper.ExecuteNoneQuery("03WM_StockWM_U2", CommandType.StoredProcedure
                                                     , helper.CreateParameter("PLANTCODE", sPlantcode, DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("SHIPNO", helper.RSMSG, DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("CARNO", sCarnum, DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("CUSTCODE", sCustcode, DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("WORKER", sWorker, DbType.String, ParameterDirection.Input)
                                                     , helper.CreateParameter("MAKER", LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                                     );
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

  
    }
}
/****
 * 서희 commit 확인
 * */
