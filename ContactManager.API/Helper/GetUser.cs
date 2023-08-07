using ContactManager.API.Entities;
using System.Security.Claims;

namespace ContactManager.API.Helper
{
    public interface IGetUser
    {
        User Get();
    }
    public class GetUser : IGetUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUser(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        public User Get()
        {
            var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var user = identity.Claims;
                return new User
                {
                    Username = user.FirstOrDefault(u => u.Type == ClaimTypes.Name).Value,
                    Id = Convert.ToInt32(user.FirstOrDefault(u => u.Type == "Id")?.Value)
                };
            }
            return null;
        }
    }
}
