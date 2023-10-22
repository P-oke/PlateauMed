using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateauMedTask.Domain.Entities.Enum
{
    public enum Title
    {
        [Description("Mr.")]
        Mr = 1,
        [Description("Mrs.")]
        Mrs,
        [Description("Miss")]
        Miss,
        [Description("Dr.")]
        Dr,
        [Description("Professor")]
        Prof


    }
}
