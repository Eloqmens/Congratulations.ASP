using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congratulations.Domain.Enum
{
    public enum TypeUser
    {
        [Display(Name = "Сотрудник")]
        Sotrudnik = 0,
        [Display(Name = "Друг")]
        Drug = 1,
        [Display(Name = "Знакомый")]
        Znakomie = 2,
    }
}
