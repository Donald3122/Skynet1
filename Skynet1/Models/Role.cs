namespace Skynet1.Models
{
    public partial class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        // Навигационное свойство для связи с Employee
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
