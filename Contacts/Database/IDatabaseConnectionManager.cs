using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    interface IDatabaseConnectionManager
    {
        void Open();

        IDbConnection Connection { get; }

        void Close();
    }
}
