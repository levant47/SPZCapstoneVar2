using System.Collections.Generic;

namespace SPZCapstoneVar2.UserControls
{
    public interface IElementUserControl
    {
        List<ConnectionPinUserControl> Pins { get; set; }
    }
}
