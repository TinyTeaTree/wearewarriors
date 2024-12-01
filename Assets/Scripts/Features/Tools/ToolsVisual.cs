using System;
using System.Collections;
using System.Collections.Generic;
using Codice.CM.Common;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class ToolsVisual : BaseVisual<Tools>
    {
        private List<ToolVisual> _toolVisuals;
        public List<ToolVisual> AllTools => _toolVisuals;

        private void Start()
        {
            StartCoroutine(DetectThrowClickRoutine());
        }

        public void SetToolVisuals(List<ToolVisual> toolVisuals)
        {
            _toolVisuals = toolVisuals;
        }

        private IEnumerator DetectThrowClickRoutine()
        {
            int floorLayerMask = LayerMask.GetMask("Floor");
            while (true)
            {
                var holdingTool = Feature.GetHoldingTool();

                if (Input.GetMouseButtonDown(0) && holdingTool != null)
                {
                    // Todo: Check If player touch the UI and if yes dont throw the tool.
                    
                    Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayerMask))
                    {
                        Vector3 dropPoint = hit.point;
                        holdingTool.SetFeature(Feature);
                        Feature.ThrowTool(holdingTool, dropPoint);
                    }
                }

                yield return null;
            }
        }
    }
}