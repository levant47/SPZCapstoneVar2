using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;

namespace SPZCapstoneVar2.Utilities
{
    public static class HitTestingUtilities
    {
        public static List<UIElement> GetAllHitUIElements(UIElement reference, Point point)
        {
            var allHitUIElements = new List<UIElement>();
            VisualTreeHelper.HitTest(reference, null, (hitTestResult) =>
            {
                allHitUIElements.Add(hitTestResult.VisualHit as UIElement ?? throw new System.Exception("WTF"));
                return HitTestResultBehavior.Continue;
            }, new PointHitTestParameters(point));
            if (allHitUIElements.Count > 1)
            {
                Debugger.Break();
            }
            return allHitUIElements;
        }

        public static bool IsUIElementHit(UIElement reference, UIElement target, Point point) =>
            GetAllHitUIElements(reference, point).Contains(target);
    }
}
