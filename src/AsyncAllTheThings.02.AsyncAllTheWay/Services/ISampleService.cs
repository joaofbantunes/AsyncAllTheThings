using System.Threading.Tasks;

namespace AsyncAllTheThings._02.AsyncAllTheWay.Services
{
    public interface ISampleService
    {
        Task<int> GetSomethingAsync();

        Task<int> GetSomethingProtectedFromHammeringAsync();

        Task<int> GetSomethingProtectedFromHammeringWithExceptionAsync();
    }
}
