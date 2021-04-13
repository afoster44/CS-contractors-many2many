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
    public class ContractorsController : ControllerBase
    {

        private readonly ContractorsService _service;
        private readonly JobsService _jservice;

        public ContractorsController(ContractorsService service, JobsService jservice)
        {
            _service = service;
            _jservice = jservice;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contractor>> GetAll()
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
        public ActionResult<Contractor> Get(int id)
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
        public async Task<ActionResult<Contractor>> Create([FromBody] Contractor newContractor)
        {
            try
            {
                // NOTE HttpContext == 'req'
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                newContractor.CreatorId = userInfo.Id;
                newContractor.Creator = userInfo;
                return Ok(_service.Create(newContractor));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Contractor>> Edit([FromBody] Contractor updated, int id)
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
        public async Task<ActionResult<Contractor>> Delete(int id)
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

        [HttpGet("{id}/jobs")]
        [Authorize]
        public ActionResult<IEnumerable<JobContractorViewModel>> GetJobsByContractorId(int id)
        {
            try
            {
                return Ok(_jservice.GetJobsByContractorId(id));
            }
            catch (System.Exception err)
            {
                return BadRequest(err.Message);
            }
        }

    }
}