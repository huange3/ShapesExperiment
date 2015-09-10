using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapesExperiment
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            generateID();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (conditionOrderTB.Text == "")
            {
                MessageBox.Show("Invalid condition order entered. Please try again.");
                return;
            }

            if (participantIDTB.Text == "")
            {
                MessageBox.Show("Invalid participant ID entered. Please try again.");
                return;
            }

            if (setupExperiment()) runExperiment();
        }

        private void generateID()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, 8)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());

            participantIDTB.Text = result;
        }

        private void selectBackground(PictureBox currBox)
        {
            DialogResult dialog = colorDialog.ShowDialog();

            if (dialog == DialogResult.OK) currBox.BackColor = colorDialog.Color;
        }

        private void backgroundA_Click(object sender, EventArgs e)
        {
            selectBackground(backgroundA);
        }

        private void backgroundB_Click(object sender, EventArgs e)
        {
            selectBackground(backgroundB);
        }

        private void backgroundC_Click(object sender, EventArgs e)
        {
            selectBackground(backgroundC);
        }

        private void backgroundD_Click(object sender, EventArgs e)
        {
            selectBackground(backgroundD);
        }

        private void backgroundX_Click(object sender, EventArgs e)
        {
            selectBackground(backgroundX);
        }

        private Boolean setupExperiment()
        {
            return true;
        }

        private void runExperiment()
        {

        }
    }
}
