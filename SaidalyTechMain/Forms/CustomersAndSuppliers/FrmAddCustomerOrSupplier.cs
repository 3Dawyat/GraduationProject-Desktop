using DevExpress.XtraEditors;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.DB_Models;
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
    public partial class FrmAddCustomerOrSupplier : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        IService<TbCustomers> _Customer = StartUp<IService<TbCustomers>>.Services();
        IService<TbSuppliers> _Supplier = StartUp<IService<TbSuppliers>>.Services();
        TbCustomers commingCustomer;
        TbSuppliers commingSupplier;
        SharedFunctions shared = new SharedFunctions();
        int id = 0;
        bool IsSupplier;
        public FrmAddCustomerOrSupplier(int Id, bool isSupplier)
        {
            InitializeComponent();
            this.IsSupplier = isSupplier;
            id = Id;
            FillWithSupplierOrCustomer();
        }
        public FrmAddCustomerOrSupplier(bool isSupplier)
        {
            this.IsSupplier = isSupplier;
            InitializeComponent();
        }

        private async void FillWithSupplierOrCustomer()
        {
            if (this.IsSupplier)
            {
                commingSupplier = await _Supplier.GetObjectBy(s => s.Id ==Convert.ToInt32( id));

                textAdress.Text = commingSupplier.Address;
                textName.Text = commingSupplier.Name;
                textNote.Text = commingSupplier.Note;
                textPhone.Text = commingSupplier.Phone;

            }
            else
            {
                commingCustomer = await _Customer.GetObjectBy(s => s.Id == id);
                textCreditLimit.Text = commingCustomer.CreditLimit.ToString();
                textAdress.Text = commingCustomer.Address;
                textName.Text = commingCustomer.Name;
                textNote.Text = commingCustomer.Note;
                textPhone.Text = commingCustomer.Phone;
            }
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textName.Text.Trim()))
            {
                if (this.IsSupplier)
                {
                    TbSuppliers supplier = new TbSuppliers
                    {
                        Id = Convert.ToInt32(id),
                        Address = textAdress.Text.Trim(),
                        Name = textName.Text.Trim(),
                        Phone = textPhone.Text.Trim(),
                        Note = textNote.Text.Trim(),
                        IsActive = true
                    };
                    if (Convert.ToInt32(id)>0)
                    {
                        if (await _Supplier.Edit(supplier))
                        {
                            MessageBox.Show("تم تعديل المورد بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                        }
                    }
                    else 
                    {
                        if (await _Supplier.Add(supplier))
                        {
                            MessageBox.Show("تم اضافه المورد بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                        }
                    }
                }
                else
                {
                    TbCustomers cust = new TbCustomers
                    {
                        Id = id,
                        Address = textAdress.Text.Trim(),
                        Name = textName.Text.Trim(),
                        Note = textNote.Text.Trim(),
                        IsActive = true,
                        Phone = string.IsNullOrEmpty(textPhone.Text.Trim()) ? "0" : textPhone.Text.Trim()
                    };
                    decimal creditLimit;
                    cust.CreditLimit = decimal.TryParse(textCreditLimit.Text.Trim(), out creditLimit) ? creditLimit : 0;
                    if ( id>0)
                    {
                        if (await _Customer.Edit(cust))
                        {
                            MessageBox.Show("تم تعديل العميل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Close();
                        }
                    }
                    else
                    {
                        if (!await _Customer.Any(a => a.Name == cust.Name))
                        {
                            if (await _Customer.Add(cust))
                            {
                                MessageBox.Show("تم اضافه العميل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("هذا العميل موجود بالفعل", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                        }

                    }
                }
            }
            else
                MessageBox.Show("برجاء اكمال البيانات الأساسيه و التاكد من ان لديك صلاحيه", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textName.Select();
        }
        private void FrmAddCustomerOrSupplier_Load(object sender, EventArgs e)
        {
            if (this.IsSupplier)
            {
                layCreditLimit.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
            textName.Select();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar));
        }

        private void textPhone_Leave(object sender, EventArgs e)
        {
            textPhone.Text = string.IsNullOrEmpty(textPhone.Text) ? "0" : textPhone.Text;

        }
    }
}