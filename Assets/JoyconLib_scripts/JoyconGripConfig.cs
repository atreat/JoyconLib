using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JoyconGripConfig : MonoBehaviour
{
    public enum GripMode : uint
    {
        NONE,
        SINGLE,
        TUPLE
    };

    private List<Joycon> allJoycons;

    // Use this for initialization
    void Start()
    {
        allJoycons = JoyconManager.Instance.allJoyCons;
    }

    // Update is called once per frame
    void Update()
    {
        Joycon leftJoycon = null;
        Joycon rightJoycon = null;

        GripMode currentMode = GripMode.NONE;

        foreach (Joycon j in allJoycons)
        {
            if (checkSingle(j)) {
                currentMode = GripMode.SINGLE;
                leftJoycon  = j.isLeft ? j : null;
                rightJoycon = j.isLeft ? null : j;
            }

            if (pressingShoulder1(j))
            {
                if (j.isLeft && leftJoycon == null) { leftJoycon = j; }
                if (!j.isLeft && rightJoycon == null) { rightJoycon = j; }
            }

        }

        if (leftJoycon != null && rightJoycon != null)
        {
            currentMode = GripMode.TUPLE;
        }

        var joyconsStr = "";
        if (currentMode != GripMode.NONE)
        {
            joyconsStr += (leftJoycon != null) ? "LEFT " : "";
            joyconsStr += (rightJoycon != null) ? "RIGHT" : "";
        }

        Debug.Log("GRIP_MODE: " + currentMode + "(" + joyconsStr + ")");
    }

    private bool checkSingle(Joycon j)
    {
        return j.GetButton(Joycon.Button.SL) 
            && j.GetButton(Joycon.Button.SR);
    }

    private bool pressingShoulder1(Joycon j)
    {
        return j.GetButton(Joycon.Button.SHOULDER_1);
    }

}
