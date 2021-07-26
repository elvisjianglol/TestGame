using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DashMeter : MonoBehaviour
{
    [SerializeField] private PlayerMovement dashScript;

    [SerializeField] private Image dashingBG;

    [SerializeField] private Image arrow1;
    [SerializeField] private Image arrow2;
    [SerializeField] private Image arrow3;
    [SerializeField] private Image arrow4;

    private Color normalArrow = new Color(1f, 1f, 1f, 1f);
    private Color usedArrow = new Color(1f, 1f, 1f ,0.6392f);

    private Color normalBG = new Color(0.7529f, 1f, 8935f, 1f);
    private Color usedBG = new Color(0.7529f, 1f, 8935f, 0.85f);
 
   

    // Update is called once per frame
    void Update()
    {


        if(dashScript.canDash)
        {
            arrow1.color = normalArrow;
            arrow2.color = normalArrow;
            arrow3.color = normalArrow;
            arrow4.color = normalArrow;

            dashingBG.color = normalBG;
        }
        else
        {
            // to add a transparency effect

            arrow1.color = usedArrow;
            arrow2.color = usedArrow;
            arrow3.color = usedArrow;
            arrow4.color = usedArrow;

            dashingBG.color = usedBG;
        }
        

    }
}
