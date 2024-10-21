using Core;
using UnityEngine;

namespace Game
{
    public class GardenVisual : BaseVisual<Garden>
    {
        [SerializeField] private Camera _camera;

        [SerializeField] private Transform _avatarStartSpot;

        public Transform AvatarStartSpot => _avatarStartSpot;

    }
}