using System.Collections.Generic;


namespace SAESchedule
{

    /// <summary>
    /// The Amount of Money a User can make per hour * 10
    /// </summary>
    public enum SalaryType : byte
    {
        STARTER = 105,
        REGULAR = 125
    }

    /// <summary>
    /// A User is a Person which can have Shifts
    /// </summary>
    [System.Serializable]
    public sealed class User
    {

        /// <summary>
        /// The Users lastname
        /// </summary>
        public readonly string LastName;

        /// <summary>
        /// The Users given Name
        /// </summary>
        public readonly string GivenName;

        /// <summary>
        /// The Users faculty. e.g. Art, Prog, ...
        /// </summary>
        public readonly string Faculty;

        /// <summary>
        /// A unique ID for the User
        /// </summary>
        public string ID { get { return string.Format("USER_{0}_{1}", LastName, GivenName).ToLower();} }

        /// <summary>
        /// The Name used in the Shifts for the Infoscreen
        /// Example: Lukas is Lukas K because of Lukas Lambrecht
        /// </summary>
        public string ShiftID { get; private set; }

        /// <summary>
        /// The maximal amoutn of money the user wants to earn in one month
        /// </summary>
        public float MaxRevenue { get; private set; }

        /// <summary>
        /// The SalaryType of the User
        /// </summary>
        public SalaryType SalaryType { get; private set; }

        /// <summary>
        /// The Hourly Salary the User will get
        /// </summary>
        public float HourlySalary { get { return ((float)SalaryType) / 10.0f; } }

        public User(string _lastName, string _givenName, string _faculty, string _shiftID, float _maxRevenue, SalaryType _salaryType)
        {
            this.LastName = _lastName;
            this.GivenName = _givenName;
            this.Faculty = _faculty;
            this.ShiftID = _shiftID;
            this.MaxRevenue = _maxRevenue;
            this.SalaryType = _salaryType;
        }

        /// <summary>
        /// Changes the ShiftID of the
        /// </summary>
        /// <param name="_newShiftID"></param>
        /// <returns></returns>
        public void ChangeShiftID(string _newShiftID)
        {
            // set the new value
            ShiftID = _newShiftID;
        }
        
        /// <summary>
        /// Changes the max revenue for this user
        /// </summary>
        /// <param name="_newMaxRevenue"></param>
        public void ChangeMaxRevenue(float _newMaxRevenue)
        {
            // set the new value
            MaxRevenue = _newMaxRevenue;

            // recalculate the shifts
            ShiftScheduler.Calculate();
        }

        /// <summary>
        /// Changes the Salary of the User
        /// </summary>
        /// <param name="_newSalaryType"></param>
        public void ChangeSalaryType(SalaryType _newSalaryType)
        {
            // set the new value
            SalaryType = _newSalaryType;

            // recalculate the shifts
            ShiftScheduler.Calculate();
        }
        

        /// <summary>
        /// Returns Maximum nummber of Hours
        /// </summary>
        public int GetMaxHours()
        {
            int maxH = (int)(MaxRevenue / ((float)SalaryType /10.0f));

            return maxH;
        }


        
    }

}