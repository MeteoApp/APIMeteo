using APIMeteo.Interfaces.DataLayers;
using APIMeteo.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIMeteo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        #region Properties
        private readonly IRoomDatalayer _roomDatalayer;
        #endregion

        #region Constructor
        public RoomController(IRoomDatalayer roomDatalayer)
        {
            _roomDatalayer = roomDatalayer;
        }
        #endregion

        #region Methods
        #region Read
        [HttpGet]
        public async Task<ActionResult<List<Room>>> GetAllRoomss()
        {
            try 
            {
                List<Room> rooms = await _roomDatalayer.GetAllRooms();

                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #endregion
    }
}