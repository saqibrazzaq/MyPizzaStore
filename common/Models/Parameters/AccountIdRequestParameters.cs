using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.Models.Parameters
{
    public class AccountIdRequestParameters
    {
        [Required]
        public Guid? AccountId { get; set; }
    }
}
