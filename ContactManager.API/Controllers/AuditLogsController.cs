using ContactManager.API.Helper;
using ContactManager.API.Models;
using ContactManager.API.Services.AuditLogsServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.API.Controllers
{
    [ApiController]
    [Route("api/auditlogs")]
    [Authorize]
    public class AuditLogsController : ControllerBase
    {
        private readonly IContactLogsService service;
        private readonly IGetUser _getUser;

        public AuditLogsController(IContactLogsService service,
                                    IGetUser getUser)
        {
            this.service = service;
            this._getUser = getUser;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactLogDto>>> GetLogs()
        {
            var user = _getUser.Get();
            var username = user.Username;
            try
            {
                var logs = await service.GetLog(username);
                if(logs == null)
                {
                    return NotFound();
                }
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong");
            }
        }
    }
}
