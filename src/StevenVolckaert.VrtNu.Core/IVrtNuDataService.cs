namespace StevenVolckaert.VrtNu
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVrtNuDataService
    {
        Task<IEnumerable<ProgramCategory>> GetAllProgramCategoriesAsync();

        /// <summary>
        ///     Gets all programs, ordered alphabetically.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerable{Program}"/> that contains all programs, ordered alphabetically.
        /// </returns>
        Task<IEnumerable<Program>> GetAllProgramsAsync();

        Task<IEnumerable<LiveChannel>> GetLiveChannelsAsync();
    }
}
