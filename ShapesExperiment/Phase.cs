using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ShapesExperiment
{
    public class Phase
    {
        public char Label { get; set; }
        public Color BackgroundColor { get; set; }
        public int Observations { get; set; }
        public decimal Density { get; set; }
        public int RankType { get; set; }
        public decimal ResponseIndex { get; set; }
        public int ResponseValue { get; set; }
        public List<Trial> Trials { get; set; }

        public Phase(char label, Color color, int m, decimal w, int rank)
        {
            this.Label = label;
            this.BackgroundColor = color;
            this.Observations = m;
            this.Density = w;
            this.RankType = rank;
            this.Trials = new List<Trial>();
        }
    }
}
