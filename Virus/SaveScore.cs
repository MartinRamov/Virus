using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Virus
{
    public partial class SaveScore : Form
    {
        public string Name { set; get; }
        public bool Save { set; get; }

        public SaveScore()
        {
            InitializeComponent();
            Save = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSaveScore_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim().Length == 0)
            {             
                errorProvider1.SetError(tbName, "Внесете го вашето име");
            }
            else
            {
                errorProvider1.SetError(tbName, null);
                Name = tbName.Text.Trim();
                tbName.Text = "";
                Save = true;
                MessageBox.Show("Your score has been saved", "Score saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSaveScore.Enabled = false;
            }
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Retry;
            Close();
        }
    }
}
