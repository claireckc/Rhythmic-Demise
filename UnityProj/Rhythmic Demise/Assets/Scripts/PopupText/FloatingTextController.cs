using UnityEngine;
using System.Collections;

public class FloatingTextController : MonoBehaviour {
    private static FloatingText popupText;
    private static FloatingText comboPopUpText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("CanvasUI");

        if(!popupText)
            popupText = Resources.Load<FloatingText>("Prefabs/PopupTextParent");

        if (!comboPopUpText)
            comboPopUpText = Resources.Load<FloatingText>("Prefabs/ComboPopUpParent");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        FloatingText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(location.position.x + Random.Range(-.5f, .5f), location.position.y + Random.Range(-.5f, .5f)));

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.setText(text);
    }

    public static void CreateFloatingText(string text, Vector3 position)
    {
        FloatingText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(position.x + Random.Range(-.5f, .5f), position.y + Random.Range(-.5f, .5f)));

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.setText(text);
    }

    public static void CreateComboPopUp(string text, Vector3 position)
    {
        FloatingText instance = Instantiate(comboPopUpText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(new Vector2(position.x + Random.Range(-.5f, .5f), position.y + Random.Range(-.5f, .5f)));

        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        instance.setText(text);
    }
}
