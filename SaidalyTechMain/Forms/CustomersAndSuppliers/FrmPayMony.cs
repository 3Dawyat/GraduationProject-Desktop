using DevExpress.XtraEditors;
using FluentDesignForm.Properties;
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

namespace SaidalyTechMain.Forms.CustomersAndSuppliers
{
    public partial class FrmPayMony : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        IService<TbSafes> _Safes = StartUp<IService<TbSafes>>.Services();
        IService<TbCustomers> _Customers = StartUp<IService<TbCustomers>>.Services();
        IService<TbSuppliers> _Suppliers = StartUp<IService<TbSuppliers>>.Services();
        IService<TbTransactionTypes> _TransactionTypes = StartUp<IService<TbTransactionTypes>>.Services();
        IService<TbTransaction> _Transaction = StartUp<IService<TbTransaction>>.Services();
        IService<TbShifts> _Shifts = StartUp<IService<TbShifts>>.Services();
        IService<VwSuppliersBalance> _SupplierBalance = StartUp<IService<VwSuppliersBalance>>.Services();
        IService<VwCustomersBalance> _CustomerBalance = StartUp<IService<VwCustomersBalance>>.Services();
        TbTransaction CurentTransaction = new TbTransaction();
        SharedFunctions shared = new SharedFunctions();

        List<VwCustomersBalance> lstCustomerBalance = new List<VwCustomersBalance>();
        List<VwSuppliersBalance> lstSupplierBalance = new List<VwSuppliersBalance>();
        List<TbSafes> ListSafes = new List<TbSafes>();
        List<TbCustomers> ListCustomers = new List<TbCustomers>();
        List<TbSuppliers> ListSuppliers = new List<TbSuppliers>();
        List<TbTransactionTypes> ListTransActioTypes = new List<TbTransactionTypes>();
        static string CastOrSupName;

        bool isSupplier = false;
        int TranssctionId = 0;

        public FrmPayMony(bool IsSupplier)
        {
            InitializeComponent();
            isSupplier = IsSupplier;
        }
        public FrmPayMony(bool IsSupplier, int transactionId)
        {
            InitializeComponent();
            isSupplier = IsSupplier;
            TranssctionId = transactionId;


        }
        async Task<bool> TestOpenShift()
        {
            bool retu = false;
            TbShifts shift = await _Shifts.GetObjectBy(a => a.DeviceName == Environment.MachineName && a.CloseDateTime == null && a.CloseUserId == null);
            if (shift == null)
            {
                DialogResult dialogResult = MessageBox.Show("يجب فتح فتره أولا هل تريد فتح فتره ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);

                if (dialogResult == DialogResult.Yes)
                {
                    TbShifts newShift = new TbShifts();
                    newShift.DeviceName = Environment.MachineName;
                    newShift.OpenUserId = Settings.Default.UserId;
                    newShift.OpenDateTime = Convert.ToDateTime(TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Egypt Standard Time")).ToString("dd-MM-yyyy hh:mm tt"));

                    var result = XtraInputBox.Show("ادخل رصيد افتتاح الفتره", "رصيد افتتاح الفتره", "");
                    decimal mony;
                    if (decimal.TryParse(result.Trim(), out mony))
                    {
                        newShift.OpeningBalance = mony;

                        if (await _Shifts.Add(newShift))
                        {

                            Settings.Default.ShiftId = newShift.Id;
                            Settings.Default.Save();
                            retu = true;
                        }
                    }
                    else
                        MessageBox.Show($"ادخل قيم صحيحه", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                Settings.Default.ShiftId = shift.Id;
                Settings.Default.Save();
                retu = true;
            }

            return retu;
        }
        private async void fillData()
        {
            if (isSupplier)
            {
                simpleLabelItem1.Text = "تسديد الى مورد";
                TextCustomer.Text = "المورد";
                ListSuppliers = await _Suppliers.GetAll();
                lstSupplierBalance = await _SupplierBalance.GetAll();
                lpeCustomersOrSuppliers.Properties.DataSource = ListSuppliers.Where(s => s.IsActive == true).Select(s => new { s.Id, s.Name }).ToList();
                lpeCustomersOrSuppliers.Properties.ValueMember = "Id";
                lpeCustomersOrSuppliers.Properties.DisplayMember = "Name";

            }
            else
            {
                simpleLabelItem1.Text = "تحصيل من عميل";
                ListCustomers = await _Customers.GetAll();
                lstCustomerBalance = await _CustomerBalance.GetAll();
                lpeCustomersOrSuppliers.Properties.DataSource = ListCustomers.Where(c => c.IsActive == true).Select(s => new { s.Id, s.Name }).ToList();
                lpeCustomersOrSuppliers.Properties.ValueMember = "Id";
                lpeCustomersOrSuppliers.Properties.DisplayMember = "Name";
            }
            ListSafes = await _Safes.GetListBy(s => s.IsActive == true);
            lpeSafe.Properties.DataSource = ListSafes.Select(s => new { s.Id, s.Name }).ToList();
            lpeSafe.Properties.ValueMember = "Id";
            lpeSafe.Properties.DisplayMember = "Name";


            ListTransActioTypes = await _TransactionTypes.GetAll();
            lpeTransactionType.Properties.DataSource = ListTransActioTypes.Select(s => new { s.Id, s.Name }).ToList();
            lpeTransactionType.Properties.ValueMember = "Id";
            lpeTransactionType.Properties.DisplayMember = "Name";

            if (TranssctionId == 0)
            {
                DtpDate.DateTime = DateTime.Now;
                NumValue.Value = 0;
                TextNote.Text = "";
                lpeCustomersOrSuppliers.EditValue = null;
                lpeTransactionType.EditValue = 3;
            }
            else
            {
                CurentTransaction = await _Transaction.GetObjectBy(t => t.Id == TranssctionId);
                DtpDate.DateTime = (DateTime)CurentTransaction.Date;
                NumValue.Value = (decimal)CurentTransaction.Qty;
                lpeSafe.EditValue = CurentTransaction.SafeId;
                TextNote.Text = CurentTransaction.note;
                lpeCustomersOrSuppliers.EditValue = CurentTransaction.CustId != null ? CurentTransaction.CustId : CurentTransaction.SupplierId;
                lpeTransactionType.EditValue = 3;
                btnPay.Text = "تعديل";
            }
        }
        private async void btnPay_Click(object sender, EventArgs e)
        {
            if (lpeTransactionType.EditValue != null && lpeCustomersOrSuppliers.EditValue != null && DtpDate.EditValue != null && NumValue.Value > 0 && lpeSafe.EditValue != null)
            {
                if (TranssctionId != 0)
                {
                    if (MessageBox.Show("هل انت متاكد من تعديل المعامله ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {

                        if (isSupplier)
                        {

                            CurentTransaction.SupplierId = Convert.ToInt32(lpeCustomersOrSuppliers.EditValue);
                            CurentTransaction.Date = DtpDate.DateTime;
                            CurentTransaction.Qty = NumValue.Value;
                            CurentTransaction.IsIncomming = false;
                            CurentTransaction.SafeId = Convert.ToInt32(lpeSafe.EditValue);
                            CurentTransaction.TransactionTypeId = Convert.ToInt32(lpeTransactionType.EditValue);
                            CurentTransaction.note = TextNote.Text;
                        }

                        else
                        {
                            CurentTransaction.CustId = Convert.ToInt32(lpeCustomersOrSuppliers.EditValue);
                            CurentTransaction.Date = DtpDate.DateTime;
                            CurentTransaction.Qty = NumValue.Value;
                            CurentTransaction.IsIncomming = true;
                            CurentTransaction.SafeId = Convert.ToInt32(lpeSafe.EditValue);
                            CurentTransaction.TransactionTypeId = Convert.ToInt32(lpeTransactionType.EditValue);
                            CurentTransaction.note = TextNote.Text;


                        }

                        if (await _Transaction.Edit(CurentTransaction))
                        {


                            if (isSupplier)
                            {
                                var suppllier = await _SupplierBalance.GetObjectBy(a => a.SupplierId == Convert.ToInt32(lpeCustomersOrSuppliers.EditValue));

                                Print("تسديد الي مورد", CurentTransaction.Id, CastOrSupName, (decimal)CurentTransaction.Qty, (decimal)suppllier.balance, CurentTransaction.note);
                            }
                            else
                            {
                                var Customer = await _CustomerBalance.GetObjectBy(a => a.CustomerId == Convert.ToInt32(lpeCustomersOrSuppliers.EditValue));
                                Print("تحصيل من عميل", CurentTransaction.Id, CastOrSupName, (decimal)CurentTransaction.Qty, (decimal)Customer.balance, CurentTransaction.note);

                            }
                            MessageBox.Show("تم اضافة المعامله بنجاح");
                            CastOrSupName = "";
                            fillData();

                        }
                        else
                        {
                            MessageBox.Show(" حدث مشكله اثناء اضافة المعامله");

                        }
                    }

                }
                else
                {
                    if (MessageBox.Show("هل انت متاكد من اضافة المعامله؟", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        TbTransaction transaction;
                        if (isSupplier)
                        {
                            transaction = new TbTransaction()
                            {
                                SupplierId = Convert.ToInt32(lpeCustomersOrSuppliers.EditValue),
                                Date = DtpDate.DateTime,
                                Qty = NumValue.Value,
                                IsIncomming = false,
                                SafeId = Convert.ToInt32(lpeSafe.EditValue),
                                TransactionTypeId = Convert.ToInt32(lpeTransactionType.EditValue),
                                UserId = Settings.Default.UserId,
                                note = TextNote.Text,
                                SheftId = Settings.Default.ShiftId,
                            };

                        }
                        else
                        {
                            transaction = new TbTransaction()
                            {
                                CustId = Convert.ToInt32(lpeCustomersOrSuppliers.EditValue),
                                Date = DtpDate.DateTime,
                                Qty = NumValue.Value,
                                IsIncomming = true,
                                SafeId = Convert.ToInt32(lpeSafe.EditValue),
                                TransactionTypeId = Convert.ToInt32(lpeTransactionType.EditValue),
                                UserId = Properties.Settings.Default.UserId,
                                note = TextNote.Text,
                                SheftId = Properties.Settings.Default.ShiftId,
                            };


                        }

                        if (await _Transaction.Add(transaction))
                        {


                            if (isSupplier)
                            {
                                var suppllier = await _SupplierBalance.GetObjectBy(a => a.SupplierId == Convert.ToInt32(lpeCustomersOrSuppliers.EditValue));

                                Print("تسديد الي مورد", transaction.Id, CastOrSupName, (decimal)transaction.Qty, (decimal)suppllier.balance, transaction.note);
                            }
                            else
                            {
                                var Customer = await _CustomerBalance.GetObjectBy(a => a.CustomerId == Convert.ToInt32(lpeCustomersOrSuppliers.EditValue));
                                Print("تحصيل من عميل", transaction.Id, CastOrSupName, (decimal)transaction.Qty, (decimal)Customer.balance, transaction.note);

                            }
                            MessageBox.Show("تم اضافة المعامله بنجاح");
                            CastOrSupName = "";
                            fillData();

                        }
                        else
                        {
                            MessageBox.Show(" حدث مشكله اثناء اضافة المعامله");

                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("تاكد من اكمال البيانات");
            }


        }
        private void Print(string Title, int Id, string Name, decimal PayQty, decimal Remain, string Notes)
        {

            //ReportDocument report = new ReportDocument();
            //FileServices fileServices = new FileServices();
            //report.Load(fileServices.GetReportFile("CryPayment.rpt"));
            //FrmCryPreview frm = new FrmCryPreview();
            //report.SetParameterValue("Title", Title);
            //report.SetParameterValue("Id", Id);
            //report.SetParameterValue("Name", Name);
            //report.SetParameterValue("Payed", PayQty);
            //report.SetParameterValue("Remain", Remain);
            //report.SetParameterValue("Notes", Notes);
            //report.SetParameterValue("User", Settings.Default.UserName);
            //if (shared.Question(MessageBox.Show("معاينه قبل الطباعه ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)))
            //{
            //    report.PrintToPrinter(1, true, 1, 0);
            //    frm.crystalReportViewer1.ReportSource = report;
            //    frm.Show();
            //}
            //else
            //{
            //    report.PrintToPrinter(1, true, 1, 0);
            //}

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private async void FrmPayMony_Load(object sender, EventArgs e)
        {

            fillData();
            if (!await TestOpenShift())
                this.Close();
        }

        private void lpeCustomersOrSuppliers_EditValueChanged(object sender, EventArgs e)
        {
            if (lpeCustomersOrSuppliers.EditValue != null)
            {
                CastOrSupName = lpeCustomersOrSuppliers.Text;
                if (isSupplier)
                {

                    var suppllier = lstSupplierBalance.Where(s => s.SupplierId == Convert.ToInt32(lpeCustomersOrSuppliers.EditValue)).FirstOrDefault();
                    if (suppllier != null)
                        lableBalance.Text = Convert.ToString(suppllier.balance);
                    else
                        lableBalance.Text = "0";
                }
                else
                {
                    var Customer = lstCustomerBalance.Where(s => s.CustomerId == Convert.ToInt32(lpeCustomersOrSuppliers.EditValue)).FirstOrDefault();
                    if (Customer != null)
                        lableBalance.Text = Convert.ToString(Customer.balance);
                    else
                        lableBalance.Text = "0";
                }
            }
        }
    }
}