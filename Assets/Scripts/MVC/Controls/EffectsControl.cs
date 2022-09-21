using System.Collections;
using UnityEngine;

namespace Controls
{
    public class EffectsControl : MonoBehaviour
    {
        [SerializeField] 
        private float _destroyTime;
        private void OnEnable()
        {
            StartCoroutine(Destroy(_destroyTime));
        }
        private IEnumerator Destroy(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);
        }
    }
}