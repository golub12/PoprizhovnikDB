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
    public partial class AgentForm : Form
    {
        Model1 db = new Model1();
        public AgentForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            agentBindingSource.DataSource = db.Agent.ToList();
            agentTypeBindingSource.DataSource = db.AgentType.ToList();
        }

        private void agentBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            Agent agent = (Agent)agentBindingSource.Current;
            try
            {
                if (agent == null) return;
                if (agent.Logo != "")
                {
                    string str = agent.Logo.Substring(1);
                    LogoPict.Image = Image.FromFile(str);
                }
                else
                {
                    LogoPict.Image = Image.FromFile("agents\\picture.png");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AgentAdd frm = new AgentAdd();
            frm.db = db;
            frm.ag = null;
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                agentBindingSource.DataSource = null;
                agentBindingSource.DataSource = db.Agent.ToList();
            }
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.InnerException.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Agent ag = (Agent)agentBindingSource.Current;
            AgentAdd frm = new AgentAdd();
            frm.db = db;
            frm.ag = ag;
            DialogResult dr = frm.ShowDialog();
            if (dr == DialogResult.OK)
            {
                agentBindingSource.DataSource = null;
                agentBindingSource.DataSource = db.Agent.ToList();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Agent ag = (Agent)agentBindingSource.Current;
            DialogResult dr = MessageBox.Show("Удалить " + ag.Title + "?", "Удаление",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                db.Agent.Remove(ag);
                try
                {
                    db.SaveChanges();
                    agentBindingSource.DataSource = db.Agent.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.InnerException.InnerException.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                agentBindingSource.DataSource = db.Agent.OrderByDescending(ag => ag.Priority).ToList();
            }
            else
            {
                agentBindingSource.DataSource = db.Agent.OrderBy(a => a.Priority).ToList();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                agentBindingSource.DataSource = db.Agent.OrderByDescending(ag => ag.Title).ToList();
            }
            else
            {
                agentBindingSource.DataSource = db.Agent.OrderBy(a => a.Title).ToList();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            agentBindingSource.DataSource = db.Agent.OrderBy(ag => ag.ID).ToList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            agentBindingSource.DataSource = db.Agent.Where(p => p.Title.Contains(textBox1.Text)
            || p.Email.Contains(textBox1.Text)).ToList();
        }
    }
}
