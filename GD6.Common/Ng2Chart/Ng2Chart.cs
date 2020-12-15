using System;
using System.Collections.Generic;
using System.Text;

namespace GD6.Common
{
    public class Ng2Chart
    {
        public List<string> Labels { get; set; }
        public List<Ng2ChartLine> Dados { get; set; }

        public Ng2Chart()
        {
            this.Labels = new List<string>();
            this.Dados = new List<Ng2ChartLine>();
        }
    }

    public class Ng2ChartLine
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public List<int?> Data { get; set; }

        public Ng2ChartLine(string label)
        {
            this.Label = label;
            this.Data = new List<int?>();
        }
        public Ng2ChartLine(int id, string label) : this(label)
        {
            Id = id;
        }
    }
}
