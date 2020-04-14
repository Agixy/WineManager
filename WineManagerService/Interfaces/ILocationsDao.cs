using Model;
using System.Collections.Generic;

namespace WineManagerService.Interfaces
{
    public interface ILocationsDao
    {
        IList<Location> GetAllLocations();
        IList<Location> GetLocationsBySkrot(string wineSkrot);
        void GetWineFromLocation(Location checkedLocation, int wineId);
        void AddWineToLocation(int locationId, int wineId);
        bool CheckIsLocationFull(int id);
        Location GetLocationByName(string locationName);
    }
}
