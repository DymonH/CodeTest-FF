using Finflow.Hlopov.Application.Interfaces;
using Finflow.Hlopov.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Finflow.Hlopov.Web.Controllers
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Gets a clients list.
        /// </summary>
        /// <remarks>
        /// Sample request: GET /client
        /// </remarks>
        [HttpGet]
        public async Task<IEnumerable<ClientModel>> GetClients()
        {
            return await _clientService.GetClientList();
        }

        /// <summary>
        /// Gets a Client by id.
        /// </summary>
        /// <remarks>
        /// Sample request: GET /client/1   
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>A client with specified id</returns>
        /// <response code="404">If the client is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientModel>> GetClient(int id)
        {
            var client = await _clientService.GetClientById(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Updates a Client.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /client/3
        ///     {
        ///         "name": "Dymon-updated",
        ///         "surname": "Hlopov",
        ///         "dateOfBirth": "1981-04-16T00:00:00",
        ///         "id": 3
        ///     }
        ///
        /// </remarks>
        /// <param name="clientModel"></param>
        /// <returns>A newly created Client</returns>
        /// <response code="204">No content if updated successfully</response>
        /// <response code="400">If the client is null</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutClient(int id, ClientModel clientModel)
        {
            if (id != clientModel.Id)
            {
                return BadRequest();
            }

            await _clientService.Update(clientModel);
            return NoContent();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Creates a Client.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /client
        ///     {
        ///         "name": "Dymon",
        ///         "surname": "Hlopov",
        ///         "dateOfBirth": "1981-04-16T00:00:00"
        ///     }
        ///
        /// </remarks>
        /// <param name="clientModel"></param>
        /// <returns>A newly created Client</returns>
        /// <response code="201">Returns the newly created client</response>
        /// <response code="400">If the client is null</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ClientModel>> PostClient(ClientModel clientModel)
        {
            if (clientModel == null)
            {
                return BadRequest();
            }

            var createdClient = await _clientService.Create(clientModel);
            return CreatedAtAction("GetClient", new { id = createdClient.Id }, createdClient);
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>   
        /// <remarks>
        /// Sample request: DELETE /client/1
        /// </remarks>
        /// <returns>Deleted cient</returns>
        /// <response code="404">If the client is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClientModel>> DeleteClient(int id)
        {
            var client = await _clientService.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }

            await _clientService.Delete(client);
            return client;
        }
    }
}