using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport.Implementation;
using SaidalyTechMain.BL.IServices;
using SaidalyTechMain.Consts;
using SaidalyTechMain.DB_Models;
using SaidalyTechMain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using DevExpress.XtraPrinting.Native;

namespace SaidalyTechMain.Forms.StoresAndItems
{
    public partial class FrmItem : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        #region objects
        IService<TbCategories> _category = StartUp<IService<TbCategories>>.Services();
        IService<TbItems> _item = StartUp<IService<TbItems>>.Services();
        IService<TbItemUnits> _ItemUnits = StartUp<IService<TbItemUnits>>.Services();
        TbItems CommingItem;
        TbItemUnits CommingItemUnits;
        List<TbCategories> lstCategories = new List<TbCategories>();
        OpenFileDialog fileDialog = new OpenFileDialog();
        string imageName = "";
        int Id = 0;
        #endregion
        public FrmItem()
        {
            InitializeComponent();
        }
        public FrmItem(int id)
        {
            InitializeComponent();
            Id = id;
        }
        private async void FrmItem_Load(object sender, EventArgs e)
        {
            await LoadDate();

            if (Id > 0)
            {
                await LoadItem();
            }
        }
        private async Task LoadDate()
        {
            CommingItem = new TbItems();
            CommingItemUnits = new TbItemUnits();
            lstCategories = await _category.GetAll();
            lpeCategoies.Properties.DataSource = lstCategories.Select(a => new { a.Id, a.Name }).ToList();
            lpeCategoies.Properties.ValueMember = "Id";
            lpeCategoies.Properties.DisplayMember = "Name";
            textName.Text = "";
            textCompany.Text = "";
            textPamphlet.Text = "";
            textDosage.Text = "";
            textActiveEngidient.Text = "";
            textPrice.Text = "";
            textPurchasePrice.Text = "";
            textBarcode.Text = "";
            textComposition.Text = "";
            lpeCategoies.EditValue = null;
            itemImage.Image = null;

        }
        private async Task LoadItem()
        {
            CommingItem = await _item.GetObjectBy(i => i.Id == Id);
            CommingItemUnits = await _ItemUnits.GetObjectBy(iu => iu.ItemId == Id);
            textName.Text = CommingItem.Name;
            textCompany.Text = CommingItem.Company;
            textPamphlet.Text = CommingItem.Pamphlet;
            textDosage.Text = CommingItem.Dosage;
            textActiveEngidient.Text = CommingItem.ActiveIngredient;
            textBarcode.Text = CommingItemUnits.Barcode;
            textComposition.Text = CommingItem.Composition;
            textPrice.Text = Convert.ToString(CommingItemUnits.SalesPrice);
            textPurchasePrice.Text = Convert.ToString(CommingItemUnits.PuchasePrice);
            lpeCategoies.EditValue = CommingItem.CategoryId;
            imageName = CommingItem.ImageName;
            try
            {
                var imageUrl = $"{WebApp.BaseUrl}{WebApp.ImagePath}{CommingItem.ImageName}";
                WebRequest request = WebRequest.Create(imageUrl);
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                itemImage.Image = Image.FromStream(stream);
                response.Close();
            }
            catch
            {
            }
        }
        private bool CheckInputs()
        {
            if (string.IsNullOrEmpty(textName.Text))
                return false;
            if (lpeCategoies.EditValue == null)
                return false;
            if (string.IsNullOrEmpty(textActiveEngidient.Text))
                return false;
            if (string.IsNullOrEmpty(textPrice.Text))
                return false;
            if (string.IsNullOrEmpty(textPamphlet.Text))
                return false;
            if (string.IsNullOrEmpty(textDosage.Text))
                return false;

            return true;
        }
        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckInputs())
            {


                var item = new TbItems()
                {
                    ActiveIngredient = textActiveEngidient.Text,
                    CategoryId = Convert.ToInt32(lpeCategoies.EditValue),
                    Composition = textComposition.Text,
                    IsActive = true,
                    Dosage = textDosage.Text,

                    Name = textName.Text,
                    Pamphlet = textPamphlet.Text,
                    ImageName = imageName,
                    Company = textCompany.Text,
                    IsDeleted = false,
                };


                if (Id == 0)
                {
                    item.Id = 0;
                    if (await _item.Add(item))
                    {
                        var itemUnit = new TbItemUnits()
                        {
                            ItemId = item.Id,
                            UnitId = 1,
                            SalesPrice = Convert.ToInt32(textPrice.Text),
                            IsActive = true,
                            PuchasePrice = Convert.ToInt32(textPurchasePrice.Text),
                            Barcode = textBarcode.Text,
                        };

                        if (await _ItemUnits.Add(itemUnit))
                        {
                            MessageBox.Show("تمت العمليه بنجاح");
                            await LoadDate();
                        }
                        else
                            MessageBox.Show("حدث مشكله اثناء العمليه");
                    }
                    else
                        MessageBox.Show("حدث مشكله اثناء العمليه");
                }
                else
                {
                    item.ImageName = imageName;
                    item.Id = Id;
                    if (await _item.Edit(item))
                    {
                        var IcommingtemUnit = await _ItemUnits.GetObjectBy(ii => ii.ItemId == item.Id);

                        IcommingtemUnit.SalesPrice = Convert.ToDecimal(textPrice.Text);
                        IcommingtemUnit.IsActive = true;
                        IcommingtemUnit.PuchasePrice = Convert.ToDecimal(textPurchasePrice.Text);
                        IcommingtemUnit.Barcode = textBarcode.Text;

                        if (await _ItemUnits.Edit(IcommingtemUnit))
                        {
                            MessageBox.Show("تمت العمليه بنجاح");
                            await LoadDate();
                        }
                        else
                            MessageBox.Show("حدث مشكله اثناء العمليه");
                    }
                    else
                        MessageBox.Show("حدث مشكله اثناء العمليه");
                }
                await UploadImageToWeb();
                Id = 0;
            }
            else
            {
                MessageBox.Show("تاكد من ااكمال البانات اولا");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAddImage_Click(object sender, EventArgs e)
        {
            fileDialog.Filter = " jpg files(*.jpg)|*.jpg|PNG files(*.png)|*.png|All Files(*.*)|*.*";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                itemImage.ImageLocation = fileDialog.FileName;
                imageName = fileDialog.SafeFileName;
            }
        }
        private async Task UploadImageToWeb()
        {
            if (!string.IsNullOrEmpty(fileDialog.FileName))
            {
                string filePath = fileDialog.FileName;

                using (HttpClient httpClient = new HttpClient())
                using (MultipartFormDataContent content = new MultipartFormDataContent())
                {
                    // Create a stream from the selected file
                    FileStream fileStream = File.OpenRead(filePath);
                    // Add the file stream to the multipart form data content
                    content.Add(new StreamContent(fileStream), "file", Path.GetFileName(filePath));

                    // Make the API request
                    var url = $"{WebApp.BaseUrl}{WebApp.UploadItemImage}";
                    HttpResponseMessage response = await httpClient.PostAsync(url, content);
                }

            }

        }
    }
}
