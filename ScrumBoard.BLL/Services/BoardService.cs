using Microsoft.Extensions.Caching.Memory;
using ScrumBoard.BLL.DTO;
using ScrumBoard.BLL.Interfaces;
using ScrumBoard.DAL.Entities;
using ScrumBoard.DAL.Repositories;
using ScrumBoardLibrary.Interfaces;

namespace ScrumBoard.BLL.Services;

public class BoardService : IBoardService
{
    private readonly IRepository<Board> _repository;

    public BoardService(IRepository<Board> repository)
    {
        this._repository = repository;
    }


    public void CreateBoard(string? name)
    {
        this._repository.Create(name);
    }

    public BoardDTO GetBoard(int? id)
        => new BoardDTO(_repository.GetAll().Find(b => b.Id == id));
    

    public void RemoveBoard(int? id)
    {
        this._repository.Remove(id);
    }

    public List<BoardDTO> GetAllBoards()
    {
        var boards = _repository.GetAll();
        var boardsDto = boards.Select(b => new BoardDTO(b)).ToList();
        return boardsDto;
    }

    public ColumnDTO CreateColumn(int? boardId, string columnName)
        => new ColumnDTO(((BoardRepository)_repository).CreateColumn(boardId, columnName));


    public void RenameColumn(int? boardId, int? columnId, string newName)
    {
        ((BoardRepository)_repository).RenameColumn(boardId, columnId, newName);
    }


    public void RemoveColumn(int? boardId, int? columnId)
    {
        ((BoardRepository)_repository).RemoveColumn(boardId, columnId);
    }

    public TaskDTO CreateTask(int boardId, int? columnId, string taskName, string taskDescription, int priority)
        => new TaskDTO(((BoardRepository)_repository).CreateTask(boardId, columnId, taskName, taskDescription, priority));

    public void ChangeTask(int boardId, int taskId, string? newName, string? newDesc, int? newPrior)
    {
        ((BoardRepository)_repository).ChangeTask(boardId, taskId, newName, newDesc, newPrior);
    }

    public void RemoveTask(int boardId, int taskId)
    {
        ((BoardRepository)_repository).RemoveTask(boardId,taskId);
    }

    public void MoveTask(int boardId, int taskId, int colToId, int newPriority)
    {
        ((BoardRepository)_repository).MoveTask(boardId,taskId,colToId,newPriority);
    }
    
    
    
    void IDisposable.Dispose()
    {
        //todo
    }
}