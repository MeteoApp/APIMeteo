using APIMeteo.Interfaces.DataLayers;
using APIMeteo.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIMeteo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureController : ControllerBase
    {
        #region Properties
        private readonly IMeasuresDatalayer _measureDatalayer;
        private readonly IRoomDatalayer _roomDatalayer;
        private readonly IAlertDatalayer _alertDatalayer;
        #endregion

        #region Constructor
        public MeasureController(IMeasuresDatalayer measureDatalayer, IRoomDatalayer roomDatalayer, IAlertDatalayer alertDatalayer)
        {
            _measureDatalayer = measureDatalayer;
            _roomDatalayer = roomDatalayer;
            _alertDatalayer = alertDatalayer;
        }
        #endregion

        #region Methods

        #region Create
        [HttpPost]
        public async Task<ActionResult<Measure>> AddMeasure(Measure measure)
        {
            try
            {
                Measure measureAdded = await _measureDatalayer.CreateMeasure(measure);

                return Ok(measureAdded);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Read
        [HttpGet]
        public async Task<ActionResult<List<Measure>>> GetAllmeasures()
        {
            try 
            {
                List<Measure> measures = await _measureDatalayer.GetAllMeasures();

                return Ok(measures);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Measure>> GetmeasureById(int id)
        {
            try
            {
                Measure measure = await _measureDatalayer.GetMeasureById(id);

                return Ok(measure);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("room/{roomId}")]
        public async Task<ActionResult<List<Measure>>> GetmeasuresByRoom(int roomId)
        {
            try
            {
                List<Measure> measures = await _measureDatalayer.GetMeasuresByRoom(roomId);

                return Ok(measures);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("last")]
        public async Task<ActionResult<List<Measure>>> GetLastMeasureInRoom()
        {
            try
            {
                List<Measure> measures = await _measureDatalayer.GetLastMeasureInRoom();

                return Ok(measures);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("AddDataTest")]
        public async Task<IActionResult> AddData()
        {
            var rooms = new List<Room>
            {
                new Room { RoomName = "Room1", RoomId = 4 },
                new Room { RoomName = "Room2", RoomId = 5 },
                new Room { RoomName = "Room3", RoomId = 6 }
            };

            foreach (var room in rooms)
            {
                //await _roomDatalayer.AddRoom(room);
            }

            var random = new Random();
            foreach (var room in rooms)
            {
                for (int i = 0; i < 20; i++)
                {
                    var measure = new Measure
                    {
                        Room = room,
                        Altitude = random.Next(0, 1000),
                        Temperature = random.Next(-10, 35),
                        Humidity = random.Next(0, 100),
                        Pressure = random.Next(950, 1050),
                        Date = DateTime.Now.AddMinutes(-i * 10)
                    };
                    await _measureDatalayer.CreateMeasure(measure);
                }
            }

            foreach (var room in rooms)
            {
                for (int i = 0; i < 2; i++)
                {
                    var alert = new Alert
                    {
                        Measure = new Measure { Room = room },
                        AlertMessage = $"Alert {i + 1} for {room.RoomName}"
                    };

                    await _alertDatalayer.CreateAlert(alert);
                }
            }
        return Ok();
        }
        #endregion
        
        #region Update
        [HttpPut]
        public async Task<ActionResult<Measure>> Updatemeasure(Measure measure)
        {
            try
            {
                Measure measureUpdated = await _measureDatalayer.UpdateMeasure(measure);

                return Ok(measureUpdated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Deletemeasure(int id)
        {
            try
            {
                bool isDeleted = await _measureDatalayer.DeleteMeasure(id);

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