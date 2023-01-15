using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum TrackPosition
{
    Port,
    Starboard
}

public static class TrackPositionExtensions
{
    public static float GetPositionMultiplier(this TrackPosition trackPosition)
    {
        return trackPosition == TrackPosition.Port
            ? -1
            : 1;
    }
    public static TrackPosition GetOpposite(this TrackPosition trackPosition)
    {
        return trackPosition == TrackPosition.Port
            ? TrackPosition.Starboard
            : TrackPosition.Port;
    }
}