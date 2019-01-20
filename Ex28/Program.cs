using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Ex28
{
    class Program
    {
        private static string connectionString = 
            "Data Source = EALSQL1.Eal.Local;" +
            "Database = C_DB10_2018;" +
            "User ID = C_STUDENT10;" +
            "Password = C_OPENDB10;";

        static void Main(string[] args)
        {
            Program program = new Ex28.Program();
            program.Run();
        }

        public void Run()
        {
            bool runWhileTrue = true;
            while (runWhileTrue)
            {
                Console.Clear();
                Console.WriteLine("1. Insert a pet");
                Console.WriteLine("2. Show all pets");
                int menuInput = int.Parse(Console.ReadLine());
                switch (menuInput)
                {
                    case 0:
                        runWhileTrue = false;
                        break;
                    case 1:
                        Console.WriteLine("==== Insert a Pet ====");
                        Console.Write("Name: ");
                        string name = Console.ReadLine();
                        Console.Write("Type: ");
                        string type = Console.ReadLine();
                        Console.Write("Breed: ");
                        string breed = Console.ReadLine();
                        Console.Write("DOB: ");
                        string dob = Console.ReadLine();
                        Console.Write("Weight: ");
                        int weight = int.Parse(Console.ReadLine());
                        InsertRow(name, type, breed, dob, weight);
                        Console.Clear();
                        break;
                    case 2:
                        Console.WriteLine("==== Show all Pets ====");
                        ShowPets();
                        Console.Clear();
                        break;
                    default:

                        break;
                }
            }

        }

        public void InsertRow(string name, string type, string breed, string dob, int weight)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO PET (PetName, PetType, PetBreed, PetDOBl, PetWeight, OwnerID) VALUES " +
                    "(@PetName, @PetType, @PetBreed, @PetDOBl, @PetWeight, @OwnerID)";
                using(SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        command.Parameters.Add(new SqlParameter("@PetName", name));
                        command.Parameters.Add(new SqlParameter("@PetType", type));
                        if(breed == "") {
                            command.Parameters.Add(new SqlParameter("@PetBreed", "Unknown"));
                        } else {
                            command.Parameters.Add(new SqlParameter("@PetBreed", breed));
                        }
                        command.Parameters.Add(new SqlParameter("@PetDOBl", dob));
                        command.Parameters.Add(new SqlParameter("@PetWeight", weight));
                        command.Parameters.Add(new SqlParameter("@OwnerID", 1));
                        
                        con.Open();
                        command.ExecuteNonQuery();
                        con.Close();

                        Console.WriteLine("Row has been added!");
                        Console.ReadLine();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }
        }

        public void ShowPets()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM PET";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    try
                    {
                        con.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                int PetID = int.Parse(reader["PetID"].ToString());
                                string PetName = reader["PetName"].ToString();
                                string PetType = reader["PetType"].ToString();
                                string PetBreed = reader["PetBreed"].ToString();
                                string PetDOBl = reader["PetDOBl"].ToString();
                                string petWeight = reader["petWeight"].ToString();
                                int OwnerID = int.Parse(reader["OwnerID"].ToString());
                                Console.WriteLine(PetID + ", " + PetName + ", " + PetType + ", " + PetBreed + ", " + PetDOBl + ", " + petWeight + ", " + OwnerID);
                            }
                        }
                        
                        con.Close();
                        
                        Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
