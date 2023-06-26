using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.DB_Models;
using SaidalyTechMain.Properties;
using SaidalyTechMain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SaidalyTechMain.Forms.StoresAndItems
{
    public partial class FrmStoresTransfer : DevExpress.XtraEditors.XtraForm
    {

        IService<VwItemsWithUnits> _Items = StartUp<IService<VwItemsWithUnits>>.Services();
        IService<TbStores> _Stores = StartUp<IService<TbStores>>.Services();
        IService<VwEachStoreInventory> _StoreInventory = StartUp<IService<VwEachStoreInventory>>.Services();
        IService<TbStoresTransaction> _StoresTransaction = StartUp<IService<TbStoresTransaction>>.Services();
        IService<TbStoreTransactionDetails> _StoresTransactionDetails = StartUp<IService<TbStoreTransactionDetails>>.Services();
        RepositoryItemMemoEdit repoMemo = new RepositoryItemMemoEdit();
        List<VwItemsWithUnits> AllItems = new List<VwItemsWithUnits>();
        List<TbStores> AllStores = new List<TbStores>();
        TbStoresTransaction CurentOperation = new TbStoresTransaction();
        List<TbStoreTransactionDetails> listTransactionDetails = new List<TbStoreTransactionDetails>();
        ZiadDataSet Data = new ZiadDataSet();
        SharedFunctions shared = new SharedFunctions();
        int Id;

        public FrmStoresTransfer()
        {
            InitializeComponent();
        }

        private async void FrmStoresTransfer_Load(object sender, EventArgs e)
        {
            await LoadData();
        }
        async Task LoadData()
        {
            AllStores = await _Stores.GetAll();
            AllItems = await _Items.GetAll();

            listTransactionDetails = new List<TbStoreTransactionDetails>();
            Data.Tables["DtStoreTransfer"].Rows.Clear();
            GcData.DataSource = Data.Tables["DtStoreTransfer"];
            repoMemo.ReadOnly = true;

            lpeItems.Properties.DataSource = AllItems.Select(a => new { Id = a.ItemUnitId, Name = a.Name });
            lpeItems.Properties.DisplayMember = "Name";
            lpeItems.Properties.ValueMember = "Id";

            lpeTo.Properties.DataSource = AllStores.Select(a => new { a.Id, a.Name });
            lpeTo.Properties.DisplayMember = "Name";
            lpeTo.Properties.ValueMember = "Id";

            lpeFrom.Properties.DataSource = AllStores.Select(a => new { a.Id, a.Name });
            lpeFrom.Properties.DisplayMember = "Name";
            lpeFrom.Properties.ValueMember = "Id";

            textDate.DateTime = DateTime.UtcNow.ToLocalTime();
            if (Id != 0)
            {
                CurentOperation = await _StoresTransaction.GetObjectBy(a => a.Id == Id);
                textDate.DateTime = (DateTime)CurentOperation.Date;
                //  textQty.Text = CurentOperation.Qty.ToString();
                //lpeItems.EditValue = CurentOperation.ItemUnitId;
                // lpeItems.SelectedText = AllItems.FirstOrDefault(a => a.unitItemId == CurentOperation.ItemUnitId).Name;

                lpeFrom.EditValue = CurentOperation.IdStoreFrom;
                lpeFrom.SelectedText = AllStores.FirstOrDefault(a => a.Id == CurentOperation.IdStoreFrom).Name;
                lpeTo.EditValue = CurentOperation.IdStoreTo;
                lpeTo.SelectedText = AllStores.FirstOrDefault(a => a.Id == CurentOperation.IdStoreTo).Name;
                textNote.Text = CurentOperation.Note;
            }
        }

        private async Task<bool> FillTransactionDetails(int transactionId)
        {
            listTransactionDetails = new List<TbStoreTransactionDetails>();

            for (int i = 0; i < GvData.RowCount; i++)
            {
                listTransactionDetails.Add(new TbStoreTransactionDetails()
                {
                    Id = 0,
                    ItemUnitId = Convert.ToInt32(GvData.GetRowCellValue(i, GvData.Columns[0].FieldName)),
                    TransactionId = transactionId,
                    Qty = Convert.ToDecimal(GvData.GetRowCellValue(i, GvData.Columns[2].FieldName)),
                });

            }

            return await _StoresTransactionDetails.AddRange(listTransactionDetails);
        }
        bool TestInput() => (lpeItems.EditValue != null &&
           (lpeFrom.EditValue != null && lpeTo.EditValue != null) &&
           !string.IsNullOrEmpty(textQty.Text) &&
           (Convert.ToDecimal(textQty.Text) <= Convert.ToDecimal(Avilable1.Text)));

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (TestInput())
            {
                if (Convert.ToInt32(lpeFrom.EditValue) != Convert.ToInt32(lpeTo.EditValue))
                {
                    {
                        TbStoresTransaction StoresTransaction = new TbStoresTransaction
                        {
                            Date = textDate.DateTime,
                            IdStoreFrom = Convert.ToInt32(lpeFrom.EditValue),
                            IdStoreTo = Convert.ToInt32(lpeTo.EditValue),
                            userId = Settings.Default.UserId,
                            Note = textNote.Text,
                        };
                        if (await _StoresTransaction.Add(StoresTransaction))
                        {

                            if (await FillTransactionDetails(StoresTransaction.Id))
                            {
                                MessageBox.Show("تم الأضافة المعامله بنجاح !", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Close();
                            }
                            else
                                MessageBox.Show("حدث خطأ أثناء اضافة تفاصيل المعامله", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                            await LoadData();
                        }
                        else
                            MessageBox.Show("حدث خطأ أثناء اضافة المعامله", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);

                    }
                }
                else
                {
                    MessageBox.Show("تأكد من اختيار مخزنين مختلفين", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

            }
            else
            {
                MessageBox.Show("تأكد من صحة البيانات المدخله و انك تمتلك صلاحيه", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private async void lpeFrom_EditValueChanged(object sender, EventArgs e)
        {
            if (lpeItems.EditValue != null)
            {
                if (lpeFrom.EditValue != null)
                {
                    var item = await _StoreInventory.GetObjectBy(a => a.ItemUnitId == Convert.ToInt32(lpeItems.EditValue) && a.StoreId == Convert.ToInt32(lpeFrom.EditValue));

                    if (item != null)
                        Avilable1.Text = item.Qty.ToString();
                    else
                        Avilable1.Text = "0";
                }
                else
                    Avilable1.Text = "0";
            }
            else
                MessageBox.Show("برجاء اختيار صنف اولا", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);

        }

        private async void lpeTo_EditValueChanged(object sender, EventArgs e)
        {
            if (lpeItems.EditValue != null)
            {
                if (lpeTo.EditValue != null)
                {
                    var item = await _StoreInventory.GetObjectBy(a => a.ItemUnitId == Convert.ToInt32(lpeItems.EditValue) && a.StoreId == Convert.ToInt32(lpeTo.EditValue));

                    if (item != null)
                        Avilable2.Text = item.Qty.ToString();
                    else
                        Avilable2.Text = "0";
                }
                else
                    Avilable2.Text = "0";
            }
            else
                MessageBox.Show("برجاء اختيار صنف اولا", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);

        }

        private void lpeItems_EditValueChanged(object sender, EventArgs e)
        {
            lpeTo_EditValueChanged(null, null);
            lpeFrom_EditValueChanged(null, null);
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

            if (TestInput())
            {
                if (Convert.ToInt32(lpeFrom.EditValue) != Convert.ToInt32(lpeTo.EditValue))
                {
                    Data.Tables["DtStoreTransfer"].Rows.Add(Convert.ToInt32(lpeItems.EditValue), AllItems.Where(i => i.ItemUnitId == Convert.ToInt32(lpeItems.EditValue)).Select(a => a.Name).FirstOrDefault(), Convert.ToInt32(textQty.Text));
                }
                else
                {
                    MessageBox.Show("تأكد من اختيار مخزنين مختلفين", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }

                GvData.Columns[0].ColumnEdit = repoMemo;
                GvData.Columns[1].ColumnEdit = repoMemo;
                GvData.Columns[0].BestFit();
                GvData.Columns[1].BestFit();
                GvData.Columns[2].BestFit();
            }
            else
            {
                MessageBox.Show("تأكد من صحة البيانات المدخله", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }
}