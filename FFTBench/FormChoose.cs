using FFTBench.Benchmark;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FFTBench
{
    public partial class FormChoose : Form
    {
        public FormChoose()
        {
            InitializeComponent();
        }

        internal void SetDataContext(List<ITest> tests)
        {
            this.tests.Items.Clear();

            foreach (var item in tests)
            {
                this.tests.Items.Add(
                    new ListViewItem(new string[] { "", item.ToString() })
                    {
                        Checked = item.Enabled,
                        Tag = item
                    });
            }
        }

        private void Tests_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Tag is ITest test)
            {
                test.Enabled = e.Item.Checked;
            }
        }

        private void DeselectAll_Click(object sender, System.EventArgs e)
        {
            foreach(ListViewItem item in tests.Items)
            {
                if (item.Tag is ITest test)
                {
                    test.Enabled =
                    item.Checked = false;
                }
            }
        }
    }
}
