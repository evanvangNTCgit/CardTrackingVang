using System;
using System.Collections.Generic;
using System.Text;

namespace CardTrackingVang.Services
{
    public static class VangAnimations
    {
        public async static void FlipImageAnimation(Image imageToFlip)
        {
            await imageToFlip.RotateYToAsync(180, length: 1000);
            await imageToFlip.RotateYToAsync(360, length: 1000);
            imageToFlip.RotationY = 0;
        }
    }
}
