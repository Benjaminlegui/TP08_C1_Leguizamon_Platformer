using UnityEngine;

namespace Helpers
{
    public static class MathHelpers
    {
        public static float AnimationMapping(float value, float min1, float max1, float min2,  float max2, bool clamp = false )
        {
            float val = min2 + (max2 - min2) * ((value - min1) / (max1 - min1));
            
            return clamp ? Mathf.Clamp(val, Mathf.Min(min2, max2), Mathf.Max(min2, max2)) : val;
        }
    }
}
