using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary;

namespace BackendUnitTest
{
    public class TestBase : BaseSqlServerConnections
    {
        public TestBase()
        {
            DefaultCatalog = "NorthWindAzure2";
        }

        protected bool SetInUse(string pTableName, string pIdentifier, List<int> pIdentifiers, bool pInUse = true)
        {
            var inConditions = $"({string.Join(",", pIdentifiers.ToArray())})";
            var updateStatement = $"UPDATE dbo.{pTableName} SET InUse = @InUse, ModifiedDate = GETDATE() WHERE {pIdentifier} IN {inConditions}";

            using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (SqlCommand cmd = new SqlCommand { Connection = cn })
                {
                    cmd.CommandText = updateStatement;
                    try
                    {
                        cmd.Parameters.AddWithValue("@InUse", pInUse);
                        cn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        mHasException = true;
                        mLastException = e;
                    }
                }
            }

            return IsSuccessFul;
        }

        protected bool GetInUse(string pTableName, string pIdentifier, List<int> pIdentifiers, bool pInUse = true)
        {
            var inConditions = $"({string.Join(",", pIdentifiers.ToArray())})";
            var updateStatement = $"SELECT COUNT(InUse) FROM dbo.{pTableName} WHERE {pIdentifier} IN {inConditions} AND InUse = @InUse";

            using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (SqlCommand cmd = new SqlCommand { Connection = cn })
                {
                    cmd.CommandText = updateStatement;
                    try
                    {
                        cmd.Parameters.AddWithValue("@InUse", pInUse);
                        cn.Open();
                        var temp = (int) cmd.ExecuteScalar();
                        return temp == pIdentifiers.Count;
                    }
                    catch (Exception e)
                    {
                        mHasException = true;
                        mLastException = e;
                    }
                }
            }

            return IsSuccessFul;
        }

        protected bool ResetCategoryInUseField(bool pInUse = true)
        {

            var updateStatement = "UPDATE dbo.Categories SET InUse = @InUse";
            var selectStatementInUse = "SELECT COUNT(CategoryID) FROM dbo.Categories WHERE InUse = @InUse";
            var selectStatementTotal = "SELECT COUNT(CategoryID) FROM dbo.Categories";

            using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (SqlCommand cmd = new SqlCommand { Connection = cn })
                {
                    cmd.CommandText = updateStatement;
                    try
                    {
                        cmd.Parameters.AddWithValue("@InUse", pInUse);
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = selectStatementTotal;
                        var totalRows = (int) cmd.ExecuteScalar();
                        cmd.CommandText = selectStatementInUse;
                        return totalRows == (int) cmd.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        mHasException = true;
                        mLastException = e;
                    }
                }
            }

            return IsSuccessFul;
        }
        protected bool ResetCustomersInUseField(bool pInUse = true) 
        {

            var updateStatement = "UPDATE dbo.Customers SET InUse = @InUse";
            var selectStatementInUse = "SELECT COUNT(CustomerIdentifier) FROM dbo.Customers WHERE InUse = @InUse";
            var selectStatementTotal = "SELECT COUNT(CustomerIdentifier) FROM dbo.Customers";

            using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (SqlCommand cmd = new SqlCommand { Connection = cn })
                {
                    cmd.CommandText = updateStatement;
                    try
                    {
                        cmd.Parameters.AddWithValue("@InUse", pInUse);
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = selectStatementTotal;
                        var totalRows = (int)cmd.ExecuteScalar();
                        cmd.CommandText = selectStatementInUse;
                        var temp = (int) cmd.ExecuteScalar();
                        return totalRows == temp;
                    }
                    catch (Exception e)
                    {
                        mHasException = true;
                        mLastException = e;
                    }
                }
            }

            return IsSuccessFul;
        }

        protected bool ResetContactsInUseField(bool pInUse = true)
        {

            var updateStatement = "UPDATE dbo.Contact SET InUse = @InUse";
            var selectStatementInUse = "SELECT COUNT(ContactIdentifier) FROM dbo.Contact WHERE InUse = @InUse";
            var selectStatementTotal = "SELECT COUNT(ContactIdentifier) FROM dbo.Contact";

            using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (SqlCommand cmd = new SqlCommand { Connection = cn })
                {
                    cmd.CommandText = updateStatement;
                    try
                    {
                        cmd.Parameters.AddWithValue("@InUse", pInUse);
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = selectStatementTotal;
                        var totalRows = (int)cmd.ExecuteScalar();
                        cmd.CommandText = selectStatementInUse;
                        return totalRows == (int)cmd.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        mHasException = true;
                        mLastException = e;
                    }
                }
            }

            return IsSuccessFul;
        }

    }
}
