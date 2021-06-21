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
    public partial class MM_StockMM : DC00_WinForm.BaseMDIChildForm
    {
        //그리드 셋팅 할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtill = new UltraGridUtil();
        //공장 변수 입력
        //private sPlantCode = LoginInfo
        public MM_StockMM()
        {
            InitializeComponent();
        }

        private void MM_StockMM_Load(object sender, EventArgs e)
        {   // 그리드 셋팅하고 시작한다.
            try
            {
                _GridUtill.InitializeGrid(this.grid1, false, true, false, "", false); //그리드1 의 기본 설정 내용
                // PLANTCODE값을 보여줄때는 공장으로, null값 허용, varchar형식, 130,130, 문자열은 왼쪽 정렬, 보여주고 수정은X)
                _GridUtill.InitColumnUltraGrid(grid1, "PLANTCODE"  , "공장"      , true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMCODE"   , "품목"      , true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "ITEMNAME"   , "품목명"    , true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "WHCODE"     , "창고"      , true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "STOCKQTY"   , "재고수량"  , true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Right, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "UNITCODE"   , "단위"      , true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "CUSTCODE"   , "거래처"    , true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "CUSTNAME"   , "거래처명"  , true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKER"      , "등록자"    , true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKEDATE"   , "생성일시"  , true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);


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

                //부서 콤보 그리드뷰
                dtTemp = _Common.Standard_CODE("WHCODE");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "WHCODE", dtTemp, "CODE_ID", "CODE_NAME");

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
                string sPlantcode = cboPlantCode_H.Value.ToString();
                string sItemcode = txtItem_H.Text.ToString();
                string sItemName = txtItemName_H.Text.ToString();



                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("19MM_StockMM_S1", CommandType.StoredProcedure
                                          , helper.CreateParameter("PLANTCODE", sPlantcode, DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ITEMCODE", sItemcode, DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ITEMNAME", sItemName, DbType.String, ParameterDirection.Input)
                                          );
                this.ClosePrgForm();

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

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            //바코드 발행
            if (grid1.ActiveRow == null) return;
            DataRow drRow = ((DataTable)this.grid1.DataSource).NewRow();
            drRow["ITEMCODE"] = Convert.ToString(this.grid1.ActiveRow.Cells["ITEMCODE"].Value);
            drRow["ITEMNAME"] = Convert.ToString(this.grid1.ActiveRow.Cells["ITEMNAME"].Value);
            drRow["CUSTNAME"] = Convert.ToString(this.grid1.ActiveRow.Cells["CUSTNAME"].Value);
            drRow["STOCKQTY"] = Convert.ToString(this.grid1.ActiveRow.Cells["STOCKQTY"].Value);
            drRow["MATLOTNO"] = Convert.ToString(this.grid1.ActiveRow.Cells["MATLOTNO"].Value);
            drRow["UNITCODE"] = Convert.ToString(this.grid1.ActiveRow.Cells["UNITCODE"].Value);

            //바코드 디자인 선언
            Report_LotBacode repBarCode = new Report_LotBacode();
            //레포트 북 선언
            Telerik.Reporting.ReportBook repBook = new Telerik.Reporting.ReportBook();
            //바코드 디자이너에 데이터 등록
            repBarCode.DataSource = drRow;
            //레포트 북에 디자이너 등록
            repBook.Reports.Add(repBarCode);

            //미리보기 창 활성화
            ReportViewer repViewer = new ReportViewer(repBook, 1);
            repViewer.ShowDialog();
        }
    }
}
