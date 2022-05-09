namespace CarRepairApp.Services
{
    public interface IIdentityService<TIdentity>
    {
        bool IsCanClearIdentity { get; }
        TIdentity StrongIdentity { get; set; }
        TIdentity WeakIdentity { get; set; }
        void Logout();
    }
}
