using UnityEngine;

public class PlayerBehabiurs : MonoBehaviour
{
    public Animator anim;
    public void AnimAttack()
    {
        anim.SetTrigger("attack");
    }
    public void AnimDash()
    {
        anim.SetTrigger("dash");
    }
    public void AnimWalkOn()
    {
        anim.SetBool("Walking",true);
    }
    public void AnimWalkOff()
    {
        anim.SetBool("Walking", false);
    }
}
