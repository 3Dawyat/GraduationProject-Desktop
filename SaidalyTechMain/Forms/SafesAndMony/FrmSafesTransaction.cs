using DevExpress.XtraEditors;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.DB_Models;
using SaidalyTechMain.Forms.MainForms;
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

namespace SaidalyTechMain.Forms.SafesAndMony
{
    public partial class FrmSafesTransaction : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        IService<TbSafes> _Safe = StartUp<IService<TbSafes>>.Services();
        IService<TbStockTransactions> _StockTransaction = StartUp<IService<TbStockTransactions>>.Services();
        IService<VwSafesBalance> _SafesBalance = StartUp<IService<VwSafesBalance>>.Services();
        List<TbSafes> AllSafes = new List<TbSafes>();
        List<VwSafesBalance> AllSafesBalance = new List<VwSafesBalance>();
        TbStockTransactions CurentOperation = new TbStockTransactions();
        int Id;


        public FrmSafesTransaction()
        {
            InitializeComponent();
        }


        private async void FrmSafesTransaction_Load(object sender, EventArgs e)
        {
            await LoadData();
        }
        private void lpeStockFrom_EditValueChanged(object sender, EventArgs e)
        {
            if (lpeStockFrom.EditValue != null)
            {
                try
                {
                    labelBalanceFrom.Text = AllSafesBalance.FirstOrDefault(a => a.Id == Convert.ToInt32(lpeStockFrom.EditValue)).Balance.ToString();
                }
                catch
                {
                    labelBalanceFrom.Text = " 0";
                }
            }
            else
                labelBalanceFrom.Text = " ";
        }

        private void lpeStockTo_EditValueChanged(object sender, EventArgs e)
        {
            if (lpeStockTo.EditValue != null)
            {
                try
                {

                    labelBalanceTo.Text = AllSafesBalance.FirstOrDefault(a => a.Id == Convert.ToInt32(lpeStockTo.EditValue)).Balance.ToString();
                }
                catch
                {
                    labelBalanceTo.Text = " 0";
                }

                
            }
            else
                labelBalanceTo.Text = " ";
        }

        private void textMony_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);

        }

        async Task LoadData()
        {



            AllSafesBalance = await _SafesBalance.GetAll();
            AllSafes = await _Safe.GetListBy(a => a.IsActive == true);

            lpeStockFrom.Properties.DataSource = AllSafes.Select(a => new { a.Id, a.Name });
            lpeStockFrom.Properties.DisplayMember = "Name";
            lpeStockFrom.Properties.ValueMember = "Id";

            lpeStockTo.Properties.DataSource = AllSafes.Select(a => new { a.Id, a.Name });
            lpeStockTo.Properties.DisplayMember = "Name";
            lpeStockTo.Properties.ValueMember = "Id";
            dtpDate.DateTime = DateTime.UtcNow.ToLocalTime();
            if (Id != 0)
            {
                CurentOperation = await _StockTransaction.GetObjectBy(a => a.Id == Id);
                dtpDate.DateTime = (DateTime)CurentOperation.Date;
                textMony.Text = CurentOperation.Qty.ToString();
                lpeStockFrom.EditValue = CurentOperation.SafeFromId;
                lpeStockFrom.SelectedText = AllSafes.FirstOrDefault(a => a.Id == CurentOperation.SafeFromId).Name;
                lpeStockTo.EditValue = CurentOperation.SafeToId;
                lpeStockTo.SelectedText = AllSafes.FirstOrDefault(a => a.Id == CurentOperation.SafeToId).Name;
                textNote.Text = CurentOperation.Note;
            }



        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (TestInput())
            {
                if (Id != 0)
                {
                    var safe = AllSafesBalance.FirstOrDefault(a => a.Id == Convert.ToInt32(lpeStockFrom.EditValue));
                    if (safe.Balance >= Convert.ToDecimal(textMony.Text))
                    {
                        CurentOperation.Date = dtpDate.DateTime;
                        CurentOperation.Qty = Convert.ToDecimal(textMony.Text);
                        CurentOperation.SafeFromId = Convert.ToInt32(lpeStockFrom.EditValue);
                        CurentOperation.SafeToId = Convert.ToInt32(lpeStockTo.EditValue);
                        CurentOperation.Note = textNote.Text;
                        if (!await _StockTransaction.Edit(CurentOperation))
                        {
                            MessageBox.Show("حدث خطأ أثناء حفظ العمليه برجاء المحاوله لاحقا", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            MessageBox.Show("تم التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("المبلغ المحدد أكبر من رصيد الخزنه", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                }
                else
                {

                    var safe = AllSafesBalance.FirstOrDefault(a => a.Id == Convert.ToInt32(lpeStockFrom.EditValue));
                    if (safe.Balance >= Convert.ToDecimal(textMony.Text))
                    {
                        if (!await _StockTransaction.Add(new TbStockTransactions
                        {
                            Date = dtpDate.DateTime,
                            Qty = Convert.ToDecimal(textMony.Text),
                            SafeFromId = Convert.ToInt32(lpeStockFrom.EditValue),
                            SafeToId = Convert.ToInt32(lpeStockTo.EditValue),
                            UserId = Settings.Default.UserId,
                            Note = textNote.Text,
                        }))
                            MessageBox.Show("حدث خطأ أثناء اضافة المعامله", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                        else
                        {
                            textMony.Text = "";
                            textNote.Text = "";
                            await LoadData();
                            MessageBox.Show("تم الأضافة المعامله بنجاح !", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("المبلغ المحدد أكبر من رصيد الخزنه", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }

                }
            }
            else
            {
                MessageBox.Show("تأكد من صحة البيانات المدخله واختيار خزنتين مختلفتين", "", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        bool TestInput() => ((lpeStockFrom.EditValue != null && lpeStockTo.EditValue != null) && (Convert.ToInt32(lpeStockFrom.EditValue) != Convert.ToInt32(lpeStockTo.EditValue)) && !string.IsNullOrEmpty(textMony.Text));

    }
}