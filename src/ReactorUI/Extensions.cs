using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReactorUI
{
    public static class Extensions
    {
        public static bool IsUndefined(this SKSize size) => float.IsPositiveInfinity(size.Width) && float.IsPositiveInfinity(size.Height);
    }
}
