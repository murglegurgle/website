﻿namespace Momentum.XpSystems.Core.Models.Cosmetic
{
    public class Levels
    {
        public int MaxLevels { get; set; }
        public int StartingValue { get; set; }
        public int LinearScaleBaseIncrease { get; set; }
        public int LinearScaleInterval { get; set; }
        public decimal LinearScaleIntervalMultiplier { get; set; }
        public int StaticScaleStart { get; set; }
        public decimal StaticScaleBaseMultiplier { get; set; }
        public int StaticScaleInterval { get; set; }
        public decimal StaticScaleIntervalMultiplier { get; set; }
    }
}