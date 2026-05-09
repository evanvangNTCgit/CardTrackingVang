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

        /// <summary>
        /// Throws a pokeball image. at the pokemon.
        /// </summary>
        /// <param name="pokemonImage"></param>
        /// <param name="pokeballImage"></param>
        /// <returns></returns>
        public static (Animation, bool) ThrowPokeBallAnimation(Image pokemonImage, Image pokeballImage)
        {
            pokeballImage.IsVisible = true;

            double startX = pokeballImage.TranslationX;
            double startY = pokeballImage.TranslationY;

            double targetX = pokemonImage.TranslationX;
            double targetY = pokemonImage.TranslationY;

            Random rnd = new Random();
            int captured = rnd.Next(1, 9); // Lets give user 33% chance to catch.
            bool success = captured > 6;

            var parentAnimation = new Animation();

            parentAnimation.Add(0, 0.8, new Animation(v => pokeballImage.Rotation = v, 0, 720, easing: Easing.Linear));

            parentAnimation.Add(0, 0.8, new Animation(v => pokeballImage.Scale = v, 2.0, 1.0, easing: Easing.CubicOut));

            parentAnimation.Add(0, 0.8, new Animation(v => pokeballImage.TranslationX = v, startX, targetX, easing: Easing.CubicOut));

            double peakY = targetY - 300;
            parentAnimation.Add(0, 0.4, new Animation(v => pokeballImage.TranslationY = v, startY, peakY, easing: Easing.CubicOut));

            parentAnimation.Add(0.4, 0.8, new Animation(v => pokeballImage.TranslationY = v, peakY, peakY, easing: Easing.CubicIn, finished: () =>
            {
                pokeballImage.IsVisible = false;

                if (success)
                {
                    pokemonImage.IsVisible = false;
                }
            }));

            parentAnimation.Add(0.9, 1, new Animation(v => pokeballImage.TranslationY = v, peakY, startY, easing: Easing.CubicIn));

            return (parentAnimation, success);
        }
    }
}
