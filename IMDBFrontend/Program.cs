using Microsoft.Data.SqlClient;
using System.Data;

string connectionString = "Server=localhost;Database=IMDB;" +
               "Integrated security=True;TrustServerCertificate=True";
int choice = 0;
do
{
    choice = Menu();
    switch (choice)
    {
        case 1:
            SearchMovie();
            break;
        case 2:
            SearchPerson();
            break;
        case 3:
            AddMovie();
            break;
        case 4:
            DeleteMovie();
            break;
        case 5:
            UpdateMovie();
            break;
        case 6:
            Console.WriteLine("Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid choice");
            break;
    }
} while (choice != 6);

int Menu()
{
    Console.WriteLine("Welcome to IMDB \n" +
        "Pick an option");
    Console.WriteLine("1. Search for a movie");
    Console.WriteLine("2. Search for a person");
    Console.WriteLine("3. Add a movie");
    Console.WriteLine("4. Delete a movie");
    Console.WriteLine("5. Update a movie");
    Console.WriteLine("6. Exit");

    int choice = Convert.ToInt32(Console.ReadLine());
    return choice;
}

void SearchMovie()
{
    Console.WriteLine("Enter the name of the movie");
    string movieName = Console.ReadLine();
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        string query = "SELECT * FROM Titles WHERE PrimaryTitle like @Name";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", "%"+movieName+"%");
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(reader["Name"]);
        }
        connection.Close();
    }
}

void SearchPerson()
{
    Console.WriteLine("Enter the name of the person");
    string personName = Console.ReadLine();
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        string query = "SELECT * FROM Persons WHERE Name like @Name";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", "%"+personName+"%");
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(reader["Name"]);
        }
        connection.Close();
    }
}

void AddMovie()
{
    Console.WriteLine("Enter the name of the movie");
    string movieName = Console.ReadLine();
    Console.WriteLine("Enter the year of the movie");
    int year = Convert.ToInt32(Console.ReadLine());
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        string query = "INSERT INTO Titles (Name, Year) VALUES (@Name, @Year)";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", movieName);
        command.Parameters.AddWithValue("@Year", year);
        command.ExecuteNonQuery();
        connection.Close();
    }
}


void DeleteMovie()
{
    Console.WriteLine("Enter the Tconst of the movie to delete");
    string tconst = Console.ReadLine();
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        SqlCommand cmd = new SqlCommand("DeleteTitle", connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Tconst", tconst));
        cmd.ExecuteNonQuery();
        connection.Close();
    }
}

void UpdateMovie()
{
    Console.WriteLine("Enter the name of the movie to update");
    string movieName = Console.ReadLine();
    Console.WriteLine("Enter the new year of the movie");
    int year = Convert.ToInt32(Console.ReadLine());
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        string query = "UPDATE Titles SET Year = @Year WHERE PrimaryTitle = @Name";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", movieName);
        command.Parameters.AddWithValue("@Year", year);
        command.ExecuteNonQuery();
        connection.Close();
    }
}