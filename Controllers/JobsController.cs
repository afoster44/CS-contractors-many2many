using System;
using System.Collections.Generic;
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

    public class JobsController : ControllerBase
    {
        private readonly JobsService _service;
        private readonly ContractorsService _cservice;

        public JobsController(JobsService service, ContractorsService cservice)
        {
            _service = service;
            _cservice = cservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Job>> GetAll()
        {
            try
            {
                return Ok(_service.GetAll());
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("{id}")]  // NOTE '{}' signifies a var parameter
        public ActionResult<Job> Get(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Authorize]
        // NOTE ANYTIME you need to use Async/Await you will return a Task
        public async Task<ActionResult<Job>> Create([FromBody] Job newJob)
        {
            try
            {
                // NOTE HttpContext == 'req'
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newJob.CreatorId = userInfo.Id;
                newJob.Creator = userInfo;
                return Ok(_service.Create(newJob));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Job>> Edit([FromBody] Job updated, int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                updated.CreatorId = userInfo.Id;
                updated.Creator = userInfo;
                updated.Id = id;
                return Ok(_service.Edit(updated));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Job>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_service.Delete(id, userInfo.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/contractors")]
        [Authorize]
        public ActionResult<IEnumerable<ContractorJobViewModel>> GetContractorsByJobId(int id)
        {
            try
            {
                return Ok(_cservice.GetContractorsByJobId(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}