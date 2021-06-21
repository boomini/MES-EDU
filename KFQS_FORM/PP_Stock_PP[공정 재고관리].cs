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
    public partial class PP_Stock_PP : DC00_WinForm.BaseMDIChildForm
    {
        //그리드 셋팅 할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtill = new UltraGridUtil();
        //공장 변수 입력
        //private sPlantCode = LoginInfo
        public PP_Stock_PP()
        {
            InitializeComponent();
        }
        private void PP_Stock_PP_Load(object sender, EventArgs e)
        {   // 그리드 셋팅하고 시작한다.
            try
            {
                _GridUtill.InitializeGrid(this.grid1, false, true, false, "", false); //그리드1 의 기본 설정 내용
                // PLANTCODE값을 보여줄때는 공장으로, null값 허용, varchar형식, 130,130, 문자열은 왼쪽 정렬, 보여주고 수정은X)
                _GridUtill.InitColumnUltraGrid(grid1, "CHK"       , "원자재출고취소" , true, GridColDataType_emu.CheckBox, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "PLANTCODE" , "공장"           , true, GridColDataType_emu.VarChar, 170, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMCODE"  , "품목코드"       , true, GridColDataType_emu.VarChar, 170, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMNAME"  , "품목명"         , true, GridColDataType_emu.VarChar, 170, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMTYPE"  , "품목구분"       , true, GridColDataType_emu.VarChar, 170, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "LOTNO"     , "LOTNO"          , true, GridColDataType_emu.VarChar, 170, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "STOCKQTY"  , "재고수량"       , true, GridColDataType_emu.VarChar, 170, 130, Infragistics.Win.HAlign.Right, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "UNITCODE"  , "단위"           , true, GridColDataType_emu.VarChar, 170, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "WHCODE"    , "입고창고"       , true, GridColDataType_emu.VarChar, 170, 130, Infragistics.Win.HAlign.Left, true, true);
                
                //셋팅 내역을 바인딩
                _GridUtill.SetInitUltraGridBind(grid1);

                //콤보 박스 셋팅
                Common _Common = new Common();
                DataTable dtTemp = new DataTable();
                //PLANTCODE 기준정보 가져와서 데이터 테이블에 추가
                dtTemp = _Common.Standard_CODE("PLANTCODE");
                //데이터 테이블에 있는 데이터를 해당 콤보박스에 추가
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME"); // 그리드 상에 콤보박스를 넣는 방법
                                                                                                          //콤보박스에 데이터를 집어넣는 함수를 사용 , 공장콤보박스에 코드아이디라는 컬럼의 아이템 이름을 밸류로 쓰고 보여주는 내용으로는 네임을 사용할거다. 
                                                                                                          // 전체선택은 ALL로 표시하되 아무것도 없게 표시하라.

                //ITEMTYPE 조회부 콤보박스
                dtTemp = _Common.Standard_CODE("ITEMTYPE");
                Common.FillComboboxMaster(this.cboItemType_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

                dtTemp = _Common.Standard_CODE("WHCODE");     //입고 
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WHCODE", dtTemp, "CODE_ID", "CODE_NAME"); 
                
                dtTemp = _Common.Standard_CODE("UNITCODE");   //단위
                UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE", dtTemp, "CODE_ID", "CODE_NAME");

               
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
                string sPlantcode      = cboPlantCode_H.Value.ToString();
                string sItemType       = cboItemType_H.Value.ToString();
                string sLotno          = txtLOTNo_H.Text.ToString();
        

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("19PP_Stock_PP_S1", CommandType.StoredProcedure
                                          , helper.CreateParameter("PLANTCODE" , sPlantcode , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ITEMTYPE"  , sItemType  , DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("LOTNO"     , sLotno     , DbType.String, ParameterDirection.Input)
                                        );
                this.ClosePrgForm();
                //조회중이라고 뜨는 동그란 표시를 꺼주는 코드 base 내에 설정되어있음

                if (dtTemp.Rows.Count > 0) // 데이터가 있으면 바인딩해서 그리드에 출력
                {
                    grid1.DataSource = dtTemp;
                    grid1.DataBinds(dtTemp);
                }
                else
                {
                    _GridUtill.Grid_Clear(grid1);
                    ShowDialog("조회할 데이터가 없습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                }
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
            DBHelper helper = new DBHelper("", false);
            try
            {
                if (this.ShowDialog("원자재 생산 출고 취소를 하시겠습니까 ? ") == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["CHK"]) == "0") continue;

                    helper.ExecuteNoneQuery("19PP_Stock_PP_U1"
                                            , CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE"      , Convert.ToString(dt.Rows[i]["PLANTCODE"])  , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("LOTNO"          , Convert.ToString(dt.Rows[i]["LOTNO"])      , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ITEMCODE"       , Convert.ToString(dt.Rows[i]["ITEMCODE"])   , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("QTY"            , Convert.ToString(dt.Rows[i]["STOCKQTY"])   , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("UNITCODE"       , Convert.ToString(dt.Rows[i]["UnitCode"])   , DbType.String, ParameterDirection.Input)
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
        // 생산한 제품에 lot번호 발행
        private void ultraButton1_Click(object sender, EventArgs e)
        {
            if (grid1.ActiveRow == null) return;
            if (Convert.ToString(this.grid1.ActiveRow.Cells["ITEMTYPE"].Value)=="FERT")
            {
                DBHelper helper = new DBHelper(false);
                try
                {
                    string sPlantCode = Convert.ToString(grid1.ActiveRow.Cells["PLANTCODE"].Value);
                    string sLotno     = Convert.ToString(grid1.ActiveRow.Cells["LOTNO"].Value);

                    DataTable dtTemp = helper.FillTable("19PP_StockPP_S2", CommandType.StoredProcedure
                                                                         , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                                                         , helper.CreateParameter("LOTNO"    , sLotno, DbType.String, ParameterDirection.Input)
                                                                         );
                    if (dtTemp.Rows.Count==0)
                    {
                        ShowDialog("바코드 정보를 조회 할 내용이 없습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                        return;
                    }
                    // 조회할 내역이 있다면 바코드 발행
                    Report_LotBacodeFERT sReportFert = new Report_LotBacodeFERT();
                    // 바코드 디자인이 첨부될 레포트 북 클래스 선언
                    Telerik.Reporting.ReportBook repBook = new Telerik.Reporting.ReportBook();
                    // 바코드 디자인에 데이터 바인딩
                    sReportFert.DataSource = dtTemp;
                    // 레포트 북에 디자인 추가
                    repBook.Reports.Add(sReportFert);

                    // 레포트 미리보기 창에 레포트 북 등록 및 출력 장수 입력
                    ReportViewer BarcodeViewer = new ReportViewer(repBook,1);
                    // 미리보기 창 호출
                    BarcodeViewer.ShowDialog();
                }
                catch (Exception ex)
                {
                    ShowDialog(ex.ToString());
                }
                finally 
                {
                    helper.Close();
                }
            }

        }
    }
}
