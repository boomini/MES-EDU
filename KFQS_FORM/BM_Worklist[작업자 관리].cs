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
    public partial class BM_Worklist : DC00_WinForm.BaseMDIChildForm
    {
        //그리드 셋팅 할 수 있도록 도와주는 함수 클래스
        UltraGridUtil _GridUtill = new UltraGridUtil();
        //공장 변수 입력
        //private sPlantCode = LoginInfo
        public BM_Worklist()
        {
            InitializeComponent();
        }

        private void BM_Worklist_Load(object sender, EventArgs e)
        {   // 그리드 셋팅하고 시작한다.
            try
            {
                _GridUtill.InitializeGrid(this.grid1, false, true, false, "", false); //그리드1 의 기본 설정 내용
                // PLANTCODE값을 보여줄때는 공장으로, null값 허용, varchar형식, 130,130, 문자열은 왼쪽 정렬, 보여주고 수정은X)
                _GridUtill.InitColumnUltraGrid(grid1, "PLANTCODE", "공장", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKERID", "작업자 ID", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "WORKERNAME", "작업자 명", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "BANCODE", "작업 반", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "GRPID", "그룹", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "DEPTCODE", "부서", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "PHONE", "연락처", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "INDATE", "입사일", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "OUTDATE", "퇴사일", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "USEFLAG", "사용 여부", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, true);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKEDATE", "등록일시", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "MAKER", "등록자", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "EDITDATE", "수정일시", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);
                _GridUtill.InitColumnUltraGrid(grid1, "EDITTOR", "수정자", true, GridColDataType_emu.VarChar, 130, 130, Infragistics.Win.HAlign.Left, true, false);

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

                dtTemp = _Common.Standard_CODE("BANCODE");
                Common.FillComboboxMaster(this.cboBanCode_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "BANCODE", dtTemp, "CODE_ID", "CODE_NAME");

                dtTemp = _Common.Standard_CODE("USEFLAG");
                Common.FillComboboxMaster(this.cboUseFlag_H, dtTemp, dtTemp.Columns["CODE_ID"].ColumnName, dtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "USEFLAG", dtTemp, "CODE_ID", "CODE_NAME");

                //부서 콤보 그리드뷰
                dtTemp = _Common.Standard_CODE("DEPTCODE");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "DEPTCODE", dtTemp, "CODE_ID", "CODE_NAME");
                //그룹 콤보 그리드뷰
                dtTemp = _Common.Standard_CODE("GRPID");
                UltraGridUtil.SetComboUltraGrid(this.grid1, "GRPID", dtTemp, "CODE_ID", "CODE_NAME");
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
                string sWorkerid = txtWokerID_H.Text.ToString();
                string sWorkerName = txtWorkerName_H.Text.ToString();
                string sBancode = cboBanCode_H.Value.ToString();
                string sUseflag = cboUseFlag_H.Value.ToString();

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("19BM_WorkList_S1", CommandType.StoredProcedure
                                          , helper.CreateParameter("PLANTCODE", sPlantcode, DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("WORKERID", sWorkerid, DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("WORKERNAME", sWorkerName, DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("BANCODE", sBancode, DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("USEFLAG", sUseflag, DbType.String, ParameterDirection.Input));
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

            this.grid1.ActiveRow.Cells["PLANTCODE"].Value = "1000";
            this.grid1.ActiveRow.Cells["GRPID"].Value = "SW";
            this.grid1.ActiveRow.Cells["USEFLAG"].Value = "Y";
            this.grid1.ActiveRow.Cells["INDATE"].Value = DateTime.Now.ToString("yyyy-MM-dd");

            grid1.ActiveRow.Cells["MAKER"].Activation = Activation.NoEdit;
            grid1.ActiveRow.Cells["MAKEDATE"].Activation = Activation.NoEdit;
            grid1.ActiveRow.Cells["EDITDATE"].Activation = Activation.NoEdit;
            grid1.ActiveRow.Cells["EDITOR"].Activation = Activation.NoEdit;
        }
        public override void DoDelete()
        {
            base.DoDelete();
            this.grid1.DeleteRow();
        }
        public override void DoSave()
        {
            base.DoSave();
            DataTable dtTemp = new DataTable();
            dtTemp = grid1.chkChange();
            if (dtTemp.Rows.Count == 0) return;
            DBHelper helper = new DBHelper("", true);
            try
            {
                if (ShowDialog("해당 사항을 저장 하시겠습니까?", DC00_WinForm.DialogForm.DialogType.YESNO) == System.Windows.Forms.DialogResult.Cancel) 
                { return; }

                foreach (DataRow drrow in dtTemp.Rows)
                {
                    

                    switch (drrow.RowState)
                    {
                        case DataRowState.Deleted:
                            drrow.RejectChanges();//삭제된 줄을 복원시켜라
                            helper.ExecuteNoneQuery("19BM_WorkList_D1", CommandType.StoredProcedure, helper.CreateParameter("PLANTCODE", Convert.ToString(drrow["PLANTCODE"]), DbType.String, ParameterDirection.Input)
                                                                                                   , helper.CreateParameter("WORKERID", Convert.ToString(drrow["WORKERID"]), DbType.String, ParameterDirection.Input));
                            break;

                            if (Convert.ToString(drrow["WORKERID"]) == string.Empty)// 아이디 안넣으면 불가
                            {
                                this.ClosePrgForm();
                                this.ShowDialog("작업자 ID를 입력하세요.", DC00_WinForm.DialogForm.DialogType.OK);
                                return;
                            }

                        case DataRowState.Added:
                            helper.ExecuteNoneQuery("19BM_WorkList_I1"
                                                    , CommandType.StoredProcedure
                                                    , helper.CreateParameter("PLANTCODE"  , Convert.ToString(drrow["PLANTCODE"])  , DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("WORKERID"   , Convert.ToString(drrow["WORKERID"])   , DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("WORKERNAME" , Convert.ToString(drrow["WORKERNAME"]) , DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("GRPID"      , Convert.ToString(drrow["GRPID"])      , DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("DEPTCODE"   , Convert.ToString(drrow["DEPTCODE"])   , DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("BANCODE"    , Convert.ToString(drrow["BANCODE"])    , DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("USEFLAG"    , Convert.ToString(drrow["USEFLAG"])    , DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("PHONENO"    , Convert.ToString(drrow["PHONENO"])    , DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("INDATE"     , Convert.ToString(drrow["INDATE"])     , DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("OUTDATE"    , Convert.ToString(drrow["OUTDATE"])    , DbType.String, ParameterDirection.Input)
                                                    , helper.CreateParameter("MAKER"      , LoginInfo.UserID                      , DbType.String, ParameterDirection.Input));
                            break;
                        case DataRowState.Modified:
                            helper.ExecuteNoneQuery("19BM_WorkList_U1"
                                                   , CommandType.StoredProcedure
                                                   , helper.CreateParameter("PLANTCODE"  , Convert.ToString(drrow["PLANTCODE"])  , DbType.String, ParameterDirection.Input)
                                                   , helper.CreateParameter("WORKERID"   , Convert.ToString(drrow["WORKERID"])   , DbType.String, ParameterDirection.Input)
                                                   , helper.CreateParameter("WORKERNAME" , Convert.ToString(drrow["WORKERNAME"]) , DbType.String, ParameterDirection.Input)
                                                   , helper.CreateParameter("GRPID"      , Convert.ToString(drrow["GRPID"])      , DbType.String, ParameterDirection.Input)
                                                   , helper.CreateParameter("DEPTCODE"   , Convert.ToString(drrow["DEPTCODE"])   , DbType.String, ParameterDirection.Input)
                                                   , helper.CreateParameter("BANCODE"    , Convert.ToString(drrow["BANCODE"])    , DbType.String, ParameterDirection.Input)
                                                   , helper.CreateParameter("USEFLAG"    , Convert.ToString(drrow["USEFLAG"])    , DbType.String, ParameterDirection.Input)
                                                   , helper.CreateParameter("PHONENO"    , Convert.ToString(drrow["PHONENO"])    , DbType.String, ParameterDirection.Input)
                                                   , helper.CreateParameter("INDATE"     , Convert.ToString(drrow["INDATE"])     , DbType.String, ParameterDirection.Input)
                                                   , helper.CreateParameter("OUTDATE"    , Convert.ToString(drrow["OUTDATE"])    , DbType.String, ParameterDirection.Input)
                                                   , helper.CreateParameter("EDITOR"     , LoginInfo.UserID                      , DbType.String, ParameterDirection.Input));


                            break;
                    }
                }
                if (helper.RSCODE=="S")
                {
                    helper.Commit();
                    this.ShowDialog("정상적으로 등록 되었습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                    DoInquire();
                }
            }
            catch (Exception ex)
            {
                helper.Rollback();
            }
            finally
            {
                helper.Close();
            }

        }
    }
}
