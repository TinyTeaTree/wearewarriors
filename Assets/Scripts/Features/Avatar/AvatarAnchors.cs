using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    [Serializable]
    public class AvatarAnchors
    {
        public TTools toolID;
        public Transform anchorPoint;
    }
}