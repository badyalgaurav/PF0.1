using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartyFund.DataAccess.Implementation.Repositories
{
    class Test
    {
        public void test()
        {

        int totalAmount = 4000;
        int withdraw = 3999;
        int leftamount= totalAmount - withdraw;
        decimal userCurrentAmount = 500;
        decimal withdrawPercentage = (withdraw * 100) / totalAmount;
        for (int i = 1; i <= 5; i++)
        {
            var leftCurrentAmount = (userCurrentAmount * withdrawPercentage) / 100;
            //here we will add new row to db with operation(this will used if need to get short report list)
        }
        }
    }
}
