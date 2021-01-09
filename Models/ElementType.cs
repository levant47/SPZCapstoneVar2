using System.ComponentModel;

namespace SPZCapstoneVar2.Models
{
    public enum ElementType
    {
        [Description("AND gate")]
        AND_GATE = 1,
        [Description("OR gate")]
        OR_GATE = 2,
        [Description("NOT gate")]
        NOT_GATE = 3,
    }
}
