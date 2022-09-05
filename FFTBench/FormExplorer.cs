using FFTBench.Benchmark;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FFTBench
{
    public partial class FormExplorer : Form
    {
        OxyPlot.WindowsForms.PlotView plot1, plot2, plot3;

        Dictionary<string, Func<int, double[]>> signals;

        public FormExplorer()
        {
            InitializeComponent();
        }

        private void FormExplorer_Load(object sender, EventArgs e)
        {
            plot1 = CreateOxyPlot(1);
            plot2 = CreateOxyPlot(2);
            plot3 = CreateOxyPlot(3);

            this.splitContainer1.Panel1.Controls.Add(plot1); // Top
            this.splitContainer2.Panel1.Controls.Add(plot2); // Middle
            this.splitContainer2.Panel2.Controls.Add(plot3); // Bottom

            var tests = Util.LoadTests();

            foreach (var item in tests)
            {
                comboFFTs.Items.Add(item);
            }

            signals = new Dictionary<string, Func<int, double[]>>();

            signals["Sine"] = SignalGenerator.Sine;
            signals["Sine 2"] = SignalGenerator.Sine2;
            signals["Sine 3"] = SignalGenerator.Sine3;
            signals["Square"] = SignalGenerator.Square;
            signals["Triangle"] = SignalGenerator.Triangle;
            signals["Sawtooth"] = SignalGenerator.Sawtooth;
            signals["Bump"] = SignalGenerator.Bump;
            signals["Test"] = SignalGenerator.TestArray;

            foreach (var item in signals)
            {
                comboSignals.Items.Add(item.Key);
            }

            for (int i = 7; i < 16; i++)
            {
                comboSize.Items.Add(Util.Pow(2, i));
            }
            comboSize.Items.Add(200);

            comboFFTs.SelectedIndex = 0;
            comboSignals.SelectedIndex = 0;
            comboSize.SelectedIndex = 0;

            comboFFTs.SelectedIndexChanged += combo_SelectedIndexChanged;
            comboSignals.SelectedIndexChanged += combo_SelectedIndexChanged;
            comboSize.SelectedIndexChanged += combo_SelectedIndexChanged;
        }

        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComputeFFT();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            ComputeFFT();
        }

        private OxyPlot.WindowsForms.PlotView CreateOxyPlot(int i)
        {
            var plot = new OxyPlot.WindowsForms.PlotView();

            plot.Dock = System.Windows.Forms.DockStyle.Fill;
            plot.Location = new System.Drawing.Point(0, 0);
            plot.Margin = new System.Windows.Forms.Padding(0);
            plot.Name = "plot" + i;
            plot.BackColor = Color.White;
            plot.Size = new System.Drawing.Size(500, 250);
            plot.TabIndex = 0;

            return plot;
        }

        private void ComputeFFT()
        {
            try
            {
                var fft = comboFFTs.SelectedItem as ITest;
                var generator = signals[comboSignals.SelectedItem.ToString()];
                var signal = generator((int)comboSize.SelectedItem);

                plot1.Model = PlotSignal(signal); // Input signal.
                plot2.Model = PlotSpectrum(fft.Spectrum(signal, false, out double[] restoredSignal));
                plot3.Model = PlotSignal(restoredSignal); // Inverse FFT.
            }
            catch (Exception e)
            {
                plot1.Model?.Series.Clear();
                plot1.Model?.InvalidatePlot(true);
                plot2.Model?.Series.Clear();
                plot2.Model?.InvalidatePlot(true);
                plot3.Model?.Series.Clear();
                plot3.Model?.InvalidatePlot(true);
                MessageBox.Show(e.Message);
            }
        }

        private PlotModel PlotSignal(double[] signal)
        {
            var pm = new PlotModel
            {
                Title = string.Empty,
                PlotType = PlotType.XY,
                Background = OxyColors.White
            };

            var ls = new LineSeries();
            for (int i = 0; i < signal.Length; i++)
            {
                ls.Points.Add(new DataPoint(i, signal[i]));
            }

            pm.Series.Add(ls);

            pm.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
            });

            pm.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                IsPanEnabled = false,
                IsZoomEnabled = false,
            });

            return pm;
        }

        private PlotModel PlotSpectrum(double[] spectrum)
        {
            var pm = new PlotModel
            {
                Title = string.Empty,
                PlotType = PlotType.XY,
                Background = OxyColors.White
            };

            var ls = new LineSeries();
            for (int i = 0; i < spectrum.Length; i++)
            {
                ls.Points.Add(new DataPoint(i, spectrum[i]));
            }

            pm.Series.Add(ls);

            pm.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                IsPanEnabled = false,
                IsZoomEnabled = false,
            });

            pm.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                IsPanEnabled = false,
                IsZoomEnabled = false,
            });

            return pm;
        }
    }
}
