using System;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Services;
using UnityEngine;

namespace Game
{
    public class Marks : BaseVisualFeature<MarksVisual>, IMarks
    {
        [Inject] public IHud Hud { get; set; }
        [Inject] public ICamera Camera { get; set; }
        
        private MarksConfig Config { get; set; }
        
        public async Task Load()
        {
            Config = _bootstrap.Services.Get<ILocalConfigService>().GetConfig<MarksConfig>();

            await CreateVisual();

            Hud.SetHudOnCanvas(_visual.Canvas);
        }

        public string AddMark(Transform anchor, TMark type, string id = null)
        {
            if (anchor == null)
            {
                Notebook.NoteError("No Anchor Provided");
                return null;
            }
            
            if (id == null)
            {
                id = Guid.NewGuid().GetHashCode().ToString();
            }

            if (_visual.GetMarkById(id) != null)
            {
                Notebook.NoteError($"Mark {id} already exists");
                return null;
            }
            
            var markPrefab = Config.Visuals.FirstOrDefault(v => v.Type == type);

            var markInstance = Summoner.CreateAsset(markPrefab, _visual.transform);
            markInstance.Id = id;
            markInstance.SetAnchor(anchor);

            _visual.TakeMark(markInstance);

            return id;
        }

        public T GetMark<T>(string id) where T : BaseMarkVisual
        {
            var mark = _visual.GetMarkById(id);
            if (mark == null)
            {
                Notebook.NoteError($"No Mark for {id} found");
                return null;
            }

            return (T)mark;
        }

        public void UpdateMarkPosition(BaseMarkVisual mark)
        {
            var anchor = mark.Anchor;

            if (anchor == null)
            {
                Notebook.NoteError($"Mark anchor has been Destroyed {mark.Id}");
                return;
            }

            UnityEngine.Camera worldCam = Camera.WorldCamera;
            UnityEngine.Camera hudCamera = Hud.HudCamera;

            var screenPoint = worldCam.WorldToScreenPoint(anchor.position);
            screenPoint.z = _visual.Canvas.planeDistance;
            var hudPoint = hudCamera.ScreenToWorldPoint(screenPoint);

            mark.transform.position = hudPoint;
        }

        public void RemoveMark(string id)
        {
            var doomedMark = _visual.GetMarkById(id);
            if (doomedMark == null)
            {
                Notebook.NoteError($"Mark {id} does not exist");
                return;
            }

            doomedMark.SelfDestroy();

            _visual.RemoveMark(doomedMark);
        }
    }
}