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
            listView1.Items.Clear();

            foreach (var item in tests)
            {
                var lvi = new ListViewItem(new string[] { "", item.ToString() });

                lvi.Checked = item.Enabled;
                lvi.Tag = item;

                listView1.Items.Add(lvi);
            }
        }

        private void listView1_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            var item = e.Item.Tag as ITest;

            if (item != null)
            {
                item.Enabled = e.Item.Checked;
            }
        }
    }
}
