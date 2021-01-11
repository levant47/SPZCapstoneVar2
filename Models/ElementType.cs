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
        [Description("XOR gate")]
        XOR_GATE = 4,
        [Description("NAND gate")]
        NAND_GATE = 5,
        [Description("NOR gate")]
        NOR_GATE = 6,
        [Description("XNOR gate")]
        XNOR_GATE = 7,
        [Description("Input Element")]
        INPUT_ELEMENT = 8,
        [Description("Output Element")]
        OUTPUT_ELEMENT = 9,
    }
}
