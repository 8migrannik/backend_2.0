namespace ScrumBoard.BLL.DTO;
using ScrumBoard.DAL.Entities;

public class ColumnDTO
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public List<Task> Tasks { get; set; }

    public ColumnDTO(Column column)
    {
        this.Id = column.Id;
        this.Name = column.Name;
        this.Tasks = new(column.Tasks);
    }
    
}