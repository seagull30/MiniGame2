using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public void TitlebuttonAction()
    {
        GameManager.Instance.goTitle();
    }
}
