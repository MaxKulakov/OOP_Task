using System;
using System.Collections.Generic;
using System.IO;

namespace OOP
{
    // Добавление пар для удобства написание программы
    public class Pair<T, U>
    {
        public Pair() {}

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public T First { get; set; }
        public U Second { get; set; }
    };

    
    // класс Пользователя
    public class Profile
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int totalScore;
        public int TotalScore
        {
            get { return totalScore; }
            set { totalScore = value; }
        }


        // Получение количества игроков в сохранении
        public int GetCountUsers()
        {
            int countUsers = 0;
            string path = @"/Users/kulakov/RiderProjects/OOP/OOP/users.save";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    String tempString = sr.ReadToEnd();
                    string[] subs = tempString.Split('\n');
                    countUsers = Int32.Parse(subs[0]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return countUsers;
        }


        // Считывание имён и счёта игроков
        public List<Profile> GetUsersList(int countUsers)
        {
            Profile user1 = new Profile();
            Profile user2 = new Profile();
            Profile user3 = new Profile();

            List<Profile> users = new List<Profile>();

            string path = @"/Users/kulakov/RiderProjects/OOP/OOP/users.save";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    String tempString = sr.ReadToEnd();
                    string[] subs = tempString.Split('\n');

                    switch (countUsers)
                    {
                        case 1:
                            user1.Name = subs[1];
                            user1.TotalScore = Int32.Parse(subs[2]);
                            users.Add(user1);
                            break;
                        case 2:
                            user1.Name = subs[1];
                            user1.TotalScore = Int32.Parse(subs[2]);
                            user2.Name = subs[3];
                            user2.TotalScore = Int32.Parse(subs[4]);
                            users.Add(user1);
                            users.Add(user2);
                            break;
                        case 3:
                            user1.Name = subs[1];
                            user1.TotalScore = Int32.Parse(subs[2]);
                            user2.Name = subs[3];
                            user2.TotalScore = Int32.Parse(subs[4]);
                            user3.Name = subs[5];
                            user3.TotalScore = Int32.Parse(subs[6]);
                            users.Add(user1);
                            users.Add(user2);
                            users.Add(user3);
                            break;
                        default:
                            Console.WriteLine("Users > 3");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return users;
        }
    }


    public class Game
    {
        // Выбор профиля
        public int SelectProfile(List<Profile> users, int countUsers)
        {
            int selectUser = 0;

            switch (countUsers)
            {
                case 1:
                    Console.WriteLine("Выберите профиль:\n1-" + users[0].Name);
                    break;
                case 2:
                    Console.WriteLine("Выберите профиль:\n1-" + users[0].Name + "\n2-" + users[1].Name);
                    break;
                case 3:
                    Console.WriteLine("Выберите профиль:\n1-" + users[0].Name + "\n2-" + users[1].Name +
                                      "\n3-" + users[2].Name);
                    break;
                default: break;
            }
            try
            {
                selectUser = Int32.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
            }

            Console.Clear();
            return selectUser;
        }

        // Выбор сложности
        public int GetDifficult()
        {
            int difficult = 0;
            Console.WriteLine("Выберите сложность:\n1-Легко\n2-Средне\n3-Сложно");
            try
            {
                difficult = Int32.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
            }
            Console.Clear();
            return difficult;
        }


        // Выбор словаря для заданной сложности
        public List<Pair<string, string>> GetDictonaryPool(int difficult)
        {
            List<Pair<string, string>> dict = new List<Pair<string, string>>();

            string path = @"/Users/kulakov/RiderProjects/OOP/OOP/library.game";
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    String tempString = sr.ReadToEnd();
                    string[] subs = tempString.Split('*');
                    switch (difficult)
                    {
                        case 1:
                            subs = subs[1].Split('\n');
                            break;
                        case 2:
                            subs = subs[2].Split('\n');
                            break;
                        case 3:
                            subs = subs[3].Split('\n');
                            break;
                        default: break;
                    }

                    for (int i = 1; i < subs.Length - 1; i += 2)
                    {
                        Pair<string, string> tempPair = new Pair<string, string>();
                        tempPair.First = subs[i];
                        tempPair.Second = subs[i + 1];
                        dict.Add(tempPair);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return dict;
        }

        
        // Базовое меню выбора
        public void Menu()
        {
            Console.Clear();
            int selectItem = 0;
            Console.WriteLine("Добро пожаловать в игру!\n1-Начать\n2-Выйти");
            try
            {
                selectItem = Int32.Parse(Console.ReadLine());
            }
            catch (Exception e) {}
            
            switch (selectItem)
            {
                case 1:
                    Console.Clear();

                    Profile pfl = new Profile();
                    int usersCount = pfl.GetCountUsers();
                    List<Profile> users = new List<Profile>();
                    users = pfl.GetUsersList(usersCount);
                    
                    Game gm = new Game();
                    int profileNumber = gm.SelectProfile(users, usersCount);
                    int difficult = gm.GetDifficult();
                    List<Pair<string, string>> dict = new List<Pair<string, string>>();
                    dict = gm.GetDictonaryPool(difficult);
                    gm.Play(users, profileNumber, difficult, dict);
                    break;

                case 2:
                    Environment.Exit(0);
                    break;
                default: break;
            }
        }


        // Проведение игры
        public void Play(List<Profile> users, int profileNumber, int difficult, List<Pair<string, string>> dict)
        {
            Console.Clear();
            int countQuestion = 1;
            int score = 0;
            string tempAnswer = null;
            while (countQuestion != dict.Count + 1)
            {
                Console.Clear();
                Console.WriteLine("Ваш счёт: " + score);
                Console.WriteLine("Вопрос: " + countQuestion + " / " + dict.Count + Environment.NewLine);
                
                Console.WriteLine(dict[countQuestion - 1].First);
                tempAnswer = Console.ReadLine();

                if (dict[countQuestion - 1].Second == tempAnswer)
                {
                    score += difficult;
                }
                else if (difficult != 1)
                    score -= difficult;
                countQuestion += 1;
            }
            Console.Clear();
            Console.WriteLine("Ваш счёт: " + score + "\n\nСпасибо за игру!\nВы будете перенаправлены в меню");
            users[profileNumber-1].TotalScore += score; 
            SaveScore(users, profileNumber, score);
            System.Threading.Thread.Sleep(1500);
            Menu();
        }


        // Сохранение результатов игры
        public void SaveScore(List<Profile> users, int profileNumber, int score)
        {
            string writePath = @"/Users/kulakov/RiderProjects/OOP/OOP/users.save";

            string text = users.Count.ToString() + "\n";
            switch (users.Count)
            {
                case 1:
                    text += users[0].Name.ToString() + "\n" + users[0].TotalScore.ToString() + "\n";
                    break;
                case 2: 
                    text += users[0].Name.ToString() + "\n" + users[0].TotalScore.ToString() + "\n" +
                            users[1].Name.ToString() + "\n" + users[1].TotalScore.ToString();
                    break;
                case 3:
                    text += users[0].Name.ToString() + "\n" + users[0].TotalScore.ToString() + "\n" +
                            users[1].Name.ToString() + "\n" + users[1].TotalScore.ToString() + "\n" +
                            users[2].Name.ToString() + "\n" + users[2].TotalScore.ToString();
                    break;
                default: break;
            }
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
            }
            catch(Exception e) {}
        }
    }
}