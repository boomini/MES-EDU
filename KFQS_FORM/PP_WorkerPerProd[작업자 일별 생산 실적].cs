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
    public partial class PP_WorkerPerProd : DC00_WinForm.BaseMDIChildForm
    {
        //그리드를 셋팅할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtil = new UltraGridUtil();
        // 공장 변수 입력
        private String plantCode = LoginInfo.PlantCode;
        public PP_WorkerPerProd()
        {
            InitializeComponent();
        }

        private void PP_WorkerPerProd_Load(object sender, EventArgs e)
        {
            // 그리드를 셋팅한다.
            try
            {
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",       "공장", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKER",    "작업자", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "PRODDATE",        "생산일자", true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Center, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE",  "작업장", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME",  "작업장명", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",        "품목", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",        "품명", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY",         "생산수량", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "BADQTY",          "불량수량", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "TOTALQTY",         "총생산량", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ERRORPERCENT",           "불량률", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",        "생산일시", true, GridColDataType_emu.DateTime24, 120, 120, Infragistics.Win.HAlign.Center, true, false);
                _GridUtil.SetInitUltraGridBind(grid1);

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();


                
                #region ▶ COMBOBOX ◀
                dtTemp = _Common.Standard_CODE("PLANTCODE");  // 공장
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");


                dtTemp = _Common.GET_Workcenter_Code();  // 작업장
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERNAME", dtTemp, "CODE_ID", "CODE_NAME");
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
                string sStartDate        = string.Format("{0:yyyy-MM-dd}",dtpStart_H.Value);
                string sEndDate          = string.Format("{0:yyyy-MM-15}",dtpEnd_H.Value);
                string sWoreker = Convert.ToString(txtWorkerId.Value);




                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("03PP_WorkerPerProd _S1", CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE",      sPlantCode,      DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("WORKER", sWoreker, DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("STARTDATE",     sStartDate,      DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ENDDATE",        sEndDate,        DbType.String, ParameterDirection.Input)
                                           );

                this.ClosePrgForm();
                //this.grid1.DataSource = dtTemp;
                if (dtTemp.Rows.Count > 0)
                {
                    //sub-total
                    DataTable dtSubTotal = dtTemp.Clone();
                    string sWorkerRow = Convert.ToString(dtTemp.Rows[0]["WORKER"]);
                    double sProdQty   = Convert.ToDouble(dtTemp.Rows[0]["PRODQTY"]);
                    double sBadQty    = Convert.ToDouble(dtTemp.Rows[0]["BADQTY"]);
                    double sTotalQty = Convert.ToDouble(dtTemp.Rows[0]["TOTALQTY"]);
                  /*  dtSubTotal.Rows.Add(new object[] { Convert.ToString(dtTemp.Rows[0]["PLANTCODE"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["WORKER"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["PRODDATE"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["WORKCENTERCODE"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["WORKCENTERNAME"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["ITEMCODE"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["ITEMNAME"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["PRODQTY"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["BADQTY"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["TOTALQTY"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["ERRORPERCENT"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["MAKEDATE"]) });*/

                    for(int i=1; i<dtTemp.Rows.Count; i++)
                    {
                        if(sWorkerRow == Convert.ToString(dtTemp.Rows[i]["WORKER"]))
                        {
                            sProdQty = sProdQty + Convert.ToDouble(dtTemp.Rows[i]["PRODQTY"]);
                            sBadQty = sBadQty + Convert.ToDouble(dtTemp.Rows[i]["BADQTY"]);
                            sTotalQty = sTotalQty + Convert.ToDouble(dtTemp.Rows[i]["TOTALQTY"]);

                            dtSubTotal.Rows.Add(new object[] { Convert.ToString(dtTemp.Rows[0]["PLANTCODE"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["WORKER"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["PRODDATE"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["WORKCENTERCODE"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["WORKCENTERNAME"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["ITEMCODE"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["ITEMNAME"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["PRODQTY"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["BADQTY"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["TOTALQTY"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["ERRORPERCENT"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["MAKEDATE"]) });
                            continue;
                        }
                        else
                        {
                            dtSubTotal.Rows.Add(new object[] { "", "", "", "", "", "", "합   계 :", sProdQty, sBadQty, sTotalQty, Convert.ToString(Math.Round(sBadQty * 100 / sProdQty, 1)) + " %", "" });

                            sProdQty = sProdQty + Convert.ToDouble(dtTemp.Rows[i]["PRODQTY"]);
                            sBadQty = sBadQty + Convert.ToDouble(dtTemp.Rows[i]["BADQTY"]);
                            sTotalQty = sTotalQty + Convert.ToDouble(dtTemp.Rows[i]["TOTALQTY"]);

                            dtSubTotal.Rows.Add(new object[] { Convert.ToString(dtTemp.Rows[0]["PLANTCODE"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["WORKER"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["PRODDATE"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["WORKCENTERCODE"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["WORKCENTERNAME"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["ITEMCODE"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["ITEMNAME"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["PRODQTY"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["BADQTY"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["TOTALQTY"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["ERRORPERCENT"]) ,
                                                       Convert.ToString(dtTemp.Rows[0]["MAKEDATE"]) });

                            sWorkerRow = Convert.ToString(dtTemp.Rows[i]["WORKER"]);
                        }
                    }
                    dtSubTotal.Rows.Add(new object[] { "", "", "", "", "", "", "합   계 :", sProdQty, sBadQty, sTotalQty, Convert.ToString(Math.Round(sBadQty * 100 / sProdQty, 1)) + " %", "" });
                    this.grid1.DataSource = dtSubTotal;

                    this.grid1.DataSource = dtTemp;
                    //grid1.DataBinds(dtTemp);
                    this.grid1.DisplayLayout.Override.MergedCellContentArea = MergedCellContentArea.VirtualRect;
                    this.grid1.DisplayLayout.Bands[0].Columns["PLANTCODE"].MergedCellStyle = MergedCellStyle.Always;
                    this.grid1.DisplayLayout.Bands[0].Columns["WORKER"].MergedCellStyle = MergedCellStyle.Always;
                    this.grid1.DisplayLayout.Bands[0].Columns["PRODDATE"].MergedCellStyle = MergedCellStyle.Always;
                    this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERCODE"].MergedCellStyle = MergedCellStyle.Always;
                    this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERNAME"].MergedCellStyle = MergedCellStyle.Always;
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

        

     

     
       
    }
}
