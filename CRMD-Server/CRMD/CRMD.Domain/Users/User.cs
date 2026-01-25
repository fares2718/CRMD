using CRMD.Domain.Attributes;

namespace CRMD.Domain.Users
{
    public class User
    {
        [IgnoreOn(enOperationMode.Add)]
        public int UserId { get; set; }
        [IgnoreOn(enOperationMode.Update)]
        public int EmployeeId { get; set; }
        [IgnoreOn(enOperationMode.Update)]
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

    }
}