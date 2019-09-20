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

        public void AddMember(string name, int seniority, string bossName)
        {
            Member newMember = FindMemberByName(name);
            if(newMember == null)
            {
                newMember = new Member(name, seniority);
                CurrentMembers.Add(newMember);
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
                    CurrentMembers.Add(newMemberBoss);
                }

                newMember.Boss = newMemberBoss;
                newMemberBoss.Subordinates.Add(newMember);
            }
        }

        public void RemoveMember(string exitingMemberName)
        {
            RemoveMember(FindMemberByName(exitingMemberName));
        }

        public void RemoveMember(Member exitingMember)
        {
            Member newBoss = null;

            if(exitingMember.Boss != null)
            {
                Member exitingMemberBoss = exitingMember.Boss;
                List<Member> exitingMemberSubordinates = exitingMember.Subordinates;
                List<Member> exitingMemberBossSubordinates = exitingMemberBoss.Subordinates;

                exitingMemberBoss.Subordinates.Remove(exitingMember);

                if(exitingMemberBossSubordinates.Count > 0)
                {
                    int maxSeniority = 0;
                    for(int i = 0; i < exitingMemberBossSubordinates.Count; i++)
                    {
                        if(exitingMemberBossSubordinates[i].Seniority > maxSeniority)
                        {
                            newBoss = exitingMemberBossSubordinates[i];
                            maxSeniority = exitingMemberBossSubordinates[i].Seniority;
                        }
                    }

                    foreach(Member subordinate in exitingMember.Subordinates)
                    {
                        subordinate.Boss = newBoss;
                        newBoss.Subordinates.Add(subordinate);
                    } 
                }
                else if(exitingMemberSubordinates.Count > 0)
                {
                    int maxSeniority = 0;
                    for(int i = 0; i < exitingMemberSubordinates.Count; i++)
                    {
                        if(exitingMemberSubordinates[i].Seniority > maxSeniority)
                        {
                            newBoss = exitingMemberSubordinates[i];
                            newBoss.Boss = exitingMemberSubordinates[i].Boss;
                            maxSeniority = exitingMemberSubordinates[i].Seniority;
                        }
                    }

                    newBoss.Boss = exitingMember.Boss;
                    exitingMember.Boss.Subordinates.Add(newBoss);

                    foreach(Member subordinate in exitingMember.Subordinates)
                    {
                        if(subordinate.Name != newBoss.Name)
                        {
                            subordinate.Boss = newBoss;
                            newBoss.Subordinates.Add(subordinate);
                        }
                    }
                }
            }
            else{
                List<Member> exitingMemberSubordinates = exitingMember.Subordinates;

                int maxSeniority = 0;
                for(int i = 0; i < exitingMemberSubordinates.Count; i++)
                {
                    if(exitingMemberSubordinates[i].Seniority > maxSeniority)
                    {
                        newBoss = exitingMemberSubordinates[i];
                        newBoss.Boss = exitingMemberSubordinates[i].Boss;
                        maxSeniority = exitingMemberSubordinates[i].Seniority;
                    }
                }

                newBoss.Boss = null;

                foreach(Member subordinate in exitingMember.Subordinates)
                {
                    if(subordinate.Name != newBoss.Name)
                    {
                        subordinate.Boss = newBoss;
                        newBoss.Subordinates.Add(subordinate);
                    }
                }
            }

            CurrentMembers.Remove(exitingMember);
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
            PrintHierarchyRecursive(highestBoss, "\n", HasSubordinates(highestBoss));
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

        public void PrintBossHierarchyOfMember(string memberName)
        {
            PrintBossHierarchyOfMember(FindMemberByName(memberName));
        }

        public void PrintBossHierarchyOfMember(Member member)
        {
            if(member == null) return;

            string hierarchyArrow = " --> ";
            if(member.Boss == null)
            {
                hierarchyArrow = String.Empty;
            }
            Console.Write(member.Name + hierarchyArrow);

            PrintBossHierarchyOfMember(member.Boss);
        }

        public void PrintHighestBoss()
        {
            Console.WriteLine("Highest Boss: " + FindHighestBoss().Name);
        }
    }
}