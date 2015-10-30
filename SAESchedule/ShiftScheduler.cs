﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace SAESchedule
{

    /// <summary>
    /// Responsible for calculating the shifts
    /// </summary>
    public static class ShiftScheduler
    {

        /// <summary>
        /// The current month
        /// </summary>
        private static Month Current;

        /// <summary>
        /// A List which contains all months
        /// </summary>
        private static List<Month> allMonths;

        /// <summary>
        /// A List which contains all Users
        /// </summary>
        private static List<User> allUsers;

        /// <summary>
        /// The Settings used to generate shifts
        /// </summary>
        private static ShiftSettings settings;

        /// <summary>
        /// Static Constructor for initializing the class
        /// </summary>
        static ShiftScheduler()
        {
            allUsers = new List<User>();
        }

        /// <summary>
        /// Loads all Settings from the specified file
        /// </summary>
        /// <param name="_stream"></param>
        /// <returns></returns>
        public static bool Load(Stream _stream)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves all Settings to the specified file
        /// </summary>
        /// <param name="_stream"></param>
        /// <returns></returns>
        public static bool Save(Stream _stream)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Calculates the Shifts for the given settings
        /// </summary>
        public static void Calculate()
        {
            // generate new shifts
            Current.GenerateShifts(settings);

            Dictionary<string, int> hourDictionary = new Dictionary<string,int>();

            // iterate over all shifts
            foreach(Shift shift in Current.GetShifts())
            {
                // calculate the shift
                CalculateShift(shift, ref hourDictionary);
            }
        }

        private static void CalculateShift(Shift _shift, ref Dictionary<string, int> _hours)
        {
            // variable to store the individual weights for each user
            Dictionary<string, float> weightPerUser = new Dictionary<string, float>();

            foreach(Shift.UserShift us in _shift.possibleUsers)
            {
                // check if the user is eligible for the shift
                if(_shift.ShiftFaculty != GetUserByID(us.User).Faculty)
                {
                    Debug.WriteLine("Faculty mismatch between User and Shift detected!");
                    continue;
                }

                // check if the user has no entry in the hours dictionary
                if(!_hours.ContainsKey(us.User)) { _hours[us.User] = 0; }

                // calculate the weight for the user and add it
                weightPerUser.Add(us.User, GetUserWeight(us.User, us.Preference, _hours[us.User]));
            }

            // iterate over all keys
            foreach(string key in weightPerUser.Keys.ToArray())
            {
                // check if the user has no weight
                if(weightPerUser[key] <= 0.0f)
                {
                    // remove the user
                    weightPerUser.Remove(key);
                }
            }

            // check if there is anyone left to fill the shift
            if(weightPerUser.Count == 0)
            {
                // TODO: Think of a better way to signal that nobody can take the shift
                _shift.UserID = "NOONE";
                return;
            }

            // get the user with the highest weight
            KeyValuePair<string, float> p = weightPerUser.Max();

            // set him as the one who will take the shift
            _shift.UserID = p.Key;

            // increase his time
            _hours[p.Key] = _hours[p.Key] + _shift.Hours;
        }


        private static float GetUserWeight(string _user, Shift.UserShiftPreference _preference, int _hours)
        {
            // get the user
            User user = GetUserByID(_user);

            // TODO: Think of a better weight
            return ((float)_preference) * (1 / ((_hours * user.HourlySalary) / user.MaxRevenue));
        }

        private static User GetUserByID(string _id)
        {
            return allUsers.Where(m => m.ID == _id).First();
        }

    }

}