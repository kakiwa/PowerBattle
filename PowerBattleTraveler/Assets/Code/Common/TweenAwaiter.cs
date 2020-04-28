using UniRx.Async;

/// <summary>
/// いちいちCommonと一緒にusingするのめんどいのでTweenのnamespaceに突っ込みます
/// </summary>
namespace DG.Tweening {

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
