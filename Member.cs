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

        public Member(string _name, int _seniority = 0, Member _boss = null, List<Member> _subordinates = null)
        {
            Name = _name;
            Seniority = _seniority;
            Boss = _boss;

            if(_subordinates != null)
            {
                Subordinates = new List<Member>(_subordinates);
            }
            else 
            {
                Subordinates = new List<Member>();
            }
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
            else if(!Seniority.Equals(otherMember.Seniority)) return false;
            else if(!Boss.Name.Equals(otherMember.Boss.Name)) return false;

            foreach(Member subordinate in Subordinates)
            {
                if(otherMember.Subordinates.Select(x => x.Name).Contains(subordinate.Name)) return false;
            }
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
    }
}