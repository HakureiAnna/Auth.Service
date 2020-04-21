using System.Threading.Tasks;
using Auth.Service.Models.DB;

namespace Auth.Service.Services
{
    public interface IMemberService
    {
        Task<Member> GetMemberWithCredentialsAsync(string id, string password);

        Task<bool> CheckMemberExistsAsync(string id);

        Task<bool> AddMemberAsync(AddMember member);

        Task<Member> GetMemberAsync(string id);

    }
}