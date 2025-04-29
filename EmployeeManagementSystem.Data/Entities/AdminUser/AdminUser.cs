using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Entities
{
    public class AdminUser
    {
        public Guid Id { get; set; }

        public string EMail { get; set; }

        [NotMapped]  // Bunu UI tarafında kullanıcağımdan dolayı veri tabanına yansımasını istemedim, veri tabanında HashedPassword benim için yeterli
        public string Password { get; set; }

        public string HashedPassword { get; set; }

    }
}
