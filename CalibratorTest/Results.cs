using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace FakeColourFinder
{
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
        }

        public void ListResults()
        {
            if (!ResultSet.CheckForResultDirectory())
            {
                return;
            }

            string resultsPath = ResultSet.GetResultDirectoryPath();
            List<string> paths = Directory.GetFiles(resultsPath, "*.xml").OrderByDescending(item => item).ToList();

            _resultList.Items.Clear();
            foreach (string path in paths)
            {
                _resultList.Items.Add(Path.GetFileName(path));
            }

            if (_resultList.Items.Count > 0)
            {
                _resultList.SelectedIndex = 0;
            }
        }

        private void _btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetCellDiff(DataGridViewCell cell, double diff)
        {
            if (Math.Abs(diff) > 3.0)
            {
                cell.Style.BackColor = Color.DarkRed;
                cell.Style.ForeColor = Color.White;
            }

            string format = "{0:F2}%";
            if (diff >= 0)
            {
                format = "+" + format;
            }
            

            cell.Value = string.Format(format, diff);
        }

        private void _resultList_SelectedValueChanged(object sender, EventArgs e)
        {
            _resultsGrid.Rows.Clear();

            if (_resultsGrid.ColumnCount == 0)
            {
                return;
            }

            if (_resultList.SelectedItem == null)
            {
                return;
            }

            if (!ResultSet.CheckForResultDirectory())
            {
                return;
            }

            string error = "";
            ResultSet results = null;
            if (!ResultSet.Load(Path.Combine(ResultSet.GetResultDirectoryPath(), (string)_resultList.SelectedItem), out results, out error))
            {
                MessageBox.Show(error, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (ResultItem item in results.Results)
            {
                int newRow = _resultsGrid.Rows.Add();
                _resultsGrid.Rows[newRow].Cells["_colGray"].Value = item.GrayLevel;
                SetCellDiff(_resultsGrid.Rows[newRow].Cells["_colBrightness"], item.YDiff);
                SetCellDiff(_resultsGrid.Rows[newRow].Cells["_colR"], item.RDiff);
                SetCellDiff(_resultsGrid.Rows[newRow].Cells["_colG"], item.GDiff);
                SetCellDiff(_resultsGrid.Rows[newRow].Cells["_colB"], item.BDiff);
            }
           
        }

        private void _resultsGrid_SelectionChanged(object sender, EventArgs e)
        {
            _resultsGrid.ClearSelection();
        }

        private void Results_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
