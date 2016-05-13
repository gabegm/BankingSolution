using CommonLayer;

namespace DataLayer
{
    public class ConnectionClass
    {
        public DBModelEntities Entity { get; set; }

        public ConnectionClass()
        {
            Entity = new DBModelEntities();
        }
    }
}
