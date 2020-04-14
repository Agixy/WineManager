using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace WineManagerService
{
    public class WineDao : SqlExecuter
    {
        private readonly int homeScreenId;
        private int articlePosiionX = 0;
        private int articlePosiionY = 0;
        private const string connectionString = @"Server=81.95.197.33,1058;Integrated Security=false;user id =XXXXX;password=XXXXX;database=Marta";
     
        public WineDao(int homeScreenId)
        {
            this.homeScreenId = homeScreenId;
        }

        public (int, int) GetScreenSize()
        {
            var sql = $@"SELECT [EKR_Szerokosc]
                               ,[EKR_Wysokosc]
                        FROM [EKR_Ekrany] 
                        WHERE [EKR_ID] = {homeScreenId}";

            SqlDataReader reader = ExecuteSqlQuery(connectionString, sql);

            if (reader != null)
            {
                while (reader.Read())
                {
                    return (reader.GetInt16(0), reader.GetInt16(1));
                }
            }
            else
            {
                throw new Exception(@"Nie udało się pobrać rozmiariu ekranu z tabeli [EKR_Ekrany]");
            }
            return (0, 0);
        }


        public IList<WineTypeButtonDto> GetAllButtons()
        {
            var result = new List<WineTypeButtonDto>();

            var homeViewButtons = GetAllButtonsByParentScreenId(homeScreenId);
            result.AddRange(homeViewButtons);

            var restOfParentScreenIds = homeViewButtons.Select(b => b.ScreenId);

            foreach (var id in restOfParentScreenIds)
            {
                if (id > 0)
                {
                    var buttons = GetAllButtonsByParentScreenId(id);

                    result.AddRange(buttons);
                }
            }

            return result;
        }


        public IList<WineTypeButtonDto> GetAllButtonsByParentScreenId(int parentButtonId)
        {
            var results = new List<WineTypeButtonDto>();

            string sqlQuery = $@"SELECT   [PRZ_ID]
                             ,[PRZ_EKRIDParent]
                             ,[PRZ_EKRID]
                             ,[PRZ_ARTID]
                             ,[PRZ_KATID]
                             ,[PRZ_PozycjaX]
                             ,[PRZ_PozycjaY]
                             ,[PRZ_Wysokosc]
                             ,[PRZ_Szerokosc]
                             ,[PRZ_Opis]
                             ,[PRZ_Kolor]
                             ,[PRZ_FontName]
                             ,[PRZ_FontSize]
                             ,[PRZ_CzyFontBold]
                             ,[PRZ_CzyFontItalic]
                             ,[PRZ_CzyFontUnderline]
                             ,[PRZ_FontColor]
                     FROM PRZ_Przyciski 
                        WHERE PRZ_EKRIDParent = {parentButtonId}";

            SqlDataReader reader = ExecuteSqlQuery(connectionString, sqlQuery);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    int articleId = reader.GetIntFromNullableColumn(3);
                    int categoryId = reader.GetIntFromNullableColumn(4);

                    var wineId = articleId > 0 ? articleId : 0;
                    var wineDto = articleId > 0 ? GetWineById(wineId) : new WineDto(wineId, reader.GetString(9));
                    results.Add(new WineTypeButtonDto()
                    {
                        Wine = wineDto,
                        ScreenId = reader.GetIntFromNullableColumn(2),
                        ParentButtonId = reader.GetInt32(1),
                        CategoryId = categoryId,
                        ArticleId = articleId,
                        Height = reader.GetInt16(7),
                        Width = reader.GetInt16(8),
                        PositionX = reader.GetInt16(5),
                        PositionY = reader.GetInt16(6),
                        ColorCode = reader.GetInt32(10),
                        FontSize = reader.GetDecimal(12),
                        FontColorCode = reader.GetIntFromNullableColumn(16)
                    });
                }
            }
            else
            {
                throw new Exception("No data found in PRZ_Przyciski");
            }

            reader.Close();
            return results;
        }

        public IList<WineTypeButtonDto> GetArticlesByCategoryId(int categoryId)
        {
            var results = new List<WineTypeButtonDto>();

            string sqlQuery = $@"SELECT AXK.[AXK_ARTID]
                         ,AXK.[AXK_KATID]
                         ,ART.[ART_ID]
                         ,ART.[ART_Nr]
                         ,ART.[ART_Skrot]
                         ,ART.[ART_Nazwa]     
                   FROM [AXK_ArtXKat] AXK,  [ART_Artykuly] ART         
                      WHERE AXK.[AXK_KATID] = {categoryId} AND AXK.[AXK_ARTID] = ART.[ART_ID]";

            SqlDataReader reader = ExecuteSqlQuery(connectionString, sqlQuery);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    results.Add(new WineTypeButtonDto()
                    {
                        Id = reader.GetInt32(2),
                        Wine = new WineDto(reader.GetInt32(2), reader.GetString(5), reader.GetString(4)),
                        PositionX = articlePosiionX,
                        PositionY = articlePosiionY,
                        Height = 1,
                        Width = 7
                    });

                    articlePosiionY += 1;
                }
            }
            else
            {
                throw new Exception("Nie znaleziono win w kategorii.");
            }

            articlePosiionX = 0;
            articlePosiionY = 0;

            return results;
        }


        public WineDto GetWineBySkrot(string wineSkrot)
        {
            string sql = $@"SELECT ART.[ART_ID]
                                  ,ART.[ART_Nr]
                                  ,ART.[ART_Nazwa]
	                              ,KAT.KAT_Nazwa
                           FROM [ART_Artykuly] ART 
                           INNER JOIN [AXK_ArtXKat] AXK on ART.ART_ID = AXK.AXK_ARTID
                           INNER JOIN [KAT_Kategorie] KAT on KAT.KAT_ID = AXK.AXK_KATID
                           WHERE ART.ART_Nr = {wineSkrot}";  // W- addto skrot


            SqlDataReader reader = ExecuteSqlQuery(connectionString, sql);

            if (reader != null)
            {
                while (reader.Read())
                {
                    return new WineDto(
                        reader.GetInt32(0),
                        reader.GetString(2),
                        reader[1].ToString())
                    { CategoryName = reader.GetString(3) };
                }
            }

            return null;
        }       

        public WineDto GetWineById(int wineId)
        {
            string sqlQuery = $@"SELECT [ART_ID]
                                       ,[ART_Nr]
                                       ,[ART_Skrot]
                                       ,[ART_Nazwa]
                                FROM [ART_Artykuly] where ART_ID = {wineId}";

            var reader = ExecuteSqlQuery(connectionString, sqlQuery);

            if (reader != null)
            {
                while (reader.Read())
                {
                    return new WineDto(reader.GetInt32(0), reader.GetString(3), reader.GetString(2));
                }
            }
            else
            {
                throw new Exception("Nie znaleziono win.");
            }

            return null;
        }
    }

    public static class SqlDataReaderExtensions
    {
        public static int GetIntFromNullableColumn(this SqlDataReader reader, int columnIndex)
        {
            if (reader.IsDBNull(columnIndex))
            {
                return -1;
            }
            else
            {
                return reader.GetInt32(columnIndex);
            }
        }
    }
}
