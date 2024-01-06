using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutageManagementSystem
{
    interface IUserInputHelper
    {
        // Metoda za traženje unosa formata izveštaja od korisnika
        string PromptForReportFormat();
    }
}
