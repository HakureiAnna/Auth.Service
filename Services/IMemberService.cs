using System.Threading.Tasks;
using Auth.Service.Models.DB;

namespace Auth.Service.Services
{
    public interface IMemberService
    {
        bool CheckValidMember(string id, string passCode);
        
        Task<bool> CheckMemberExists(string id);

        Task<bool> AddMemberAsync(Member member);
    }
}