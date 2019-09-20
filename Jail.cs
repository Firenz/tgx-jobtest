using System;
using System.Collections.Generic;
using System.Linq;

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

        public void Enter(string memberName, ref CriminalOrganization criminalOrganization)
        {
            Member enteringMember = criminalOrganization.RemoveMember(memberName);
            ArrestedMembers.Add(enteringMember);
        }

        public void Exit(string memberName, ref CriminalOrganization criminalOrganization)
        {
            Member exitingMember = FindArrestedMemberByName(memberName);
            criminalOrganization.ReturnMember(exitingMember);
            ArrestedMembers.Remove(exitingMember);
        }

        public Member FindArrestedMemberByName(string name)
        {
            return ArrestedMembers.FirstOrDefault(x => x.Name.Equals(name));
        }
    }
}
