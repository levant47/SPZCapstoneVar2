﻿using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace SPZCapstoneVar2.UserControls
{
    public partial class NORElementUserControl : UserControl, IElementUserControl
    {
        public List<ConnectionPinUserControl> Pins { get; set; }

        public NORElementUserControl()
        {
            InitializeComponent();

            Pins = new List<ConnectionPinUserControl> { OutputPin, InputPin1, InputPin2 };
        }
    }
}
