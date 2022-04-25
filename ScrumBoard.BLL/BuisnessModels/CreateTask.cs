using ScrumBoard.BLL.DTO;

namespace ScrumBoard.BLL.BuisnessModels;

public class CreateTask
{
    public string? Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public ColumnDTO Column { get; set; }
    
}