using ScrumBoard.BLL.DTO;

namespace ScrumBoard.BLL.Interfaces;

public interface IBoardService : IDisposable
{
    List<BoardDTO> GetAllBoards();
    public void CreateBoard(string? name);
    public BoardDTO GetBoard(int? id);
    public void RemoveBoard(int? id);
}