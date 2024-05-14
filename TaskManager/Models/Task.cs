
namespace TaskManager.Models
{
    public class Task
    {
        public Task(string name, bool isDone, string description, int id){
            Name = name;
            IsDone = isDone;
            Description = description;
            Id = id;
        }
        public string Name { get; set; }
        public bool IsDone { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
    }

}