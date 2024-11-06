using Game;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickInputCapture : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private JoystickVisual _visual;
    [SerializeField] private RectTransform _outsideKnob;
    [SerializeField] private RectTransform _insideKnob;
    [SerializeField] private RectTransform _edgeAnchor;

    private bool _isPressing;

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressing = false;
    }

    void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            _isPressing = false; //Safety Precaution, the OnPointerUp seems to be working fine
        }
        
        if (_isPressing)
        {
            _visual.ReportJoystick(Input.mousePosition);
        }
        else
        {
            _visual.ReportJoystick(Vector2.zero);
            ResetKnobPosition();
        }
    }

    private void ResetKnobPosition()
    {
        _insideKnob.anchoredPosition = Vector2.zero;
    }

    public Vector2 MoveKnob(Vector3 worldPoint)
    {
        _insideKnob.position = new Vector3(worldPoint.x, worldPoint.y, _insideKnob.position.z);

        var fullSize = Vector3.Distance(_edgeAnchor.position, _outsideKnob.position);
        var knobSizeX = _insideKnob.position.x - _outsideKnob.position.x;
        var knobXNormalized = Mathf.Clamp(knobSizeX / fullSize, -1f, 1f);
            
        var knobSizeY = _insideKnob.position.y - _outsideKnob.position.y;
        var knobYNormalized = Mathf.Clamp(knobSizeY / fullSize, -1f, 1f);

        var delta = _insideKnob.position - _outsideKnob.position;
        if (delta.magnitude > fullSize)
        {
            _insideKnob.position = _outsideKnob.position + delta.normalized * fullSize;
        }

        return new Vector2(knobXNormalized, knobYNormalized);
    }
}