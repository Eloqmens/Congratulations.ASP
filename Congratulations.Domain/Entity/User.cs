using Congratulations.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulations.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TypeUser TypeUser { get; set; }
        public DateTime DateBirthDay { get; set; }
        public byte[]? Avatar { get; set; }
    }
}
