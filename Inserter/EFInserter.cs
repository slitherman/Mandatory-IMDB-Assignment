using Mandatory_IMDB_Assignment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandatory_IMDB_Assignment.Inserter
{
    internal class EFInserter
    {
        public ImdbContext _context { get; set; }
        public EFInserter(ImdbContext context)
        {
            _context = context;
        }

        public void InserAlltData(List<Title> titleBasicsData,
        List<TitleCrew> titleCrewData,
        List<TitleDirector> titleDirectorsData,
        List<TitleWriter> titleWritersData,
        List<Genre> genresData,
        List<NameBasic> nameBasicsData,
        List<PrimaryProfession> primaryProfessionsData,
        List<KnownForTitle> knownForTitlesData)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    _context.Set<Title>().AddRange(titleBasicsData);
                    _context.Set<TitleCrew>().AddRange(titleCrewData);
                    _context.Set<TitleDirector>().AddRange(titleDirectorsData);
                    _context.Set<TitleWriter>().AddRange(titleWritersData);
                    _context.Set<Genre>().AddRange(genresData);
                    _context.Set<NameBasic>().AddRange(nameBasicsData);
                    _context.Set<PrimaryProfession>().AddRange(primaryProfessionsData);
                    _context.Set<KnownForTitle>().AddRange(knownForTitlesData);

                    int rowsAffected = _context.SaveChanges();

                    transaction.Commit();

                    Console.WriteLine($"Data inserted successfully. Rows affected: {rowsAffected}");

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Transaction failed due to exception: {ex.Message}");
                    transaction.Rollback();
                    throw ex;
                }

            }
        }
        public void DeleteAllData(List<Title> titles, List<TitleCrew> titleCrews, List<TitleDirector> titleDirectors, List<TitleWriter> titleWriters, List<Genre> genres, List<NameBasic> nameBasics, List<PrimaryProfession> primaryProfessions, List<KnownForTitle> knownForTitles)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Set<Title>().RemoveRange(titles);
                    _context.Set<TitleCrew>().RemoveRange(titleCrews);
                    _context.Set<TitleDirector>().RemoveRange(titleDirectors);
                    _context.Set<TitleWriter>().RemoveRange(titleWriters);
                    _context.Set<Genre>().RemoveRange(genres);
                    _context.Set<NameBasic>().RemoveRange(nameBasics);
                    _context.Set<PrimaryProfession>().RemoveRange(primaryProfessions);
                    _context.Set<KnownForTitle>().RemoveRange(knownForTitles);

                    int rowsAffected = _context.SaveChanges();

                    transaction.Commit();

                    Console.WriteLine($"Data deleted successfully. Rows affected: {rowsAffected}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Transaction failed due to exception: {ex.Message}");
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
        public void InsertMovie(string tconst, string titleType, string primaryTitle, string originalTitle, int isAdult, int? startYear, int? endYear, int? runTimeMinutes)
        {
            var options = new DbContextOptionsBuilder<ImdbContext>()
                .UseSqlServer("server=localhost;database=IMDB; user id=mathew;password=1234;TrustServerCertificate=True")
                .Options;

            using (var context = new ImdbContext(options))
            {
                try
                {
                    int? startYearNullable = (startYear == 0) ? null : startYear;
                    int? endYearNullable = (endYear == 0) ? null : endYear;
                    int? runtimeMinutesNullable = (runTimeMinutes == 0) ? null : runTimeMinutes;
                    bool isAdultBool = MapToBool(isAdult);

                    var movie = new Title
                    {
                        Tconst = tconst,
                        TitleType = titleType,
                        PrimaryTitle = primaryTitle,
                        OriginalTitle = originalTitle,
                        IsAdult = isAdultBool,
                        StartYear = startYearNullable,
                        EndYear = endYear,
                        RuntimeMinutes = runtimeMinutesNullable,
                    };

                    context.Add(movie);
                    context.SaveChanges();

                    Console.WriteLine($" THe movie {primaryTitle} has successfully been added");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Insert failed due to exception: {ex.Message}");
                    while (ex.InnerException != null)
                    {
                        ex = ex.InnerException;
                        Console.WriteLine("Inner Exception:");
                        Console.WriteLine(ex.ToString());
                    }

                }
            }
        }
        public void DeleteMovieInfo(string tconst)
        {

            var options = new DbContextOptionsBuilder<ImdbContext>()
               .UseSqlServer("server=localhost;database=IMDB; user id=mathew;password=1234;TrustServerCertificate=True")
               .Options;

            using (var context = new ImdbContext(options))
            {
                try
                {
                    var movie = _context.Titles.FirstOrDefault(x => x.Tconst == tconst);
                    if (movie != null)
                    {
                        _context.Titles.Remove(movie);
                        _context.SaveChanges();
                        Console.WriteLine($" The movie with the id {tconst} has just been deleted");
                    }
                    else
                    {
                        Console.WriteLine("Movie was not found");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Deletetion failed due to exception: {ex.Message}");
                }
            }
          
        }

        public void UpdateMovieInfo(string tconst, string titleType, string primaryTitle, string originalTitle, int isAdult, int? startYear, int? endYear, int? runTimeMinutes)
        {
            var options = new DbContextOptionsBuilder<ImdbContext>()
         .UseSqlServer("server=localhost;database=IMDB; user id=mathew;password=1234;TrustServerCertificate=True")
         .Options;
            var movie = _context.Titles.FirstOrDefault( x => x.Tconst == tconst);
            if(movie == null)
            {
                Console.WriteLine("Movie not found");
            }
            try
            {
                int? startYearNullable = (startYear == 0) ? null : startYear;
                int? endYearNullable = (endYear == 0) ? null : endYear;
                int? runtimeMinutesNullable = (runTimeMinutes == 0) ? null : runTimeMinutes;
                bool isAdultBool = MapToBool(isAdult);

                
                
                    movie.TitleType = titleType;
                    movie.PrimaryTitle = primaryTitle;
                    movie.OriginalTitle = originalTitle;
                    movie.IsAdult = MapToBool(isAdult);
                    movie.StartYear = startYear;
                    movie.EndYear = endYear;
                    movie.RuntimeMinutes = runTimeMinutes;

                    _context.SaveChanges();
                    Console.WriteLine("Update successful");
                    Console.WriteLine($"Id {tconst} TitleType {titleType} PrimaryTitle {primaryTitle} OriginalTitle {originalTitle} IsAdult {isAdult} StartYear {startYear} EndYear {endYear}");



                _context.SaveChanges();
            }
            catch(Exception ex) 
            {
                Console.WriteLine($"Update failed due to exception: {ex.Message}");
            }
        }

        public void AddPerson(string nconst, string? primaryName, int? birthYear,int? deathYear)
        {
            var options = new DbContextOptionsBuilder<ImdbContext>()
            .UseSqlServer("server=localhost;database=IMDB; user id=mathew;password=1234;TrustServerCertificate=True")
            .Options;

            int? birthYearNullable = (birthYear == 0) ? null : birthYear;
            int? deathYearNullable = (deathYear == 0) ? null : deathYear;

            using (var context  = new ImdbContext(options))
            {
                try
                {
                    var person = new NameBasic
                    {
                        Nconst = nconst,
                        PrimaryName = primaryName,
                        BirthYear = birthYearNullable,
                        DeathYear = deathYearNullable,


                    };
                    context.Add(person);
                    context.SaveChanges();
                    Console.WriteLine($"Person added: Id {nconst} Name {primaryName} BirthYear {birthYear} DeathYear {deathYear}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Insert failed due to exception: {ex.Message}");

                }

            }

        }

      private bool MapToBool(int value)
        {
            return value == 1;
        }
        public List<Staff> FindStaffSP(string userParam1,int userParam2)
        {

            var sql = $"exec FindPerson '{userParam1}',{userParam2}";
            var res = _context.Staff.FromSqlRaw(sql).ToList();
            if (res.Count > 0)
            {
                foreach (var item in res)
                {
                    Console.WriteLine($" Name: {item.PrimaryName} BirthYear: {item.BirthYear} ");
                }
            }
            else
            {
                Console.WriteLine("Person not found");
            }

            return res;
        }
        public List<GenresAndTitlesPublic> FindMovieTitleSP(string userParam)
        {
            var sql = $"exec Find_movie_title '{userParam}'";
            var res = _context.GenresAndTitlesPublics.FromSqlRaw(sql).ToList();
         
            if (res.Count > 0)
            {
                foreach(var item in res)
                {
                    Console.WriteLine(item.PrimaryTitle, item.OriginalTitle);
                }
            }
            else
            {
                Console.WriteLine("Movie not found");
            }
            return res;
        }

    }
}
