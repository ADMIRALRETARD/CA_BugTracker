using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CA_BugTracker
{
    public class Repository
    {
        public List<Task> tasks;
        public List<User> users;
        public List<Project> projects;

        public Repository()
        {
            projects = new List<Project>
            {
                new Project
                {
                  ProjectName="Один"
                },
                new Project{
                  ProjectName="ПроектДва"
                },
                new Project{
                  ProjectName="НомерТри"
                },
            };

            users = new List<User>
            {
                new User
                {
                    FirstName="Евгений",
                    LastName="Замятин"
                },
                new User
                {
                    FirstName="Эмили",
                    LastName="Бронте"
                },
                new User
                {
                    FirstName="Линус",
                    LastName="Торвальдс"
                }
            };
            tasks = new List<Task>
            {
                new Task
                {
                    Theme ="ЭтоНазвание",
                    Type="Тип1",
                    Priority="Высокий",
                    Description="Описание1",
                    User=users[0],
                    Project=projects[0],
                },
                new Task
                {
                    Theme="ВтороеНазвание",
                    Type="Тип2",
                    Priority="Средний",
                    Description="Описание2",
                    User=users[1],
                    Project=projects[1],

                },
                new Task
                {
                    Theme="ЕщеНазвание",
                    Type="Тип3",
                    Priority="Низкий",
                    Description="Описание",
                    User=users[2],
                    Project=projects[2],
                }
            };

        }

        #region Add

        public void AddTask(Task task)
        {
            tasks.Add(task);
        }
        public void AddProject(Project project)
        {
            projects.Add(project);
        }
        public void AddUser(User user)
        {
            users.Add(user);
        }
        #endregion


        #region Delete


        public void DeleteTask(Task task)
        {
            tasks.Remove(task);
        }


        public void DeleteProject(Project project)
        {
            projects.Remove(project);
        }

        public void DeleteUser(User user)
        {
            users.Remove(user);
        }

        #endregion


        ///Проверки на наличие в списках
        #region Validation
        ///Проверка на наличие Пользователя в списке Пользователя(для контроллера)
        public bool IsContainUser(string user)
        {
            if (users.Contains(users.Find(u => u.FirstName == user || u.LastName == user)))
            {
                return true;
            }
            return false;

        }
        //Проверка на наличие Проекта в списке Проекты(для контроллера)
        public bool IsContainProject(string project)
        {
            if (projects.Contains(projects.Find(p => p.ProjectName == project)))
            {
                return true;
            }
            return false;

        }

        // Проверка на наличие задачи в списке Задачи
        public bool IsContainTask(int removeTask)
        {
            if (FindTaskId(removeTask) != null)
            {
                return true;
            }
            return false;
        }
        //Проверка на наличие пользователя в списке Задачи
        public bool IsContainUserInTask(int removeUser)
        {
            if (tasks.Contains(tasks.Find(u => u.User.Id == Convert.ToInt32(removeUser))))
            {
                return true;
            }
            return false;
        }
        //Проверка наличия пользователя в списке Пользователи
        public bool IsUserContainList(int removeUser)
        {
            if (users.Contains(FindUserId(removeUser)))
            {
                return true;
            }
            return false;
        }
        //Проверка на наличие проекта в списке Задачи
        public bool IsProjectContainInTask(int removeProject)
        {
            if (tasks.Contains(tasks.Find(p => p.Project.Id == removeProject)))
            {
                return true;
            }
            return false;
        }
        //Проверка на наличие проекта в списке Проекты
        public bool IsProjectContainList(int removeProject)
        {
            if (projects.Contains(FindProjectId(removeProject)))
            {
                return true;
            }
            return false;
        }
        #endregion
        #region FindObjects
        public User FindUserId(int removeUser)
        {
            return users.Find(u => u.Id == Convert.ToInt32(removeUser));
        }
        public Task FindTaskId(int removeTask)
        {
            return tasks.Find(f => f.Id == removeTask);
        }
        public Project FindProjectId(int removeProject)
        {
            return projects.Find(r => r.Id == removeProject);
        }
        #endregion
    }
}
