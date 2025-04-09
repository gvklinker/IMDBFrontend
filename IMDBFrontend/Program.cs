using IMDBFrontend;
using Microsoft.Data.SqlClient;
using System.Data;

string connectionString = "Server=localhost;Database=IMDB;" +
               "Integrated security=True;TrustServerCertificate=True";
int choice = 0;
do
{
    choice = Menu();
    Console.Clear();
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
            AddPerson();
            break;
        case 5:
            DeleteMovie();
            break;
        case 6:
            UpdateMovie();
            break;
        case 7:
            Console.WriteLine("Goodbye!");
            return;
        default:
            Console.WriteLine("Invalid choice");
            break;
    }
} while (choice != 7);

int Menu()
{
    Console.WriteLine("Welcome to IMDB \n" +
        "Pick an option");
    Console.WriteLine("1. Search for a title");
    Console.WriteLine("2. Search for a person");
    Console.WriteLine("3. Add a title");
    Console.WriteLine("4. Add a person");
    Console.WriteLine("5. Delete a title");
    Console.WriteLine("6. Update a title");
    Console.WriteLine("7. Exit");

    int choice = Convert.ToInt32(Console.ReadLine());
    return choice;
}

void SearchMovieX()
{
    Console.WriteLine("Enter the name of the title");
    string movieName = Console.ReadLine();
    string liked = string.Format("%{0}%", movieName);
    //List<Title> titles = new List<Title>();
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        string query = "SELECT * FROM GetTitles(@Name)";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", liked);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Title title = new Title() {Tconst = reader["Tconst"].ToString(), PrimaryTitle = reader["PrimaryTitle"].ToString(), OriginalTitle = reader["OriginalTitle"].ToString(), 
                StartYear = reader["StartYear"].ToString(), EndYear = reader["EndYear"].ToString() , Type = reader["Type"].ToString(), Genres = reader["Genres"].ToString() };
            title.ToString();
        }
        connection.Close();
    }
}
void SearchMovie()
{
    Console.WriteLine("Enter the name of the title");
    string movieName = Console.ReadLine();
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        string query = "SELECT * FROM Titles WHERE PrimaryTitle LIKE @Name";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", "%" + movieName + "%");
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Title title = new Title()
            {
                Tconst = reader["Tconst"].ToString(),
                PrimaryTitle = reader["PrimaryTitle"].ToString(),
                OriginalTitle = reader["OriginalTitle"].ToString(),
                StartYear = reader["StartYear"].ToString(),
                EndYear = reader["EndYear"].ToString(),
                //Type = reader["Type"].ToString(),
                //Genres = reader["Genres"].ToString()
            };
            Console.WriteLine(title.ToString());
        }
        connection.Close();
    }
}

void SearchPersonX()
{
    Console.WriteLine("Enter the name of the person");
    string personName = Console.ReadLine();
    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        string query = "SELECT * FROM GetPersons(@Name)";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", "%"+personName+"%");
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Person person = new Person() {Nconst = reader["Nconst"].ToString(), Name = reader["PrimaryName"].ToString(), BirthYear = Convert.ToInt32(reader["BirthYear"]), 
                DeathYear = Convert.ToInt32(reader["DeathYear"]), Professions = reader["Professions"].ToString() };
            Console.WriteLine(person.ToString()); 
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
        string query = "SELECT * FROM Persons WHERE PrimaryName LIKE @Name";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Name", "%" + personName + "%");
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Person person = new Person()
            {
                Nconst = reader["Nconst"].ToString(),
                Name = reader["PrimaryName"].ToString(),
                BirthYear = Convert.ToInt32(reader["BirthYear"]),
                //DeathYear = Convert.ToInt32(reader["DeathYear"]),
                //Professions = reader["Professions"].ToString()
            };
            Console.WriteLine(person.ToString());
        }
        connection.Close();
    }
}



void AddMovie()
{
    Console.WriteLine("Enter the tconst of the title");
    string tconst = Console.ReadLine();
    Console.WriteLine("Enter the primary title of the title");
    string name = Console.ReadLine();
    Console.WriteLine("Enter the original title of the title");
    string originalTitle = Console.ReadLine();
    Console.WriteLine("Enter the start year of the title");
    int year = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter the end year of the title");
    int? endYear = Convert.ToInt32(Console.ReadLine());
    //if (endYear == 0) endYear = null;
    Console.WriteLine("Enter the new type of the title");
    int type = Convert.ToInt32(Console.ReadLine());
    if (type <= 0)
    {
        type = 1;
    }
    Console.WriteLine("Enter the new value for whether the title is for adults only (1 if it is, 0 if it isn't)");
    int isAdult = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter the new runtime in minutes for the title");
    int runtime = Convert.ToInt32(Console.ReadLine());

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        SqlCommand command = new SqlCommand("CreateTitle", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@Tconst", tconst));
        command.Parameters.Add(new SqlParameter("@PrimaryTitle", name));
        command.Parameters.Add(new SqlParameter("@TitleType", type));
        command.Parameters.Add(new SqlParameter("@StartYear", year));
        command.Parameters.Add(new SqlParameter("@EndYear", endYear));
        command.Parameters.Add(new SqlParameter("@OriginalTitle", originalTitle));
        command.Parameters.Add(new SqlParameter("@IsAdult", isAdult));
        command.Parameters.Add(new SqlParameter("@RuntimeMinutes", runtime));
        command.ExecuteNonQuery();
        connection.Close();
    }
}

void AddPerson()
{
    Console.WriteLine("Enter the Nconst of the person");
    string nconst = Console.ReadLine();
    Console.WriteLine("Enter the name of the person");
    string name = Console.ReadLine();
    Console.WriteLine("Enter the birthyear of the person");
    int year = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter the deathyear of the person");
    int dYear = Convert.ToInt32(Console.ReadLine());

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        SqlCommand command = new SqlCommand("CreatePerson", connection);
        connection.Open();
        command.Parameters.Add(new SqlParameter("@Nconst", nconst));
        command.Parameters.Add(new SqlParameter("@PrimaryName", name));
        command.Parameters.Add(new SqlParameter("@BirthYear", year));
        command.Parameters.Add(new SqlParameter("@DeathYear", dYear ));
        command.ExecuteNonQuery();
        connection.Close();
    }
}

void DeleteMovie()
{
    Console.WriteLine("Enter the Tconst of the title you want to delete");
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
    Console.WriteLine("Enter the tconst of the title you want to update");
    string tconst = Console.ReadLine();
    Console.WriteLine("Enter the new primary title of the title");
    string name = Console.ReadLine();
    Console.WriteLine("Enter the new original title of the title");
    string originalTitle = Console.ReadLine();
    Console.WriteLine("Enter the new start year of the title");
    int year = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter the new end year of the title");
    int endYear = Convert.ToInt32(Console.ReadLine());
    //Console.WriteLine("Enter the new type of the title");
    Console.WriteLine("Enter the new value for whether the title is for adults only (1 if it is, 0 if it isn't)");
    int isAdult = Convert.ToInt32(Console.ReadLine());
    Console.WriteLine("Enter the new runtime in minutes for the title");
    int runtime = Convert.ToInt32(Console.ReadLine());

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();
        //string query = "UPDATE Titles SET Year = @Year WHERE PrimaryTitle = @tconst";
        SqlCommand command = new SqlCommand("UpdateTitle", connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.Add(new SqlParameter("@Tconst", tconst));
        command.Parameters.Add(new SqlParameter("@PrimaryTitle", name));
        command.Parameters.Add(new SqlParameter("@StartYear", year));
        command.Parameters.Add(new SqlParameter("@EndYear", endYear));
        command.Parameters.Add(new SqlParameter("@OriginalTitle", originalTitle));
        command.Parameters.Add(new SqlParameter("@IsAdult", isAdult));
        command.Parameters.Add(new SqlParameter("@RuntimeMinutes", runtime));
        command.ExecuteNonQuery();
        connection.Close();
    }
}