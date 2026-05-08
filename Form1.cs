using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProductSalesAggregator
{
    public partial class Form1 : Form
    {
        private List<(string name, decimal price)> products = new List<(string, decimal)>();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("商品名を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("金額に正の数値を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            products.Add((txtProductName.Text, price));
            RefreshDataGrid();
            txtProductName.Clear();
            txtPrice.Clear();
            txtProductName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("削除する商品を選択してください。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedIndex = dataGridView1.SelectedRows[0].Index;
            products.RemoveAt(selectedIndex);
            RefreshDataGrid();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("すべてのデータをクリアしますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                products.Clear();
                RefreshDataGrid();
                ClearResults();
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (products.Count == 0)
            {
                MessageBox.Show("入力されたデータがありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            decimal total = products.Sum(p => p.price);
            decimal average = products.Average(p => p.price);
            decimal max = products.Max(p => p.price);
            decimal min = products.Min(p => p.price);

            lblTotal.Text = $"¥{total:F2}";
            lblAverage.Text = $"¥{average:F2}";
            lblMax.Text = $"¥{max:F2}";
            lblMin.Text = $"¥{min:F2}";
            lblCount.Text = products.Count.ToString();
        }

        private void RefreshDataGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = products.Select((p, i) => new { 番号 = i + 1, 商品名 = p.name, 金額 = $"¥{p.price:F2}" }).ToList();
        }

        private void ClearResults()
        {
            lblTotal.Text = "¥0.00";
            lblAverage.Text = "¥0.00";
            lblMax.Text = "¥0.00";
            lblMin.Text = "¥0.00";
            lblCount.Text = "0";
        }
    }
}
