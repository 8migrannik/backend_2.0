using Microsoft.AspNetCore.Mvc;
using ScrumBoard.BLL.DTO;
using ScrumBoard.BLL.Interfaces;
using ScrumBoard.BLL.Services;

namespace ScrumBoard.WebAPI.Controllers;

[Route("api/boards")]
[ApiController]
public class BoardController : ControllerBase
{
    private readonly IBoardService _boardService;

    public BoardController(IBoardService boardService)
    {
        _boardService = boardService;
    }

    [HttpGet]
    public IActionResult GetAllBoards()
    {
        List<BoardDTO> boards;
        try
        {
            boards = _boardService.GetAllBoards();
        }
        catch (Exception e)
        {
            boards = new List<BoardDTO>();
        }
        return new OkObjectResult(boards);
    }

    [HttpPost]
    public IActionResult CreateBoard(string name)
    {
        try
        {
            _boardService.CreateBoard(name);
        }
        catch
        {
            return BadRequest();
        }
        return Ok();
    }
    
    
    [HttpGet("{id}")]
    public IActionResult GetBoard(int? id)
    {
        BoardDTO board;
        try
        {
            board = _boardService.GetBoard(id);
        }
        catch
        {
            return NotFound();
        }
        return Ok(board);
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteBoard(int? id)
    {
        try
        {
            _boardService.RemoveBoard(id);
        }
        catch
        {
            return NotFound();
        }
        return Ok();
    }
    
    
    [HttpPost("{boardId}/columns")]
    public IActionResult CreateColumn(int? boardId, string columnName)
    {
        try
        {
            ((BoardService)_boardService).CreateColumn(boardId, columnName);
        }
        catch
        {
            return NotFound("No such board");
        }
        return Ok();
    }
    
    [HttpPut("{boardId}/columns")]
    public IActionResult RenameColumn(int? boardId, int? columnId, string newName)
    {
        try
        {
            ((BoardService)_boardService).RenameColumn(boardId,columnId,newName);
        }
        catch
        {
            return NotFound("No such column");
        }
        return Ok();
    }
    
    [HttpDelete("{boardId}/columns/{columnId}")]
    public IActionResult RemoveColumn(int? boardId, int? columnId)
    {
        try
        {
            ((BoardService)_boardService).RemoveColumn(boardId,columnId);
        }
        catch
        {
            return NotFound("No such column");
        }
        return Ok();
    }
    
    
    [HttpPost("{boardId}/tasks")]
    public IActionResult CreateTask(int boardId, int? columnId, string taskName, string taskDescription, int priority)
    {
        try
        {
            ((BoardService)_boardService).CreateTask(boardId, columnId, taskName, taskDescription, priority);
        }
        catch
        {
            return NotFound("No such column or board");
        }
        return Ok();
    }
    
    
    [HttpPut("{boardId}/task")]
    public IActionResult ChangeTask(int boardId, int columnId, int taskId, string? newName, string? newDesc, int? newPrior)
    {
        try
        {
            ((BoardService)_boardService).ChangeTask(boardId,taskId,newName,newDesc,newPrior);
        }
        catch
        {
            return NotFound("No such task or board");
        }
        return Ok();
    }
    
    [HttpDelete("{boardId}/task/{taskId}")]
    public IActionResult RemoveTask(int boardId, int taskId)
    {
        try
        {
            ((BoardService)_boardService).RemoveTask(boardId,taskId);
        }
        catch
        {
            return NotFound("No such task or board");
        }
        return Ok();
    }
    
    
    [HttpPut("{boardId}/task/{taskId}")]
    public IActionResult MoveTask(int boardId, int taskId, int colToId, int newPriority)
    {
        try
        {
            ((BoardService)_boardService).MoveTask(boardId,taskId,colToId,newPriority);
        }
        catch
        {
            return NotFound("No such task or column");
        }
        return Ok();
    }
}