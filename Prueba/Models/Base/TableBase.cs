using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prueba.Models.Base
{
    public class TableBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
