using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CResistanceBar : MonoBehaviour 
{
	private Image _strengthBar;
	public float increase;
	private Coroutine _increaseStrengthCoroutine;

	void Start()
	{
		_strengthBar = GetComponent<Image>();
		_increaseStrengthCoroutine = StartCoroutine(IncreaseStrength());
	}

	void OnEnable()
    {
        CPlayerInput.OnStrength += SetStrengthAmount;
    }
    
    void OnDisable()
    {
        CPlayerInput.OnStrength -= SetStrengthAmount;
    }

    private void SetStrengthAmount(float amount)
    {
		if(_strengthBar.fillAmount + amount >=0)
        	_strengthBar.fillAmount += amount;
    }

	public IEnumerator IncreaseStrength()
	{
		//Cambiar despues por condicion de termino
		while(true)
		{
			if(_strengthBar.fillAmount + increase <= 1)
				_strengthBar.fillAmount += increase;
			

			yield return new WaitForSeconds(.1f);
		}
	}
}
