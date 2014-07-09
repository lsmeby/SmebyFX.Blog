using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using NUnit.Framework;
using SmebyFX_blog.DBMigration;

namespace SmebyFX_blog.Post.IntegrationTest
{
    [SetUpFixture]
    public class SetupDatabase
    {
        [SetUp]
        public void MigrateDatabaseToLatestVersion()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["blogDBconnectionString"].ConnectionString;
            CreateIfNeededFrom(connectionString);
            Migrate.MigrateToLatestVersion(connectionString);
        }

        private void CreateIfNeededFrom(string connString)
        {
            var databaseDirectory = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\SmebyFX_blog.Data"));
            AppDomain.CurrentDomain.SetData("DataDirectory", databaseDirectory.FullName);

            var connStringParsed = new SqlConnectionStringBuilder(connString);
            if (connStringParsed.DataSource == @"(LocalDB)\v11.0")
            {
                var mdbFile = new FileInfo(connStringParsed.AttachDBFilename.Replace("|DataDirectory|", databaseDirectory.FullName));

                if (!mdbFile.Exists)
                {
                    var emptyFileName = mdbFile.Directory.FullName + @"\" +
                                        mdbFile.Name.Substring(0, mdbFile.Name.IndexOf('.')) +
                                        "_empty" + mdbFile.Extension;
                    var emptyFile = new FileInfo(emptyFileName);
                    if (!emptyFile.Exists)
                    {
                        throw new ArgumentException("Could not find " + emptyFileName);
                    }
                    emptyFile.CopyTo(mdbFile.FullName);
                }
            }
        }
    }
}
