using System;
using S

namespace Database
{
    static class DatabaseFactory
    {
        public static IDbConnection CreateDbConnection(string server, string db, string user, string pass)
        {
            DbConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = db,
                UserID = user,
                Password = pass
            };

            return new SqlConnection(builder.ConnectionString);
        }

        public static IDbCommand CreateCommand(string storedProcName, params DbParameter[] parameters)
        {
            var storedProcedure = new SqlCommand(storedProcName)
            {
                CommandType = CommandType.StoredProcedure
            };

            storedProcedure.Parameters.AddRange(parameters);
            return storedProcedure;
        }
    }
}
