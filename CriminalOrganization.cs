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

        public void AddOrUpdateMember(string name, int seniority, string bossName, string[] subordinateNames)
        {
            
            Member newMember = FindMemberByName(name);
            if(newMember == null)
            {
                newMember = new Member(name, seniority);
            }
            else
            {
                newMember.Seniority = seniority;
            }
            
            if(!bossName.Equals(String.Empty))
            {
                Member newMemberBoss = FindMemberByName(bossName);
                if(newMemberBoss == null)
                {
                    newMemberBoss = new Member(bossName);
                }
            }
            
            if(subordinateNames != null && subordinateNames.Length > 0)
            {
                foreach(string newSubordinateName in subordinateNames)
                {
                    Member newSubordinate = FindMemberByName(newSubordinateName);
                    if(newSubordinate == null)
                    {
                        newSubordinate = new Member(newSubordinateName);
                    }

                    newMember.Subordinates.Add(newSubordinate);
                }
            }
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
                if(HasSubordinates(exitingMember))
                {
                    Member equalSeniorityMember = FindFirstMemberWithEqualSeniority(exitingMember);
                    if(equalSeniorityMember != null)
                    {
                        newBoss = equalSeniorityMember;
                    }
                    else
                    {
                        newBoss = PromoteSubordinate(exitingMember);
                        newBoss.Boss = exitingMember.Boss;
                    }
                }              
            }

            foreach(Member subordinate in exitingMember.Subordinates)
            {
                if(!subordinate.Equals(newBoss))
                {
                    subordinate.ChangeBoss(newBoss);
                }
            }
        }

        public void ReturnMember(string returningMemberName)
        {
            ReturnMember(FindMemberByName(returningMemberName));
        }

        public void ReturnMember(Member returningMember)
        {
            foreach(Member subordinate in returningMember.Subordinates)
            {
                subordinate.ChangeBoss(returningMember);
            }
            
            if(returningMember.Boss != null)
            {
                returningMember.Boss.Subordinates.Add(returningMember);
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

        public Member FindHighestBoss()
        {
            return CurrentMembers.FirstOrDefault(x => IsHighestBoss(x));
        }

        public bool IsHighestBoss(Member member)
        {
            return member.Boss == null;
        }

        public bool HasSubordinates(Member member)
        {
            return member.Subordinates.Count > 0;
        }

        public void PrintHierarchy()
        {
            Member highestBoss = FindHighestBoss();
            PrintHierarchyRecursive(highestBoss, "", HasSubordinates(highestBoss));
        }

        private void PrintHierarchyRecursive(Member member, string indent, bool hasSubordinates)
        {
            Console.Write(indent + "+- " + member.Name);
            indent += !hasSubordinates ? "   " : "|  ";

            for(int i = 0; i < member.Subordinates.Count; i++)
            {
                PrintHierarchyRecursive(member.Subordinates[i], indent, HasSubordinates(member.Subordinates[i]));
            }
        }

        public void PrintBossHierarchyOfMember(Member member)
        {
            if(member == null) return;

            string hierarchyArrow = " --> ";
            if(member.Boss == null)
            {
                hierarchyArrow = String.Empty;
            }
            Console.WriteLine(member.Name + hierarchyArrow);

            PrintBossHierarchyOfMember(member.Boss);
        }

        public void PrintHighestBoss()
        {
            Console.WriteLine("Highest Boss: " + FindHighestBoss().Name);
        }
    }
}