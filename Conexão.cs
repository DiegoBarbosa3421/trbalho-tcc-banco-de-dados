
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conection
{
    /*
     * Classe contendo a string de conecção do banco de dados mysql
     */
    public class Conexão
    {
        private static string conexão = "server=localhost; database=sgagricolatcc; user id=root; password=root; port=3306;";
        public static string stringConecçaõ()
        {
             return conexão;
        }

        
    }
}
