using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UILevelController : MonoBehaviour
{
    public TextMeshProUGUI levelText;

    private void Start()
    {
        levelText.text = GameManager.Instance.levelValue.ToString("F0");
    }


}
