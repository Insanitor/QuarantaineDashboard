using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


/**
* @Author Dennis Dupont
* @Version 1.0
* @Date 29/5/2020
*/

public class DragWindow : MonoBehaviour, IDragHandler
{

    [SerializeField] RectTransform dragRectTransform;
    Canvas canvas;

    /// <summary>
    /// Finds the Canvas with the tag 'Canvas'
    /// </summary>
    /// @Author Dennis Dupont
    /// @Status Done
    /// @Date 29/5/2020
    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }

    /// <summary>
    /// Used to move a windows position, relative to the Canvas' Scale Factor
    /// </summary>
    /// @DAuthor ennis Dupont
    /// @Status Done
    /// @Date 29/5/2020
    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
