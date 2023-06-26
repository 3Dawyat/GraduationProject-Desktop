using DevExpress.XtraEditors;
using Newtonsoft.Json;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.Consts;
using SaidalyTechMain.DB_Models;
using SaidalyTechMain.Properties;
using SaidalyTechMain.Shared;
using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaidalyTechMain.Forms.MainForms
{
    public partial class FrmLogin : XtraForm
    {
        private readonly IService<VwDesktopUsers> _users;
        private readonly SharedFunctions _shared;
        public FrmLogin()
        {
            _users = StartUp<IService<VwDesktopUsers>>.Services();
            _shared = new SharedFunctions();
            InitializeComponent();
        }
        [DllImport("user32.dll")]
        public static extern uint SetWindowDisplayAffinity(IntPtr hwnd, uint dwAffinity);
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            const uint WDA_MONITOR = 1;
            SetWindowDisplayAffinity(this.Handle, WDA_MONITOR);
        }



        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                var user = await _users.GetObjectBy(a => a.Email == textEmail.Text && a.Pass == textPassword.Text);
                if (user != null)
                {
                    Settings.Default.UserId = user.Id;
                    Settings.Default.UserName = user.FullName;
                    Settings.Default.JopId = user.RoleId;
                    Settings.Default.Save();
                    Hide();
                    _shared.OpenForm(new FrmMain(user.RoleId));

                }
                else
                {
                    MessageBox.Show("البيانات المدخله غير صحيحه", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private bool ValidateInputs() =>
            (!string.IsNullOrEmpty(textEmail.Text) && !string.IsNullOrEmpty(textEmail.Text));

        private async void ForgotPasswordLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (ValidateInputs())
            {
                var user = await _users.GetObjectBy(a => a.Email == textEmail.Text);
                if (user != null)
                {
                    var url = WebApp.BaseUrl + WebApp.ResetMyPassword;
                    var data = new { email = textEmail.Text };
                    var result = await _shared.CallApi(data, url);
                    if (result)
                    {
                        MessageBox.Show("تم ارسال كلمة السر الجديده الي البريد المدخل", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("حدث خطأ أثناء ارسال الكود الخاص بك الي الايميل المدخل", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

    }
}