using SkiaSharp;
using System;

namespace ReactorUI
{
    public abstract class VisualNode
    {
        public VisualNode Parent { get; private set; }

        public VisualNodeCollection Children { get; }

        public VisualNode()
        {
            Children = new VisualNodeCollection(this, ChildAdded, ChildRemoved);
        }

        private void ChildAdded(VisualNode node, int index)
        {
            node.Parent = this;
        }

        private void ChildRemoved(VisualNode node, int index)
        {
            node.Parent = null;
        }


        ///Measure Pass
        private SKSize _previousAvailableSize;
        private bool _measureIsDirty = true;
        public void Measure(SKSize availableSize)
        {
            if (_layoutSuspended)
                return;

            if (!this.IsVisible)
            {
                this.DesiredSize = new Size();
                this._measureIsDirty = false;
                return;
            }

            var isCloseToPreviousMeasure = _previousAvailableSize.IsEmpty ? false : availableSize.IsCloseTo(this._previousAvailableSize);

            if (!this._measureIsDirty && isCloseToPreviousMeasure)
                return;

            this._previousAvailableSize = availableSize;
            var desiredSize = this.MeasureCore(availableSize);
            if (double.IsNaN(desiredSize.Width) ||
                double.IsInfinity(desiredSize.Width) ||
                double.IsNaN(desiredSize.Height) ||
                double.IsInfinity(desiredSize.Height))
                throw new ArgumentException("measure pass must return valid size");

            this.DesiredSize = desiredSize;
            this._measureIsDirty = false;
        }

        protected virtual SKSize MeasureCore(SKSize availableSize)
        {
            return new SKSize();
        }
        public void Render(SKRect layoutRect, DrawingContext context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (layoutRect.IsEmpty)
                return;

            RenderCore(layoutRect, context);
        }

        protected abstract void RenderCore(SKRect layoutRect, DrawingContext context);
    }
}
