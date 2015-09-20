using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapesExperiment
{
    public partial class mainForm : Form
    {
        public List<Phase> Phases;
        public int TrialDuration = 0;
        public int TrialRestDuration = 0;
        public decimal MoneyValue;
        public decimal RewardValue;

        public mainForm()
        {
            InitializeComponent();
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            generateID();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            Regex re = new Regex(@"/(\A[abc]+\z)/i");
            Match match;

            if (conditionOrderTB.Text == "")
            {
                MessageBox.Show("Invalid condition order entered. Please try again.");
                return;
            }

            match = re.Match(conditionOrderTB.Text);

            if (!match.Success)
            {
                MessageBox.Show("Invalid condition order entered. Please try again.");
                return;
            }

            if (participantIDTB.Text == "")
            {
                MessageBox.Show("Invalid participant ID entered. Please try again.");
                return;
            }

            if (setupPhases()) viewBoard();
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

        private Boolean setupPhases()
        {
            var conditionOrderStr = "";
            var observationCount = 0;
            Color currAColor;
            Color currBColor;
            Color currCColor;
            decimal currBDensity;
            decimal currCDensity;
            Phase currPhase = null;
            char currChar;

            try
            {
                // Initialize and set up phases
                Phases = new List<Phase>();

                conditionOrderStr = conditionOrderTB.Text;

                currAColor = backgroundA.BackColor;
                currBColor = backgroundB.BackColor;
                currCColor = backgroundC.BackColor;

                this.TrialDuration = (int)trialDurationVal.Value;
                this.TrialRestDuration = (int)trialRestDurationVal.Value;
                this.MoneyValue = startingAmountVal.Value;
                this.RewardValue = rewardVal.Value;

                observationCount = (int)mVal.Value;
                currBDensity = condBwVal.Value;
                currCDensity = condCwVal.Value;

                foreach(char c in conditionOrderStr)
                {
                    currChar = Char.ToUpper(c);

                    if (currChar == 'A')
                    {
                        currPhase = new Phase('A', currAColor, observationCount, 0, Constants.NoRank);
                    }

                    if (currChar == 'B')
                    {
                        currPhase = new Phase('B', currBColor, observationCount, currBDensity, Constants.LessThan);
                    }

                    if (currChar == 'C')
                    {
                        currPhase = new Phase('C', currCColor, observationCount, currCDensity, Constants.GreaterThan);
                    }

                    Phases.Add(currPhase);
                }

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while setting up experiment: " + e.Message);
                return false;
                throw;
            }
        }

        private void viewBoard()
        {
            mainBoard newBoard = null;
            try
            {
                // open our experiment window
                newBoard = new mainBoard();
                newBoard.PhaseQueue = new Queue<Phase>(this.Phases);
                newBoard.TrialDuration = this.TrialDuration;
                newBoard.TrialRestDuration = this.TrialRestDuration;
                newBoard.MoneyValue = this.MoneyValue;
                newBoard.RewardValue = this.RewardValue;

                newBoard.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while running experiment: " + e.Message);
                throw;
            }
        }

    }
}
