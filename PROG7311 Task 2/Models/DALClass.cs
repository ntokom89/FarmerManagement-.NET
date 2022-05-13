using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Web;
using PROG7311_Task_2.Controllers;
namespace PROG7311_Task_2.Models
{
    public class DALClass
    {

        public static string connectionS = "Data Source=DESKTOP-7F27R3N;Initial Catalog=Student_DB;Integrated Security=True";



        public static  void RegisterEmployee(string userID, SecureString password, string email, int employeeID, string firstName, string surname )
        {

            using (SqlConnection con = new SqlConnection(connectionS)){

                SqlCommand cmd = new SqlCommand("AddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@UserPassword", userID);
                cmd.Parameters.AddWithValue("@EmailAddress", userID);
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@FirstName", firstName);
                cmd.Parameters.AddWithValue("@Surname", surname);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }



        public static bool checkPassword(string userId, SecureString password)
        {
            bool bool1 = new Boolean();//Delcare a new Boolean variable bool1.

            //Using the SqlConnection with the connection string.
            using (SqlConnection con = new SqlConnection(connectionS))
            {
                bool1 = false;
                SqlCommand sqlCmd = new SqlCommand("SELECT COUNT(*) FROM [dbo].[Users] WHERE UserId = @UserId AND Password = HASHBYTES('SHA2_512', @Password)", con);//The commmand sql that implements the query  that counts the row where the module code and number of credits match with the connection to the database.
                                                                                                                                                                             //sqlCmd.Parameters.AddWithValue("@Username", username) (Vamnu, 2015)(Gigoyan, 2015);

                //sqlCmd parameters that will first add the attribute and the datatype of it in SQL Server. Then equate the value of that attribute to the value in this program.
                sqlCmd.Parameters.Add("@UserId", SqlDbType.VarChar);
                sqlCmd.Parameters["@UserId"].Value = userId;
                sqlCmd.Parameters.Add("@Password", SqlDbType.VarChar);
                sqlCmd.Parameters["@Password"].Value = password;

                con.Open();//Open the connection
                int isCorrectPassword = (int)sqlCmd.ExecuteScalar();//Execute and  retun a single row of the output from the query and convert it to int.
                //if isCorrectPassword is equal to 1 then 
                if (isCorrectPassword == 1)
                {
                    //Make bool1 be equal to true.
                    bool1 = true;
                }
                //Otherwise
                else
                {
                    //Make bool1 be equal to false.
                    bool1 = false;
                }
                con.Close();//Close the connection.
            }
            return bool1;//return bool1 value.
        }

        public static  void displayProducts()
        {
            using (SqlConnection con = new SqlConnection(connectionS))
            {
                string command = "SELECT ProductID,ProductName,ProductDescription, ProductType,ProductPrice FROM Product";

                SqlCommand sqlCmd = new SqlCommand(command, con);//Sql command that will implement the query shown on top with the connection to the database (Vamnu, 2015).


                //sqlCmd parameters that will first add the attribute and the datatype of it in SQL Server. Then equate the value of that attribute to the value in this program.
        
                con.Open();//Open the connection
                SqlDataReader reader = sqlCmd.ExecuteReader();//Execute the reader
                //While the reader reads the output for each row.
                while (reader.Read())
                {
                    Product product = new Product();//Create a new Module object

                    //retrieve the values for the attributes of the Module class
                    product.ProductId = Convert.ToInt32(reader["ProductID"].ToString());
                    product.ProductName = reader["ProductName"].ToString();
                    product.ProductDescription = reader["ProductDescription"].ToString();
                    product.ProductType = reader["ProductType"].ToString();
                    product.ProductPrice = Convert.ToDouble(reader["ProductPrice"].ToString());

                    Product.products.Add(product);//Add the Module to the ModulesList.
                }
                reader.Close();//close the reader
                con.Close();//close connection
            }
        }

        public static void addProduct(Farmer farmer, int productId, String productName, String productDescription, String productType, DateTime date, Double productPrice)
        {
            using(SqlConnection con = new SqlConnection(connectionS))
            {
                SqlCommand sqlCmd = new SqlCommand("AddProduct", con);//The command that implements the storage procedure that adds values to both Users table  and Students table with the connection to database used (Aside, 2014).
                sqlCmd.CommandType = CommandType.StoredProcedure;//Specify the command type of sqlCmd to stored procedure (Aside, 2014).

                //sqlCmd parameters that will add with the value the attribute and their values into the stored procedure (Aside, 2014).
                sqlCmd.Parameters.AddWithValue("@ProductID", productId);
                sqlCmd.Parameters.AddWithValue("@ProductName", productName);
                sqlCmd.Parameters.AddWithValue("@ProductDescription", productDescription);
                sqlCmd.Parameters.AddWithValue("@ProductType", productType);
                sqlCmd.Parameters.AddWithValue("@ProductPrice", productPrice);
                sqlCmd.Parameters.AddWithValue("@FarmerId", farmer.FarmerID);
                sqlCmd.Parameters.AddWithValue("@DateAddedProduct", productPrice);


                con.Open();//Open the connection.
                sqlCmd.ExecuteNonQuery();//Execute the stored procedure that is given (Aside, 2014).
                con.Close();//Close the connection
            }
        }

        public void addFarmer(Employee employee, String userID, SecureString password, String email, int farmerID, String name, String surname, DateTime date)
        {
            using (SqlConnection con = new SqlConnection(connectionS))
            {

                SqlCommand cmd = new SqlCommand("AddFarmer", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UserID", userID);
                cmd.Parameters.AddWithValue("@UserPassword", password);
                cmd.Parameters.AddWithValue("@EmailAddress", email);
                cmd.Parameters.AddWithValue("@FarmerID", farmerID);
                cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);
                cmd.Parameters.AddWithValue("@FirstName", name);
                cmd.Parameters.AddWithValue("@Surname", surname);
                cmd.Parameters.AddWithValue("@DateAdded", date);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public static void displayFarmers()
        {

            using (SqlConnection con = new SqlConnection(connectionS))
            {
                string command = "SELECT FarmerId,UserID,FarmerName, FarmerSurname FROM Farmer";

                SqlCommand sqlCmd = new SqlCommand(command, con);//Sql command that will implement the query shown on top with the connection to the database (Vamnu, 2015).


                //sqlCmd parameters that will first add the attribute and the datatype of it in SQL Server. Then equate the value of that attribute to the value in this program.

                con.Open();//Open the connection
                SqlDataReader reader = sqlCmd.ExecuteReader();//Execute the reader
                //While the reader reads the output for each row.
                while (reader.Read())
                {
                    Farmer farmer = new Farmer();//Create a new Module object

                    //retrieve the values for the attributes of the Module class
                    farmer.FarmerID = Convert.ToInt32(reader["FarmerId"].ToString());
                    farmer.UserID = reader["UserID"].ToString();
                    farmer.FarmerName = reader["FarmerName"].ToString();
                    farmer.FarmerSurname = reader["FarmerSurname"].ToString();


                    Farmer.farmerList.Add(farmer);//Add the Module to the ModulesList.
                }
                reader.Close();//close the reader
                con.Close();//close connection
            }
        }

        public static  void displayProductByFarmer(int farmerID, string farmerName)
        {

            using (SqlConnection con = new SqlConnection(connectionS))
            {
                string command = "SELECT FarmerId,UserID,FarmerName, FarmerSurname FROM Farmer";

                SqlCommand sqlCmd = new SqlCommand(command, con);//Sql command that will implement the query shown on top with the connection to the database (Vamnu, 2015).


                //sqlCmd parameters that will first add the attribute and the datatype of it in SQL Server. Then equate the value of that attribute to the value in this program.

                con.Open();//Open the connection
                SqlDataReader reader = sqlCmd.ExecuteReader();//Execute the reader
                //While the reader reads the output for each row.
                while (reader.Read())
                {
                    Farmer farmer = new Farmer();//Create a new Module object

                    //retrieve the values for the attributes of the Module class
                    farmer.FarmerID = Convert.ToInt32(reader["FarmerId"].ToString());
                    farmer.UserID = reader["UserID"].ToString();
                    farmer.FarmerName = reader["FarmerName"].ToString();
                    farmer.FarmerSurname = reader["FarmerSurname"].ToString();


                    Farmer.farmerList.Add(farmer);//Add the Module to the ModulesList.
                }
                reader.Close();//close the reader
                con.Close();//close connection
            }
        }

        public static User selectUser(User userObj)
        {
            //Using the SqlConnection with the connection string.
            using (SqlConnection con = new SqlConnection(connectionS))
            {

                SqlCommand sqlCmd = new SqlCommand("SELECT UserID, UserType, EmailAddress FROM Students WHERE UserID = @UserID", con);
                //SqlCommand sqlCmd2 = new SqlCommand("SELECT StudentNum, FirstName, Surname FROM Students WHERE Username = @Username", con);//The sql Command that will execute the query that will get the student details with the connection to the database.

                //sqlCmd parameters that will first add the attribute and the datatype of it in SQL Server. Then equate the value of that attribute to the value in this program.
                sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar);
                sqlCmd.Parameters["@UserID"].Value = userObj.userId;
                con.Open();//Open the connection
                SqlDataReader reader = sqlCmd.ExecuteReader();//Execute the reader.
                //While the reader reads the output for each row.
                while (reader.Read())
                {
                    //Retrieve the values for Student Number, first name, surname
                   // userObj.Use = Convert.ToInt32(reader["StudentNum"].ToString());
                    userObj.userType = reader["UserType"].ToString();
                    userObj.userEmail = reader["EmailAddress"].ToString();
                }
                reader.Close();//Close the reader
                con.Close();//close the connection
            }
            return userObj;
        }

        public static Employee selectEmployee(User userObj)
        {
            Employee employee = new Employee();
            //Using the SqlConnection with the connection string.
            using (SqlConnection con = new SqlConnection(connectionS))
            {

                SqlCommand sqlCmd = new SqlCommand("SELECT UserID, EmployeeID, EmployeeName, EmployeeSurname FROM Employee WHERE UserID = @UserID", con);
                //SqlCommand sqlCmd2 = new SqlCommand("SELECT StudentNum, FirstName, Surname FROM Students WHERE Username = @Username", con);//The sql Command that will execute the query that will get the student details with the connection to the database.

                //sqlCmd parameters that will first add the attribute and the datatype of it in SQL Server. Then equate the value of that attribute to the value in this program.
                sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar);
                sqlCmd.Parameters["@UserID"].Value = userObj.userId;
                con.Open();//Open the connection
                SqlDataReader reader = sqlCmd.ExecuteReader();//Execute the reader.
                //While the reader reads the output for each row.
                while (reader.Read())
                {
                    //Retrieve the values for Student Number, first name, surname
                    // userObj.Use = Convert.ToInt32(reader["StudentNum"].ToString());
                    employee.UserID = reader["UserID"].ToString();
                    employee.EmployeeID = Convert.ToInt32(reader["EmailAddress"].ToString());
                    employee.EmployeeName = reader["EmployeeName"].ToString();
                    employee.EmployeeSurname = reader["EmployeeSurname"].ToString();
                }
                reader.Close();//Close the reader
                con.Close();//close the connection
            }
            return employee;
        }

        public static Farmer selectFarmer(User userObj)
        {
            Farmer farmer = new Farmer();
            //Using the SqlConnection with the connection string.
            using (SqlConnection con = new SqlConnection(connectionS))
            {

                SqlCommand sqlCmd = new SqlCommand("SELECT UserID, FarmerID, FarmerName, FarmerSurname FROM Students WHERE UserID = @UserID", con);
                //SqlCommand sqlCmd2 = new SqlCommand("SELECT StudentNum, FirstName, Surname FROM Students WHERE Username = @Username", con);//The sql Command that will execute the query that will get the student details with the connection to the database.

                //sqlCmd parameters that will first add the attribute and the datatype of it in SQL Server. Then equate the value of that attribute to the value in this program.
                sqlCmd.Parameters.Add("@UserID", SqlDbType.VarChar);
                sqlCmd.Parameters["@UserID"].Value = userObj.userId;
                con.Open();//Open the connection
                SqlDataReader reader = sqlCmd.ExecuteReader();//Execute the reader.
                //While the reader reads the output for each row.
                while (reader.Read())
                {
                    //Retrieve the values for Student Number, first name, surname
                    // userObj.Use = Convert.ToInt32(reader["StudentNum"].ToString());
                    farmer.UserID = reader["UserID"].ToString();
                    farmer.FarmerID = Convert.ToInt32( reader["FarmerID"].ToString());
                    farmer.FarmerName = reader["FarmerName"].ToString();
                    farmer.FarmerSurname = reader["FarmerSurname"].ToString();
                }
                reader.Close();//Close the reader
                con.Close();//close the connection
            }
            return farmer;
        }
    }
}