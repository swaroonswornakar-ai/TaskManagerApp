using System;

namespace TaskManagerApp
{
    public abstract class BaseTask : ITask
    {
        private string _title;
        private DateTime _dueDate;
        private bool _isComplete;

        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Title cannot be empty.");
                _title = value;
            }
        }

        public DateTime DueDate
        {
            get { return _dueDate; }
            set { _dueDate = value; }
        }

        public bool IsComplete
        {
            get { return _isComplete; }
            set { _isComplete = value; }
        }

        public string Category { get; set; }
        public string Description { get; set; }

        public void MarkComplete()
        {
            _isComplete = true;
        }

        public abstract string GetDetails();
    }
}
