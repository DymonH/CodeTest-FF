using Finflow.Hlopov.Application.Interfaces;
using Finflow.Hlopov.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finflow.Hlopov.Web.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class RemmittanceController : ControllerBase
    {
        private readonly IRemmittanceService _remmittanceService;

        public RemmittanceController(IRemmittanceService remmittanceService)
        {
            _remmittanceService = remmittanceService;
        }

        /// <summary>
        /// Gets a remmittance list.
        /// </summary>
        /// <remarks>
        /// Sample request: GET /Remmittance
        /// </remarks>
        [HttpGet]
        public async Task<IEnumerable<RemmittanceModel>> GeRemmittances()
        {
            return await _remmittanceService.GetRemmittanceList();
        }

        /// <summary>
        /// Gets a Remmittance by id.
        /// </summary>
        /// <remarks>
        /// Sample request: GET /Remmittance/f0f1deca-315a-4422-bd6a-f2a7f463b7c3   
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A remmittance with specified id</returns>
        /// <response code="404">If the remmittance is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RemmittanceModel>> GetRemmittance(Guid id)
        {
            var remmittance = await _remmittanceService.GetRemmittanceById(id);

            if (remmittance == null)
            {
                return NotFound();
            }

            return remmittance;
        }

        /// <summary>
        /// Updates remmittance status.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /remmittance/f0f1deca-315a-4422-bd6a-f2a7f463b7c3
        ///     1
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="statusId"></param>
        /// <returns>Updates Remmittance status</returns>
        /// <response code="204">No content if updated successfully</response>
        /// <response code="404">If the Remmittance is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RemmittanceModel>> UpdateStatus(Guid id, [FromBody] int statusId)
        {
            var remmittance = await _remmittanceService.GetRemmittanceById(id);
            if (remmittance == null)
            {
                return NotFound();
            }

            remmittance = await _remmittanceService.UpdateStatus(id, statusId);
            return remmittance;
        }

        /// <summary>
        /// Creates a Remmittance.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /remmittance
        ///{
        ///    "code": "some-code",
        ///    "receiver": {
    	///        "id": 2
        ///    },
        ///    "sender": {
    	///        "id": 1
        ///    },
        ///    "funds": {
    	///        "receiveAmount": 200,
    	///        "sendAmount": 190,
    	///        "rate": 2,
    	///        "SendCurrency": 643,
    	///        "ReceiveCurrency": 643
        ///    }
        ///}
        ///
        /// </remarks>
        /// <param name="remmittanceModel"></param>
        /// <returns>A newly created Remmittance</returns>
        /// <response code="201">Returns the newly created Remmittance</response>
        /// <response code="400">If the RemmittanceModel is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RemmittanceModel>> PostRemmittance(RemmittanceModel remmittanceModel)
        {
            if (remmittanceModel == null)
            {
                return BadRequest();
            }

            var created = await _remmittanceService.Create(remmittanceModel);
            return CreatedAtAction("GetRemmittance", new { id = created.Id }, created);
        }
    }
}