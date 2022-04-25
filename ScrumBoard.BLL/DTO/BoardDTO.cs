using ScrumBoard.DAL.Repositories;

 using Task = ScrumBoard.DAL.Entities.Task;
// using Column = ScrumBoard.DAL.Entities.Column;
// using Board = ScrumBoard.DAL.Entities.Board;
using ScrumBoard.DAL.Entities;
namespace ScrumBoard.BLL.DTO;

public class BoardDTO 
{
    public int? Id { get; set; }
    public string Name { get; init; }
    public List<Column> Columns { get; set; }
    public List<Task> Tasks { get; set; }

    public BoardDTO(Board board)
    {
        this.Id = board.Id;
        this.Name = board.Name;
        this.Columns = new(board.Columns);
        this.Tasks = new(board.Tasks);
    }
    
}