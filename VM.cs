//Group 5: Sairam Ganesh Yedida Harivenkata,Nikitaben Jashvantsinh Raj,Krishna Chaitanya Potharajuusing System.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System_Dependencies___Final_Assignment
{
    class VM : INotifyPropertyChanged
    {
        public BindingList<string> Items { get; set; } = new BindingList<string>();
        public string Text { get; set; }

        public List<Softwares> SoftwareList = new List<Softwares>();

        public void process()
        {
            Items.Add(Text);
            int flag;
            string[] words = Text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words[0] == "DEPEND")
            {


                for (int i = 1; i < words.Length; i++)
                {
                    flag = 0;
                    for (int j = 0; j < SoftwareList.Count; j++)
                    {
                        if (SoftwareList[j].Name == words[i])
                        {
                            flag = 1;
                        }

                    }
                    if (flag == 0)
                    {
                        SoftwareList.Add(new Softwares
                        {
                            Name = words[i],
                            isInstalled = false,
                            isExplicit = false,
                        });
                    }

                }

                for (int i = 2; i < words.Length; i++)
                {
                    for (int j = 0; j < SoftwareList.Count; j++)
                    {
                        if (SoftwareList[j].Name == words[1])
                        {
                            SoftwareList[j].IDependOn.Add(words[i]);

                        }
                        if (SoftwareList[j].Name == words[i])
                        {
                            SoftwareList[j].OthersDependOnMe.Add(words[1]);
                        }
                    }
                }

            }
            else if (words[0] == "LIST")
            {
                for (int j = 0; j < SoftwareList.Count; j++)
                {
                    if (SoftwareList[j].isInstalled) { Items.Add($"    {SoftwareList[j].Name} is installed"); }
                }
            }

            else if (words[0] == "INSTALL")
            {
                int check = 0;
                int index = 0;
                for (int j = 0; j < SoftwareList.Count; j++)
                {
                    if (SoftwareList[j].Name == words[1])
                    {
                        check = 1;
                        index = j;
                        if (SoftwareList[j].isInstalled)
                        {
                            check = 3;
                        }
                    }
                }
                if (check == 1)
                {
                    SoftwareList[index].isExplicit = true;
                    install(words[1]);

                }
                else if (check == 0)
                {
                    SoftwareList.Add(new Softwares
                    {
                        Name = words[1],
                        isInstalled = true,
                        isExplicit = true,
                    });
                    Items.Add($"    installing {words[1]}");
                }
                else if (check == 3)
                {
                    Items.Add($"    {words[1]} is already installed.");
                }
            }
            else if (words[0] == "REMOVE")
            {
                string mastercommand = "";
                for (int j = 0; j < SoftwareList.Count; j++)
                {
                    if (SoftwareList[j].Name == words[1] && !SoftwareList[j].OthersDependOnMe.Any())
                    {
                        SoftwareList[j].isExplicit = false;
                    }
                }
                mastercommand = words[1];
                uninstall(words[1],mastercommand);
            }

            else if (words[0] == "END")
            {
                System.Windows.Application.Current.Shutdown();
            }

            else
            {
                Items.Add("    Wrong Entry.");
            }
        }

        public void install(string s)
        {
            for (int j = 0; j < SoftwareList.Count; j++)
            {
                if (SoftwareList[j].Name == s)
                {
                    if (SoftwareList[j].IDependOn.Any())
                    {
                        foreach (string depends in SoftwareList[j].IDependOn)
                            install(depends);
                        if (!SoftwareList[j].isInstalled)
                        {
                            SoftwareList[j].isInstalled = true;
                            Items.Add($"    installing {SoftwareList[j].Name}");
                        }
                    }
                    else
                    {
                        if (!SoftwareList[j].isInstalled)
                        {
                            SoftwareList[j].isInstalled = true;
                            Items.Add($"    installing {SoftwareList[j].Name}");
                        }
                    }
                }
            }
        }

        public void uninstall(string s,string mc)
        {
            int isinstalled = 0;
            for (int j = 0; j < SoftwareList.Count; j++)
            {
                if (SoftwareList[j].Name == s)
                {
                    isinstalled = 1;
                    if (SoftwareList[j].Name == s && !SoftwareList[j].isInstalled)
                    {
                        Items.Add($"    {SoftwareList[j].Name} is not installed.");

                    }
                    else if (!SoftwareList[j].OthersDependOnMe.Any())
                    {

                        foreach (string idenpend in SoftwareList[j].IDependOn)
                        {
                            for (int l = 0; l < SoftwareList.Count; l++)
                            {
                                if (SoftwareList[l].Name == idenpend)
                                {
                                    for (int i = 0; i < SoftwareList[l].OthersDependOnMe.Count; i++)
                                    {
                                        if (SoftwareList[l].OthersDependOnMe[i] == SoftwareList[j].Name)
                                            SoftwareList[l].OthersDependOnMe.RemoveAt(i);
                                    }
                                }
                            }

                        }

                        foreach (string depends in SoftwareList[j].IDependOn)
                            uninstall(depends,mc);

                        if (SoftwareList[j].isInstalled)
                        {
                            if (!SoftwareList[j].isExplicit)
                            {
                                SoftwareList[j].isInstalled = false;
                                Items.Add($"    Removing {SoftwareList[j].Name}");
                            }

                        }

                    }
                    else if (SoftwareList[j].OthersDependOnMe.Any())
                    {
                        if(SoftwareList[j].Name==mc)
                        Items.Add($"    {SoftwareList[j].Name} is still needed");
                    }
                }
            }
            if (isinstalled == 0)
            {
                Items.Add($"    {s} is not installed.");
            }
        }

        #region Prop Change
        public event PropertyChangedEventHandler PropertyChanged;

        private void propChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
