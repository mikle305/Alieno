using UnityEngine;

namespace Additional.Utils
{
    public static class UiUtils
    {
        public static TextMesh CreateWorldText(
            string text,
            Vector3 localPosition,
            int fontSize,
            Transform parent = null,
            Color? color = null,
            TextAnchor textAnchor = TextAnchor.MiddleCenter,
            TextAlignment textAlignment = TextAlignment.Center,
            int sortingOrder = 0)
        {
            Transform transform = new GameObject("WorldText", typeof(TextMesh)).transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            var textMesh = transform.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.fontStyle = FontStyle.Bold;
            textMesh.color = color ?? Color.black;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }
    }
}