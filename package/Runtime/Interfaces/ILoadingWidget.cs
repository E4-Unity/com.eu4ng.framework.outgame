using System.Threading.Tasks;

namespace Eu4ng.Framework.OutGame
{
    public interface ILoadingWidget
    {
        void FadeOut(float duration);

        void FadeIn(float duration);

        void ShowLoadingScreen();

        void HideLoadingScreen();

        void UpdateLoadingProgress(float progress);

        void UpdateLoadingState(string state);
    }
}
