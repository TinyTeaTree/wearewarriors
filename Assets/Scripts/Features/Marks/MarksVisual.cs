using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Game
{
    public class MarksVisual : BaseVisual<Marks>
    {
        [SerializeField] private Canvas _canvas;
        public Canvas Canvas => _canvas;

        private List<BaseMarkVisual> _marks = new();

        public BaseMarkVisual GetMarkById(string id)
        {
            return _marks.FirstOrDefault(m => m.Id == id);
        }

        public void TakeMark(BaseMarkVisual markInstance)
        {
            _marks.Add(markInstance);
        }

        void Update()
        {
            foreach (var mark in _marks)
            {
                Feature.UpdateMarkPosition(mark);
            }
        }

        public void RemoveMark(BaseMarkVisual doomedMark)
        {
            _marks.Remove(doomedMark);
        }
    }
}