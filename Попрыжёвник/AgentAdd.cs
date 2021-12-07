using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Попрыжёвник
{
    public partial class AgentAdd : Form
    {
        public Model1 db { get; set; }
        public Agent ag  { get; set; }
        public AgentAdd()
        {
            InitializeComponent();
        }

        private void AgentAdd_Load(object sender, EventArgs e)
        {
            if (ag == null)
            {
                agentBindingSource.AddNew();
                this.Text = "New driver";
            }
            else
            {
                agentBindingSource.Add(ag);
                this.Text = "Correct";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ag == null)
            {
                ag = (Agent)agentBindingSource.List[0];
                db.Agent.Add(ag);
            }
            try
            {
                db.SaveChanges();
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(" Error " + ex.InnerException.InnerException.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
