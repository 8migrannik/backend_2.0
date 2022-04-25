namespace ScrumBoard.DAL.Entities
{
    public class Column
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        private List<Task> _tasks;

        public List<Task> Tasks
        {
            get => new(_tasks);
            set { this._tasks = new List<Task>(value); }
        }

        public void SortTasksByPriority(List<Task> tasks)
        {
            if (tasks.Count == 0) return;

            for (int i = 0; i < tasks.Count - 1; i++)
            for (int j = 0; j < tasks.Count - i - 1; j++)
                if (tasks[j].Priority > tasks[j + 1].Priority)
                    (tasks[j], tasks[j + 1]) = (tasks[j + 1], tasks[j]);

            for (int i = 0; i < this._tasks.Count - 1; i++)
                if (_tasks[i].Priority == _tasks[i + 1].Priority)
                    _tasks[i + 1].Priority++;

            while (tasks[0].Priority > 1)
            {
                foreach (var t in tasks)
                    t.Priority--;
            }
        }


        public Column(string name, List<Task> tasks = null)
        {
            this.Name = name;
            this.Id = name.GetHashCode();

            if (tasks is null)
                this._tasks = new();
            else this._tasks = tasks.ToList();

            foreach (var t in _tasks)
                t.Column = this;


            SortTasksByPriority(_tasks);
        }

        public void AddTask(Task task)
        {
            task.Column = this;

            if (task.Priority > this._tasks.Count)
            {
                if (_tasks.Count == 0)
                    task.Priority = 1;
                else task.Priority = _tasks.Count+1;
            }

            if (this._tasks.Count == 0)
                this._tasks.Add(task);
            else this._tasks.Insert(task.Priority - 1, task);


            SortTasksByPriority(_tasks);
        }

        public void RemoveTask(Task task)
        {
            //task.Column = null;
            this._tasks.Remove(task);
            SortTasksByPriority(_tasks);
        }
    }
}