using UnityEngine;
using DG.Tweening;

namespace Markers
{
    public sealed class TargetMarker : MonoBehaviour
    {
        [Header("Parameters")]
        // Начальный размер маркера.
        [SerializeField][Min(0)] private float m_startSize = 0.25f;
        // Конечный размер маркера.
        [SerializeField][Min(0)] private float m_finishSize = 0.5f;
        // Время перехода из startSize в FinishSise.
        [SerializeField][Min(0.0001f)] private float m_duration = 0.5f;

        // Функция анимации. Подобрать функцию можно на этом сайте:
        // https://easings.net/
        [SerializeField] private Ease m_ease = Ease.InOutSine;

        // Поле куда будем кэшировать анимацию.
        private Tweener _tween;

        // Метод показа маркера
        public void Show(Vector3 worldPosition)
        {
            // Завершаем анимацию, если она есть.
            _tween?.Kill();

            // Активируем маркер.
            gameObject.SetActive(true);

            // Устанавливаем маркер на нужную позицию.
            transform.position = worldPosition;

            // Устанавливаем начальный размер маркера.
            transform.localScale = Vector3.one * m_startSize;

            // Запускаем анимацию и кэшируем ее.
            // DOScale принимает в себя конечный резултат и длительность анимации.
            // SetEase устанавливает анимационную функцию.
            // SetLoops. -1 говорит о том что анимация бесконечныя,
            // а LoopType.Yoyo о том что анимацию в DOSюcale надо проиграть в обратную сторону.
            _tween = transform
                .DOScale(Vector3.one * m_finishSize, m_duration)
                .SetEase(m_ease)
                .SetLoops(-1, LoopType.Yoyo);
        }

        // Метод скрытия маркера.
        public void Hide()
        {
            // Завершаем анимацию, если она есть.
            _tween?.Kill();
            _tween = null;

            // Деактивируем маркер.
            gameObject.SetActive(false);
        }
    }
}