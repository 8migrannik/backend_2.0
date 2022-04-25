namespace ScrumBoard.DAL.Entities
{
    public class Board
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public List<Column> Columns => new(this._columns);
        public List<Task> Tasks => new(this._tasks);

        private const int MaxCapacity = 10;
        private List<Column> _columns = new(MaxCapacity);
        private List<Task> _tasks = new(); //todo: do i need to store?

        public Board(string name, List<Column> columns = null)
        {
            //this.Id = id;
            this.Name = name;
            this.Id = name.GetHashCode(); // todo

            if (columns is not null)
                this._columns.AddRange(columns);

            if (this._columns is not null)
                foreach (var c in _columns)
                foreach (var t in c.Tasks)
                    this._tasks.Add(t);
        }

        public Board(Board other)
        {
            this.Name = other.Name;
            this._columns = new List<Column>(other._columns);
            this._tasks = new List<Task>(other._tasks);
        }
        public bool AddColumn(Column column)
        {
            if (this._columns.Count < MaxCapacity && column is not null)
            {
                this._columns.Add(column);
                return true;
            }

            return false;
        }

        public void AddTask(Task task)
        {
            if (this._tasks.Find(t => t.Name == task.Name && t.Column.Name == task.Column.Name) is not null)
                return;

            var col = task.Column;

            if (col is null)
                col = this._columns.First();

            if (!this._columns.Contains(col))
                this.AddColumn(col);
            //this._columns.Add(task.Column);

            if (!col.Tasks.Contains(task))
                col.AddTask(task);

            this._tasks.Add(task);
        }

        public void MoveTask(string taskName, Column columnFrom, Column columnTo, int newPriority) //todo: test
        {
            var task = this._tasks.Find(t => t.Name.ToLower() == taskName.ToLower()
                                             && t.Column.Name.ToLower() == columnFrom.Name.ToLower());
            RemoveTask(task);
            task.Priority = newPriority;
            task.Column = columnTo;
            AddTask(task);
        }

        public void RemoveTask(Task task) // todo: test
        {
            this._tasks.Remove(task);
            task.Column?.RemoveTask(task);
        }

        public void RemoveColumn(int? columnId)
        {
            var column = this.Columns.Find(c => c.Id == columnId);
            var tasks = column.Tasks;
            foreach (var t in tasks)
                this.RemoveTask(t);
            this._columns.Remove(column);

        }
    }
}