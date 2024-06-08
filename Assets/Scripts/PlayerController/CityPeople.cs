using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityPeople
{
    public class CityPeople : MonoBehaviour
    {
        private AnimationClip[] myClips;
        private Animator animator;

        void Start()
        {
            animator = GetComponent<Animator>();
            if (animator != null)
            {
                myClips = animator.runtimeAnimatorController.animationClips;
                PlayAnyClip();
                //StartCoroutine(ShuffleClips());
            }
            else
            {
                Debug.Log("No animator found in component");
            }

        }

        void PlayAnyClip()
        {
            var clips = myClips[Random.Range(0, myClips.Length)];
            animator.CrossFadeInFixedTime(clips.name, 1.0f, -1, Random.value * clips.length);
        }

        IEnumerator ShuffleClips()
        {
            while (true)
            {
                yield return new WaitForSeconds(15.0f + Random.value * 5.0f);
                PlayAnyClip();
            }
        }

    }
}
