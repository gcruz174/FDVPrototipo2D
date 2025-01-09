using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CoinCounter : MonoBehaviour
    {
        [SerializeField] 
        private TMP_Text coinCountText;
        [SerializeField] 
        private float coinCollectDuration = 0.1f;

        private int _currentCoinCount;
        private int _targetCoinCount;
        private float _timer;

        private void OnEnable()
        {
            Coin.OnCollected += OnCollected;
        }

        private void OnDisable()
        {
            Coin.OnCollected -= OnCollected;
        }

        private void OnCollected(int amount)
        {
            _targetCoinCount += amount;
        }

        private void Update()
        {
            if (_currentCoinCount == _targetCoinCount)
                return;
            
            _timer += Time.deltaTime;
            if (_timer < coinCollectDuration) return;
            
            _currentCoinCount++;
            coinCountText.text = _currentCoinCount.ToString();
            coinCountText.transform.DOScale(Vector3.one, 0.1f).From(Vector3.zero);
            _timer = 0f;
        }
    }
}
