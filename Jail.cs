using System;
using System.Collections.Generic;

namespace bamboohr_jobtest
{
    public class Jail
    {
        private List<Member> arrestedMembers;

        public List<Member> ArrestedMembers { get => arrestedMembers; set => arrestedMembers = value; }

        public Jail()
        {
            ArrestedMembers = new List<Member>();
        }

        public void Enter(string memberName, ref List<Member> criminalOrganization)
        {
            //...
        }

        public void Exit(string memberName, ref List<Member> criminalOrganization)
        {
            //...
        }
    }
}
