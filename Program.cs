using Azure.Core.Pipeline;
using Mandatory_IMDB_Assignment.Inserter;
using Mandatory_IMDB_Assignment.Mapper;
using Mandatory_IMDB_Assignment.Models;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;

namespace Mandatory_IMDB_Assignment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataReader dataReader = new DataReader();
            ImdbContext context = new ImdbContext();
            EFInserter eF = new EFInserter(context);

            List<Title> titleReader = dataReader.ReadDataFromFile("C:\\imdb dataset\\title\\data.tsv", Title.titleMapper);
            List<Genre> genreReader = dataReader.ReadDataFromFile("C:\\imdb dataset\\title\\data.tsv", Genre.gennreMapper);
            List<KnownForTitle> knownForTitleReader = dataReader.ReadDataFromFile("C:\\imdb dataset\\name\\data.tsv", KnownForTitle.knownForMapper);
            List<NameBasic> nameBasics = dataReader.ReadDataFromFile("C:\\imdb dataset\\name\\data.tsv", NameBasic.nameMapper);
            List<PrimaryProfession> primaryProfessions = dataReader.ReadDataFromFile("C:\\imdb dataset\\name\\data.tsv", PrimaryProfession.profMapper);
            List<TitleCrew> titleCrewReader = dataReader.ReadDataFromFile("C:\\imdb dataset\\crew\\data.tsv", TitleCrew.TitleMapper);
            List<TitleDirector> titleDirectors = dataReader.ReadDataFromFile("C:\\imdb dataset\\crew\\data.tsv", TitleDirector.directorMapper);
            List<TitleWriter> titleWriterReader = dataReader.ReadDataFromFile("C:\\imdb dataset\\crew\\data.tsv", TitleWriter.writerMapper);
        

            while (true)
            {
                ConsoleUIOptions();

                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {  
                    case "1":

                       Console.Clear();
                       Console.WriteLine("Enter password");
                        string enteredPass = Console.ReadLine();
                        string correctPass = "fai789"; 
                        Console.WriteLine("Inserting Data...");
                        if (enteredPass == correctPass)
                        {
                            Console.WriteLine("Password is correct. Inserting Data...");
                            eF.InserAlltData(titleReader, titleCrewReader, titleDirectors, titleWriterReader, genreReader, nameBasics, primaryProfessions, knownForTitleReader);
                           
                        }
                        else
                        {
                            Console.WriteLine("Incorrect password. Access denied.");

                            SetTimer(2000, () => ClearCurrentConsoleLine(3));
                        }

                        break;

                    case "2":
                        Console.WriteLine("Enter the password: ");
                        string enteredPassword = Console.ReadLine();
                        string correctpassword = "fai789";
                        if (enteredPassword == correctpassword)
                        {
                            Console.WriteLine("Password is correct. Deleting Data...");
                            eF.DeleteAllData(titleReader, titleCrewReader, titleDirectors, titleWriterReader, genreReader, nameBasics, primaryProfessions, knownForTitleReader);
              
                        }
                        else
                        {
                            Console.WriteLine("Incorrect password. Access denied.");
                            SetTimer(2000, () => ClearCurrentConsoleLine(3));
                        }
                        break;
                    case "3":

                        Console.WriteLine("Enter Movie Details:");
                        Console.Write("Tconst: ");
                        string tconst = Console.ReadLine();
                        Console.Write("Title Type: ");
                        string titleType = Console.ReadLine();
                        Console.Write("Primary Title: ");
                        string primaryTitle = Console.ReadLine();
                        Console.Write("Original Title: ");
                        string originalTitle = Console.ReadLine();
                        Console.Write("Is Adult (1 for Yes, 0 for No): ");
                        int isAdult = int.Parse(Console.ReadLine());
                        Console.Write("Start Year: ");
                        int? startYear = int.TryParse(Console.ReadLine(), out var parsedStartYear) ? parsedStartYear : (int?)null;

                        Console.Write("End Year (leave empty for null): ");
                        string endYearInput = Console.ReadLine();
                        int? endYear = !string.IsNullOrEmpty(endYearInput) ? int.Parse(endYearInput) : (int?)null;

                        Console.Write("Runtime Minutes (leave empty for null): ");
                        string runTimeMinutesInput = Console.ReadLine();
                        int? runTimeMinutes = !string.IsNullOrEmpty(runTimeMinutesInput) ? int.Parse(runTimeMinutesInput) : (int?)null;
                        eF.InsertMovie(tconst, titleType, primaryTitle, originalTitle, isAdult, startYear, endYear, runTimeMinutes);
                        Console.WriteLine("Movie Inserted Successfully.");
                        break;

                    case "4":
                        Console.WriteLine("Enter Tconst of Movie to Delete:");
                        string deletedMovieTconst = Console.ReadLine();
                        Console.WriteLine("Deleting Movie...");
                        eF.DeleteMovieInfo(deletedMovieTconst);
                   
                        break;

                    case "5":
                        Console.WriteLine("Enter Movie Details for Update:");
                        Console.Write("Tconst: ");
                        string tconstUpdated = Console.ReadLine();
                        Console.Write("Title Type: ");
                        string titleTypeUpdated = Console.ReadLine();
                        Console.Write("Primary Title: ");
                        string primaryTitleUpdated = Console.ReadLine();
                        Console.Write("Original Title: ");
                        string originalTitleUpdated = Console.ReadLine();
                        Console.Write("Is Adult (1 for Yes, 0 for No): ");
                        int isAdultUpdated = int.Parse(Console.ReadLine());
                        Console.Write("Start Year: ");
                        string startyaerUpdatedInput = Console.ReadLine();
                        int? startYearUpdated = !string.IsNullOrEmpty(startyaerUpdatedInput) ? int.Parse(startyaerUpdatedInput) : (int?)null;
                        Console.Write("End Year: ");
                        string endyaerUpdatedInput = Console.ReadLine();
                        int? endYearUpdated = !string.IsNullOrEmpty(endyaerUpdatedInput) ? int.Parse(endyaerUpdatedInput) : ( int?)null;
                        Console.Write("Runtime Minutes: ");
                        string runTimeUpdatedInput = Console.ReadLine();
                        int? runTimeMinutesUpdated = !string.IsNullOrEmpty(runTimeUpdatedInput) ? int.Parse(runTimeUpdatedInput) : (int?)null;

                        Console.WriteLine("Updating Movie Info...");
                        eF.UpdateMovieInfo(tconstUpdated, titleTypeUpdated, primaryTitleUpdated, originalTitleUpdated, isAdultUpdated, startYearUpdated, endYearUpdated, runTimeMinutesUpdated);
                       
                        break;

                    case "6":
                        Console.WriteLine("Enter Person Details:");
                        Console.Write("Nconst: ");
                        string nconstToAdd = Console.ReadLine();
                        Console.Write("Name: ");
                        string? nameToAdd = Console.ReadLine();
                        Console.Write("Birth Year: ");
                        string birthYearToAdd = Console.ReadLine();
                        int? birtYearToAddInput = !string.IsNullOrEmpty(birthYearToAdd) ? int.Parse(birthYearToAdd) : (int?)null;
                        Console.Write("Death Year: ");
                        string deathYearToAdd = Console.ReadLine();
                        int? deathYearToAddInput = !string.IsNullOrEmpty(deathYearToAdd) ? int.Parse(deathYearToAdd) : (int?)null;

                        Console.WriteLine("Adding Person...");
                        eF.AddPerson(nconstToAdd, nameToAdd, birtYearToAddInput, deathYearToAddInput);
                       
                        break;

                    case "7":
                        Console.WriteLine("Enter Search Parameters:");
                        Console.Write("Name to Search: ");
                        string? userParam1 = Console.ReadLine();
                        Console.Write("Birth Year to Search: ");
                        string userParam2Input = Console.ReadLine();
                        int userParaam2 = int.Parse(userParam2Input);

                        Console.WriteLine("Searching for Staff...");
                        var staffResults = eF.FindStaffSP(userParam1, userParaam2);
                     
                        break;

                    case "8":
                        Console.Write("Enter Movie Title to Search: ");
                        string findMovie = Console.ReadLine();
                        Console.WriteLine("Searching for Movie Titles...");
                        var movieResults = eF.FindMovieTitleSP(findMovie);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid option. Please select a valid option.");
                       
               
                        break;
                }

            }
    }
        public static void ConsoleUIOptions()
        {
           
            Console.WriteLine("IMDB Database Console UI", Console.ForegroundColor = ConsoleColor.Yellow);
            Console.WriteLine("1. Insert CSV records [ADMINS ONLY]", Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine("2. Delete all records from database [ADMINS ONLY]", Console.ForegroundColor = ConsoleColor.Red);
            Console.WriteLine("3. Insert Movie", Console.ForegroundColor = ConsoleColor.White);
            Console.WriteLine("4. Delete Movie", Console.ForegroundColor = ConsoleColor.White);
            Console.WriteLine("5. Update Movie Info", Console.ForegroundColor = ConsoleColor.White);
            Console.WriteLine("6. Add Person", Console.ForegroundColor = ConsoleColor.White);
            Console.WriteLine("7. Find Staff", Console.ForegroundColor = ConsoleColor.White);
            Console.WriteLine("8. Find Movie Title", Console.ForegroundColor = ConsoleColor.White);
            Console.WriteLine("9. Exit", Console.ForegroundColor = ConsoleColor.White);
        }
        private static void SetTimer(int delay, Action TimerAction)
        {
            Timer timer = new Timer(_ => TimerAction(), null, delay, Timeout.Infinite);
        }

   
        public static void ClearCurrentConsoleLine(int line)
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, line);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}