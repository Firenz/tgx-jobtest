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

        public Member RemoveMember(string exitingMemberName)
        {
            return RemoveMember(FindMemberByName(exitingMemberName));
        }

        public Member RemoveMember(Member exitingMember)
        {
            if(exitingMember == null) return null;

            Member newBoss = null;

            if(exitingMember.Boss != null)
            {
                exitingMember.Boss.Subordinates.Remove(exitingMember);

                if(exitingMember.Boss.Subordinates.Count > 0)
                {
                    newBoss = PromoteSubordinate(exitingMember.Boss);

                    foreach(Member subordinate in exitingMember.Subordinates)
                    {
                        subordinate.Boss = newBoss;
                        newBoss.Subordinates.Add(subordinate);
                    } 
                }
                else if(exitingMember.Subordinates.Count > 0)
                {
                    newBoss = PromoteSubordinate(exitingMember);
                    newBoss.Boss = exitingMember.Boss;
                    exitingMember.Boss.Subordinates.Add(newBoss);
                    UpdateBossSubordinates(exitingMember.Subordinates, newBoss);
                }
            }
            else
            {
                newBoss = PromoteSubordinate(exitingMember);
                newBoss.Boss = null;
                UpdateBossSubordinates(exitingMember.Subordinates, newBoss);
            }

            CurrentMembers.Remove(exitingMember);
            return exitingMember;
        }

        private void UpdateBossSubordinates(List<Member> subordinates, Member newBoss)
        {
            foreach (Member subordinate in subordinates)
            {
                if (subordinate.Name != newBoss.Name)
                {
                    subordinate.Boss = newBoss;
                    newBoss.Subordinates.Add(subordinate);
                }
            }
        }

        public void ReturnMember(Member returningMember)
        {
            if(returningMember == null) return;

            if(returningMember.Subordinates.Count > 0)
            {
                List<Member> subordinateInJail = new List<Member>();
                foreach(Member subordinate in returningMember.Subordinates)
                {
                    if(CurrentMembers.Contains(subordinate))
                    {
                        Member previousBoss = subordinate.Boss;
                        if(previousBoss != null)
                        {
                            previousBoss.Subordinates.Remove(subordinate);
                        }
                        
                        subordinate.Boss = returningMember;
                    }
                    else
                    {
                        subordinateInJail.Add(subordinate);
                    }
                }

                foreach(Member subordinate in subordinateInJail)
                {
                    returningMember.Subordinates.Remove(subordinate);
                }
            }

            if(returningMember.Boss != null)
            {
                returningMember.Boss.Subordinates.Add(returningMember);
            }
            
            CurrentMembers.Add(returningMember);
        }

        private Member PromoteSubordinate(Member member){
            Member promotedSubordinate = null;

            int maxSeniority = 0;
            for(int i = 0; i < member.Subordinates.Count; i++)
            {
                if(maxSeniority < member.Subordinates[i].Seniority)
                {
                    maxSeniority = member.Subordinates[i].Seniority;
                    promotedSubordinate = member.Subordinates[i];
                }
            }

            return promotedSubordinate;
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

            string hierarchyArrow = member.Boss == null ? String.Empty : " --> ";
            Console.Write(member.Name + hierarchyArrow);

            PrintBossHierarchyOfMember(member.Boss);
        }

        public void PrintHighestBoss()
        {
            Console.WriteLine("\n\nHighest Boss: " + FindHighestBoss().Name);
        }
    }
}