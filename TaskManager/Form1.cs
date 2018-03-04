using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Permissions;


namespace TaskManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
      
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Process process in Process.GetProcesses())
            {
                dataGridView1.Rows.Add(User.ProcessName(process), User.ProcessId(process), User.ProcessMainWindowHandle(process), User.WorkingSet(process),
                    User.StartTime(process), User.ThreadsC(process), User.ModulesC(process), User.GetProcessOwner(int.Parse(User.ProcessId(process))), User.ProsessPriority(process));
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                StringBuilder str = new StringBuilder();
                Process process = new Process();
                process = Process.GetProcessById(int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()));
                try
                {
                    foreach (ProcessModule module in process.Modules)
                    {
                        str.Append(string.Format("Module Name: {0} Memory Adress: {1} \nRequired Memory: {2}\n", module.ModuleName, module.BaseAddress, module.ModuleMemorySize));
                    }
                    MessageBox.Show(str.ToString());
                }
                catch
                {
                    MessageBox.Show("Access is denied");
                }
            }
            else if (e.ColumnIndex == 5)
            {
                StringBuilder str = new StringBuilder();
                Process process = new Process();
                process = Process.GetProcessById(int.Parse(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()));
                try
                {
                    foreach (ProcessThread thread in process.Threads)
                    {
                        str.Append(string.Format("ThreadId: {0}  StartTime: {1} Priority Level: {2}\n", thread.Id, thread.StartTime, thread.PriorityLevel));
                    }
                    MessageBox.Show(str.ToString());
                }
                catch
                {
                    MessageBox.Show("Access is denied");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process proc = Process.GetProcessById(int.Parse(dataGridView1[1, dataGridView1.CurrentCellAddress.Y].Value.ToString()));
                proc.Kill();
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
            }
            catch
            {
                MessageBox.Show("Access denied");
            }
        }
    }
}
