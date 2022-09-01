using FFTBench.Benchmark;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FFTBench
{
    public partial class FormMain : Form
    {
        OxyPlot.WindowsForms.PlotView plot;

        CancellationTokenSource cts;

        bool busy;

        List<ITest> tests;

        EnvironmentHelper env;
        string report;

        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeOxyPlot();

            comboSize1.SelectedIndex = 0;
            comboSize2.SelectedIndex = comboSize2.Items.Count - 1;
            comboRepeat.SelectedIndex = 1;

            tests = Util.LoadTests();
        }

        private void InitializeOxyPlot()
        {
            this.plot = new OxyPlot.WindowsForms.PlotView();

            this.plot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plot.Location = new System.Drawing.Point(0, 0);
            this.plot.Margin = new System.Windows.Forms.Padding(0);
            this.plot.Name = "plot";
            this.plot.BackColor = Color.White;
            this.plot.Size = new System.Drawing.Size(500, 250);
            this.plot.TabIndex = 0;

            this.panel1.Controls.Add(this.plot);
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            if (busy)
            {
                if (cts != null)
                {
                    statusLabel.Text = "Cancelling ...";
                    btnRun.Enabled = false;

                    cts.Cancel();
                }

                return;
            }

            busy = true;

            btnRun.Image = Properties.Resources.stop;

            List<int> testLengths = new List<int>();
            for (int index = comboSize1.SelectedIndex; index <= comboSize2.SelectedIndex; index++)
            {
                testLengths.Add(int.Parse(comboSize1.Items[index].ToString()));
            }

            int repeat = int.Parse(comboRepeat.SelectedItem.ToString());

            progressBar.Value = 0;
            progressBar.Minimum = 0;
            progressBar.Maximum = tests.Count * testLengths.Count;

            cts = new CancellationTokenSource();

            var progress = new Progress<Tuple<int, string>>(t =>
                {
                    string status = t.Item2;

                    if (status != null)
                    {
                        statusLabel.Text = "Testing " + status + " ...";
                    }

                    int k = t.Item1;

                    if (k < progressBar.Maximum)
                    {
                        progressBar.Value = k;
                    }
                });

            try
            {
                var results = await RunBenchmark(testLengths, repeat, tests, progress, cts.Token);

                this.plot.Model = CreatePlotModel(results);
                this.plot.Model.PlotView.ActualController.BindMouseDown(OxyMouseButton.Left, PlotCommands.ZoomRectangle);
                this.plot.Model.PlotView.ActualController.BindKeyDown(OxyKey.R, PlotCommands.Reset);

                //report = CreateReport(results);
                this.report = CreateReportHtml(results);
                System.IO.File.WriteAllText($"{System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}\\report.html",this.report);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                btnRun.Image = Properties.Resources.play;
                btnRun.Enabled = true;

                statusLabel.Text = string.Empty;

                progressBar.Value = 0;
                busy = false;
            }
        }

        private async Task<Dictionary<int, TestResult>> RunBenchmark(List<int> testLengths,
            int repeat, List<ITest> tests, IProgress<Tuple<int, string>> progress,
            CancellationToken cancellationToken)
        {
            return await Task<Dictionary<int, TestResult>>.Run(() =>
            {
                // Group by FFT size.
                var results = new Dictionary<int, TestResult>();

                var benchmark = new BenchmarkRunner();

                int k = 0;

                foreach (var test in tests)
                {
                    if (!test.Enabled)
                    {
                        continue;
                    }

                    var name = test.ToString();

                    progress.Report(new Tuple<int, string>(k, name));

                    for (int i = 0; i < testLengths.Count; i++)
                    {
                        int size = testLengths[i];

                        var data = SignalGenerator.Sawtooth(size);

                        var result = benchmark.Run(test, data, repeat);

                        if (results.ContainsKey(size))
                        {
                            results[size].Add(name, result);
                        }
                        else
                        {
                            // TestResult will group by name.
                            results.Add(size, new TestResult(name, result));
                        }

                        progress.Report(new Tuple<int, string>(k++, null));

                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }

                return results;
            });
        }

        private static PlotModel CreatePlotModel(Dictionary<int, TestResult> results)
        {
            var model = new PlotModel
            {
                Title = "Benchmark Result",
                IsLegendVisible = true,
            };
            model.Legends.Add(new OxyPlot.Legends.Legend()
            {
                IsLegendVisible = true,
                LegendPlacement = OxyPlot.Legends.LegendPlacement.Outside,
                LegendPosition = OxyPlot.Legends.LegendPosition.RightMiddle,
                LegendOrientation = OxyPlot.Legends.LegendOrientation.Vertical,
                LegendBorderThickness = 0,
            });

            var categoryAxis = new CategoryAxis 
            {
                Position = AxisPosition.Bottom,
                Key = "Category",
                Title = "Size",
            };

            var valueAxis = new LinearAxis
            {
                Key = "Value",
                Unit = "ms",
                Title = "Total run time",
                Position = AxisPosition.Left,
                MinimumPadding = 0,
                MaximumPadding = 0.06,
                AbsoluteMinimum = -1,
                ExtraGridlines = new double[] { 0 },
                ExtraGridlineStyle = LineStyle.Solid,
                ExtraGridlineThickness = 1,
                ExtraGridlineColor = OxyColors.Red,
                MajorGridlineThickness = 1,
                MinorGridlineThickness = 1,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColors.Gray,
                MinorGridlineColor = OxyColors.LightGray,
                IsZoomEnabled = true,
            };

            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);

            var series = new Dictionary<string, BarSeries>();

            BarSeries s;

            foreach (var item in results)
            {
                int size = item.Key;
                var benchmark = item.Value;

                foreach (var result in benchmark.Results)
                {
                    var name = result.Key;

                    if (!series.TryGetValue(name, out s))
                    {
                        s = new BarSeries()
                        {
                            Title = name,
                            IsStacked = false,
                            StrokeColor = OxyColors.Black,
                            StrokeThickness = 1,
                            XAxisKey = "Value",
                            YAxisKey = "Category"
                        };

                        series[name] = s;
                    }

                    s.Items.Add(new BarItem { Value = result.Value.Total });
                }

                categoryAxis.Labels.Add(size.ToString());
            }

            foreach (var item in series.Values)
            {
                model.Series.Add(item);
            }

            return model;
        }

        private void btnExplore_Click(object sender, EventArgs e)
        {
            var form = new FormExplorer();

            form.ShowDialog();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            var form = new FormChoose();

            form.SetDataContext(tests);
            form.ShowDialog();
        }

        private async void btnCopy_Click(object sender, EventArgs e)
        {
            btnCopy.Enabled = false;

            string report = await GetReport();

            Clipboard.SetText(report);

            btnCopy.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var sfd = new SaveFileDialog();

            sfd.FileName = "benchmark.png";
            sfd.Filter = "PNG file (*.png)|*.png";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (plot.Model != null)
                {
                    PngExporter.Export(plot.Model, sfd.FileName, 600, 400);
                }
            }
        }

        private async Task<string> GetReport()
        {
            return await Task<string>.Run(() =>
            {
                if (env == null)
                {
                    env = EnvironmentHelper.GetCurrentInfo();
                }

                var sb = new StringBuilder();

                sb.AppendLine(env.ToFormattedString());
                sb.Append(report);

                return sb.ToString();
            });
        }

        private string CreateReport_(Dictionary<int, TestResult> results)
        {
            var s = new StringBuilder();

            foreach (var item in results)
            {
                s.AppendLine();
                s.AppendFormat("Size: {0}", item.Key);
                s.AppendLine();

                foreach (var result in item.Value.Results)
                {
                    s.AppendFormat("{0,16}: ", result.Key);
                    s.AppendFormat("{0,6} ", result.Value.Total);
                    s.AppendFormat("[{0:0.00,6}]\n", result.Value.Average);
                }
            }

            return s.ToString();
        }

        // Generate HTML table (for CodeProject article). 
        private string CreateReportHtml(Dictionary<int, TestResult> results)
        {
            var s = new StringBuilder();

            var sorted = new Dictionary<string, Dictionary<int, BenchmarkResult>>();

            // Preprocess:
            foreach (var item in results)
            {
                int size = item.Key;

                foreach (var result in item.Value.Results)
                {
                    string name = result.Key;

                    Dictionary<int, BenchmarkResult> r;

                    if (!sorted.TryGetValue(name, out r))
                    {
                        r = new Dictionary<int, BenchmarkResult>();
                        sorted.Add(name, r);
                    }

                    r.Add(size, result.Value);
                }
            }

            s.AppendLine("<style>\r\n" +
                "td {\r\n\ttext-align: center;\r\n}\r\n\r\n" +
                ".textLeft {\r\n\ttext-align: left;\r\n}\r\n\r\n" +
                ".rightBorder {\r\n  border-right: 1px solid black;\r\n  border-collapse: collapse;\r\n}\r\n\r\n" +
                "tr:nth-child(even) {\r\n  background-color: #D6EEEE;\r\n}\r\n" +
                "</style>");

            s.AppendLine("<table>");
            
            s.AppendLine("\t\t<tr>");
            s.Append("\t\t\t<th class=\"rightBorder\"></th>");
            foreach (var size in sorted.First().Value.Keys)
            {
                s.Append("<th colspan=\"3\" class=\"rightBorder\">Size</th>");
            }
            s.AppendLine("\n\t\t</tr>");

            s.Append("\t\t\t<th class=\"rightBorder\"></th>");
            foreach (var size in sorted.First().Value.Keys)
            {
                s.Append("<th colspan=\"3\" class=\"rightBorder\">" + size.ToString() + "</th>");
            }
            s.AppendLine("\n\t\t</tr>");

            s.AppendLine("\t\t<tr>");
            s.Append("\t\t\t<th class=\"rightBorder textLeft\">Name</th>");
            foreach (var size in sorted.First().Value.Keys)
            {
                s.Append("<th>Total [ms]</th><th>Average [ms]</th><th class=\"rightBorder\">Average [ticks]</th>");
            }
            s.AppendLine("\n\t\t</tr>");

            foreach (var item in sorted)
            {
                s.AppendLine("\t\t<tr>");
                s.AppendFormat("\t\t\t<td class=\"rightBorder textLeft\">" + item.Key + "</td>");

                foreach (var result in item.Value)
                {
                    s.AppendFormat("<td>" + result.Value.Total + "</td>");
                    s.AppendFormat("<td>" + result.Value.Average.ToString("0.00") + "</td>");
                    s.AppendFormat("<td class=\"rightBorder\">" + result.Value.AverageTicks.ToString("0.00") + "</td>");
                }

                s.AppendLine("\n\t\t</tr>");
            }
            s.AppendLine("\n</table>");

            return s.ToString();
        }
    }
}
