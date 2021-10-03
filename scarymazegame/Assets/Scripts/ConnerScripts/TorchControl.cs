using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchControl : MonoBehaviour
{

    public Light m_Torch;

    // Start is called before the first frame update
    void Start()
    {
        m_Torch = GetComponent<Light>();
        m_Torch.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.F))
        {
            m_Torch.enabled = !m_Torch.enabled;
        }
    }
}
