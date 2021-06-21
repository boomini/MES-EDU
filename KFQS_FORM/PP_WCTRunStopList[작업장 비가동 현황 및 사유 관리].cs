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
using DC_POPUP;

namespace KFQS_Form
{
    public partial class PP_WCTRunStopList : DC00_WinForm.BaseMDIChildForm
    {
        //그리드 셋팅 할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtill = new UltraGridUtil();
        //공장 변수 입력
        //private sPlantCode = LoginInfo
        public PP_WCTRunStopList()
        {
            InitializeComponent();
        }

        private void PP_WCTRunStopList_Load(object sender, EventArgs e)
        {   
            try
            {
                _GridUtill.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtill.InitColumnUltraGrid(grid1, "PLANTCODE"      , "공장"          , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "RSSEQ"          , "SEQ"           , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, false, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKCENTERCODE" , "작업장"        , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKCENTERNAME" , "작업장명"      , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ORDERNO"        , "작업지시번호"  , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMCODE"       , "품목코드"      , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMNAME"       , "품명"          , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKER"         , "작업자"        , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "RUNSTOP"        , "가동/비가동"   , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "RSSTARTDATE"    , "시작 일시"     , true, GridColDataType_emu.DateTime   , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "RSENDDATE"      , "종료 일시"     , true, GridColDataType_emu.DateTime   , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "SPENDTIME"      , "소요 시간(분)" , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "PRODQTY"        , "양품 수량"     , true, GridColDataType_emu.Double     , 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "BADQTY"         , "불량 수량"     , true, GridColDataType_emu.Double     , 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "REMARK"         , "사유"          , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKER"          , "등록자"        , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKEDATE"       , "등록 일시"     , true, GridColDataType_emu.DateTime   , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "EDITOR"         , "수정자"        , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "EDITDATE"       , "수정 일시"     , true, GridColDataType_emu.DateTime   , 130, 130, Infragistics.Win.HAlign.Right, true, false);
                

                //셋팅 내역을 바인딩
                _GridUtill.SetInitUltraGridBind(grid1);


                //콤보 박스 셋팅
                Common _Common = new Common();
                DataTable dtTemp = new DataTable();

                //공장 콤보 박스
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                //공장 콤보 박스
                dtTemp = _Common.GET_Workcenter_Code();     //작업장
                Common.FillComboboxMaster(this.cboWorkPlace, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERCODE", dtTemp, "CODE_ID", "CODE_NAME");

 
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
            try 
            {
                //작업 지시 사항 조회
                string sPlantCode = Convert.ToString(cboPlantCode_H.Value);
                string sWorkePlace = Convert.ToString(cboWorkPlace.Value);
                string sStartDate = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sEndDate = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);
                

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("19PP_WCTRunStopList_S1", CommandType.StoredProcedure
                                          , helper.CreateParameter("PLANTCODE"      , sPlantCode       , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("WORKCENTERCODE" , sWorkePlace      , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("STARTDATE"      , sStartDate       , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ENDDATE"        , sEndDate         , DbType.String, ParameterDirection.Input)
                                          );
                grid1.DataSource = dtTemp;
                grid1.DataBinds();
                this.ClosePrgForm();

                this.grid1.DisplayLayout.Override.MergedCellContentArea = MergedCellContentArea.VisibleRect;
                this.grid1.DisplayLayout.Bands[0].Columns["PLANTCODE"].MergedCellStyle = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERCODE"].MergedCellStyle = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERNAME"].MergedCellStyle = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["ORDERNO"].MergedCellStyle = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["ITEMCODE"].MergedCellStyle = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["ITEMNAME"].MergedCellStyle = MergedCellStyle.Always;
            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally { helper.Close(); }
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
                  
                        case DataRowState.Modified:
                           

                            helper.ExecuteNoneQuery("19PP_WCTRunStopList_U1", CommandType.StoredProcedure
                                                  , helper.CreateParameter("PLANTCODE"     , drRow["PLANTCODE"].ToString()      , DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("WORKCENTERCODE", drRow["WORKCENTERCODE"].ToString() , DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("ORDERNO"       , drRow["ORDERNO"].ToString()        , DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("RSSEQ"         , drRow["RSSEQ"].ToString()          , DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("REMARK"        , drRow["REMARK"].ToString()         , DbType.String, ParameterDirection.Input)
                                                  , helper.CreateParameter("MAKER"         , LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                                  );


                            break;
                    }
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
                this.ShowDialog("R00002", DC00_WinForm.DialogForm.DialogType.OK);    // 데이터가 저장 되었습니다.
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

        private void grid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            CustomMergedCellEvalutor CM1 = new CustomMergedCellEvalutor("ORDERNO", "ITEMCODE");
            e.Layout.Bands[0].Columns["ITEMCODE"].MergedCellEvaluator = CM1;
            e.Layout.Bands[0].Columns["ITEMNAME"].MergedCellEvaluator = CM1;
        }
    }
}
