using APIMeteo.Models;

namespace APIMeteo.Interfaces.DataLayers
{
    public interface IMeasuresDatalayer
    {
        /// <summary>
        /// Gets all measures.
        /// </summary>
        /// <returns>A collection of measures.</returns>
        Task<List<Measure>> GetAllMeasures();

        /// <summary>
        /// Gets a measure by its identifier.
        /// </summary>
        /// <param name="id">The measure identifier.</param>
        /// <returns>The measure with the specified identifier.</returns>
        Task<Measure> GetMeasureById(int id);

        /// <summary>
        /// Gets all measures for a room.
        /// </summary>
        /// <param name="roomId">The room identifier.</param>
        /// <returns>A collection of measures for the specified room.</returns>
        Task<List<Measure>> GetMeasuresByRoom(int roomId);

        /// <summary>
        /// Gets the last measure in each room.
        /// </summary>
        /// <returns>Last measure each room</returns>
        Task<List<Measure>> GetLastMeasureInRoom();

        /// <summary>
        /// Creates a new measure.
        /// </summary>
        /// <param name="measure">The measure to create.</param>
        /// <returns>The created measure.</returns>
        Task<Measure> CreateMeasure(Measure measure);

        /// <summary>
        /// Updates an existing measure.
        /// </summary>
        /// <param name="measure">The measure to update.</param>
        /// <returns>The updated measure.</returns>
        Task<Measure> UpdateMeasure(Measure measure);

        /// <summary>
        /// Deletes a measure by its identifier.
        /// </summary>
        /// <param name="id">The measure identifier.</param>
        /// <returns>True if the measure was deleted; otherwise, false.</returns>
        Task<bool> DeleteMeasure(int id);
    }
}