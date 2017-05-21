using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.IO;

using ManagerApp_GUI.View;
using ManagerApp_GUI.Model;
using System.Security.Cryptography;
using System.Drawing.Imaging;

namespace ManagerApp_GUI
{
    public partial class ItemForm : Form, IItemManagerView
    {
        ItemManager _model;
        Dictionary<int, int> _warehouseIdByIndex = new Dictionary<int, int>();
        Dictionary<int, int> _itemIdByIndex = new Dictionary<int, int>();
        Dictionary<int, int> _mediaIdByIndex = new Dictionary<int, int>();
        Dictionary<int, int> _categoryIdByIndex = new Dictionary<int, int>();
        Dictionary<int, int> _categoryIndexById = new Dictionary<int, int>();

        public static string GetMD5HashFromFile(string filename)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var buffer = md5.ComputeHash(File.ReadAllBytes(filename));
                var sb = new StringBuilder();
                for (int i = 0; i < buffer.Length; i++)
                {
                    sb.Append(buffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }

        public ItemForm()
        {
            InitializeComponent();
        }

        public void AddCategory(int id, string name)
        {
            _categoryIdByIndex.Add(CategorySelector.Items.Count, id);
            _categoryIndexById.Add(id, CategorySelector.Items.Count);
            CategorySelector.Items.Add($"[#{id}] {name}");
        }

        public void AddFoundItem(int id, string name)
        {
            _itemIdByIndex.Add(FoundItemsPanel.Items.Count, id);
            FoundItemsPanel.Items.Add($"[#{id}] {name}");
        }

        public void AddMedia(int id, int priority)
        {
            _mediaIdByIndex.Add(imageList.Items.Count, id);
            imageList.Items.Add($"[{priority}] {id}");
        }

        public void AddWarehouse(int id, string name, int count)
        {
            _warehouseIdByIndex.Add(WarehouseList.Items.Count, id);
            WarehouseList.Items.Add($"[#{id}] {name} - {count} pcs.");
        }

        public void ClearCategoryList()
        {
            CategorySelector.Items.Clear();
            _categoryIdByIndex.Clear();
            _categoryIndexById.Clear();
        }

        public void ClearIitemList()
        {
            FoundItemsPanel.Items.Clear();
            _itemIdByIndex.Clear();
        }

        public void ClearMediaList()
        {
            imageList.Items.Clear();
            _mediaIdByIndex.Clear();
        }

        public void ClearWarehouseList()
        {
            WarehouseList.Items.Clear();
            _warehouseIdByIndex.Clear();
        }

        public void SetItemCategory(int id)
        {
            int index;
            if (!_categoryIndexById.TryGetValue(
                id,
                out index))
            {
                return;
            }
            CategorySelector.SelectedIndex = index;
        }

        public void SetItemDescription(string description)
        {
            ItemDescriptionTextBox.Text = description;
        }

        public void SetItemName(string name)
        {
            ItemNameTextBox.Text = name;
        }

        public void SetItemPrice(int price)
        {
            ItemPriceTextBox.Text = price.ToString();
        }

        private void ItemForm_Load(object sender, EventArgs e)
        {
            _model = new ItemManager(this);
            _model.Init();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            _model.SearchItems(SearchBox.Text);
        }

        private void ItemsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int item;
            if (!_itemIdByIndex.TryGetValue(
                FoundItemsPanel.SelectedIndex,
                out item))
            {
                return;
            }
            _model.SelectItem(item);
        }

        public void SetImage(string url)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://localhost/{url}");
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential("admin", "admin");

            var requestStream = request.GetResponse().GetResponseStream();
            var image = Image.FromStream(requestStream);
            ItemImage.Image = image;
        }

        public void ClearImage()
        {
            ItemImage.Image = null;
        }

        private void imageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int media;
            if (!_mediaIdByIndex.TryGetValue(
                imageList.SelectedIndex,
                out media))
            {
                return;
            }
            _model.OpenMedia(media);
        }

        public void ClearProperties()
        {
            ItemPropertyGrid.Rows.Clear();
        }

        public void AddProperty(int id, string name, IList<Tuple<int, string>> variants, int selectionId, string text)
        {
            gridUpdateActive = false;

            int index = ItemPropertyGrid.Rows.Add();
            ItemPropertyGrid.Rows[index].Tag = id;
            ItemPropertyGrid.Rows[index].Cells["Property"].Value = name;
            var variantsComboBox = (DataGridViewComboBoxCell)ItemPropertyGrid.Rows[index].Cells["Variant"];

            variantsComboBox.Items.Add($"none");
            for (int i = 0; i < variants.Count; i++)
            {
                variantsComboBox.Items.Add($"{variants[i].Item1}) {variants[i].Item2}");

                if (variants[i].Item1 == selectionId)
                {
                    variantsComboBox.Value = $"{variants[i].Item1}) {variants[i].Item2}";
                }
            }

            if (text != null)
            {
                ItemPropertyGrid.Rows[index].Cells["Text"].Value = name;
            } else
            {
                ItemPropertyGrid.Rows[index].Cells["Text"].Value = null;
            }

            gridUpdateActive = true;
        }

        private void ItemPropertyGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!gridUpdateActive || e.RowIndex < 0) return;
            var row = ItemPropertyGrid.Rows[e.RowIndex];
            var property = (int) row.Tag;
            var variantId = -1;
            var parts = ((string)row.Cells["Variant"].Value).Split(')');
            if (parts.Length > 0)
            {
                if (!int.TryParse(parts[0], out variantId))
                {
                    variantId = -1;
                }
            }
            var text = (string)row.Cells["Text"].Value;
            _model.EditProperty(property, variantId, (text == "")?null:text);
        }

        bool gridUpdateActive = true;

        private void AddToWarehouseButton_Click(object sender, EventArgs e)
        {
            int warehouse;
            if (!_warehouseIdByIndex.TryGetValue(
                WarehouseList.SelectedIndex,
                out warehouse))
            {
                return;
            }
            _model.AddToWarehouse(warehouse);
        }

        public void ShowError(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void RemoveFromWarehouseButton_Click(object sender, EventArgs e)
        {
            int warehouse;
            if (!_warehouseIdByIndex.TryGetValue(
                WarehouseList.SelectedIndex,
                out warehouse))
            {
                return;
            }
            _model.RemoveFromWarehouse(warehouse);
        }

        private void RemoveImageButton_Click(object sender, EventArgs e)
        {
            int media;
            if (!_mediaIdByIndex.TryGetValue(
                imageList.SelectedIndex,
                out media))
            {
                return;
            }
          
            _model.RemoveMedia(media);
        }

        private void UploadImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images (*.png;*.jpg;*.bmp)|*.png;*.jpg;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string extension = "png";
                string hash = GetMD5HashFromFile(ofd.FileName);
                string fileName = hash + '.' + extension;
                string thumbnailName = hash + "_thumb." + extension;
                Image image = Image.FromFile(ofd.FileName);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://localhost/{fileName}");
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("admin", "admin");

                var requestStream = request.GetRequestStream();
                image.Save(requestStream, ImageFormat.Png);
                requestStream.Flush();
                requestStream.Close();

                Bitmap thumbnail = new Bitmap(image, new Size(256, 256));

                request = (FtpWebRequest)WebRequest.Create($"ftp://localhost/{thumbnailName}");
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential("admin", "admin");

                requestStream = request.GetRequestStream();
                thumbnail.Save(requestStream, ImageFormat.Png);
                requestStream.Flush();
                requestStream.Close();

                _model.UploadMedia(fileName, thumbnailName);
            }
        }

        private void ImagePriorityUpButton_Click(object sender, EventArgs e)
        {
            int media;
            if (!_mediaIdByIndex.TryGetValue(
                imageList.SelectedIndex,
                out media))
            {
                return;
            }

            _model.DecMediaPriority(media);
        }

        private void ImagePriorityDownButton_Click(object sender, EventArgs e)
        {
            int media;
            if (!_mediaIdByIndex.TryGetValue(
                imageList.SelectedIndex,
                out media))
            {
                return;
            }

            _model.IncMediaPriority(media);
        }

        private void SaveItemButton_Click(object sender, EventArgs e)
        {
            int category;
            if (!_categoryIdByIndex.TryGetValue(CategorySelector.SelectedIndex, out category))
            {
                return;
            }
            _model.SaveItem(ItemNameTextBox.Text, int.Parse(ItemPriceTextBox.Text), category, ItemDescriptionTextBox.Text);
        }

        private void NewItemButton_Click(object sender, EventArgs e)
        {
            int category;
            if (!_categoryIdByIndex.TryGetValue(CategorySelector.SelectedIndex, out category))
            {
                return;
            }
            _model.NewItem(ItemNameTextBox.Text, int.Parse(ItemPriceTextBox.Text), category, ItemDescriptionTextBox.Text);
        }

        public void SetEditControlsEnabled(bool state)
        {
            MainPanel.Enabled = state;
        }

        private void DeleteItemButton_Click(object sender, EventArgs e)
        {
            _model.DeleteItem();
        }

        public void SimulateSearch(string query = null)
        {
            if (query == null)
            {
                _model.SearchItems(SearchBox.Text);                
            } else
            {
                _model.SearchItems(query);
                SearchBox.Text = query;
            }
        }
    }
}
