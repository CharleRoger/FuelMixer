﻿using EdyCommonTools;
using System;
using UnityEngine;

namespace FuelMixer
{
    [Serializable]
    public class IgnitorResource : IConfigNode
    {
        [SerializeField]
        public string Name = null;
        [SerializeField]
        private float Amount = 0;
        [SerializeField]
        private float ScaledAmount = 0;
        [SerializeField]
        public float AddedIgnitionPotential = 1;
        [SerializeField]
        public bool AlwaysRequired = false;

        public float GetAmount(float massRate)
        {
            if (Amount != 0) return Amount;
            if (ScaledAmount != 0) return ScaledAmount * massRate;
            return 0;
        }

        public void Load(ConfigNode node)
        {
            Name = node.GetValue("name");
            if (node.HasValue("Amount")) Amount = Mathf.Max(0.0f, float.Parse(node.GetValue("Amount")));
            if (node.HasValue("ScaledAmount")) ScaledAmount = Mathf.Max(0.0f, float.Parse(node.GetValue("ScaledAmount")));
            if (node.HasValue("AddedIgnitionPotential")) AddedIgnitionPotential = Mathf.Max(0.0f, float.Parse(node.GetValue("AddedIgnitionPotential")));
            if (node.HasValue("AlwaysRequired")) AlwaysRequired = bool.Parse(node.GetValue("AlwaysRequired"));
        }

        public void Save(ConfigNode node)
        {
            node.AddValue("name", Name);
            node.AddValue("Amount", Mathf.Max(0.0f, Amount));
            node.AddValue("ScaledAmount", Mathf.Max(0.0f, ScaledAmount));
            node.AddValue("AddedIgnitionPotential", Mathf.Max(0.0f, AddedIgnitionPotential));
            node.AddValue("AlwaysRequired", AlwaysRequired);
        }

        public override string ToString()
        {
            var str = Name + ':' + Amount.ToString("F3") + '/' + ScaledAmount.ToString("F3") + '/' + AddedIgnitionPotential.ToString("F3") + '/' + AlwaysRequired.ToString();
            return str;
        }

        public static IgnitorResource FromString(string str)
        {
            IgnitorResource ignitorResource = new IgnitorResource();

            var split = str.Split(':');
            var values = split[1].Split('/');

            ignitorResource.Name = split[0];
            ignitorResource.Amount = float.Parse(values[0]);
            ignitorResource.ScaledAmount = float.Parse(values[1]);
            ignitorResource.AddedIgnitionPotential = float.Parse(values[2]);
            ignitorResource.AlwaysRequired = bool.Parse(values[3]);

            return ignitorResource;
        }
    }
}