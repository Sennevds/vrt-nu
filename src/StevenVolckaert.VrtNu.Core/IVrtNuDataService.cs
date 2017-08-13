namespace StevenVolckaert.VrtNu
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IVrtNuDataService
    {
        Task<IEnumerable<LiveChannel>> GetLiveChannelsAsync();
    }
}
