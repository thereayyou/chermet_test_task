using System.ComponentModel.DataAnnotations;

namespace testTask.Models
{
    /// <summary>
    ///  Модель для описани таблицы Department
    /// </summary>
    public class Department
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
