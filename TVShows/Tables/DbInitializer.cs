using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace TVShows.Tables
{
    class DbInitializer
    {
        public static void Initialize(Context db)
        {
            db.Database.EnsureCreated();
            
            if (db.TVShows.Any())
            {
                return;
            }

            Random rand = new Random();
            int id;
            string[] genres = { "Викторина", "Разговорное", "Здоровье", "Интервью", "Новости", "Шоу талантов", "Путешествия", "Юмор" };
            string[] description = { "18+", "12+", "0+", "16+" };
            for (int i = 0; i < 5; i++)
            {
                db.Genres.Add(new Genre
                {
                    NameGenre = genres[rand.Next(8)],
                    DescriptionOfGenre = description[rand.Next(4)]
                });
            }
            db.SaveChanges();

            string[] showName = { "Пусть говорят", "Жить здорово", "Вести", "Что? Кто? А?", "Решка и Пешка" };
            string[] showDesc = { "Учавствуют взрослые", "Учавствуют взрослые и дети", "Учавствуют дети" };
            for (int i = 0; i < 5; i++)
            {
                id = rand.Next(1, 6);
                db.TVShows.Add(new TVShow
                {
                    NameShow = showName[id - 1],
                    Duration = rand.Next(20, 60) + " мин.",
                    Rating = rand.Next(0, 100) + " %",
                    DescriptionShow = showDesc[rand.Next(3)],
                    GenreID = id
                });
            }
            db.SaveChanges();

            string[] guests = { "Андрей Малахов", "Елена Малышева", "Казимир", "Александр Лукашенко", "Оптимус" };
            for (int i = 0; i < 5; i++)
            {
                id = rand.Next(1, 6);
                db.SchedulesForWeek.Add(new ScheduleForWeek
                {
                    StartTime = rand.Next(23) + ":" + rand.Next(59),
                    GuestsEmployees = guests[rand.Next(5)],
                    TVShowID = id
                });
            }
            db.SaveChanges();

            string[] lfo = { "Зубенко Михаил Николаевич", "Малышева Полина Казимировна", "Лев Николаевич Толстой", "Шупкин Сергей Александрович", "Оптимус Бодя Александрович" };
            string[] organization = { "EPAM", "IBM", "Первый канал", "OP" };
            string[] goalOfRequest = { "Закрытие шоу", "Нарушение авторских прав", "Изменение возрастных ограничений" };
            for (int i = 0; i < 5; i++)
            {
                id = rand.Next(1, 6);
                db.CitizensAppeals.Add(new CitizensAppeal
                {
                    LFO = lfo[rand.Next(5)],
                    Organization = organization[rand.Next(4)],
                    GoalOfRequest = goalOfRequest[rand.Next(3)],
                    ScheduleForWeekID = id
                });
            }
            db.SaveChanges();
        }
    }
}
