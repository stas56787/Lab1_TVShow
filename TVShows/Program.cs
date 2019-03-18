using System;
using TVShows.Tables;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TVShows
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var dbContext = new Context())
            {
                DbInitializer.Initialize(dbContext);

                Console.WriteLine("====== Будет выполнена выборка данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                SelectGenres(dbContext);

                Console.WriteLine("====== Будет выполнена выборка данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                SelectTVShows(dbContext);

                Console.WriteLine("====== Будет выполнена группировка данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                Group(dbContext);

                Console.WriteLine("====== Будет выполнена выборка данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                SelectCitizenAppealsForShow(dbContext);

                Console.WriteLine("====== Будет выполнена выборка данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                SelectScheduleOfShows(dbContext);

                Console.WriteLine("====== Будет выполнена вставка данных в \"ОДИН\" (нажмите любую клавишу) ========");
                Console.ReadKey();
                InsertIntoOnes(dbContext);

                Console.WriteLine("====== Будет выполнена вставка данных в \"МНОГО\" (нажмите любую клавишу) ========");
                Console.ReadKey();
                InsertIntoManys(dbContext);

                Console.WriteLine("====== Будет выполнено удаление данных в \"ОДИН\" (нажмите любую клавишу) ========");
                Console.ReadKey();
                DeleteFromOnes(dbContext);

                Console.WriteLine("====== Будет выполнено удаление данных в \"МНОГО\" (нажмите любую клавишу) ========");
                Console.ReadKey();
                DeleteFromManys(dbContext);

                Console.WriteLine("====== Будет выполнено обновление данных (нажмите любую клавишу) ========");
                Console.ReadKey();
                Update(dbContext);

                Console.ReadKey();
            }
        }

        static void TablePart(string firstSymbol, string secondSymbol, string thirdSymbol, string[] hat,
            int[] longestWords)
        {
            Console.Write(firstSymbol);
            for (var h = 0; h < hat.Length; h++)
            {
                Console.Write(new string('─', longestWords[h]));
                if (h < hat.Length - 1)
                {
                    Console.Write(secondSymbol);
                }
            }

            Console.WriteLine(thirdSymbol);
        }

        static void Print(string sqltext, IEnumerable items)
        {
            Console.WriteLine(sqltext);

            // Heading
            var attributes = new string[0];

            // Stores the length of the longest words
            var longestWords = new int[0];

            // Create array for body
            var numberOfQueries = 0;
            var tuples = new string[numberOfQueries][];

            // Flag to fill the heading
            var isFirstTry = true;

            foreach (var item in items)
            {
                var allWordsList = new List<string>();

                numberOfQueries++;
                Array.Resize(ref tuples, numberOfQueries);

                // get Key-Value
                var cleanItem = item.ToString().Remove(0, 1);
                cleanItem = cleanItem.Remove(cleanItem.Length - 1);
                var keyValueStrings = cleanItem.Split(',');

                foreach (var keyValue in keyValueStrings)
                {
                    var keyValueSplit = keyValue.Split("=");
                    allWordsList.AddRange(keyValueSplit);
                }

                if (isFirstTry)
                {
                    var columnList = new List<string>();
                    isFirstTry = false;
                    for (var i = 0; i < allWordsList.ToArray().Length; i += 2)
                    {
                        columnList.Add(allWordsList[i]);
                        attributes = columnList.ToArray();
                        Array.Resize(ref longestWords, attributes.Length);
                        for (var j = 0; j < attributes.Length; j++)
                        {
                            longestWords[j] = attributes[j].Length;
                        }
                    }
                }

                var rowList = new List<string>();

                for (var i = 1; i < allWordsList.ToArray().Length; i += 2)
                {
                    rowList.Add(allWordsList[i]);
                }

                for (var i = 0; i < rowList.ToArray().Length; i++)
                {
                    if (rowList[i].Length > longestWords[i])
                    {
                        longestWords[i] = rowList[i].Length;
                    }
                }

                tuples[numberOfQueries - 1] = new string[rowList.ToArray().Length];

                for (var n = 0; n < rowList.ToArray().Length; n++)
                {
                    tuples[numberOfQueries - 1][n] = rowList[n];
                }
            }

            // Create frame for table
            TablePart("┌", "┬", "┐", attributes, longestWords);

            // Draw attributes
            Console.Write("│");
            for (var h = 0; h < attributes.Length; h++)
            {
                Console.Write(attributes[h].PadRight(longestWords[h]) + "│");
            }

            Console.WriteLine();

            // Draw tuples
            for (var k = 0; k < tuples.Length; k++)
            {
                TablePart("├", "┼", "┤", attributes, longestWords);

                Console.Write("│");

                for (var j = 0; j < tuples[k].Length; j++)
                {
                    Console.Write(tuples[k][j].PadRight(longestWords[j]) + "│");
                }

                Console.WriteLine();
            }

            TablePart("└", "┴", "┘", attributes, longestWords);
            Console.WriteLine("\n");
        }

        static void SelectGenres(Context db)
        {
            var queryLINQ = from genre in db.Genres
                select new
                {
                    Название = genre.NameGenre,
                    Описание = genre.DescriptionOfGenre
                };
            Print("1. Список жанров: ", queryLINQ.ToList());
        }

        static void SelectTVShows(Context db)
        {
            var queryLINQ = from show in db.TVShows
                where show.NameShow == "Решка и Пешка"
                select new
                {
                    Название_Шоу = show.NameShow,
                    Длительность = show.Duration,
                    Рейтинг = show.Rating,
                    Описание_Шоу = show.DescriptionShow
                };
            Print("2. Список ТВ шоу с названием Решка и Пешка: ", queryLINQ.ToList());
        }

        static void SelectCitizenAppealsForShow(Context db)
        {
            var queryLINQ = from appeal in db.CitizensAppeals
                join schedule in db.SchedulesForWeek
                    on appeal.ScheduleForWeekID equals schedule.ScheduleForWeekID
                select new
                {
                    ФИО = appeal.LFO,
                    Начало_Транслирования_Шоу = schedule.StartTime
                };
            Print("4. Обращения граждан к шоу: ", queryLINQ.ToList());
        }

        static void SelectScheduleOfShows(Context db)
        {
            var queryLINQ = from show in db.TVShows
                join schedule in db.SchedulesForWeek
                    on show.TVShowID equals schedule.TVShowID
                where show.DescriptionShow == "Учавствуют дети" && schedule.GuestsEmployees == "Андрей Малахов"
                select new
                {
                    Название_Шоу = show.NameShow,
                    Длительность = show.Duration,
                    Рейтинг = show.Rating,
                    Описание_Шоу = show.DescriptionShow,
                    Время_Начала_Шоу = schedule.StartTime,
                    Приглашенные_Гости = schedule.GuestsEmployees
                };
            Print("5. Расписание шоу: ", queryLINQ.ToList());
        }

        static void InsertIntoOnes(Context db)
        {
            var genre = new Genre
            {
                NameGenre = "Интервью",
                DescriptionOfGenre = "16+"
            };

            db.Genres.Add(genre);
            db.SaveChanges();

            Console.WriteLine("6. Была произведена вставка данных в таблину на стороне ОДИН\n");
        }

        static void InsertIntoManys(Context db)
        {
            Random rand = new Random();
            var idCount = (from genre in db.Genres select genre.GenreID).Last();
            int rnd = rand.Next(1, idCount);

            var tvShow = new TVShow
            {
                NameShow = "Что? Кто? А?",
                Duration = "60 мин.",
                Rating = "99 %",
                DescriptionShow = "Учавствуют взрослые и дети",
                GenreID = rnd
            };

            db.TVShows.Add(tvShow);
            db.SaveChanges();

            Console.WriteLine("7. Была произведена вставка данных в таблину на стороне МНОГО\n");
        }

        static void Update(Context db)
        {
            var oldGenreName = "Интервью";

            var updateGenre = db.Genres.FirstOrDefault(genre => genre.NameGenre == oldGenreName);

            if (updateGenre != null)
            {
                updateGenre.NameGenre = "Разговорное";

                db.SaveChanges();
                Console.WriteLine("10. Обновление данных произведено успешно\n");
            }
        }

        static void DeleteFromManys(Context db)
        {
            var removeAppeal = "Малышева Полина Казимировна";

            var appealTo = db.CitizensAppeals.Where(appeal => appeal.LFO == removeAppeal);

            db.CitizensAppeals.RemoveRange(appealTo);
            db.SaveChanges();
            Console.WriteLine("9. Удаление прошло успешно\n");
        }

        static void DeleteFromOnes(Context db)
        {
            var scheduleToRemove = "15:15";

            int? id = null;

            foreach (var schedule in db.SchedulesForWeek)
            {
                if (schedule.StartTime == scheduleToRemove)
                {
                    id = schedule.ScheduleForWeekID;
                }
            }

            if (id != null)
            {
                var citizenAppeal = db.CitizensAppeals.Where(appeal => appeal.ScheduleForWeekID == id);

                var scheduleFor = db.SchedulesForWeek.Where(schedule => schedule.StartTime == scheduleToRemove);

                db.CitizensAppeals.RemoveRange(citizenAppeal);
                db.SaveChanges();

                db.SchedulesForWeek.RemoveRange(scheduleFor);
                db.SaveChanges();

                Console.WriteLine("8. Удаление прошло успешно\n");
            }
        }

        static void Group(Context db)
        {
            Console.WriteLine("3. Группировка данных по описаню шоу:\n");
            var showsGroup1 = from show in db.TVShows
                              group show by show.DescriptionShow into g1
                              select new
                              {
                                Name = g1.Key,
                                Count = g1.Count(),
                                Phones = from p in g1 select p
                              };

            foreach (var group in showsGroup1)
            {
                Console.WriteLine("Шоу с описанием \"{0}\": {1}", group.Name, group.Count);
                int i = 0;
                foreach (var show in group.Phones)
                {
                    i++;
                    Console.WriteLine(i + ". " + show.NameShow);
                }

                Console.WriteLine();
            }
        }
    }
}
