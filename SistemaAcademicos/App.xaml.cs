using SistemaAcademicos.Helpers;

namespace SistemaAcademicos
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper _db;

        static SQLiteDatabaseHelper2 _db2;

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
        public static SQLiteDatabaseHelper2 Db2
        {
            get
            {
                if (_db2 == null)
                {
                    string path2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "banco_disciplinas.db3");

                    _db2 = new SQLiteDatabaseHelper2(path2);
                }
                return _db2;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
