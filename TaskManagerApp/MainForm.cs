using System;
using System.Windows.Forms;

namespace TaskManagerApp
{
    public partial class MainForm : Form
    {
        private TaskManager _taskManager = new TaskManager();

        public MainForm()
        {
            InitializeComponent();
            _taskManager.LoadFromFile();
            SetupFilter();
            LoadTasks();
        }

        private void SetupFilter()
        {
            cmbFilter.Items.Clear();
            cmbFilter.Items.Add("All");
            cmbFilter.Items.Add("Work");
            cmbFilter.Items.Add("Personal");
            cmbFilter.Items.Add("Study");
            cmbFilter.Items.Add("Other");
            cmbFilter.SelectedIndex = 0;
        }

        private void LoadTasks()
        {
            try
            {
                listBoxTasks.Items.Clear();
                var tasks = _taskManager.GetAllTasks();
                foreach (var task in tasks)
                {
                    listBoxTasks.Items.Add(task.GetDetails());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tasks: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new AddEditTaskForm(_taskManager);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadTasks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding task: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxTasks.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a task to edit.");
                    return;
                }

                int index = listBoxTasks.SelectedIndex;
                var oldTask = _taskManager.GetAllTasks()[index];
                var form = new AddEditTaskForm(_taskManager, oldTask);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadTasks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error editing task: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxTasks.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a task to delete.");
                    return;
                }

                var confirm = MessageBox.Show("Are you sure you want to delete this task?",
                    "Confirm Delete", MessageBoxButtons.YesNo);

                if (confirm == DialogResult.Yes)
                {
                    int index = listBoxTasks.SelectedIndex;
                    var task = _taskManager.GetAllTasks()[index];
                    _taskManager.DeleteTask(task);
                    LoadTasks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting task: " + ex.Message);
            }
        }

        private void btnMarkComplete_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxTasks.SelectedIndex == -1)
                {
                    MessageBox.Show("Please select a task to mark as complete.");
                    return;
                }

                int index = listBoxTasks.SelectedIndex;
                var task = _taskManager.GetAllTasks()[index];
                task.MarkComplete();
                _taskManager.SaveToFile();
                LoadTasks();
                MessageBox.Show("Task marked as complete!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error completing task: " + ex.Message);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.ToLower();
                if (string.IsNullOrWhiteSpace(keyword))
                {
                    LoadTasks();
                    return;
                }
                var filtered = _taskManager.SearchTasks(keyword);
                listBoxTasks.Items.Clear();
                foreach (var task in filtered)
                {
                    listBoxTasks.Items.Add(task.GetDetails());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message);
            }
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string selected = cmbFilter.SelectedItem.ToString();
                if (selected == "All")
                {
                    LoadTasks();
                    return;
                }
                var filtered = _taskManager.FilterByCategory(selected);
                listBoxTasks.Items.Clear();
                foreach (var task in filtered)
                {
                    listBoxTasks.Items.Add(task.GetDetails());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filter error: " + ex.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _taskManager.LoadFromFile();
            LoadTasks();
        }
    }
}
