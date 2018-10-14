using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_BugTracker
{
    public class Menu
    {
        Controller controller = new Controller();
        string command;
        
        public void MainMenu()
        {
            Console.WriteLine("Для навигации введите на клавиатуре соответствующую цифру.\nНажмите любую клавишу для продолжения...");
            Console.ReadLine();
            Console.Clear();


            Console.WriteLine("1.Показать задачи\n2.Опции\n3.Показать дополнительную информацию\n4.Сохранить на жесткий диск\n5.Выход");
            command = Console.ReadLine();
            if (command == "")
            {
                command = Console.ReadLine();
            }


            while (true)
            {
                switch (command)
                {

                    case "1":
                        controller.ShowTasks();
                        command = Console.ReadLine();
                        break;
                    case "2":
                        Options();
                        break;
                    case "3":
                        ShowMore();
                        break;
                    case "4":
                        controller.Savefile();
                        command = Console.ReadLine();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:

                        MainMenu();
                        break;
                }
            }
            
        }

        void Options()
        {
            string optioncommand;
            Console.Clear();
            Console.WriteLine("1.Добавить задачу\n2.Добавить проект\n3.Добавить пользователя\n4.Удалить задачу" +
                "\n5.Удалить проект\n6.Удалить пользователя" +
                "\n7.Главное меню");
            optioncommand = Console.ReadLine();
            if (optioncommand == "")
            {
                optioncommand = Console.ReadLine();
            }
            switch (optioncommand)
            {
                case "1":
                    controller.AddTask();
                    break;
                case "2":
                    controller.AddProject();
                    break;
                case "3":
                    controller.AddUser();
                    break;
                case "4":
                    controller.DeleteTask();
                    break;
                case "5":
                    controller.DeleteProject();
                    break;
                case "6":
                    controller.DeleteUser();
                    break;
                case "7":
                    MainMenu();
                    break;
                default:
                    Console.WriteLine("Вы не ввели число");
                    Options();
                    optioncommand = Console.ReadLine();
                    break;
            }
        }

        void ShowMore()
        {
            Console.Clear();
            Console.WriteLine("1.Показать все проекты\n2.Показать всех пользователй" +
                            "\n3.Показать список задач в проекте" +
                            "\n4.Показать список задач,назначенных на конкретного исполнителя" +
                            "\n5.Главное меню");
            string showcommand;
            showcommand = Console.ReadLine();
            switch (showcommand)
            {
                case "1":
                    controller.ShowProjects();
                    showcommand = Console.ReadLine();
                    break;
                case "2":
                    controller.ShowUsers();
                    showcommand = Console.ReadLine();
                    break;
                case "3":
                    controller.ShowTasksInProject();
                    showcommand = Console.ReadLine();
                    break;
                case "4":
                    controller.ShowTaskForUser();
                    showcommand = Console.ReadLine();
                    break;
                case "5":
                    MainMenu();
                    break;
                default:
                    MainMenu();
                    break;
                    
            }
        }
    }
}
