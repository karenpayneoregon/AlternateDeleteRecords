using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseLibrary;
using SqlRepository;

namespace BackEndData
{
    public class SqlServerOperations : BaseSqlServerConnections
    {
        public SqlServerOperations()
        {
            DefaultCatalog = "NorthWindAzure2";
        }
        /// <summary>
        /// Read Category table by InUse, true for active categories, false for
        /// inactive categories
        /// </summary>
        /// <param name="pInUse">True for active, false for inactive</param>
        /// <returns></returns>
        public DataTable Categories(bool pInUse = true)
        {
            var dt = new DataTable();
            var selectStatement = "SELECT CategoryID,CategoryName,[Description],ModifiedDate,InUse " + 
                                  "FROM dbo.Categories " + 
                                  "WHERE InUse = @InUse";

            using (SqlConnection cn = new SqlConnection {ConnectionString = ConnectionString})
            {
                using (SqlCommand cmd = new SqlCommand {Connection = cn})
                {
                    cmd.CommandText = selectStatement;
                    try
                    {
                        cmd.Parameters.AddWithValue("@InUse", pInUse);
                        cn.Open();
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch (Exception e)
                    {
                        mHasException = true;
                        mLastException = e;
                    }
                }
            }

            return dt;
        }
        /// <summary>
        /// Read Customers table by InUse, true for active Customers, false for
        /// inactive Customers
        /// </summary>
        /// <param name="pInUse">True for active, false for inactive</param>
        /// <returns></returns>
        public DataTable Customers(bool pInUse = true)
        {
            var dt = new DataTable();
            CustomerStatements cs = new CustomerStatements();
            var selectStatement = cs.SelectStandard();

            using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (SqlCommand cmd = new SqlCommand { Connection = cn })
                {
                    cmd.CommandText = selectStatement;
                    try
                    {
                        cmd.Parameters.AddWithValue("@InUse", pInUse);
                        cn.Open();
                        dt.Load(cmd.ExecuteReader());
                    }
                    catch (Exception e)
                    {
                        mHasException = true;
                        mLastException = e;
                    }
                }
            }

            return dt;
        }
        /// <summary>
        /// Get all inactive customers
        /// </summary>
        /// <returns></returns>
        public List<CustomerItem> InactiveCustomers()
        {
            var customerList = new List<CustomerItem>();

            using (SqlConnection cn = new SqlConnection {ConnectionString = ConnectionString})
            {
                using (SqlCommand cmd = new SqlCommand {Connection = cn})
                {
                    cmd.CommandText = "SELECT CustomerIdentifier, CompanyName FROM dbo.Customers WHERE [InUse] = @Active";

                    cmd.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@Active",
                        SqlDbType = SqlDbType.Bit,
                        Value = 0
                    });

                    try
                    {
                        cn.Open();
                        var reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                customerList.Add(new CustomerItem() { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        mHasException = true;
                        mLastException = e;
                    }
                }
            }

            return customerList;
        }

        /// <summary>
        /// Obtain column configurations for Customers table
        /// </summary>
        /// <returns></returns>
        public List<DataGridViewColumnDefinition> CustomerConfigurationItems()
        {
            CustomerStatements cs = new CustomerStatements();
            return cs.ConfigureDataGridViewColumns();
        }
        /// <summary>
        /// Deactivate a specific Customer record by primary key
        /// </summary>
        /// <param name="pIdentifier">Primary key</param>
        /// <returns></returns>
        public bool InactivateCustomer(int pIdentifier)
        {
            using (SqlConnection cn = new SqlConnection {ConnectionString = ConnectionString})
            {
                using (SqlCommand cmd = new SqlCommand {Connection = cn})
                {
                    cmd.CommandText = "UPDATE dbo.Customers SET InUse = 0, ModifiedDate = GETDATE() " + 
                                      "WHERE CustomerIdentifier = @Id";

                    try
                    {
                        cmd.Parameters.AddWithValue("@Id", pIdentifier);
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
        /// <summary>
        /// Activate a specific Customer record by primary key
        /// </summary>
        /// <param name="pIdentifier">Primary key</param>
        /// <returns></returns>
        public bool ActivateCustomer(int pIdentifier) 
        {
            using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (SqlCommand cmd = new SqlCommand { Connection = cn })
                {
                    cmd.CommandText = "UPDATE dbo.Customers SET InUse = 1, ModifiedDate = GETDATE() " + 
                                      "WHERE CustomerIdentifier = @Id";
                    try
                    {
                        cmd.Parameters.AddWithValue("@Id", pIdentifier);
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
        /// <summary>
        /// Activate a specific Customer record by primary key
        /// </summary>
        /// <returns></returns>
        public bool ActivateAllCustomer()
        {
            using (SqlConnection cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (SqlCommand cmd = new SqlCommand { Connection = cn })
                {
                    cmd.CommandText = "UPDATE dbo.Customers SET InUse = 1, ModifiedDate = GETDATE()";
                    try
                    {
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
    }
}
