using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public class Handler : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup group;
        [SerializeField] protected float animatingtime = 3;

        private void Awake()
        {
            group = GetComponent<CanvasGroup>();
        }
        public virtual void Init()
        {
            gameObject.SetActive(true);
            Debug.Log("Activated");
            AnimateTransparency(0, 1);
        }

        public void AnimateTransparency(float start, float end)
        {
            StartCoroutine(AnimateTransparency(group, 0, 1, animatingtime));
        }
        public IEnumerator AnimateTransparency(CanvasGroup group, float initial, float final, float time)
        {
            float temp = initial;
            while (temp != final)
            {
                temp = Mathf.Lerp(temp, final, time);
                group.alpha = temp;
                yield return null;
            }

        }
        public virtual void DeInit()
        {

            gameObject.SetActive(false);

            //AnimateTransparency(1, 0);
        }

    }
}