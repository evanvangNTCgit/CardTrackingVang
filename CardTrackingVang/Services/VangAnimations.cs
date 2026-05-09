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

        public static Animation ImageWalkAnimation(Image imageToWalk)
        {
            // var animation = new Animation(v => imageToWalk.Scale = v, 1, 2);
            var origX = imageToWalk.X;
            var origY = imageToWalk.Y;

            var parentAnimation = new Animation();
            parentAnimation.Add(0, 0.5, new Animation(v => imageToWalk.Rotation = v, 0, -50, easing: Easing.CubicInOut));
            parentAnimation.Add(0, 0.5, new Animation(v => imageToWalk.TranslationX = v, 0, -75, easing: Easing.CubicInOut));

            parentAnimation.Add(0.5, 0.95, new Animation(v => imageToWalk.Rotation = v, -50, 50, easing: Easing.CubicInOut));
            parentAnimation.Add(0.5, 0.95, new Animation(v => imageToWalk.TranslationX = v, -75, -200, easing: Easing.CubicInOut));

            parentAnimation.Add(0.95, 0.99, new Animation(v => imageToWalk.Rotation = v, 50, -50, easing: Easing.CubicInOut));
            parentAnimation.Add(0.95, 0.99, new Animation(v => imageToWalk.TranslationX = v, -200, -350, easing: Easing.CubicInOut, finished: () => { imageToWalk.Source = "pokeball.png"; }));

            parentAnimation.Add(0.99, 1, new Animation(v => imageToWalk.Rotation = v, 0, 0, easing: Easing.CubicInOut));
            parentAnimation.Add(0.99, 1, new Animation(v => imageToWalk.TranslationX = v, 0, 0, easing: Easing.CubicInOut));

            return parentAnimation;
        }
    }
}
