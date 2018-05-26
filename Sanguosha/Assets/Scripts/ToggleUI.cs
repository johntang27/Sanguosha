using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleUI : MonoBehaviour {

    Toggle m_Toggle;
    public int toggleIndex;
    public bool isCardTypeToggle = true;

	// Use this for initialization
	void Start ()
    {
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(m_Toggle); });
	}

    void ToggleValueChanged(Toggle change)
    {
        if (isCardTypeToggle)
            CardMakerUIManager.Instance.CardTypesToggle(toggleIndex);
        else
            CardMakerUIManager.Instance.FactionToggle(toggleIndex);
    }
}
