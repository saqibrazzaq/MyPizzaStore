using System.ComponentModel.DataAnnotations;

namespace auth.Entities.Database
{
    public class Account
    {
        [Key]
        public Guid AccountId { get; set; } = Guid.NewGuid();
        
    }
}
