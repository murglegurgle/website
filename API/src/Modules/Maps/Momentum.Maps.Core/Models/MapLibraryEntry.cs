﻿using System;
using Momentum.Framework.Core.Models;

namespace Momentum.Maps.Core.Models
{
    public class MapLibraryEntry : TimeTrackedModel
    {
        public Guid Id { get; set; }
        public Guid MapId { get; set; }
        public Guid UserId { get; set; }
    }
}