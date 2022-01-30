using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLevel : MonoBehaviour
{
    [SerializeField] private Transform _parentTop = null;
    [SerializeField] private Transform _parentBottom = null;
    [SerializeField] private Vector3 _topLocalPosition = Vector3.zero;
    [SerializeField] private Vector3 _bottomLocalPosition = Vector3.zero;
    [SerializeField] private bool _keepLocalPosition = true;
    [SerializeField] private bool _startTop = true;

    private bool _isTop;

    void OnEnable ()
    {
        _isTop = _startTop;

        // If we're keeping the same local position between switches, assume the current local position is being used for both top and bottom local position
        if (_keepLocalPosition)
        {
            _topLocalPosition = transform.localPosition;
            _bottomLocalPosition = transform.localPosition;
        }
    }

    public void Switch () 
    {
        if (_isTop)
        {
            transform.SetParent(_parentBottom);
            transform.localPosition = _bottomLocalPosition;
        } else 
        {
            transform.SetParent(_parentTop);
            transform.localPosition = _topLocalPosition;
        }

        _isTop = !_isTop;
    }
}
