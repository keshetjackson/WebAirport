namespace WebTerminalsServer.Dal
{
    public class DataContextFactory
    {
        private static DataContext _dbcontext;
        public static DataContext dataContext { get { return _dbcontext; } }
    }
}
