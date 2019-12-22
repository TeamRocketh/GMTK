﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    float threshold = 10f;
    public static TouchState state;
    public static float distanceMoved
    {
        get
        {
            if (state == TouchState.DRAG || state == TouchState.UP)
                return (currentPosition - downPosition).magnitude;
            return 0;
        }
    }
    public static Vector2 pullVector
    {
        get
        {
            if (state == TouchState.DRAG || state == TouchState.UP)
                return (currentPosition - downPosition).normalized;
            return Vector2.zero;
        }
    }
    public static Vector2 downPosition, currentPosition, upPosition;

    private void Awake()
    {
        state = TouchState.NONE;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        state = TouchState.DOWN;
        downPosition = Input.mousePosition;
    }

    public void OnDrag(PointerEventData data)
    {
        if ((currentPosition - downPosition).magnitude > threshold)
            state = TouchState.DRAG;
        currentPosition = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData data)
    {
        state = TouchState.UP;
        upPosition = Input.mousePosition;
    }
}

public enum TouchState
{
    NONE,
    DOWN,
    DRAG,
    UP
}