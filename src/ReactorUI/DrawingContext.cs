using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReactorUI
{
    public class DrawingContext
    {
        private readonly SKSurface _surface;
        private readonly SKImageInfo _imageInfo;

        internal DrawingContext(SKSurface surface, SKImageInfo imageInfo)
        {
            _surface = surface;
            _imageInfo = imageInfo;
        }
    }
}
