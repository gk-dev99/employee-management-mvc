using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.ViewModels
{
    public class EmployeeVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required")]
        [StringLength(50,ErrorMessage ="Name cannot exceed 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage ="Age is required")]
        [Range(18,60,ErrorMessage ="Age must be between 18 and 60")]
        public int Age { get; set; }
    }
}
