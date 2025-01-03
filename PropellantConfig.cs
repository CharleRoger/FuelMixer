﻿using System.Collections.Generic;

namespace FuelMixer
{
    public abstract class PropellantConfigBase
    {
        public virtual float ThrustMultiplier { get; protected set; } = 1;
        public virtual float IspMultiplier { get; protected set; } = 1;
        public List<Propellant> Propellants { get; protected set; } = new List<Propellant>();

        public PropellantConfigBase()
        {

        }

        public PropellantConfigBase(ConfigNode node)
        {
            if (node.HasValue("ThrustMultiplier"))
            {
                ThrustMultiplier = float.Parse(node.GetValue("ThrustMultiplier"));
            }
            if (node.HasValue("IspMultiplier"))
            {
                IspMultiplier = float.Parse(node.GetValue("IspMultiplier"));
            }
        }
    }

    public class PropellantConfig : PropellantConfigBase
    {
        public string ResourceName { get; private set; } = "";
        public bool IsOxidizer { get; private set; } = false;
        public int MixtureConstant { get; private set; } = 0;

        public PropellantConfig(ConfigNode node) : base(node)
        {
            if (node.HasValue("name")) ResourceName = node.GetValue("name");
            if (node.HasValue("IsOxidizer")) IsOxidizer = bool.Parse(node.GetValue("IsOxidizer"));
            if (node.HasValue("MixtureConstant")) MixtureConstant = int.Parse(node.GetValue("MixtureConstant"));
            Propellants.Add(GetPropellant(1, true));
        }

        public Propellant GetPropellant(float ratio, bool drawStackGauge = false)
        {
            var node = new ConfigNode();
            node.name = "PROPELLANT";
            node.AddValue("name", ResourceName);
            node.AddValue("ratio", ratio);
            node.AddValue("drawStackGauge", drawStackGauge);
            var propellant = new Propellant();
            propellant.Load(node);
            propellant.displayName = ResourceName;
            return propellant;
        }
    }
}