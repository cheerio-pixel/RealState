﻿using RealState.Domain.Common;

namespace RealState.Domain.Entities
{
    public class Upgrades : Entity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<Properties> Properties { get; set; } = null!;
    }
}
