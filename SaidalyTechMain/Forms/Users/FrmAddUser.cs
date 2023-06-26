using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Newtonsoft.Json;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.Consts;
using SaidalyTechMain.DB_Models;
using SaidalyTechMain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data.Helpers;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace SaidalyTechMain.Forms.Users
{
    public partial class FrmAddUser : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        private readonly IService<AspNetRoles> _roles = StartUp<IService<AspNetRoles>>.Services();
        private readonly IService<AspNetUserRoles> _userRoles = StartUp<IService<AspNetUserRoles>>.Services();
        private readonly IService<AspNetUsers> _user = StartUp<IService<AspNetUsers>>.Services();
        private readonly IService<VwDesktopUsers> _desktopUser = StartUp<IService<VwDesktopUsers>>.Services();
        private readonly SharedFunctions _shared = new SharedFunctions();
        string UserId;
        AspNetUsers userToEdit;
        public FrmAddUser()
        {
            InitializeComponent();
        }
        public FrmAddUser(string userId)
        {
            InitializeComponent();
            UserId = userId;
        }

        private async void FrmAddUser_Load(object sender, EventArgs e)
        {
            await LoadData();
        }
        async Task LoadData()
        {

            var genders = new List<string> { "Male", "Female" };
            lpeGender.Properties.DataSource = genders.Select(a => new { Gender = a }).ToList();
            lpeGender.Properties.DisplayMember = "Gender";
            lpeGender.Properties.ValueMember = "Gender";
            lpeGender.Text = "Select...";
            var roles = await _roles.GetListBy(a => !WebApp.AspRoles.Contains(a.Name));
            lpeJob.Properties.DataSource = roles.Select(a => new { a.Name }).ToList();
            lpeJob.Properties.ValueMember = "Name";
            lpeJob.Properties.DisplayMember = "Name";
            lpeJob.Text = "Select...";

            if (!string.IsNullOrEmpty(UserId))
            {
                userToEdit = await _user.GetObjectBy(u => u.Id == UserId);
                var deskUser = await _desktopUser.GetObjectBy(u => u.Id == UserId);
                if (userToEdit != null)
                {
                    lpeJob.EditValue = deskUser.RoleName;
                    lpeGender.EditValue = userToEdit.Gender;
                    textName.Text = userToEdit.FullName;
                    textEmail.Text = userToEdit.Email;
                    textAddress.Text = userToEdit.Address;
                    textPhone.Text = userToEdit.PhoneNumber;
                    textPassword.Text = userToEdit.Pass;
                    textConfirmPassword.Text = userToEdit.Pass;
                    textAge.Text =Convert.ToString( userToEdit.Age);
                }


            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                if (userToEdit == null)
                {
                    await CallApiForAddNewUser();
                }
                else
                {
                    await CallApiForEditUser();
                }

            }
        }
        bool ValidateData()
        {
            if (lpeGender.EditValue == null || lpeJob.EditValue == null)
                return false;
            foreach (Control control in layMain.Controls)
            {
                if (control is TextEdit textbox)
                {
                    if (string.IsNullOrEmpty(textbox.Text))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        async Task CallApiForAddNewUser()
        {
            var url = $"{WebApp.BaseUrl}{WebApp.RegisterUser}";
            var data = new
            {
                fullName = textName.Text,
                gender = lpeGender.EditValue.ToString(),
                age = textAge.Text,
                email = textEmail.Text,
                phone = textPhone.Text,
                address = textAddress.Text,
                role = lpeJob.EditValue.ToString(),
                password = textPassword.Text,
                confirmPassword = textConfirmPassword.Text,
            };
            var result = await _shared.CallApi(data, url);
            if (result)
            {
                MessageBox.Show("تمت الاضافه بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("تأكد من ادخال بيانات صحيحه !", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        async Task CallApiForEditUser()
        {
            var url = $"{WebApp.BaseUrl}{WebApp.EditUser}";
            var data = new
            {
                Id = userToEdit.Id,
                fullName = textName.Text,
                gender = lpeGender.EditValue.ToString(),
                age = textAge.Text,
                email = textEmail.Text,
                phone = textPhone.Text,
                address = textAddress.Text,
                role = lpeJob.EditValue.ToString(),
                password = textPassword.Text,
                confirmPassword = textConfirmPassword.Text,
            };
            var result = await _shared.CallApi(data, url);
            if (result)
            {
                MessageBox.Show("تمت التعديل بنجاح", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("تأكد من ادخال بيانات صحيحه !", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
