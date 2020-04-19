using DG.Tweening;
using UniRx.Async;

namespace Common {

/// <summary>
/// トゥイーンをawaitできるようにする拡張クラス
/// </summary>
public static class TweenAwaiter
{
    public static UniTask<Tween>.Awaiter GetAwaiter(this Tween self)
    {
        var source = new UniTaskCompletionSource<Tween>();

        TweenCallback onComplete = null;
        onComplete = () =>
        {
            self.onComplete -= onComplete;
            source.TrySetResult(self);
        };
        self.onComplete += onComplete;

        return source.Task.GetAwaiter();
    }
}
} // Common
