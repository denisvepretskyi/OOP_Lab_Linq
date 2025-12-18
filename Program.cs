using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Text.RegularExpressions;

namespace Practice_Linq
{
    public class Program
    {
        static void Main(string[] args)
        {
            string path = @"../../../data/results_2010.json";

            List<FootballGame> games = ReadFromFileJson(path);

            int test_count = games.Count();
            Console.WriteLine($"Test value = {test_count}.");    // 13049

            Query1(games);
            Query2(games);
            Query3(games);
            Query4(games);
            Query5(games);
            Query6(games);
            Query7(games);
            Query8(games);
            Query9(games);
            Query10(games);
            Query11(games);
            Query12(games);
            Query13(games);
            Query14(games);
            Query15(games);
        }


        // Десеріалізація json-файлу у колекцію List<FootballGame>
        static List<FootballGame> ReadFromFileJson(string path)
        {

            var reader = new StreamReader(path);
            string jsondata = reader.ReadToEnd();

            List<FootballGame> games = JsonConvert.DeserializeObject<List<FootballGame>>(jsondata);


            return games;

        }


        // Запит 1
        static void Query1(List<FootballGame> games)
        {
            //Query 1: Вивести всі матчі, які відбулися в Україні у 2012 році.

            var selectedGames = games.Where(x => x.Country == "Ukraine").Where(x => x.Date.Year == 2012);
            
            Console.WriteLine("\n======================== QUERY 1 ========================");
            foreach(var item in selectedGames)
            {
                string res = $"{item.Date:dd.MM.yyyy} ";
                res += $"{item.Home_team} - {item.Away_team}, ";
                res += $"Score: {item.Home_score} - {item.Away_score}, ";
                res += $"Country: {item.Country}";
                Console.WriteLine(res);  
            } 
        }

        // Запит 2
        static void Query2(List<FootballGame> games)
        {
            //Query 2: Вивести Friendly матчі збірної Італії, які вона провела з 2020 року.  

            var selectedGames = games.Where(x => (x.Tournament == "Friendly") && (x.Home_team == "Italy" || x.Away_team == "Italy")); // допиши запит
            selectedGames = selectedGames.Where(x => x.Date.Year >= 2020);               

            // Результат
            Console.WriteLine("\n======================== QUERY 2 ========================");

            //foreach
            foreach (var item in selectedGames)
            {
                string res = $"{item.Date:dd.MM.yyyy} ";
                res += $"{item.Home_team} - {item.Away_team}, ";
                res += $"Score: {item.Home_score} - {item.Away_score}, ";
                res += $"Country: {item.Country}";
                Console.WriteLine(res);
            } 
        }

        // Запит 3
        static void Query3(List<FootballGame> games)
        {
            //Query 3: Вивести всі домашні матчі збірної Франції за 2021 рік, де вона зіграла у нічию.

            var selectedGames = games.Where(x => x.Home_team == x.Country && x.Country == "France"); // допиши запит
            selectedGames = selectedGames.Where(x => x.Home_score == x.Away_score);
            selectedGames = selectedGames.Where(x => x.Date.Year == 2021);


            // Результат
            Console.WriteLine("\n======================== QUERY 3 ========================");

            //foreach
            foreach (var item in selectedGames)
            {
                string res = $"{item.Date:dd.MM.yyyy} ";
                res += $"{item.Home_team} - {item.Away_team}, ";
                res += $"Score: {item.Home_score} - {item.Away_score}, ";
                res += $"Country: {item.Country}";
                Console.WriteLine(res);
            }
        }

        // Запит 4
        static void Query4(List<FootballGame> games)
        {
            //Query 4: Вивести всі матчі збірної Германії з 2018 року по 2020 рік (включно), в яких вона на виїзді програла.

            var selectedGames = games.Where(x => x.Country != "Germany"); // допиши запит
            selectedGames = selectedGames.Where(x => (x.Home_team == "Germany" && x.Home_score < x.Away_score) || (x.Away_team == "Germany" && x.Away_score < x.Home_score));
            selectedGames = selectedGames.Where(x => x.Date.Year >= 2018 && x.Date.Year <= 2020);

            // Результат
            Console.WriteLine("\n======================== QUERY 4 ========================");

            //foreach
            foreach (var item in selectedGames)
            {
                string res = $"{item.Date:dd.MM.yyyy} ";
                res += $"{item.Home_team} - {item.Away_team}, ";
                res += $"Score: {item.Home_score} - {item.Away_score}, ";
                res += $"Country: {item.Country}";
                Console.WriteLine(res);
            }
        }

        // Запит 5
        static void Query5(List<FootballGame> games)
        {
            //Query 5: Вивести всі кваліфікаційні матчі (UEFA Euro qualification), які відбулися у Києві чи у Харкові, а також за умови перемоги української збірної.

            var selectedGames = games.Where(x => x.Tournament == "UEFA Euro qualification"); // допиши запит
            selectedGames = selectedGames.Where(x => x.City == "Kharkiv" || x.City == "Kyiv");
            selectedGames = selectedGames.Where(x => x.Home_team == "Ukraine" && x.Home_score > x.Away_score);

            // Результат
            Console.WriteLine("\n======================== QUERY 5 ========================");

            //foreach
            foreach (var item in selectedGames)
            {
                string res = $"{item.Date:dd.MM.yyyy} ";
                res += $"{item.Home_team} - {item.Away_team}, ";
                res += $"Score: {item.Home_score} - {item.Away_score}, ";
                res += $"Country: {item.Country}";
                Console.WriteLine(res);
            }
        }

        // Запит 6
        static void Query6(List<FootballGame> games)
        {
            //Query 6: Вивести всі матчі останнього чемпіоната світу з футболу (FIFA World Cup), починаючи з чвертьфіналів (тобто останні 8 матчів).
            //Матчі мають відображатися від фіналу до чвертьфіналів (тобто у зворотній послідовності).

            var selectedGames = games.Where(x => x.Tournament == "FIFA World Cup"); // допиши запит
            selectedGames = selectedGames.Where(x => (x.Date.Year <= games[games.Count - 1].Date.Year) && (x.Date.Year >= (games[games.Count - 1].Date.Year - 4)));
            selectedGames = selectedGames.Reverse();
            selectedGames = selectedGames.Take(8);

            // Результат
            Console.WriteLine("\n======================== QUERY 6 ========================");

            //foreach
            foreach (var item in selectedGames)
            {
                string res = $"{item.Date:dd.MM.yyyy} ";
                res += $"{item.Home_team} - {item.Away_team}, ";
                res += $"Score: {item.Home_score} - {item.Away_score}, ";
                res += $"Country: {item.Country}";
                Console.WriteLine(res);
            }
        }

        // Запит 7
        static void Query7(List<FootballGame> games)
        {
            //Query 7: Вивести перший матч у 2023 році, в якому збірна України виграла.
            var selectedGames = games.Where(x => x.Date.Year == 2023); // допиши запит
            selectedGames = selectedGames.Where(x => (x.Home_team == "Ukraine" && x.Home_score > x.Away_score) || (x.Away_team == "Ukraine" && x.Away_score > x.Home_score));
            selectedGames = selectedGames.OrderBy(x => x.Date.Year);

            FootballGame? item = selectedGames.FirstOrDefault(); // допиши запит


            // Результат
            Console.WriteLine("\n======================== QUERY 7 ========================");
            
                string res = $"{item.Date:dd.MM.yyyy} ";
                res += $"{item.Home_team} - {item.Away_team}, ";
                res += $"Score: {item.Home_score} - {item.Away_score}, ";
                res += $"Country: {item.Country}";
                Console.WriteLine(res);
            }


        // Запит 8
        static void Query8(List<FootballGame> games)
        {
            // Query 8: Перетворити всі матчі Євро-2012 (UEFA Euro), які відбулися в Україні, на матчі з наступними властивостями:
            // MatchYear - рік матчу, Team1 - назва приймаючої команди, Team2 - назва гостьової команди, Goals - сума всіх голів за матч

            var selectedGames = games.Where(x => x.Tournament == "UEFA Euro" && x.Country == "Ukraine" && x.Date.Year == 2012); // Фільтрація
            var newTypeGame = selectedGames.Select(x => new 
            {
                MatchYear = x.Date.Year,
                Team1 = x.Home_team,
                Team2 = x.Away_team,
                Goals = x.Home_score + x.Away_score 
            });

            // Результат
            Console.WriteLine("\n======================== QUERY 8 ========================");

            foreach (var match in newTypeGame)
            {
                Console.WriteLine($"{match.MatchYear} {match.Team1} - {match.Team2}, Goals: {match.Goals}");
            }
        }


        // Запит 9
        static void Query9(List<FootballGame> games)
        {
            //Query 9: Перетворити всі матчі UEFA Nations League у 2023 році на матчі з наступними властивостями:
            // MatchYear - рік матчу, Game - назви обох команд через дефіс (першою - Home_team), Result - результат для першої команди (Win, Loss, Draw)

            var selectedGames = games.Where(x => x.Tournament == "UEFA Nations League"); // допиши запит
            selectedGames = selectedGames.Where(x => x.Date.Year == 2023);
            var newTypeGames = selectedGames.Select(x => new
            {
                MatchYear = x.Date.Year,
                Game = $"{x.Home_team} - {x.Away_team}",
                Result = x.Home_score > x.Away_score? "Win" : x.Home_score < x.Away_score? "Loss" : "Draw"
            }
            );


            // Результат
            Console.WriteLine("\n======================== QUERY 9 ========================");

            //foreach
            foreach (var match in newTypeGames)
            {
                Console.WriteLine($"{match.MatchYear} {match.Game}, Result for team1: {match.Result}");
            }
        }

        // Запит 10
        static void Query10(List<FootballGame> games)
        {
            //Query 10: Вивести з 5-го по 10-тий (включно) матчі Gold Cup, які відбулися у липні 2023 р.

            var selectedGames = games.Where(x => x.Tournament == "Gold Cup"); // допиши запит
            selectedGames = selectedGames.Where(x => x.Date.Year == 2023 && x.Date.Month == 07);
            selectedGames = selectedGames.Skip(4).Take(6);


            // Результат
            Console.WriteLine("\n======================== QUERY 10 ========================");

            //foreach
            foreach (var item in selectedGames)
            {
                string res = $"{item.Date:dd.MM.yyyy} ";
                res += $"{item.Home_team} - {item.Away_team}, ";
                res += $"Score: {item.Home_score} - {item.Away_score}, ";
                res += $"Country: {item.Country}";
                Console.WriteLine(res);
            }
        }

        // Запит 11
        static void Query11(List<FootballGame> games)
        {
            //Query 11: Вивести 10 країн (без повторів) з сортуваннях від A до Z, в яких проводилися матчі у 2020 році.    

            var selectedGames = games.Where(x => x.Date.Year == 2020);
            selectedGames = selectedGames.OrderBy(x => x.Country); // допиши запит
            selectedGames = selectedGames.DistinctBy(x => x.Country);
            selectedGames = selectedGames.Take(10);

            // Результат
            Console.WriteLine("\n======================== QUERY 11 ========================");

            //foreach
            foreach (var item in selectedGames)
            {                
                string res = $"{item.Country}";
                Console.WriteLine(res);
            }
        }

        // Запит 12
        static void Query12(List<FootballGame> games)
        {
            //Query 12: Вивести назви турнірів, кількість ігор яких з 2022 року більша за 200. Турніри відсортувати за кількістю ігор за спаданням.
            //Вихідні турніри повині мати властивості: Tournament - назва турніру, Count - кількість ігор.   

            var selectedGames = games.Where(x => x.Date.Year >= 2022); // допиши запит
            var groupByTournament = selectedGames.GroupBy(x => x.Tournament);
            var newTypeGames = groupByTournament.Select(x => new
            {
                Tournament = x.Key,
                Count = x.Count()
            });
            newTypeGames = newTypeGames.Where(x => x.Count > 200);
            newTypeGames = newTypeGames.OrderByDescending(x => x.Count);


            // Результат
            Console.WriteLine("\n======================== QUERY 12 ========================");
            
            //foreach
            foreach(var item in newTypeGames)
            {
                Console.WriteLine($"{item.Tournament} - {item.Count}");
            }

        }

        // Запит 13
        static void Query13(List<FootballGame> games)
        {
            //Query 13: Вивести ТОП-3 найпопулярніши країни для проведення матчів на нейтральному полі.
            //Вихідні країни повині мати властивості: Country - назва країни, Count - кількість ігор.  

            var selectedGames = games; // допиши запит


            // Результат
            Console.WriteLine("\n======================== QUERY 13 ========================");

            //foreach

        }


        // Запит 14
        static void Query14(List<FootballGame> games)
        {
            //Query 14: Вивести ТОП-5 турнірів за середньою результативністю (результативність - сума забитих м'ячів).
            //Вихідні турніри повині мати властивості: Tournament - назва турніру, AvgGoals - середня результативність.   

            var selectedGames = games; // допиши запит


            // Результат
            Console.WriteLine("\n======================== QUERY 14 ========================");

            //foreach

        }


        // Запит 15 (**)
        static void Query15(List<FootballGame> games)
        {
            //Query 15: Вивести команди відсортовані за алфавітом, які за вечь час зіграли всього 1 гру.
            //Вихідні команди повині мати властивості: Team - назва команди, Count - кількість ігор.  

            var selectedGames = games; // допиши запит


            // Результат
            Console.WriteLine("\n======================== QUERY 15 ========================");

            //foreach

        }

    }
}