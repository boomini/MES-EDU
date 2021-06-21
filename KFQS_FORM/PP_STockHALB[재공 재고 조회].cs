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
    public partial class PP_STockHALB : DC00_WinForm.BaseMDIChildForm
    {
        //그리드 셋팅 할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtill = new UltraGridUtil();
        //공장 변수 입력
        //private sPlantCode = LoginInfo
        public PP_STockHALB()
        {
            InitializeComponent();
        }
        private void PP_STockHALB_Load(object sender, EventArgs e)
        {   // 그리드 셋팅하고 시작한다.
            try
            {
                _GridUtill.InitializeGrid(this.grid1, false, true, false, "", false); 
                _GridUtill.InitColumnUltraGrid(grid1, "PLANTCODE"      , "공장"     , false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMCODE"       , "품목"     , false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMNAME"       , "품목명"   , false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMTYPE"       , "품목구분" , false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "LOTNO"          , "LOTNO"    , false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKCENTERCODE" , "작업장"   , false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKCENTERNAME" , "작업장명" , false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "STOCKQTY"       , "재고수량" , false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Right, false, false, null, null, null, null, null);
                _GridUtill.InitColumnUltraGrid(grid1, "UNITCODE"       , "단위"     , false, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left, true, false, null, null, null, null, null);
                
                //셋팅 내역을 바인딩
                _GridUtill.SetInitUltraGridBind(grid1);

         
                Common _Common = new Common();
                DataTable rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  //사업장
                Common.FillComboboxMaster(this.cboPlantCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PlantCode", rtnDtTemp, "CODE_ID", "CODE_NAME");

                DataTable dtTemp = _Common.Standard_CODE("ITEMTYPE");  //품목 구분
                Common.FillComboboxMaster(this.cboItemName_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMTYPE", dtTemp, "CODE_ID", "CODE_NAME");


                string sPlantCode = Convert.ToString(this.cboPlantCode_H.Value);
                this.cboPlantCode_H.Value = "1000";

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
                string sPlantCode = Convert.ToString(cboPlantCode_H.Value);
                string sLotNo = this.txtLOTNo_H.Text;
                string sItemCode = cboItemName_H.Value.ToString();

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("19PP_STockHALB_S1", CommandType.StoredProcedure
                                              , helper.CreateParameter("PLANTCODE" , sPlantCode, DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("ITEMTYPE"  , sItemCode, DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("LOTNO"     , sLotNo, DbType.String, ParameterDirection.Input)
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
      
        
    }
}
