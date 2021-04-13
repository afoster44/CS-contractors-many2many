using System.Threading.Tasks;
using AmaZen.Models;
using AmaZen.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmaZen.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class JobContractController : ControllerBase
    {
        private readonly JobContractorService _service;

        public JobContractController(JobContractorService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<JobContractor>> CreateAsync([FromBody] JobContractor newWLP)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newWLP.CreatorId = userInfo.Id;
                return Ok(_service.Create(newWLP));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<string> Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok("deleted");
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

    }
}