using DevExpress.XtraEditors;
using Newtonsoft.Json;
using SaidalyTechMain;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.Consts;
using SaidalyTechMain.DB_Models;
using SaidalyTechMain.Forms.MainForms;
using SaidalyTechMain.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaidalyTechMain.Shared
{
    public class SharedFunctions
    {
        IService<TbShifts> _Shifts;

        public SharedFunctions()
        {
        }
        public bool Question(DialogResult question)
        {
            return question == DialogResult.Yes ? true : false;
        }
        public async Task<bool>CallApi(object data, string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("", content);
                return response.IsSuccessStatusCode;
            }
        }
        public string GetDeviceInformation()
        {
            string DeviceInformation = String.Empty;
            var mbs = new ManagementObjectSearcher("Select ProcessorID From Win32_processor");
            var mbsList = mbs.Get();
            foreach (ManagementObject mo in mbsList)
            {
                DeviceInformation = mo["ProcessorID"].ToString().Trim();
            }
            ManagementScope scope = new ManagementScope("\\\\" + Environment.MachineName + "\\root\\cimv2");
            scope.Connect();
            ManagementObject wmiClass = new ManagementObject(scope, new ManagementPath("Win32_BaseBoard.Tag=\"Base Board\""), new ObjectGetOptions());
            foreach (PropertyData propData in wmiClass.Properties)
            {
                if (propData.Name == "SerialNumber")
                    DeviceInformation += Convert.ToString(propData.Value).Trim();
            }
            return DeviceInformation;
        }
        public void OpenForm(Form formShow)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == formShow.GetType())
                {
                    if (frm is FrmLogin)
                        frm.Show();
                    if (frm.WindowState == FormWindowState.Minimized)
                    {
                        frm.StartPosition = FormStartPosition.CenterScreen;
                        frm.WindowState = FormWindowState.Normal;
                    }
                    frm.Focus();
                    return;
                }
            }
            formShow.Show();
        }
        public void OpenFormDialog(Form formShow)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == formShow.GetType())
                {
                    frm.Focus();
                    return;
                }
            }
            formShow.ShowDialog();
        }
        public void CloseForm(Form formShow)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.GetType() == formShow.GetType())
                {
                    if (frm is FrmLogin)
                    {
                        frm.Focus();
                    }
                    frm.Close();
                    return;
                }
            }
        }
        public void CloseAllForm()
        {
            List<Form> openForms = new List<Form>();
            foreach (Form form in Application.OpenForms)
                openForms.Add(form);

            foreach (Form frm in openForms)
                if (frm.GetType() != typeof(FrmStartInterface))
                    frm.Close();
        }
        public async Task TestOpenShift(Form form)
        {
            _Shifts = StartUp<IService<TbShifts>>.Services();
            TbShifts shift = await _Shifts.GetObjectBy(a => a.DeviceName == Environment.MachineName && a.CloseDateTime == null && a.CloseUserId == null);
            if (shift == null)
            {

                DialogResult dialogResult = MessageBox.Show("يجب فتح فتره أولا هل تريد فتح فتره ؟", "", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                if (dialogResult == DialogResult.Yes)
                {
                    TbShifts newShift = new TbShifts();
                    newShift.DeviceName = Environment.MachineName;
                    newShift.OpenUserId = Settings.Default.UserId;
                    newShift.OpenDateTime = DateTime.UtcNow.ToLocalTime();

                    var result = XtraInputBox.Show("ادخل رصيد افتتاح الفتره", "رصيد افتتاح الفتره", "");
                    decimal mony;
                    if (decimal.TryParse(result.Trim(), out mony))
                    {
                        newShift.OpeningBalance = mony;

                        if (await _Shifts.Add(newShift))
                        {
                            Settings.Default.ShiftId = newShift.Id;
                            Settings.Default.Save();
                            OpenForm(form);

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
                OpenForm(form);
            }

        }
        public byte[] ConvertImageToBytes(string sPath)
        {
            byte[] data = null;
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }

    }
}
