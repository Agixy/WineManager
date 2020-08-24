using NLog;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace View
{
    public static class TryCatchWraper
    {
        private const string loggerName = "TryCatchWraper";

        public static TResult TrySql<TResult>(Func<TResult> action)
        {
            try
            {
               return action();
            }
            catch (SqlException x)
            {
                MessageBox.Show($"Blad połączenia z bazą danych.{Environment.NewLine}Spróbuj ponownie.{Environment.NewLine}{Environment.NewLine}{x.Message}",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                               
                return default;
            }           
        }

        public static void TrySql(Action action)
        {
            ILogger logger = LogManager.GetLogger(loggerName);
            try
            {
                action();
                logger.Info("SUKCES");
            }
            catch (SqlException x)
            {               
                

                MessageBox.Show($"Blad połączenia z bazą danych.{Environment.NewLine}Spróbuj ponownie.{Environment.NewLine}{Environment.NewLine}{x.Message}",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                logger.Error(x);
                return;
            }
        }
    }
}

