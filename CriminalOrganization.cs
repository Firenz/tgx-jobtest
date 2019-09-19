using System;
using System.Collections.Generic;
using System.Linq;

namespace bamboohr_jobtest
{
    public class CriminalOrganization
    {
        List<Member> currentMembers;

        public List<Member> CurrentMembers { get => currentMembers; set => currentMembers = value; }

        public CriminalOrganization()
        {
            CurrentMembers = new List<Member>();
        }

        public CriminalOrganization(CriminalOrganization criminalOrganization)
        {
            CurrentMembers.AddRange(criminalOrganization.CurrentMembers);
        }

        public void RemoveMember(string exitingMemberName)
        {
            RemoveMember(FindMemberByName(exitingMemberName));
        }

        public void RemoveMember(Member exitingMember)
        {
            Member newBoss = null;

            if(IsHighestBoss(exitingMember))
            {
                newBoss = PromoteSubordinate(exitingMember);
                newBoss.Boss = null;
            }
            else
            {
                if(exitingMember.Subordinates.Count > 0)
                {
                    Member equalSeniorityMember = FindFirstMemberWithEqualSeniority(exitingMember);
                    if(!equalSeniorityMember.Equals(null))
                    {
                        newBoss = equalSeniorityMember;
                        newBoss.Boss = exitingMember.Boss;
                    }
                    else
                    {
                        newBoss = PromoteSubordinate(exitingMember);
                    }
                }              
            }

            foreach(Member subordinate in exitingMember.Subordinates)
            {
                if(!subordinate.Equals(newBoss))
                {
                    subordinate.Boss = newBoss;
                    newBoss.Subordinates.Add(subordinate);
                }
            }
        }

        public void ReturnMember(string returningMemberName)
        {
            ReturnMember(FindMemberByName(returningMemberName));
        }

        public void ReturnMember(Member returningMember)
        {
            if(IsHighestBoss(returningMember))
            {
                Member exBoss = CurrentMembers.FirstOrDefault(x => x.Boss.Equals(null));
                RemoveDuplicatedSubordinates(exBoss, returningMember.Subordinates);
                exBoss.Boss = returningMember;
                
                foreach(Member subordinate in returningMember.Subordinates.FindAll(x => !x.Equals(exBoss)))
                {
                    subordinate.Boss = returningMember;
                }
            }
            else{
                foreach(Member subordinate in returningMember.Subordinates)
                {
                    subordinate.Boss.Subordinates.Remove(subordinate);
                    subordinate.Boss = returningMember;
                }
            }

            CurrentMembers.Add(returningMember);
        }

        private Member PromoteSubordinate(Member exBoss){
            Member promotedSubordinate = null;

            int maxSeniority = 0;
            for(int i = 0; i < exBoss.Subordinates.Count; i++)
            {
                if(maxSeniority < exBoss.Subordinates[i].Seniority)
                {
                    maxSeniority = exBoss.Subordinates[i].Seniority;
                    promotedSubordinate = exBoss.Subordinates[i];
                }
            }

            return promotedSubordinate;
        }

        private void RemoveDuplicatedSubordinates(Member member, List<Member> duplicatedSubordinates)
        {
            foreach(Member duplicatedSubordinate in duplicatedSubordinates)
            {
                if(member.Subordinates.Contains(duplicatedSubordinate))
                {
                    member.Subordinates.Remove(duplicatedSubordinate);
                }
            }
        }

        private List<Member> FindMembersWithEqualSeniority(Member member)
        {
            return CurrentMembers.FindAll(x => x.Seniority == member.Seniority && !x.Equals(member));
        }

        private Member FindFirstMemberWithEqualSeniority(Member member)
        {
            return CurrentMembers.FirstOrDefault(x => x.Seniority == member.Seniority && !x.Equals(member));
        }

        public Member FindMemberByName(string memberName)
        {
            return CurrentMembers.FirstOrDefault(x => x.Name.Equals(memberName));
        }

        public bool IsHighestBoss(Member member)
        {
            return member.Boss.Equals(null);
        }

        public void PrintHierarchyOfMember(Member member)
        {
            if(member.Equals(null)) return;

            string hierarchyArrow = " --> ";
            if(member.Boss.Equals(null))
            {
                hierarchyArrow = String.Empty;
            }
            Console.WriteLine(member.Name + hierarchyArrow);

            PrintHierarchyOfMember(member.Boss);
        }
    }
}