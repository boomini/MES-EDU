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
    public partial class WM_ProdInStock : DC00_WinForm.BaseMDIChildForm
    {
        //그리드 셋팅 할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtill = new UltraGridUtil();
        //공장 변수 입력
        //private sPlantCode = LoginInfo
        public WM_ProdInStock()
        {
            InitializeComponent();
        }
        private void WM_ProdInStock_Load(object sender, EventArgs e)
        {   // 그리드 셋팅하고 시작한다.
            try
            {
                _GridUtill.InitializeGrid(this.grid1, false, true, false, "", false); //그리드1 의 기본 설정 내용
                                                                                      // PLANTCODE값을 보여줄때는 공장으로, null값 허용, varchar형식, 130,130, 문자열은 왼쪽 정렬, 보여주고 수정은X)
                _GridUtill.InitColumnUltraGrid(grid1, "CHK"       , "선택"     , true, GridColDataType_emu.CheckBox, 70, 100, Infragistics.Win.HAlign.Center  , true, true );
                _GridUtill.InitColumnUltraGrid(grid1, "PLANTCODE" , "공장"     , true, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left    , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "LOTNO"     , "LOTNO"    , true, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left    , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMCODE"  , "품목"     , true, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left    , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMNAME"  , "품목명"   , true, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left    , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMTYPE"  , "품목타입" , true, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left    , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WHCODE"    , "창고명"   , true, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Left    , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "STOCKQTY"  , "LOT수량"  , true, GridColDataType_emu.Double , 100, 100, Infragistics.Win.HAlign.Left    , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "UNITCODE"  , "단위"     , true, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Center  , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKER"     , "등록자"   , true, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Left    , true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKEDATE"  , "등록일시" , true, GridColDataType_emu.DateTime24, 150, 100, Infragistics.Win.HAlign.Left , true, false);

                //셋팅 내역을 바인딩
                _GridUtill.SetInitUltraGridBind(grid1);

         
                dtpStart.Value = string.Format("{0:yyyy-MM-01}", DateTime.Now);
                Common _Common = new Common();
                DataTable rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  //사업장
                Common.FillComboboxMaster(this.cboPlantCode_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PlantCode", rtnDtTemp, "CODE_ID", "CODE_NAME");
               
                rtnDtTemp = _Common.GET_ItemCodeFERT_Code("FERT");
                Common.FillComboboxMaster(this.cboItemName_H, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

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
                string sSrart     = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sEnd       = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);
                string sItemCode  = cboItemName_H.Value.ToString();

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("19WM_ProdInStock_S1", CommandType.StoredProcedure
                                              , helper.CreateParameter("PLANTCODE" , sPlantCode  , DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("STARTDATE" , sSrart      , DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("ENDDATE"   , sEnd        , DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("ITEMCODE"  , sItemCode   , DbType.String, ParameterDirection.Input)
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
            if (dt == null) return;

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

                    helper.ExecuteNoneQuery("19WM_ProdInStock_U1"
                                            , CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE" , Convert.ToString(dt.Rows[i]["PLANTCODE"])  , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("LOTNO"     , Convert.ToString(dt.Rows[i]["LOTNO"])      , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ITEMCODE"  , Convert.ToString(dt.Rows[i]["ITEMCODE"])   , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("INOUTQTY"  , Convert.ToString(dt.Rows[i]["STOCKQTY"])   , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("UNITCODE"  , Convert.ToString(dt.Rows[i]["UNITCODE"])   , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("MAKER"     , LoginInfo.UserID                           , DbType.String, ParameterDirection.Input));

                    if (helper.RSCODE != "S")
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
