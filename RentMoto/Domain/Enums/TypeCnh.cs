using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum TypeCnh
    {
        [Description("Type A")]
        A,
        [Description("Type B")]
        B,
        [Description("Type A and B")]
        Both
    }
}
