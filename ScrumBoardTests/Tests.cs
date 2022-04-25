using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ScrumBoardLibrary;

namespace ScrumBoardTests
{
    [TestFixture]
    public class Tests
    {
        // AddColumn tests
        [Test]
        public void AddColumn_Adding()
        {
            // arrange
            var col = new Column("Column");
            var board = new Board("Board");
            
            // act
            board.AddColumn(col);

            // assert
            Assert.True(board.Columns.Contains(col));
        }

        [Test]
        public void AddColumn_Overloading()
        {
            // arrange
            var board = new Board("Board");
            for (int i = 1; i <= 10; i++)
            {
                var col = new Column(i.ToString());
                board.AddColumn(col);
            }

            // act
            bool wasAdded = board.AddColumn(new Column("11th column"));
            
            // assert
            Assert.False(wasAdded);
        }
        
        
        // AddTask tests
        [Test]
        public void AddTask_AddingWithNullColumn()
        {
            // arrange
            var task = new Task("task", "task desc", 1);
            var board = new Board("Board", new List<Column> { new Column("Column") });

            // act
            board.AddTask(task);
            
            // assert
            Assert.True(board.Columns.First().Tasks.Contains(task));
        }
        
        [Test]
        public void AddTask_AddingWithColumn()
        {
            // arrange
            var task = new Task("task", "task desc", 1, new Column("Column"));
            var board = new Board("Board");
            
            // act
            board.AddTask(task);
            
            // assert
            Assert.True(board.Columns.Contains(task.Column) && board.Tasks.Contains(task));
        }
        
        // MoveTask tests
        [Test]
        public void MoveTask_Moving()
        {
            // arrange
            var col1 = new Column("Column 1");
            var col2 = new Column("Column 2");
            
            var task = new Task("task", "task desc", 1);
            var board = new Board("Board", new List<Column>{col1,col2});
            board.AddTask(task);
            
            // act
            board.MoveTask(task.Name,task.Column,col2,1);
            
            // assert
            Assert.True(board.Columns.Find(c => c.Name == col2.Name).Tasks.Contains(task) &&
                        !board.Columns.Find(c => c.Name == col1.Name).Tasks.Contains(task));
        }
        
        // RemoveTask tests
        [Test]
        public void RemoveTask_Removing()
        {
            // arrange
            var task = new Task("task", "task desc", 1, new Column("Column"));
            var board = new Board("Board");
            board.AddTask(task);

            // act
            board.RemoveTask(task);
            
            // assert
            Assert.True(!board.Tasks.Contains(task) && 
                        !board.Columns.Find(c => c.Name == task.Column.Name).Tasks.Contains(task));
        }
    }
}