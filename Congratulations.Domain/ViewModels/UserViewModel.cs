using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulations.Domain.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DateBirthDay { get; set; }
        public string TypeUser { get; set; }
        public IFormFile Avatar { get; set; }
        public byte[] Image { get; set; }

    }
}
