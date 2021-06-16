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
    public partial class PP_WCTRunStopList : DC00_WinForm.BaseMDIChildForm
    {
        //그리드를 셋팅할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtil = new UltraGridUtil();
        // 공장 변수 입력
        private String plantCode = LoginInfo.PlantCode;
        public PP_WCTRunStopList()
        {
            InitializeComponent();
        }

        private void PP_WCTRunStopList_Load(object sender, EventArgs e)
        {
            // 그리드를 셋팅한다.
            try
            {
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "RSSEQ", "키번호", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, false, true);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE", "작업장", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME", "작업장명", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO", "작업지시번호", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE", "품목", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME", "품명", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKER", "작업자", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUS", "가동/비가동", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "RSSTARTDATE", "시작일시", true, GridColDataType_emu.DateTime24, 120, 120, Infragistics.Win.HAlign.Center, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "RSENDDATE", "종료일시", true, GridColDataType_emu.DateTime24, 120, 120, Infragistics.Win.HAlign.Center, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "RUNNINGTIME", "소요시간", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY", "양품수량", true, GridColDataType_emu.Double, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "BADQTY", "불량수량", true, GridColDataType_emu.Double, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "REMARK", "사유", true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Center, true, true);
               

                
                _GridUtil.SetInitUltraGridBind(grid1);
                Common _Common = new Common();
                DataTable dtTemp = new DataTable();



                #region ▶ COMBOBOX ◀
                dtTemp = _Common.Standard_CODE("PLANTCODE");  // 공장
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");

                dtTemp = _Common.GET_Workcenter_Code();  // 작업장
                Common.FillComboboxMaster(this.cboWorkCenterCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERNAME", dtTemp, "CODE_ID", "CODE_NAME");
                // 품목코드 
                //FP  : 완제품
                //OM  : 외주가공품
                //R/M : 원자재
                //S/M : 부자재(H / W)
                //SFP : 반제품


                BizTextBoxManager btbManager = new BizTextBoxManager();
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
                string sPlantCode = Convert.ToString(cboPlantCode_H.Value);
                string sStartDate = string.Format("{0:yyyy-MM-dd}", dtpStart_H.Value);
                string sEndDate = string.Format("{0:yyyy-MM-15}", dtpEnd_H.Value);
                string sWorkcenterCode = Convert.ToString(cboWorkCenterCode_H.Value);




                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("03PP_WCTRunStopList_S1", CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("WORKCENTERCODE", sWorkcenterCode, DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("STARTDATE", sStartDate, DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ENDDATE", sEndDate, DbType.String, ParameterDirection.Input)
                                           );

                this.ClosePrgForm();
                //this.grid1.DataSource = dtTemp;
                if (dtTemp.Rows.Count > 0)
                {
                    this.grid1.DataSource = dtTemp;
                    
                    grid1.DataBinds(dtTemp);
                    this.grid1.DisplayLayout.Override.MergedCellContentArea = MergedCellContentArea.VirtualRect;
                    this.grid1.DisplayLayout.Bands[0].Columns["PLANTCODE"].MergedCellStyle = MergedCellStyle.Always;
                    this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERCODE"].MergedCellStyle = MergedCellStyle.Always;
                    this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERNAME"].MergedCellStyle = MergedCellStyle.Always;
                    this.grid1.DisplayLayout.Bands[0].Columns["ORDERNO"].MergedCellStyle = MergedCellStyle.Always;
                    this.grid1.DisplayLayout.Bands[0].Columns["ITEMCODE"].MergedCellStyle = MergedCellStyle.Always;
                    this.grid1.DisplayLayout.Bands[0].Columns["ITEMNAME"].MergedCellStyle = MergedCellStyle.Always;
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
                    if (drRow.RowState == DataRowState.Modified)
                    {
                        string sRemark = drRow["REMARK"].ToString();
                        if (sRemark == null || sRemark == "")
                        {
                            return;
                        }

                        helper.ExecuteNoneQuery("03PP_WCTRunStopList_U1", CommandType.StoredProcedure
                                                 , helper.CreateParameter("PLANTCODE", drRow["PLANTCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                 , helper.CreateParameter("RSSEQ", drRow["RSSEQ"].ToString(), DbType.String, ParameterDirection.Input)
                                                 , helper.CreateParameter("WORKCENTERCODE", drRow["WORKCENTERCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                                 , helper.CreateParameter("ORDERNO", drRow["ORDERNO"].ToString(), DbType.String, ParameterDirection.Input)
                                                 , helper.CreateParameter("REMARK", sRemark, DbType.String, ParameterDirection.Input)
                                                 , helper.CreateParameter("EDITOR", LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                                 );


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

        private void grid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            //그리드 컬럼간 머지 합병 기능 적용
            CustomMergedCellEvalutor CM1 = new CustomMergedCellEvalutor("ORDERNO", "ITEMCODE");
            e.Layout.Bands[0].Columns["ITEMCODE"].MergedCellEvaluator = CM1;
            e.Layout.Bands[0].Columns["ITEMNAME"].MergedCellEvaluator = CM1;
        }
    }   
}
