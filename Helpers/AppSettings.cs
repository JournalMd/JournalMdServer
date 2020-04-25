using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JournalMdServer.Helpers
{
    public class AppSettings
    {
        /// <summary>
        /// Secret for JWT.
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Database type for selection of connection string.
        /// sqlite, mssql, mysql
        /// </summary>
        public string DbType { get; set; }
    }
}
