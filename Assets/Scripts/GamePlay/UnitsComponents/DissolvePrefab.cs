using System.Collections;
using UnityEngine;

namespace GamePlay.UnitsComponents
{
    public class DissolvePrefab : MonoBehaviour
    {
        [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
        [field: SerializeField] public MeshFilter MeshFilter { get; private set; }
        [field: SerializeField] public Material DissolveMaterial { get; private set; }

        public void StartDissolving(float dissolveTime,Texture texture)
        {
            StartCoroutine(Dissolving(dissolveTime, texture));
        }

        public IEnumerator Dissolving(float dissolveTime,Texture texture)
        {
            float timePassed = 0;
            Material mat = Instantiate(DissolveMaterial);
            MeshRenderer.material = mat;
        
            mat.SetTexture("_MainTex",texture);
        
            while (timePassed <= dissolveTime)
            {
                timePassed += Time.deltaTime;
                float elapsedValue = Mathf.Lerp(0, 1, timePassed / dissolveTime);
                mat.SetFloat("_Cutoff",elapsedValue);
                yield return null;
            }
        }
    }
}
