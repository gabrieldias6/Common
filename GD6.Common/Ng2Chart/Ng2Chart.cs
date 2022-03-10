using System;
using System.Collections.Generic;
using System.Text;

namespace GD6.Common
{

    public class Ng2ChartLabelInfo : Ng2Chart
    {
        public List<DateTime> LabelsData { get; set; }
        public Ng2ChartLabelInfo() : base()
        {
            this.LabelsData = new List<DateTime>();
        }
    }

    public class Ng2Chart
    {
        /// <summary>
        /// Eixo X do Gráfico
        /// </summary>
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
        public List<decimal?> Data { get; set; }
        public bool? Hidden { get; set; }

        public string YAxisID { get; set; }

        public string BackgroundColor { get; set; }
        public string HoverBackgroundColor { get; set; }
        public string PointHoverBackgroundColor { get; set; }
        public string BorderColor { get; set; }

        public string Type { get; set; }

        public Ng2ChartLine(string label)
        {
            this.Label = label;
            this.Data = new List<decimal?>();
        }
        public Ng2ChartLine(int id, string label, bool hidden = false, string backgroundColor = null, string borderColor = null) : this(label)
        {
            Id = id;
            Hidden = hidden;
            BackgroundColor = backgroundColor;
            BorderColor = borderColor;
        }
    }
}
