﻿using SistemaAcademicos.Helpers;

namespace SistemaAcademicos
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _db;

        public static SQLiteDatabaseHelper Db
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "banco_periodos.db3");

                    _db = new SQLiteDatabaseHelper(path);
                }
                return _db;
            }
        }
        
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
