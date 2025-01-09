using System;
using UnityEngine;

namespace Effects
{
    public class EffectsManager : MonoBehaviour
    {
        public enum Effect
        {
            Jump,
            Land,
            CoinCollect
        }

        [SerializeField] private PoolObject jumpEffect;
        [SerializeField] private PoolObject landEffect;
        [SerializeField] private PoolObject coinCollectEffect;

        public static EffectsManager Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void SpawnEffect(Effect effect, Vector2 position)
        {
            switch (effect)
            {
                case Effect.Jump:
                    SpawnEffect(jumpEffect, position);
                    break;
                case Effect.Land:
                    SpawnEffect(landEffect, position);
                    break;
                case Effect.CoinCollect:
                    SpawnEffect(coinCollectEffect, position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(effect), effect, null);
            }
        }
        
        private void SpawnEffect(PoolObject effect, Vector2 position)
        {
            var effectInstance = effect.GetObject(true);
            effectInstance.transform.position = position;
            effectInstance.GetComponent<AudioSource>().Play();
            StartCoroutine(effect.ReturnWithDelay(effectInstance, 3.0f));
        }
    }
}
