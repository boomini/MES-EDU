﻿using DC_POPUP;
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
        //그리드를 셋팅할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtil = new UltraGridUtil();
        // 공장 변수 입력
        //private sPlantCode = LoginInfo
        public MM_StockMM()
        {
            InitializeComponent();
        }

        private void MM_StockMM_Load(object sender, EventArgs e)
        {
            // 그리드를 셋팅한다.
            try
            {
                _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
                _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장", true, GridColDataType_emu.VarChar,
                                               130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE", "품목", true, GridColDataType_emu.VarChar,
                                               130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME", "품목 명", true, GridColDataType_emu.VarChar,
                                               130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO", "LOT번호", true, GridColDataType_emu.VarChar,
                                               130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "WHCODE", "창고", true, GridColDataType_emu.VarChar,
                                               130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY", "재고수량", true, GridColDataType_emu.VarChar,
                                               130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE", "단위", true, GridColDataType_emu.VarChar,
                                               130, 130, Infragistics.Win.HAlign.Left, true, true);
                //_GridUtil.InitColumnUltraGrid(grid1, "CUSTCODE", "거래처", true, GridColDataType_emu.VarChar,
                                               //130, 130, Infragistics.Win.HAlign.Left, true, true);
                //_GridUtil.InitColumnUltraGrid(grid1, "CUSTNAME", "거래처명", true, GridColDataType_emu.VarChar,
                                               //130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKER", "생성자", true, GridColDataType_emu.VarChar,
                                               130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE", "생성일시", true, GridColDataType_emu.VarChar,
                                               130, 130, Infragistics.Win.HAlign.Left, true, true);
            
                //셋팅 내역 그리드와 바인딩
                _GridUtil.SetInitUltraGridBind(grid1); //셋팅 내역 그리드와 바인딩

                Common _Common = new Common();
                DataTable dtTemp = new DataTable();

                dtTemp = _Common.Standard_CODE("WHCODE");
                // 데이터 테이블에 있는 데이터를 해당 콤보박스에 추가
                Common.FillComboboxMaster(this.cboPlantCode_H, dtTemp,
                                        dtTemp.Columns["CODE_ID"].ColumnName,
                                        dtTemp.Columns["CODE_NAME"].ColumnName,
                                        "ALL","");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", dtTemp, "CODE_ID", "CODE_NAME");


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
                string sPlantCode = cboPlantCode_H.Value.ToString();
                string sItemCode = txtItemCode_H.Text.ToString();
                string sItemName = txtItemName_H.Text.ToString();


                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("03MM_StockMM_S1", CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE", sPlantCode, DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ITEMCODE", sItemCode, DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ITEMNAME", sItemName, DbType.String, ParameterDirection.Input));
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
            base.DoNew();

        }
        public override void DoDelete()
        {
           base.DoDelete();

        }
        public override void DoSave()
        {
           base.DoSave();
           
        }

        private void ultraButton1_Click(object sender, EventArgs e)
        {
            //바코드 발행
            if (grid1.ActiveRow == null) return; //선택된 행이 없을 경우 종료
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
