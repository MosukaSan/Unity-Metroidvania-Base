using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class log : MonoBehaviour
{
    public Text txtIsGroundedLog;

    void Update()
    {
        txtIsGroundedLog.text = $"Is Grounded: {Moving.isGrounded}";
    }
}
