﻿using AnaforaData.Model;

namespace AnaforaData.Utils.Dynamics
{
    public interface IDynamicProperty<TComponent> where TComponent : IDynamicComponent
    {
        public string Name { get; set; }
        public TComponent Component { get; set; }
    }
}