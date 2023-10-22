using Godot;
using System;

namespace Global
{
    public static class Utilities
    {
        public static bool isAnimationOver(AnimatedSprite2D sprite, string anim)
        {
            if (sprite.Animation == anim && sprite.Frame == sprite.SpriteFrames.GetFrameCount(anim) - 1)
		    {
			    return true;
		    }
            else
            {
                return false;
            }
        } 
    } 
}