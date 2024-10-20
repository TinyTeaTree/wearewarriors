using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class LoadingScreenVisual : BaseVisual<LoadingScreen>
    {
        [SerializeField] Image loadingImage;

        public void RotatingLoadingImage()
        {
            loadingImage.transform.Rotate(new Vector3(0, 0 ,5), 180f);
        }
    }
}