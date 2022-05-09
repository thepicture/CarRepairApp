namespace CarRepairApp.Services
{
    public interface INavigationService<TTarget>
    {
        TTarget CurrentTarget { get; }
        void NavigateBack();
        bool IsCanGoBack { get; }
        void Navigate<TWhere>() where TWhere : TTarget;
        void NavigateWithParameter<TWhere, TParam>(TParam param) where TWhere : TTarget;
        void NavigateToRoot();
    }
}
