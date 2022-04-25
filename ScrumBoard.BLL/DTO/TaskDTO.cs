namespace ScrumBoard.BLL.DTO;
using ScrumBoard.DAL.Entities;

public class TaskDTO
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Priority { get; set; }
    public Column Column { get; set; }

    public TaskDTO(Task task)
    {
        this.Id = task.Id;
        this.Name = task.Name;
        this.Description = task.Description;
        this.Priority = task.Priority;
        this.Column = task.Column;
    }
}