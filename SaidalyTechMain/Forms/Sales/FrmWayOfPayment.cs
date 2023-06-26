using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaidalyTechMain.Forms.Sales
{
    public partial class FrmWayOfPayment : DevExpress.XtraEditors.XtraForm
    {
        public static decimal total;
        public static decimal cash;
        public static decimal discount;
        public static bool Chek = false;
        public FrmWayOfPayment()
        {
            InitializeComponent();
        }

        private void FrmWayOfPayment_Load(object sender, EventArgs e)
        {
            cash = 0;
            discount = 0;
            Chek = false;
            lplTotal.Text = total.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Chek = true;
            cash = Convert.ToDecimal(textCash.Text);
            discount = Convert.ToDecimal(textDiscount.Text);

            total = 0;
            Close();
        }

        private void textDiscount_Leave(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = string.IsNullOrEmpty(textBox.Text) ? "0" : textBox.Text;
        }

        private void textCash_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);

        }

        private void textDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(Char.IsNumber(e.KeyChar) || e.KeyChar == 8);

        }
    }
}