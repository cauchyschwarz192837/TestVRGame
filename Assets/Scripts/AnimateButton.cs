using UnityEngine;
using UnityEngine.EventSystems;

public class AnimateButton : MonoBehaviour , IPointerEnterHandler,IPointerExitHandler {
    [SerializeField]
    public float bobAmount; // Adjust the bobbing amount
    [SerializeField]
    public float bobSpeed; // Adjust the bobbing speed
    private Vector3 originalPosition;
    public bool isMouseOver = false;
	// Update is called once per frame
    void Start()
    {
        originalPosition = transform.position;
    }
	void Update () {
		if (isMouseOver)
        {
            float yOffset = Mathf.Sin(Time.time * bobSpeed) * bobAmount;
            transform.position = originalPosition + new Vector3(0f, yOffset, 0f);
        }
        else
        {
            transform.position = originalPosition;
        }
	}
	public void OnPointerEnter(PointerEventData eventData)
	{
		isMouseOver = true;
	}
	public void OnPointerExit(PointerEventData eventData)
	{
		isMouseOver = false;
	}
}
