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
    public partial class PP_WorkerPerProd : DC00_WinForm.BaseMDIChildForm
    {
        //그리드 셋팅 할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtill = new UltraGridUtil();
        //공장 변수 입력
        //private sPlantCode = LoginInfo
        public PP_WorkerPerProd()
        {
            InitializeComponent();
        }

        private void PP_WorkerPerProd_Load(object sender, EventArgs e)
        {   
            try
            {
                _GridUtill.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtill.InitColumnUltraGrid(grid1, "PLANTCODE"      , "공장"             , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKER"         , "작업자"           , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "PRODDATE"       , "생산 일자"        , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKCENTERCODE" , "작업장"           , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKCENTERNAME" , "작업장명"         , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMCODE"       , "품목"             , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMNAME"       , "품명"             , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "PRODQTY"        , "생산수량"         , true, GridColDataType_emu.Double     , 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "BADQTY"         , "불량수량"         , true, GridColDataType_emu.Double     , 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "TOTALQTY"       , "총생산량"         , true, GridColDataType_emu.Double     , 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "BADPERCENT"     , "불량률"           , true, GridColDataType_emu.VarChar     , 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "PRODTIME"       , "생산 일시"        , true, GridColDataType_emu.VarChar    , 130, 130, Infragistics.Win.HAlign.Left, true, false);



                //셋팅 내역을 바인딩
                _GridUtill.SetInitUltraGridBind(grid1);


                //콤보 박스 셋팅
                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                
                //공장 콤보 박스
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE"      , dtTemp, "CODE_ID", "CODE_NAME");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMNAME"       , dtTemp, "CODE_ID", "CODE_NAME");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMCODE"       , dtTemp, "CODE_ID", "CODE_NAME");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERCODE" , dtTemp, "CODE_ID", "CODE_NAME");

                BizTextBoxManager btManager = new BizTextBoxManager();
                btManager.PopUpAdd(txtWorkerID, txtWorkerName, "WORKER_MASTER", new object[] { "", "", "", "", "" });
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
                string sWorker = Convert.ToString(txtWorkerID.Text);
                string sStartDate = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sEndDate = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);
                

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("19PP_WorkerPerProd_S1", CommandType.StoredProcedure
                                          , helper.CreateParameter("PLANTCODE"      , sPlantCode       , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("STARTDATE"      , sStartDate       , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("WORKER"         , sWorker          , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ENDDATE"        , sEndDate         , DbType.String, ParameterDirection.Input)
                                          );
    
                this.ClosePrgForm();

                this.grid1.DisplayLayout.Override.MergedCellContentArea                     = MergedCellContentArea.Default;
                this.grid1.DisplayLayout.Bands[0].Columns["PLANTCODE"].MergedCellStyle      = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["WORKER"].MergedCellStyle         = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["PRODDATE"].MergedCellStyle       = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERCODE"].MergedCellStyle = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERNAME"].MergedCellStyle = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["ITEMCODE"].MergedCellStyle       = MergedCellStyle.Always;
                this.grid1.DisplayLayout.Bands[0].Columns["ITEMNAME"].MergedCellStyle       = MergedCellStyle.Always;

                if (dtTemp.Rows.Count != 0)
                {
                    //SUB-TOTAL
                    DataTable dtSubTotal = dtTemp.Clone();
                    string sWorkerRow = Convert.ToString(dtTemp.Rows[0]["WORKER"]);
                    double sProdQty = Convert.ToDouble(dtTemp.Rows[0]["PRODQTY"]);
                    double sBadQty = Convert.ToDouble(dtTemp.Rows[0]["BADQTY"]);
                    double sTotalQty = Convert.ToDouble(dtTemp.Rows[0]["TOTALQTY"]);
                    dtSubTotal.Rows.Add(new object[] { Convert.ToString(dtTemp.Rows[0]["PLANTCODE"]),
                                                       Convert.ToString(dtTemp.Rows[0]["WORKER"]),
                                                       Convert.ToString(dtTemp.Rows[0]["PRODDATE"]),
                                                       Convert.ToString(dtTemp.Rows[0]["WORKCENTERCODE"]),
                                                       Convert.ToString(dtTemp.Rows[0]["WORKCENTERNAME"]),
                                                       Convert.ToString(dtTemp.Rows[0]["ITEMCODE"]),
                                                       Convert.ToString(dtTemp.Rows[0]["ITEMNAME"]),
                                                       Convert.ToString(dtTemp.Rows[0]["PRODQTY"]),
                                                       Convert.ToString(dtTemp.Rows[0]["BADQTY"]),
                                                       Convert.ToString(dtTemp.Rows[0]["TOTALQTY"]),
                                                       Convert.ToString(dtTemp.Rows[0]["BADPERCENT"]),
                                                       Convert.ToString(dtTemp.Rows[0]["PRODTIME"])
                                                      });

                    for (int i = 1; i < dtTemp.Rows.Count; i++)
                    {
                        if (sWorkerRow == Convert.ToString(dtTemp.Rows[i]["WORKER"]))
                        {
                            sProdQty = sProdQty + Convert.ToDouble(dtTemp.Rows[i]["PRODQTY"]);
                            sBadQty = sBadQty + Convert.ToDouble(dtTemp.Rows[i]["BADQTY"]);
                            sTotalQty = sTotalQty + Convert.ToDouble(dtTemp.Rows[i]["TOTALQTY"]);
                            dtSubTotal.Rows.Add(new object[] { Convert.ToString(dtTemp.Rows[0]["PLANTCODE"]),
                                                       Convert.ToString(dtTemp.Rows[i]["WORKER"]),
                                                       Convert.ToString(dtTemp.Rows[i]["PRODDATE"]),
                                                       Convert.ToString(dtTemp.Rows[i]["WORKCENTERCODE"]),
                                                       Convert.ToString(dtTemp.Rows[i]["WORKCENTERNAME"]),
                                                       Convert.ToString(dtTemp.Rows[i]["ITEMCODE"]),
                                                       Convert.ToString(dtTemp.Rows[i]["ITEMNAME"]),
                                                       Convert.ToString(dtTemp.Rows[i]["PRODQTY"]),
                                                       Convert.ToString(dtTemp.Rows[i]["BADQTY"]),
                                                       Convert.ToString(dtTemp.Rows[i]["TOTALQTY"]),
                                                       Convert.ToString(dtTemp.Rows[i]["BADPERCENT"]),
                                                       Convert.ToString(dtTemp.Rows[i]["PRODTIME"])
                                                      });
                            continue;
                        }
                        else
                        {
                            dtSubTotal.Rows.Add(new object[] { "", "", "", "", "", "", "합   계 :", sProdQty, sBadQty, sTotalQty, Convert.ToString(Math.Round(sBadQty * 100 / sProdQty, 1)) + " %", "" });
                            sProdQty = Convert.ToDouble(dtTemp.Rows[i]["PRODQTY"]);
                            sBadQty = Convert.ToDouble(dtTemp.Rows[i]["BADQTY"]);
                            sTotalQty = Convert.ToDouble(dtTemp.Rows[i]["TOTALQTY"]);
                            dtSubTotal.Rows.Add(new object[] { Convert.ToString(dtTemp.Rows[i]["PLANTCODE"]) ,
                                                               Convert.ToString(dtTemp.Rows[i]["WORKER"]),
                                                               Convert.ToString(dtTemp.Rows[i]["PRODDATE"]),
                                                               Convert.ToString(dtTemp.Rows[i]["WORKCENTERCODE"]),
                                                               Convert.ToString(dtTemp.Rows[i]["WORKCENTERNAME"]),
                                                               Convert.ToString(dtTemp.Rows[i]["ITEMCODE"]),
                                                               Convert.ToString(dtTemp.Rows[i]["ITEMNAME"]),
                                                               Convert.ToDouble(dtTemp.Rows[i]["PRODQTY"]),
                                                               Convert.ToDouble(dtTemp.Rows[i]["BADQTY"]),
                                                               Convert.ToDouble(dtTemp.Rows[i]["TOTALQTY"]),
                                                               Convert.ToString(dtTemp.Rows[i]["BADPERCENT"]),
                                                               Convert.ToString(dtTemp.Rows[i]["PRODTIME"])}
                                                               );
                            sWorkerRow = Convert.ToString(dtTemp.Rows[i]["WORKER"]);
                        }
                    }
                    dtSubTotal.Rows.Add(new object[] { "", "", "", "", "", "", "합   계 :", sProdQty, sBadQty, sTotalQty, Convert.ToString(Math.Round(sBadQty * 100 / sProdQty, 1)) + " %", "" });

                    this.grid1.DataSource = dtSubTotal;
                }
               // grid1.DataSource = dtTemp;
            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
            }
            finally { helper.Close(); }
        }

        private void grid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            /*if (Convert.ToString(e.Row.Cells["PLANTCODE"].Value) == "")
            {
                if (Convert.ToString(e.Row.Cells["ITEMNAME"].Value) == "합   계 :")
                {
                    e.Row.Appearance.BackColor = Color.Azure;
                }
            }*/
        }
    }
}
