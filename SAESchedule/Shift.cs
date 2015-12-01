using System;
using System.Collections.Generic;


namespace SAESchedule
{

    [System.Serializable]
    public sealed class Shift
    {

        /// <summary>
        /// 
        /// </summary>
        public enum UserShiftPreference : byte
        {
            NO_WORK = 0,
            ONLY_IF_NECESSARY = 1,
            MAYBE = 2,
            PREFERRED = 4
        }

        /// <summary>
        /// Contains the Settings a User has for this shift
        /// </summary>
        public sealed class UserShift
        {

            /// <summary>
            /// The User this settings belong to
            /// </summary>
            public readonly string User; // Use other name User already Class na

            /// <summary>
            /// The Preference of the user regarding this shift
            /// </summary>
            public readonly UserShiftPreference Preference;

            public UserShift(string _user, UserShiftPreference _preference)
            {
                User = _user;
                Preference = _preference;
            }

        }

        /// <summary>
        /// The User who will work
        /// </summary>
        public string UserID;

        /// <summary>
        /// Contains all possible users for this shift
        /// </summary>
        public List<UserShift> possibleUsers;

        /// <summary>
        /// The Faculty of the Shift, e.g. Art, Prog, ...
        /// </summary>
        public readonly string ShiftFaculty;

        /// <summary>
        /// The Begin of the shift
        /// </summary>
        public readonly DateTime begin;

        /// <summary>
        /// The End of the shift
        /// </summary>
        public readonly DateTime end;

        /// <summary>
        /// The Time in hours the User is working
        /// </summary>
        public int Hours { get { return (end - begin).Hours; } }

        public Shift(string _userID, string _shiftFaculty, DateTime _begin, DateTime _end)
        {
            UserID = _userID;
            ShiftFaculty = _shiftFaculty;
            begin = _begin;
            end = _end;
        }

        /// <summary>
        /// Checks if the given Time is within the Shift
        /// </summary>
        /// <param name="p_Time"></param>
        /// <returns></returns>
        public bool ContainsTimeStamp( DateTime p_Time )
        {
            if (p_Time <= begin)
                return false;

            if (p_Time >= end)
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the given TimeWindow overlaps this Shift
        /// </summary>
        /// <param name="p_TimeSpan"></param>
        /// <returns></returns>
        public bool ContainsTimeSpan( DateTime p_Start, DateTime p_End )
        {
            if (p_End <= begin)
                return false;

            if (p_Start >= end)
                return false;

            return true;
        }
    }

}