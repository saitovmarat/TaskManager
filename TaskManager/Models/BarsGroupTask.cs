
namespace TaskManager.Models
{
    
    public class BarsGroupTask(string name, bool isDone, string description, int id)
    {
        public string Name { get; set; } = name;
        public bool IsDone { get; set; } = isDone;
        public string Description { get; set; } = description;
        public int Id { get; set; } = id;
    }

}