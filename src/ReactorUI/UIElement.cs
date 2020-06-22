using System;
using System.Collections.Generic;
using System.Text;

namespace ReactorUI
{
    public abstract class UIElement : VisualNode
    {
        private Thickness _margin;
        public Thickness Margin
        {
            get => _margin;
            set
            {
                _margin = value;
                InvalidateMeasure();
            }

        }


    }
}
