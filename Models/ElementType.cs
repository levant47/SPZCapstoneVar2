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
        [Description("Input Element")]
        INPUT_ELEMENT = 4,
        [Description("Output Element")]
        OUTPUT_ELEMENT = 5,
        [Description("XOR gate")]
        XOR_GATE = 6,
        [Description("NAND gate")]
        NAND_GATE = 7,
        [Description("NOR gate")]
        NOR_GATE = 8,
    }
}
