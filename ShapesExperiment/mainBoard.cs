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
    public partial class mainBoard : Form
    {
        public List<Phase> Phases = null;
        public Queue<Phase> PhaseQueue = null;
        public Phase CurrentPhase = null;
        public Trial CurrentTrial = null;
        public List<Shape> BaselineShapes = null;
        public List<Shape> TrialShapes = null;
        public List<char> SkeletonBoard = new List<char>(64);

        public int TrialDuration = 0;
        public int TrialRestDuration = 0;
        public decimal MoneyValue;
        public decimal RewardValue;

        static Random rand = new Random();

        public mainBoard()
        {
            InitializeComponent();
        }

        private void mainBoard_Load(object sender, EventArgs e)
        {
            // make this form fullscreen
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        public void initializeBoard()
        {
            BaselineShapes = new List<Shape>();

            BaselineShapes.Add(new Shape(1, Properties.Resources.gray_01));
            BaselineShapes.Add(new Shape(2, Properties.Resources.gray_01));
            BaselineShapes.Add(new Shape(3, Properties.Resources.gray_01));
            BaselineShapes.Add(new Shape(4, Properties.Resources.gray_01));
            BaselineShapes.Add(new Shape(5, Properties.Resources.gray_01));
            BaselineShapes.Add(new Shape(6, Properties.Resources.gray_01));

            TrialShapes = new List<Shape>();

            TrialShapes.Add(new Shape(1, Properties.Resources.blue_01));
            TrialShapes.Add(new Shape(2, Properties.Resources.blue_02));
            TrialShapes.Add(new Shape(3, Properties.Resources.blue_03));
            TrialShapes.Add(new Shape(4, Properties.Resources.blue_04));
            TrialShapes.Add(new Shape(5, Properties.Resources.blue_05));
            TrialShapes.Add(new Shape(6, Properties.Resources.blue_06));

            // create ourselves a board that we can shuffle to drive 
            // the drawBoard() function
            // X & Y are buckets
            // A is shape #1, B is shape #2
            for (var i = 0; i < 64; i++)
            {
                if (i == 0) SkeletonBoard[i] = 'X';

                else if (i == 1) SkeletonBoard[i] = 'Y';

                else if (i >= 2 && i <= 32) SkeletonBoard[i] = 'A';

                else SkeletonBoard[i] = 'B';
            }
        }

        public void runTrial()
        {
            try
            {
                if (PhaseQueue.Count == 0 && Phases.Count == 0)
                {
                    MessageBox.Show("Invalid phase information received. Please try again.");
                    this.Close();
                    return;
                }

                if (PhaseQueue.Count == 0)
                {
                    MessageBox.Show("Experiment completed! Thank you for participating!");
                    outputData();
                    this.Close();
                    return;
                }

                /* MODUS OPERANDI
                ------------------
                1. Dequeue our first phase
                2. Check the phase type (special shapes for baseline)
                3. Draw our board
                4. Start the timer*/

                this.CurrentPhase = PhaseQueue.Dequeue();
                this.CurrentTrial = new Trial();
                mainTimer.Interval = this.TrialDuration * 1000;
                restTimer.Interval = this.TrialRestDuration * 1000;

                //if (drawBoard()) mainTimer.Start();

                drawBoard();

            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while running trial: " + e.Message);
                this.Close();
            }
        }

        public Boolean drawBoard()
        {
            Shape shapeA = null;
            Shape shapeB = null;
            Bitmap bucketA = null;
            Bitmap bucketB = null;

            try
            {
                // pick our two random shapes by shuffling our shape lists and
                // taking the first two shapes
                // then find, the corresponding bucket images based on the shape IDs
                if (this.CurrentPhase.Label == Constants.PhaseBaseline)
                {
                    BaselineShapes.Shuffle();
                    shapeA = BaselineShapes[0];
                    shapeB = BaselineShapes[1];
                } else
                {
                    TrialShapes.Shuffle();
                    shapeA = TrialShapes[0];
                    shapeB = TrialShapes[1];
                }

                bucketA = findBucket(shapeA.ShapeID);
                bucketB = findBucket(shapeB.ShapeID);

                // shuffle our skeleton board
                this.SkeletonBoard.Shuffle();

                for (var i = 0; i < this.SkeletonBoard.Count; i++)
                {

                }


                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occurred while drawing board: " + e.Message);
                this.Close();
                return false;
            }
        }

        public void outputData()
        {

        }

        public Bitmap findBucket(int id)
        {
            switch (id)
            {
                case 1:
                    return Properties.Resources.bucket_01;
                case 2:
                    return Properties.Resources.bucket_02;
                case 3:
                    return Properties.Resources.bucket_03;
                case 4:
                    return Properties.Resources.bucket_04;
                case 5:
                    return Properties.Resources.bucket_05;
                case 6:
                    return Properties.Resources.bucket_06;
                default:
                    return Properties.Resources.bucket_empty;
            }
        }
    }
}
