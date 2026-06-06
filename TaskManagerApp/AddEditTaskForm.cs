using System;
using System.Windows.Forms;

namespace TaskManagerApp
{
    public partial class AddEditTaskForm : Form
    {
        private TaskManager _taskManager;
        private BaseTask _existingTask;

        public AddEditTaskForm(TaskManager manager)
        {
            InitializeComponent();
            _taskManager = manager;
            _existingTask = null;
            Text = "Add New Task";
            SetupCategories();
        }

        public AddEditTaskForm(TaskManager manager, BaseTask task)
        {
            InitializeComponent();
            _taskManager = manager;
            _existingTask = task;
            Text = "Edit Task";
            SetupCategories();
            PopulateFields();
        }

        private void SetupCategories()
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.AddRange(new string[]
            {
                "Work",
                "Personal",
                "Study",
                "Other"
            });

            cmbCategory.SelectedIndex = 0;
        }

        private void PopulateFields()
        {
            if (_existingTask == null)
                return;

            txtTitle.Text = _existingTask.Title;
            txtDescription.Text = _existingTask.Description;
            dtpDueDate.Value = _existingTask.DueDate;
            cmbCategory.Text = _existingTask.Category;

            if (_existingTask is UrgentTask urgent)
            {
                chkUrgent.Checked = true;
                txtPriority.Text = urgent.PriorityLevel;
                txtPriority.Visible = true;
                lblPriority.Visible = true;
            }
        }

        private void chkUrgent_CheckedChanged(object sender, EventArgs e)
        {
            txtPriority.Visible = chkUrgent.Checked;
            lblPriority.Visible = chkUrgent.Checked;

            if (chkUrgent.Checked && string.IsNullOrWhiteSpace(txtPriority.Text))
            {
                txtPriority.Text = "High";
            }
        }

        private void txtDescription_TextChanged(object sender, EventArgs e)
        {
            // Empty method to fix designer error
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Title cannot be empty.");
                return false;
            }

            if (dtpDueDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Due date cannot be in the past.");
                return false;
            }

            if (chkUrgent.Checked &&
                string.IsNullOrWhiteSpace(txtPriority.Text))
            {
                MessageBox.Show("Enter a priority level.");
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            BaseTask task;

            if (chkUrgent.Checked)
            {
                task = new UrgentTask
                {
                    PriorityLevel = txtPriority.Text.Trim()
                };
            }
            else
            {
                task = new TaskItem();
            }

            task.Title = txtTitle.Text.Trim();
            task.Description = txtDescription.Text.Trim();
            task.DueDate = dtpDueDate.Value;
            task.Category = cmbCategory.Text;

            if (_existingTask != null)
            {
                task.IsComplete = _existingTask.IsComplete;
                _taskManager.UpdateTask(_existingTask, task);
                MessageBox.Show("Task updated successfully!");
            }
            else
            {
                _taskManager.AddTask(task);
                MessageBox.Show("Task added successfully!");
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}   
