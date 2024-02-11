using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApmDijkstra
{
    public partial class initial : Form
    {
        public initial()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            map m = new map();
            m.Show();
            this.Hide();
        }
    }
}
