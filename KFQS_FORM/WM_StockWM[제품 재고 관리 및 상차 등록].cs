using DC_POPUP;
using DC00_assm;
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
        //그리드 셋팅 할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtill = new UltraGridUtil();
        //공장 변수 입력
        //private sPlantCode = LoginInfo
        public WM_StockWM()
        {
            InitializeComponent();
        }
        private void WM_StockWM_Load(object sender, EventArgs e)
        {   // 그리드 셋팅하고 시작한다.
            try
            {
                _GridUtill.InitializeGrid(this.grid1, false, true, false, "", false); //그리드1 의 기본 설정 내용
                                                                                      // PLANTCODE값을 보여줄때는 공장으로, null값 허용, varchar형식, 130,130, 문자열은 왼쪽 정렬, 보여주고 수정은X)
                _GridUtill.InitColumnUltraGrid(grid1, "CHK"      , "선택"     , true, GridColDataType_emu.CheckBox  , 70, 100 , Infragistics.Win.HAlign.Center, true, true) ;
                _GridUtill.InitColumnUltraGrid(grid1, "PLANTCODE", "공장"     , true, GridColDataType_emu.VarChar   , 130, 100, Infragistics.Win.HAlign.Left  , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "YESNO"    , "상차 여부", true, GridColDataType_emu.VarChar   , 170, 100, Infragistics.Win.HAlign.Left  , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMCODE" , "품목"     , true, GridColDataType_emu.VarChar   , 130, 100, Infragistics.Win.HAlign.Left  , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMNAME" , "품목명"   , true, GridColDataType_emu.VarChar   , 170, 100, Infragistics.Win.HAlign.Left  , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "LOTNO"    , "LOTNO"    , true, GridColDataType_emu.VarChar   , 130, 100, Infragistics.Win.HAlign.Left  , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WHCODE"   , "입고창고" , true, GridColDataType_emu.VarChar   , 130, 100, Infragistics.Win.HAlign.Left  , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "STOCKQTY" , "재고수량" , true, GridColDataType_emu.Double   , 130, 100, Infragistics.Win.HAlign.Right , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "UNITCODE" , "단위"     , true, GridColDataType_emu.VarChar   , 130, 100, Infragistics.Win.HAlign.Left  , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKEDATE" , "등록일자" , true, GridColDataType_emu.DateTime24, 150, 100, Infragistics.Win.HAlign.Left  , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKER"    , "등록자"   , true, GridColDataType_emu.VarChar   , 130, 100, Infragistics.Win.HAlign.Left  , true, false);
                //셋팅 내역을 바인딩
                _GridUtill.SetInitUltraGridBind(grid1);

         
                dtpStart.Value = string.Format("{0:yyyy-MM-01}", DateTime.Now);
                
                Common _Common = new Common();

                DataTable rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  //사업장
                Common.FillComboboxMaster(this.cboPlantCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PlantCode", rtnDtTemp, "CODE_ID", "CODE_NAME");

        

                rtnDtTemp = _Common.Standard_CODE("YESNO");  // 상차여부
                Common.FillComboboxMaster(this.cboShipFlag, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "YESNO", rtnDtTemp, "CODE_ID", "CODE_NAME");
                
                rtnDtTemp = _Common.Standard_CODE("WHCODE");  // 입고창고
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WHCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

               
                BizTextBoxManager btManager = new BizTextBoxManager();
                btManager.PopUpAdd(btnWorkerID, txtWorkerName, "WORKER_MASTER", new object[] { "1000", "", "", "", "" });
                btManager.PopUpAdd(txtItemCode, txtItemName, "ITEM_MASTER", new object[] { "", "", "", "", "" });
                btManager.PopUpAdd(btnCustCode, txtCustName, "CUST_MASTER", new object[] { cboPlantCode_H, "", "", "" });
                
                cboPlantCode_H.Text = "1000";
            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
        }

    
        public override void DoInquire()
        {

            /*if (this.txtItemCode.Text == String.Empty)
            {
                ShowDialog("조회하실 품목을 입력 후 조회가 가능합니다.", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }*/

            DBHelper helper = new DBHelper(false);
            try
            {
                string sPlantCode  = Convert.ToString(cboPlantCode_H.Value);
                string sLotNo      = this.txtLotNo.Text;
                string sYESNO      = Convert.ToString(cboShipFlag.Value);
                string sItemcode   = this.txtItemCode.Text.ToString();
                string sSrart      = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sEnd        = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("19WM_StockWM_S1", CommandType.StoredProcedure
                                              , helper.CreateParameter("PLANTCODE" , sPlantCode    , DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("LOTNO"     , sLotNo        , DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("YESNO"     , sYESNO        , DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("ITEMCODE"  , sItemcode     , DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("STARTDATE" , sSrart        , DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("ENDDATE"   , sEnd          , DbType.String, ParameterDirection.Input)
                                              );
                grid1.DataSource = dtTemp;
                grid1.DataBinds();
                this.ClosePrgForm();
            }


            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally { helper.Close(); }

        }

        public override void DoSave()
        {

            DataTable dt = new DataTable();
            dt = grid1.chkChange();


            DBHelper helper = new DBHelper("", true);
            try
            {
                if (this.ShowDialog("선택하신 내역을 상차 등록 하시겠습니까 ?") == System.Windows.Forms.DialogResult.Cancel) return;

                string CarNo = txtCarNO.Text;
                string sWorker = btnWorkerID.Text;
                string sCustCode = btnCustCode.Text;
                if (CarNo == "")
                {
                    ShowDialog("차량 정보를 입력하지 않았습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                    return;
                }
                if (sWorker == "")
                {
                    ShowDialog("상차 작업자 정보를 입력하지 않았습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                    return;
                }
                if (sCustCode == "")
                {
                    ShowDialog("거래처 정보를 입력하지 않았습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                    return;
                }
                string ShipNo = string.Empty;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["CHK"]) == "0") continue; 
                    helper.ExecuteNoneQuery("19WM_StockWM_U2"
                                            , CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE"  , Convert.ToString(dt.Rows[i]["PLANTCODE"]) , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("LOTNO"      , Convert.ToString(dt.Rows[i]["LOTNO"])     , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("CARNO"      , CarNo                                     , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("CUSTCODE"   , sCustCode                                 , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("WORKER"     , sWorker                                   , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ITEMCODE"   , Convert.ToString(dt.Rows[i]["ITEMCODE"])  , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("SHIPQTY"   , Convert.ToString(dt.Rows[i]["STOCKQTY"])  , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("SHIPNO"     , ShipNo                                    , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("MAKER"      , LoginInfo.UserID                          , DbType.String, ParameterDirection.Input)
                                            );

                    if (helper.RSCODE == "S")
                    {
                        ShipNo = helper.RSMSG;
                    }
                    if (helper.RSCODE != "S") break;

                }

                if (helper.RSCODE != "S")
                {
                    this.ClosePrgForm();
                    helper.Rollback();
                    this.ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                    return;
                }
                helper.Commit();
                this.ClosePrgForm();
                this.ShowDialog("데이터가 저장 되었습니다.", DC00_WinForm.DialogForm.DialogType.OK);
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
