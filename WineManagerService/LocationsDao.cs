using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WineManagerService.Interfaces;

namespace WineManagerService
{
    public class LocationsDao : SqlExecuter, ILocationsDao
    {
        private const string connectionString = @"Server=81.95.197.33,1058;Integrated Security=false;user id =XXXXX;password=XXXXX;database=WineCabinet";
                
        public IList<Location> GetAllLocations()
        {
            var sqlQuery = $@"SELECT [Id], [Name], [Max_State]
                         FROM[Locations]";

            SqlDataReader reader = ExecuteSqlQuery(connectionString, sqlQuery);

            var results = new List<Location>();

            if (reader != null)
            {
                while (reader.Read())
                {
                    results.Add(new Location()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        MaxState = reader.GetInt32(2),

                    });
                }
                reader.Close();
            }
            else
            {
                throw new Exception("No data found in Locations");
            }            

            return results;
        }

        public Location GetLocationByName(string locationName)
        {
            var sql = $@"SELECT [Id], [Name], [Max_State]
                         FROM [Locations] WHERE [Name] = '{locationName}'";

            SqlDataReader reader = ExecuteSqlQuery(connectionString, sql);

            if (reader != null)
            {
                while (reader.Read())
                {
                   return new Location()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        MaxState = reader.GetInt32(2),

                    };
                }
                reader.Close();
            }
            else
            {
                throw new Exception("No data found in Locations");
            }            
            return null;            
        }

        public IList<Location> GetLocationsBySkrot(string wineSkrot)
        {
            var results = new List<Location>();

            string sqlQuery = $@"SELECT L.Id, L.Name, L.Max_State
                                 FROM [Marta].[dbo].[ART_Artykuly] A
                                 INNER JOIN [WineCabinet].[dbo].[Wine_Locations] B on A.ART_ID = B.ART_ID
                                 INNER JOIN [WineCabinet].[dbo].[Locations] L on L.Id = B.Location_ID
                                 WHERE A.ART_Skrot = {wineSkrot}";

            SqlDataReader reader = ExecuteSqlQuery(connectionString, sqlQuery);

            if (reader != null)
            {
                while (reader.Read())
                {
                    results.Add(new Location()
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        MaxState = reader.GetInt32(2),
                    });
                }
                reader.Close();
            }           
            return results;
        }

        public void GetWineFromLocation(Location location, int wineId)
        {
            var sqlQuery = $@"DELETE TOP(1) FROM [Wine_Locations]
                         WHERE ART_ID = {wineId} AND Location_ID = {location.Id}";

            ExecuteSqlQuery(connectionString, sqlQuery);
        }


        public IEnumerable<string> GetWinesInLocation(string locationName)
        {
            var wines = new List<string>();
            var sqlQuery = $@"SELECT C.ART_Nazwa, count(B.ID)
                FROM WineCabinet.dbo.Locations A
                inner join WineCabinet.dbo.Wine_Locations B on A.ID = B.Location_ID
                inner join Marta.dbo.ART_Artykuly C on C.ART_ID = B.ART_ID
                WHERE [Name] = '{locationName}'
                group by C.ART_Nazwa ";

            SqlDataReader reader = ExecuteSqlQuery(connectionString, sqlQuery);

            if (reader !=null)
            {
                while (reader.Read())
                {
                    wines.Add($"{reader.GetString(0)} [{reader.GetInt32(1)}]");
                }
                reader.Close();
            }
            return wines;
        }
 
        public void AddWineToLocation(int locationId, int wineId)
        {
            var sqlQuery = $@"INSERT INTO Wine_Locations(ART_ID, Location_ID)
                         VALUES({wineId}, {locationId})";

            ExecuteSqlQuery(connectionString, sqlQuery);
        }

        public bool CheckIsLocationFull(int locationId)
        {
            var sqlQuery = $@"SELECT [Max_State]
                         FROM[Locations] WHERE Id = {locationId}";

            var reader = ExecuteSqlQuery(connectionString, sqlQuery);

            int maxState = 0;
            if (reader != null)
            {
                while (reader.Read())
                {
                    maxState = reader.GetInt32(0);
                }
                reader.Close();
            }            

            var winesOnLocationSqlQuery = $"SELECT COUNT(*) AS WinesCount FROM  [Wine_Locations] WHERE Location_ID = {locationId}";

            SqlDataReader winesCountReader = ExecuteSqlQuery(connectionString, winesOnLocationSqlQuery);
            winesCountReader.Read();
                
            return maxState <= winesCountReader.GetInt32(0);                   
        }
    }
}
