using System;
using System.Collections.Generic;
using UnityEngine;

namespace Aezakmi
{
    public class HamsterSkinManager : GloballyAccessibleBase<HamsterSkinManager>
    {
        [SerializeField] private List<Skin> skins;
        public Skin GetSkin(int level) => skins[level % skins.Count];
    }

    [Serializable]
    public class Skin
    {
        public Texture2D baseTexture;
        public Color shirtColor;
    }
}
