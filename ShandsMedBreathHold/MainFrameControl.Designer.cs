using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Threading;
using System.Globalization;
using System.Windows.Forms.DataVisualization.Charting;

namespace ShandsMedBreathHold
{
    partial class MainFrameControl
    {
        #region Fields

		// Chart data receiver
        private DataReceiver receiver;

        //Data Receive Delegate
        public delegate void ReceiveDataDelegate(double value);
        public ReceiveDataDelegate receiveDelegate;

		// Chart control
		private System.Windows.Forms.DataVisualization.Charting.Chart chart1;

		// Form fields
		private System.ComponentModel.Container components = null;
        private System.ComponentModel.ComponentResourceManager resources = null;
		private System.Windows.Forms.Button connectBtn;
		private System.Windows.Forms.Button toggleBtn;
		private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox comboBox1;
        private int pointIndex = 0;
        private int time_scale = 10;
        private bool isConnect = false;

		#endregion // Fields

		#region Construction and Disposing

        public MainFrameControl()
		{
            Thread.CurrentThread.CurrentCulture = new CultureInfo(global::ShandsMedBreathHold.Properties.Resources.lang);
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			// Abort thread
            if (receiver != null&&isConnect)
            {
                receiver.close();
            }

			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion // Construction and Disposing

		#region Form user event handlers

		/// <summary>
		/// Page load event handler.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event arguments.</param>
		private void MainFrameControl_Load(object sender, System.EventArgs e)
		{
            receiveDelegate += new ReceiveDataDelegate(addReceiveData);
            this.comboBox1.Items.AddRange(DataReceiver.GetPortNames());
            this.comboBox1.SelectedIndex = 0;

            // 按钮状态设置
            connectBtn.Text = resources.GetString("Connect");
            connectBtn.Enabled = true;
            toggleBtn.Visible = false;
		}

		/// <summary>
		/// Start real time data simulator.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event arguments.</param>
		private void connectBtn_Click(object sender, System.EventArgs e)
		{
            if (isConnect)
            {
                receiver.close();
            }
            else
            {
                // Reset number of series in the chart.
                chart1.Series.Clear();
                Series newSeries = new Series(resources.GetString("SeriesName"));
                newSeries.ChartType = SeriesChartType.Line;
                newSeries.BorderWidth = 1;
                newSeries.Color = Color.FromArgb(224, 64, 10);
                chart1.Series.Add(newSeries);
                pointIndex = 0;
                receiver = new DataReceiver(this, comboBox1.Text);
                
            }
		}

		/// <summary>
		/// Stop real time data simulator.
		/// </summary>
		/// <param name="sender">Event sender.</param>
		/// <param name="e">Event arguments.</param>
		private void toggleBtn_Click(object sender, System.EventArgs e)
		{
            if (receiver != null)
            {
                receiver.toggleReceive();
            }
		}

		#endregion

		#region Add new data thread
        //Data thread
        public double getStep()
        {
            return 30.0 / 1380.0;
        }
        public int numberOfPointsInChart()
        {
            return 1380 * time_scale / 30;
        }
        //Data delegate insert data to chart
        public void addReceiveData(double value)
        {
            double curX = getStep() * pointIndex;
            chart1.Series[0].Points.AddXY(curX, value);
            ++pointIndex;

            // Keep a constant number of points by removing them from the left
            while (chart1.Series[0].Points.Count > numberOfPointsInChart())
            {
                // Remove data points on the left side
                chart1.Series[0].Points.RemoveAt(0);
            }
            // Adjust X axis scale
            chart1.ChartAreas[0].AxisX.Minimum = chart1.Series[0].Points[0].XValue;
            chart1.ChartAreas["Default"].AxisX.Maximum = chart1.ChartAreas["Default"].AxisX.Minimum + time_scale*1.01;

            // Redraw chart
            chart1.Invalidate();
        }

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrameControl));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.StripLine stripLine1 = new System.Windows.Forms.DataVisualization.Charting.StripLine();
            System.Windows.Forms.DataVisualization.Charting.StripLine stripLine2 = new System.Windows.Forms.DataVisualization.Charting.StripLine();
            System.Windows.Forms.DataVisualization.Charting.StripLine stripLine3 = new System.Windows.Forms.DataVisualization.Charting.StripLine();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.connectBtn = new System.Windows.Forms.Button();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.toggleBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectBtn
            // 
            this.connectBtn.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.connectBtn, "connectBtn");
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.UseVisualStyleBackColor = false;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // chart1
            // 
            this.chart1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(223)))), ((int)(((byte)(240)))));
            this.chart1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            this.chart1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.Area3DStyle.Inclination = 15;
            chartArea1.Area3DStyle.IsClustered = true;
            chartArea1.Area3DStyle.IsRightAngleAxes = false;
            chartArea1.Area3DStyle.Perspective = 10;
            chartArea1.Area3DStyle.Rotation = 10;
            chartArea1.Area3DStyle.WallWidth = 0;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisX.LabelStyle.Format = "N2";
            chartArea1.AxisX.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.IsStartedFromZero = false;
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            chartArea1.AxisY.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.AxisY.Maximum = 25D;
            chartArea1.AxisY.Minimum = 5D;
            stripLine1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(180)))), ((int)(((byte)(65)))));
            stripLine1.BorderWidth = 2;
            stripLine1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            stripLine1.IntervalOffset = 23D;
            stripLine1.Text = resources.GetString("UpLine");
            stripLine1.TextAlignment = System.Drawing.StringAlignment.Near;
            stripLine1.TextLineAlignment = System.Drawing.StringAlignment.Far;
            stripLine2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(64)))), ((int)(((byte)(10)))));
            stripLine2.BorderWidth = 2;
            stripLine2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            stripLine2.IntervalOffset = 16D;
            stripLine2.Text = resources.GetString("BaseLine");
            stripLine2.TextAlignment = System.Drawing.StringAlignment.Near;
            stripLine2.TextLineAlignment = System.Drawing.StringAlignment.Far;
            stripLine3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(64)))), ((int)(((byte)(10)))));
            stripLine3.BorderWidth = 2;
            stripLine3.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            stripLine3.IntervalOffset = 10D;
            stripLine3.Text = resources.GetString("DownLine");
            stripLine3.TextAlignment = System.Drawing.StringAlignment.Near;
            stripLine3.TextLineAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisY.StripLines.Add(stripLine1);
            chartArea1.AxisY.StripLines.Add(stripLine2);
            chartArea1.AxisY.StripLines.Add(stripLine3);
            chartArea1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(165)))), ((int)(((byte)(191)))), ((int)(((byte)(228)))));
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.TopBottom;
            chartArea1.BackSecondaryColor = System.Drawing.Color.White;
            chartArea1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            chartArea1.BorderDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Solid;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 85F;
            chartArea1.InnerPlotPosition.Width = 86F;
            chartArea1.InnerPlotPosition.X = 8.3969F;
            chartArea1.InnerPlotPosition.Y = 3.63068F;
            chartArea1.Name = "Default";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 86.76062F;
            chartArea1.Position.Width = 88F;
            chartArea1.Position.X = 5.089137F;
            chartArea1.Position.Y = 5.895753F;
            chartArea1.ShadowColor = System.Drawing.Color.Transparent;
            this.chart1.ChartAreas.Add(chartArea1);
            resources.ApplyResources(this.chart1, "chart1");
            legend1.Alignment = System.Drawing.StringAlignment.Far;
            legend1.BackColor = System.Drawing.Color.Transparent;
            legend1.DockedToChartArea = "Default";
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold);
            legend1.IsTextAutoFit = false;
            legend1.LegendStyle = System.Windows.Forms.DataVisualization.Charting.LegendStyle.Row;
            legend1.Name = "Default";
            this.chart1.Legends.Add(legend1);
            this.chart1.Name = "chart1";
            series1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(26)))), ((int)(((byte)(59)))), ((int)(((byte)(105)))));
            series1.ChartArea = "Default";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(64)))), ((int)(((byte)(10)))));
            series1.Legend = "Default";
            series1.Name = resources.GetString("SeriesName");
            series1.ShadowOffset = 1;
            this.chart1.Series.Add(series1);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.toggleBtn);
            this.panel1.Controls.Add(this.connectBtn);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox1, "comboBox1");
            this.comboBox1.Name = "comboBox1";
            // 
            // toggleBtn
            // 
            this.toggleBtn.BackColor = System.Drawing.Color.Transparent;
            this.toggleBtn.BackgroundImage = global::ShandsMedBreathHold.Properties.Resources.play_btn;
            this.toggleBtn.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.toggleBtn, "toggleBtn");
            this.toggleBtn.Name = "toggleBtn";
            this.toggleBtn.UseVisualStyleBackColor = false;
            this.toggleBtn.Click += new System.EventHandler(this.toggleBtn_Click);
            // 
            // MainFrameControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.panel1);
            this.Name = "MainFrameControl";
            resources.ApplyResources(this, "$this");
            this.Load += new System.EventHandler(this.MainFrameControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

        /// <summary>
        /// DataReceiverListener接口的实现
        /// </summary>
        public void dataReceived(double value)
        {
            chart1.Invoke(receiveDelegate, new object[] { value });
        }
        public void isOpened(bool value)
        {
            this.toggleBtn.Visible = value;
            isConnect = value;
            if (value)
            {
                this.toggleBtn.BackgroundImage = global::ShandsMedBreathHold.Properties.Resources.play_btn;
                this.connectBtn.Text = resources.GetString("Disconnect");
            }
            else
            {
                this.connectBtn.Text = resources.GetString("Connect");
            }
        }
        public void isRunning(bool value)
        {
            if (value)
            {
                this.toggleBtn.BackgroundImage = global::ShandsMedBreathHold.Properties.Resources.pause_btn;
            }
            else
            {
                this.toggleBtn.BackgroundImage = global::ShandsMedBreathHold.Properties.Resources.play_btn;
            }
        }
    }
}
