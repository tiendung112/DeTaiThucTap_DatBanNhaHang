﻿namespace DatBanNhaHang.Context
{
    public class SourseData
    {
        public static string MyConnect()
        {
            return $"Server=DUNGARTORIA;Database =DatBanNhaHang_DTB;Integrated Security = true;TrustServerCertificate=True";
            //return $"Server=tcp:datbandtb.database.windows.net,1433;Initial Catalog=datbannhahang;Persist Security Info=False;User ID=dung;Password={"Ptd1122002@"};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";  
            //return $"Server=NamJS ;Database = DatBanNhaHang_DTB;Integrated Security = true;TrustServerCertificate=True";
        }
    }
}
