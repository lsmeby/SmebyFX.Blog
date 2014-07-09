using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Announcers;
using log4net;

namespace SmebyFX_blog.DBMigration
{
    public class Migrate
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof (Migrate));

        public static void MigrateToLatestVersion(string connectionString)
        {
            var migrationContext = new RunnerContext(new Log4NetAnnouncer())
            {
                Connection = connectionString,
                Database = "SqlServer2008",
                Target = typeof(Migrate).Assembly.GetName().Name
            };

            var executor = new TaskExecutor(migrationContext);
            executor.Execute();
        }

        private class Log4NetAnnouncer : Announcer
        {
            public override void Write(string message, bool escaped)
            {
                Log.Debug(message);
            }
        }
    }
}