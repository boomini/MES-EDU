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
    public partial class PP_StockPP : DC00_WinForm.BaseMDIChildForm
    {
        //그리드를 셋팅할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtil = new UltraGridUtil();
        // 공장 변수 입력
        private String plantCode = LoginInfo.PlantCode;
        public PP_StockPP()
        {
            InitializeComponent();
        }

        private void PP_StockPP_Load(object sender, EventArgs e)
        {
            // 그리드를 셋팅한다.
            try
            {
                _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "CHK",       "선택",         true, GridColDataType_emu.CheckBox,    120, 120, Infragistics.Win.HAlign.Left,   true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",    "공장",     true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",  "입고일자",     true, GridColDataType_emu.DateTime24,    300, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",  "품목",         true, GridColDataType_emu.VarChar,     100, 120, Infragistics.Win.HAlign.Right,  true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",  "품목명",       true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMTYPE", "품목구분", true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "LOTNO",  "LOTNO",        true, GridColDataType_emu.VarChar,    200, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY",  "수량",         true, GridColDataType_emu.VarChar,   100, 120, Infragistics.Win.HAlign.Center, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE",  "단위",         true, GridColDataType_emu.VarChar,    100, 120, Infragistics.Win.HAlign.Left,   true, false);
                _GridUtil.InitColumnUltraGrid(grid1, "WHCODE", "창고", true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
                _GridUtil.SetInitUltraGridBind(grid1);


                Common _Common = new Common();
                DataTable dtTemp = new DataTable();


                
                #region ▶ COMBOBOX ◀
                dtTemp = _Common.Standard_CODE("PLANTCODE");  // 공장
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

                dtTemp = _Common.Standard_CODE("ITEMTYPE");//품목
                Common.FillComboboxMaster(this.cboItemType_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
 

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
            base.DoInquire();
            DBHelper helper = new DBHelper(false);
            _GridUtil.Grid_Clear(grid1);
            try
            {
                _GridUtil.Grid_Clear(grid1);
                string sPlantCode        = Convert.ToString(cboPlantCode_H.Value);
                string sItemType         = Convert.ToString(cboItemType_H.Value);
                string sLotNO         = Convert.ToString(txtLotNO_H.Value);

                


                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("03PP_StockPP_S1", CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE", sPlantCode,    DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ITEMTYPE", sItemType,     DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("LOTNO", sLotNO,     DbType.String, ParameterDirection.Input)
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

        
    }
}
