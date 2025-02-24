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

        #region Create
        [HttpPost]
        public async Task<ActionResult<Room>> AddRoom(Room room)
        {
            try
            {
                Room roomAdded = await _roomDatalayer.AddRoom(room);

                return Ok(roomAdded);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Read
        [HttpGet]
        public async Task<ActionResult<List<Room>>> GetAllRooms()
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoomById(int id)
        {
            try
            {
                Room room = await _roomDatalayer.GetRoomById(id);

                return Ok(room);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        
        #region Update
        [HttpPut]
        public async Task<ActionResult<Room>> UpdateRoom(Room room)
        {
            try
            {
                Room roomUpdated = await _roomDatalayer.UpdateRoom(room);

                return Ok(roomUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteRoom(int id)
        {
            try
            {
                bool isDeleted = await _roomDatalayer.DeleteRoom(id);

                return Ok(isDeleted);
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