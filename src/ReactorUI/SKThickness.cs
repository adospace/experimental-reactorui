using SkiaSharp;
using System;
using System.Drawing;

namespace ReactorUI
{
    public struct SKThickness
    {
        public SKThickness(float uniformLength)
        {
            Left = Top = Right = Bottom = uniformLength;
        }

        public SKThickness(float left, float top, float right, float bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public float Left { get; private set; }

        public float Top { get; private set; }

        public float Right { get; private set; }

        public float Bottom { get; private set; }

        public bool IsCloseTo(SKThickness other) =>
            Math.Abs(Left - other.Left) < 1e-10 &&
            Math.Abs(Top - other.Top) < 1e-10 &&
            Math.Abs(Right - other.Right) < 1e-10 &&
            Math.Abs(Bottom - other.Bottom) < 1e-10;

        public SKSize ToSize() => new SKSize(Left + Right, Top + Bottom);

        public bool IsUniformLength => (Left == Right && Left == Top && Left == Bottom);

        public float UniformLength => IsUniformLength ? Left : throw new InvalidOperationException();

        public bool Any() => Left > 0.0 || Right > 0.0 || Top > 0.0 || Bottom > 0.0;
    }
}