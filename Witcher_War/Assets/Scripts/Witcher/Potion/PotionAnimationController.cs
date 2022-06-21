using UnityEngine;

public class PotionAnimationController : MonoBehaviour
{
    private Animator potionExplotionAnimator;    

    private void Awake()
    {
        potionExplotionAnimator = GetComponent<Animator>();
        potionExplotionAnimator.SetBool("Exploted", false);
    }
    
    public void StartAnimation()
    {
        potionExplotionAnimator.SetBool("Exploted", true);          
    }
}
