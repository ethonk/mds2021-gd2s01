using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchControl : MonoBehaviour
{

    public GameObject m_Torch;

    // Start is called before the first frame update
    void Start()
    {
        m_Torch.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.F))
        {
            m_Torch.SetActive(!m_Torch.activeInHierarchy);
        }
    }
}
