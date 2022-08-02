//Group 5: Sairam Ganesh Yedida Harivenkata,Nikitaben Jashvantsinh Raj,Krishna Chaitanya Potharajuusing System.Windows;
using System.Collections.Generic;

namespace System_Dependencies___Final_Assignment
{
    class Softwares
    {
        public string Name;
        public bool isInstalled;
        public bool isExplicit;
        public List<string> IDependOn = new List<string>();
        public List<string> OthersDependOnMe = new List<string>();

    }
}
