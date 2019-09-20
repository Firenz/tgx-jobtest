using System;
using System.Collections.Generic;
using System.Linq;

namespace bamboohr_jobtest
{
    public class Member 
    {
        private string name;
        private int seniority;
        private Member boss;
        private List<Member> subordinates;

        public string Name { get => name; set => name = value; }
        public int Seniority { get => seniority; set => seniority = value; }
        public Member Boss { get => boss; set => boss = value; }
        public List<Member> Subordinates { get => subordinates; set => subordinates = value; }

        public Member()
        {
            Name = String.Empty;
            Seniority = 0;
            Boss = null;
            Subordinates = new List<Member>();
        }

        public Member(string _name, int _seniority = 0, Member _boss = null)
        {
            Name = _name;
            Seniority = _seniority;
            Boss = _boss;

            Subordinates = new List<Member>();
        }

        public Member(Member member)
        {
            Name = member.Name;
            Seniority = member.Seniority;
            Boss = member.Boss;
            Subordinates = new List<Member>(member.Subordinates);
        }

        public void ChangeBoss(Member newBoss)
        {
            Member prevBoss = Boss;

            if(prevBoss != null)
            {
                prevBoss.Subordinates.Remove(this);
            }

            if(newBoss != null)
            {
                newBoss.Subordinates.Add(this);
            }

            Boss = newBoss;
        }

        public bool Equals(Member otherMember)
        {
            if(otherMember == null) return false;
            if(!Name.Equals(otherMember.Name)) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;

            Member otherMember = obj as Member;
            return Equals(otherMember);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(name, seniority, boss, subordinates, Name, Seniority, Boss, Subordinates);
        }

        public override string ToString()
        {
            string memberString = "Name: " + Name + "\nSeniority: " + Seniority + "\nBoss: ";
            if(Boss != null) memberString += Boss.Name;
            if(Subordinates.Count > 0){
                memberString += "\nSubordinates: ";
                foreach(Member subordinate in Subordinates)
                {
                    memberString += subordinate.Name + " ";
                }
            } 
            memberString += "\n--------------";
            return memberString;
        }
    }
}