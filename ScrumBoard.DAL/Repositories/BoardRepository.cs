using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
using ScrumBoard.DAL.Entities;
using ScrumBoardLibrary.Interfaces;
using Task = ScrumBoard.DAL.Entities.Task;

namespace ScrumBoard.DAL.Repositories
{
    public class BoardRepository : IRepository<Board>
    {
        private readonly IMemoryCache _memoryCache;

        public BoardRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public List<Board> GetAll()
        {
            _memoryCache.TryGetValue("boards", out List<Board> boards);
            return boards;
        }

        public Board Get(int? id) // todo: exception "no such item"
        {
            return GetAll().Find(b => b.Id == id);
        }

        public IEnumerable<Board> Find(Func<Board, bool> predicate) // todo
        {
            throw new NotImplementedException();
        }

        public void Create(string name)
        {
            var boards = GetAll();

            if (boards is null) boards = new List<Board>();
            // if (boards.Count == 0)
            //     boards = new List<Board>();


            var board = new Board(name);

            bool isDuplicate = false;
            foreach (var b in boards)
                if (b.Id == board.Id)
                    isDuplicate = true;

            if (!isDuplicate)
                boards.Add(board);

            _memoryCache.Set("boards", boards);
        }

        public void Update(Board board) // todo: not needed
        {
            throw new NotImplementedException();
        }

        public void Remove(int? id)
        {
            var boards = GetAll();
            for (int i = 0; i < boards.Count; i++)
                if (boards[i].Id == id)
                {
                    boards.RemoveAt(i);
                    break;
                }

            _memoryCache.Set("boards", boards);
        }

        public Column CreateColumn(int? boardId, string columnName)
        {
            var board = Get(boardId);
            var col = new Column(columnName);
            board.AddColumn(col);
            return col;
        }

        public void RenameColumn(int? boardId, int? columnId, string newName)
        {
            var columns = Get(boardId).Columns;
            var col = columns.Find(c => c.Id == columnId);
            col.Name = newName;
        }

        public void RemoveColumn(int? boardId, int? columnId)
        {
            var board = Get(boardId);
            board.RemoveColumn(columnId);
        }

        public Task CreateTask(int boardId, int? columnId, string taskName, string taskDescription, int priority)
        {
            var board = Get(boardId);
            var col = board.Columns.Find(c => c.Id == columnId);
            var task = new Task(taskName, taskDescription, priority);
            task.Column = col;
            board.AddTask(task);
            return task;
        }

        public void ChangeTask(int boardId, int taskId, string? newName, string? newDesc, int? newPrior)
        {
            var task = Get(boardId).Tasks.Find(t => t.Id == taskId);
            task.Name = newName ?? task.Name;
            task.Description = newDesc ?? task.Description;
            task.Priority = newPrior ?? task.Priority;

            int capacity = task.Column.Tasks.Count;
;            if (task.Priority > capacity)
                task.Priority = capacity;
        }

        public void RemoveTask(int boardId, int taskId)
        {
            var board = Get(boardId);
            var task = board.Tasks.Find(t => t.Id == taskId);
            board.RemoveTask(task);
        }

        public void MoveTask(int boardId, int taskId, int colToId, int newPriority)
        {
            var board = Get(boardId);
            var task = board.Tasks.Find(t => t.Id == taskId);
            var colFrom = task.Column;
            var colTo = board.Columns.Find(c => c.Id == colToId);
            board.MoveTask(task.Name,colFrom,colTo,newPriority);
        }
    }
}