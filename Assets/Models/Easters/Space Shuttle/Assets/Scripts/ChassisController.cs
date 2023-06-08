using UnityEngine;

public class ChassisController : MonoBehaviour {

    private Animator _anim;
    
    void Start ()
    {
        _anim = this.GetComponent<Animator>();
	}
	
    public void FuncChassis()
    {
        _anim.SetTrigger("Chassis");
    }
}
