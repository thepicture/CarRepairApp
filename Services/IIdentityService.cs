namespace CarRepairApp.Services
{
    public interface IIdentityService<TIdentity>
    {
        TIdentity StrongIdentity { get; set; }
        TIdentity WeakIdentity { get; set; }
        void Logout();
    }
}
