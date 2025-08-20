using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskPro1.Helpers
{
    public class OnTapService : IAnimateOnTap
    {
        public async Task PulseAsync(object target)
        {
            if (target is VisualElement element)
            {
                await element.ScaleTo(0.8, 100, Easing.CubicOut);
                await element.ScaleTo(1, 100, Easing.CubicIn);
            }
        }
    }
}
