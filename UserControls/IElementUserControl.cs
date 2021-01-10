using System.Collections.Generic;

namespace SPZCapstoneVar2.UserControls
{
    public interface IElementUserControl
    {
        List<ConnectionPinUserControl> GetConnectionPins();

        List<ConnectionPinUserControl> GetInputConnectionPins();
    }
}
