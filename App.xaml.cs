using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace work1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MySql.Data.MySqlClient.MySqlConnection connection;

        public static bool DataSaveResult;
        public static bool DataSearchResult;

    }
}
