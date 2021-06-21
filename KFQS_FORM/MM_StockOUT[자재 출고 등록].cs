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
    public partial class MM_StockOUT : DC00_WinForm.BaseMDIChildForm
    {
        //그리드 셋팅 할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtill = new UltraGridUtil();
        //공장 변수 입력
        //private sPlantCode = LoginInfo
        public MM_StockOUT()
        {
            InitializeComponent();
        }
        private void MM_StockOUT_Load(object sender, EventArgs e)
        {   // 그리드 셋팅하고 시작한다.
            try
            {
                _GridUtill.InitializeGrid(this.grid1, false, true, false, "", false); //그리드1 의 기본 설정 내용
                                                                                      // PLANTCODE값을 보여줄때는 공장으로, null값 허용, varchar형식, 130,130, 문자열은 왼쪽 정렬, 보여주고 수정은X)
                _GridUtill.InitColumnUltraGrid(grid1, "CHK", "선택", false, GridColDataType_emu.CheckBox, 70, 100, Infragistics.Win.HAlign.Center, true, true, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "PLANTCODE", "공장", false, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Center, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKEDATE", "입고일자", false, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Center, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMCODE", "품목", false, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMNAME", "품목명", false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "MATLOTNO", "LOTNO", false, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "StockQty", "수량", false, GridColDataType_emu.VarChar, 70, 100, Infragistics.Win.HAlign.Right, true, false, "#,##0", null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "UnitCode", "단위", false, GridColDataType_emu.VarChar, 50, 100, Infragistics.Win.HAlign.Center, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "WHCode", "창고코드", false, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Center, false, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "WHName", "창고", false, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "StorageLocCode", "위치코드", false, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Center, false, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "StorageLocName", "위치", false, GridColDataType_emu.VarChar, 70, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKER", "입고자", false, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                //셋팅 내역을 바인딩
                _GridUtill.SetInitUltraGridBind(grid1);

         
                dtpStart.Value = string.Format("{0:yyyy-MM-01}", DateTime.Now);
                cboWHCODE_H.Value = "WH003";
                Common _Common = new Common();
                DataTable rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  //사업장
                Common.FillComboboxMaster(this.cboPlantCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PlantCode", rtnDtTemp, "CODE_ID", "CODE_NAME");

                rtnDtTemp = _Common.Standard_CODE("WHCODE", "MINORCODE = 'WH003'");  // 출고위치
                Common.FillComboboxMaster(this.cboWHCODE_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

                rtnDtTemp = _Common.Standard_CODE("STORAGELOCCODE", "RELCODE1 = 'WH003'");  // 저장위치
                Common.FillComboboxMaster(this.cboStorageloccode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

                rtnDtTemp = _Common.GET_ItemCodeFERT_Code("ROH");
                Common.FillComboboxMaster(this.cboItemName_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");


                string sPlantCode = Convert.ToString(this.cboPlantCode_H.Value);
                this.cboPlantCode_H.Value = "1000";

            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
        }
        private void dtStart_H_TextChanged(object sender, EventArgs e)
        {
            CheckData();
        }
        private bool CheckData()
        {
            //int sSrart = Convert.ToInt32(string.Format("{0:yyyyMMdd}", dtpStart.Value));
            //int sEnd = Convert.ToInt32(string.Format("{0:yyyy-MM-dd}", dtpEnd.Value));
            //if (sSrart > sEnd)
            //{
            //    this.ShowDialog("조회 시작일자가 종료일자보다 큽니다.", DC00_WinForm.DialogForm.DialogType.OK);
            //    return false;
            //}
            return true;
        }
        public override void DoInquire()
        {
            if (!CheckData())
            {
                return;
            }

            DBHelper helper = new DBHelper(false);
            try
            {
                string sPlantCode = Convert.ToString(cboPlantCode_H.Value);
                string sSrart = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sEnd = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);
                string sLotNo = this.txtLOTNo_H.Text;
                string sItemCode = cboItemName_H.Value.ToString();

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("19MM_StockOUT_S1", CommandType.StoredProcedure
                                           , helper.CreateParameter("PlantCode", sPlantCode, DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("STARTDATE", sSrart, DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("ENDDATE", sEnd, DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("ITEMCODE", sItemCode, DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("MATLOTNO", sLotNo, DbType.String, ParameterDirection.Input)
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
        public override void DoNew()
        {
            base.DoNew();
            this.grid1.InsertRow();

            //여기엔 뭘추가하지..?
        }
        public override void DoDelete()
        {
            base.DoDelete();
            this.grid1.DeleteRow();
        }
        public override void DoSave()
        {
            DataTable dt = new DataTable();
            dt = grid1.chkChange();
            if (dt == null)
                return;
            if (this.cboWHCODE_H.Value.ToString() == "")
            {
                this.ShowDialog("창고를 선택하세요", DC00_WinForm.DialogForm.DialogType.OK);
                return;
            }
            DBHelper helper = new DBHelper("", true);
            try
            {
                if (this.ShowDialog("C:Q00009") == System.Windows.Forms.DialogResult.Cancel)
                {
                    CancelProcess = true;
                    return;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["CHK"]) == "0") continue;

                    helper.ExecuteNoneQuery("19MM_StockOut_U1"
                                            , CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE"      , Convert.ToString(dt.Rows[i]["PLANTCODE"])  , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("LOTNO"          , Convert.ToString(dt.Rows[i]["MATLOTNO"])   , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ITEMCODE"       , Convert.ToString(dt.Rows[i]["ITEMCODE"])   , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("QTY"            , Convert.ToString(dt.Rows[i]["STOCKQTY"])   , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("UNITCODE"       , Convert.ToString(dt.Rows[i]["UnitCode"])   , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("WHCODE"         , Convert.ToString(cboWHCODE_H.Value)        , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("STORAGELOCCODE" , Convert.ToString(cboStorageloccode_H.Value), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("WORKERID"       , this.WorkerID                              , DbType.String, ParameterDirection.Input));

                    if (helper.RSCODE == "E")
                    {
                        this.ShowDialog(helper.RSMSG, DC00_WinForm.DialogForm.DialogType.OK);
                        helper.Rollback();
                        return;
                    }
                }

                helper.Commit();
                this.ShowDialog("데이터가 저장 되었습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                this.ClosePrgForm();
                DoInquire();
            }
                
            
            catch (Exception ex)
            {
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
