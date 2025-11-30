using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonInteractionManager : MonoBehaviour
{

    [SerializeField] private RectTransform rt;
    [SerializeField] private float hoverScale = 1.2f;

    private void Start()
    {
        rt.localScale = Vector3.one;
    }

    private void OnMouseOver()
    {
        Debug.Log("mousein");
        rt.localScale = Vector3.one * hoverScale;
    }
    private void OnMouseExit()
    {
        rt.localScale = Vector3.one;
    }
}
